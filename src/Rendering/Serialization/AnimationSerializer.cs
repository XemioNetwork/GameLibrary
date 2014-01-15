using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Rendering.Sprites;

namespace Xemio.GameLibrary.Rendering.Serialization
{
    public class AnimationSerializer : Serializer<Animation>
    {
        #region Overrides of Serializer<Animation>
        /// <summary>
        /// Reads a new animation instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public override Animation Read(IFormatReader reader)
        {
            var serializer = XGL.Components.Require<SerializationManager>();
            var content = XGL.Components.Require<ContentManager>();

            var sheet = content.Get<SpriteSheet>(reader.ReadString("SpriteSheet"));

            float frameTime = reader.ReadFloat("FrameTime");
            bool isLooped = reader.ReadBoolean("IsLooped");

            using (reader.Section("Indices"))
            {
                int length = reader.ReadInteger("Length");
                var indices = new int[length];

                for (int i = 0; i < length; i++)
                {
                    indices[i] = reader.ReadInteger("Index");
                }

                return new Animation(sheet, frameTime, indices, isLooped);
            }
        }
        /// <summary>
        /// Writes the specified animation.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="animation">The animation.</param>
        public override void Write(IFormatWriter writer, Animation animation)
        {
            var serializer = XGL.Components.Require<SerializationManager>();
            var content = XGL.Components.Require<ContentManager>();

            writer.WriteString("SpriteSheet", content.GetFileName(animation.Sheet));

            writer.WriteFloat("FrameTime", animation.FrameTime);
            writer.WriteBoolean("IsLooped", animation.IsLooped);

            using (writer.Section("Indices"))
            {
                writer.WriteInteger("Length", animation.Indices.Length);
                foreach (int index in animation.Indices)
                {
                    writer.WriteInteger("Index", index);
                }
            }
        }
        #endregion
    }
}
