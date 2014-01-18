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
                container.Add(new BooleanPropertyElement(tag, property));
            }
            else if (property.PropertyType == typeof(byte))
            {
                container.Add(new BytePropertyElement(tag, property));
            }
            else if (property.PropertyType == typeof(char))
            {
                container.Add(new CharPropertyElement(tag, property));
            }
            else if (property.PropertyType == typeof(double))
            {
                container.Add(new DoublePropertyElement(tag, property));
            }
            else if (property.PropertyType == typeof(float))
            {
                container.Add(new FloatPropertyElement(tag, property));
            }
            else if (property.PropertyType == typeof(Guid))
            {
                container.Add(new GuidPropertyElement(tag, "N", property));
            }
            else if (property.PropertyType == typeof(int))
            {
                container.Add(new IntegerPropertyElement(tag, property));
            }
            else if (property.PropertyType == typeof(long))
            {
                container.Add(new LongPropertyElement(tag, property));
            }
            else if (property.PropertyType == typeof(Rectangle))
            {
                container.Add(new RectanglePropertyElement(tag, property));
            }
            else if (property.PropertyType == typeof(short))
            {
                container.Add(new ShortPropertyElement(tag, property));
            }
            else if (property.PropertyType == typeof(string))
            {
                container.Add(new StringPropertyElement(tag, property));
            }
            else if (property.PropertyType == typeof(Vector2))
            {
                container.Add(new Vector2PropertyElement(tag, property));
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

            foreach (PropertyInfo property in ReflectionCache.GetProperties(type))
            {
                string tag = property.Name;
                if (ReflectionCache.HasCustomAttribute<TagAttribute>(property))
                {
                    tag = ReflectionCache.GetCustomAttributes(property)
                        .OfType<TagAttribute>()
                        .Single()
                        .Tag;
                }

                if (!this.HandleValueTypes(container, property, tag))
                {
                    bool isCollection = false;

                    foreach (Type interfaceType in ReflectionCache.GetInterfaces(property.PropertyType))
                    {
                        if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>))
                        {
                            Type elementType;
                            Type genericType = ReflectionCache.GetGenericArguments(interfaceType).Single();

                            if (ReflectionCache.HasCustomAttribute<DerivableAttribute>(property))
                            {
                                elementType = typeof(DerivableCollectionPropertyElement<>);
                            }
                            else
                            {
                                elementType = typeof(CollectionPropertyElement<>);
                            }

                            string elementTag = "Element";
                            if (ReflectionCache.HasCustomAttribute<ElementTagAttribute>(property))
                            {
                                elementTag = ReflectionCache.GetCustomAttributes(property)
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
                        bool isDerivable = isAbstraction || ReflectionCache.HasCustomAttribute<DerivableAttribute>(property);

                        if (isDerivable)
                        {
                            container.Add(new DerivablePropertyReferenceElement(tag, property));
                        }
                        else
                        {
                            container.Add(new PropertyReferenceElement(tag, property));
                        }
                    }
                }
            }

            return (ILayoutElement)container;
        }
        #endregion
    }
}
