using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Content.Layouts.Collections;
using Xemio.GameLibrary.Content.Layouts.Primitives;
using Xemio.GameLibrary.Content.Layouts.References;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Content.Layouts.Generation
{
    internal class LayoutGenerator : IComputationProvider<Type, ILayoutElement>
    {
        #region Private Methods
        /// <summary>
        /// Handles the value types.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="property">The property.</param>
        /// <param name="tag">The tag.</param>
        private bool HandleValueTypes(IElementContainer container, PropertyInfo property, string tag)
        {
            if (property.PropertyType == typeof(bool))
            {
                container.Add(new BooleanElement(tag, property));
            }
            else if (property.PropertyType == typeof(byte))
            {
                container.Add(new ByteElement(tag, property));
            }
            else if (property.PropertyType == typeof(char))
            {
                container.Add(new CharElement(tag, property));
            }
            else if (property.PropertyType == typeof(double))
            {
                container.Add(new DoubleElement(tag, property));
            }
            else if (property.PropertyType == typeof(float))
            {
                container.Add(new FloatElement(tag, property));
            }
            else if (property.PropertyType == typeof(Guid))
            {
                container.Add(new GuidElement(tag, "N", property));
            }
            else if (property.PropertyType == typeof(int))
            {
                container.Add(new IntegerElement(tag, property));
            }
            else if (property.PropertyType == typeof(long))
            {
                container.Add(new LongElement(tag, property));
            }
            else if (property.PropertyType == typeof(Rectangle))
            {
                container.Add(new RectangleElement(tag, property));
            }
            else if (property.PropertyType == typeof(short))
            {
                container.Add(new ShortElement(tag, property));
            }
            else if (property.PropertyType == typeof(string))
            {
                container.Add(new StringElement(tag, property));
            }
            else if (property.PropertyType == typeof(Vector2))
            {
                container.Add(new Vector2Element(tag, property));
            }
            else
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Implementation of IComputationProvider<Type, ILayoutElement>
        /// <summary>
        /// Computes the specified key.
        /// </summary>
        /// <param name="type">The type.</param>
        public ILayoutElement Compute(Type type)
        {
            Type layoutType = typeof(PersistenceLayout<>).MakeGenericType(type);
            var container = (IElementContainer)Activator.CreateInstance(layoutType);

            //TODO: Make code a little bit more object orientated.

            foreach (PropertyInfo property in Reflection.GetProperties(type))
            {
                if (Reflection.HasCustomAttribute<ExcludeAttribute>(property))
                    continue;

                string tag = property.Name;
                if (Reflection.HasCustomAttribute<TagAttribute>(property))
                {
                    tag = Reflection.GetCustomAttributes(property)
                        .OfType<TagAttribute>()
                        .Single()
                        .Tag;
                }

                if (!this.HandleValueTypes(container, property, tag))
                {
                    bool isCollection = false;

                    foreach (Type interfaceType in Reflection.GetInterfaces(property.PropertyType))
                    {
                        if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>))
                        {
                            Type elementType;
                            Type genericType = Reflection.GetGenericArguments(interfaceType).Single();

                            bool isArray = property.PropertyType.IsArray;

                            bool isCollectionAbstraction = property.PropertyType.IsAbstract || property.PropertyType.IsInterface;
                            bool isElementAbstraction = genericType.IsAbstract || genericType.IsInterface;

                            bool isDerivable = isCollectionAbstraction || Reflection.HasCustomAttribute<DerivableAttribute>(property);
                            bool hasDerivableChildren = isElementAbstraction || Reflection.HasCustomAttribute<DerivableElementsAttribute>(property);

                            if (isArray)
                            {
                                elementType = typeof(ArrayElement<>);
                            }
                            else if (isDerivable && hasDerivableChildren)
                            {
                                elementType = typeof(DerivableCollectionWithDerivableChildrenElement<>);
                            }
                            else if (isDerivable)
                            {
                                elementType = typeof(DerivableCollectionElement<>);
                            }
                            else if (hasDerivableChildren)
                            {
                                elementType = typeof(CollectionWithDerivableChildrenElement<>);
                            }
                            else
                            {
                                elementType = typeof(CollectionElement<>);
                            }

                            string elementTag = "Element";
                            if (Reflection.HasCustomAttribute<ElementTagAttribute>(property))
                            {
                                elementTag = Reflection.GetCustomAttributes(property)
                                    .OfType<ElementTagAttribute>()
                                    .Single()
                                    .Tag;
                            }

                            Type instanceType = elementType.MakeGenericType(genericType);
                            object instance = Activator.CreateInstance(instanceType, new object[]
                            {
                                tag,
                                elementTag,
                                property
                            });

                            container.Add((ILayoutElement)instance);
                            isCollection = true;

                            break;
                        }
                    }

                    if (!isCollection)
                    {
                        bool isAbstraction = property.PropertyType.IsAbstract || property.PropertyType.IsInterface;
                        bool isDerivable = isAbstraction || Reflection.HasCustomAttribute<DerivableAttribute>(property);

                        if (isDerivable)
                        {
                            container.Add(new DerivableReferenceElement(tag, property));
                        }
                        else
                        {
                            container.Add(new ReferenceElement(tag, property));
                        }
                    }
                }
            }

            return (ILayoutElement)container;
        }
        #endregion
    }
}
