using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common.Randomization
{
    public class PerlinNoise
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PerlinNoise"/> class.
        /// </summary>
        public PerlinNoise()
        {
            this._random = new RandomProxy();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PerlinNoise"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public PerlinNoise(string seed)
        {
            this._random = new RandomProxy(seed.GetHashCode());
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PerlinNoise"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public PerlinNoise(int seed)
        {
            this._random = new RandomProxy(seed);
        }
        #endregion
        
        #region Fields
        private readonly IRandom _random;
        #endregion

        #region Methods
        /// <summary>
        /// Generates a smooth noise array.
        /// </summary>
        /// <param name="baseNoise">The base noise.</param>
        /// <param name="octave">The octave.</param>
        public float[,] GenerateSmoothNoise(float[,] baseNoise, int octave)
        {
            int width = baseNoise.GetLength(0);
            int height = baseNoise.GetLength(1);

            float[,] smoothNoise = new float[width, height];

            int period = 1 << octave;
            float frequency = 1.0f / period;

            for (int x = 0; x < width; x++)
            {
                int x0 = (x / period) * period;
                int x1 = (x0 + period) % width;

                float horizontalBlend = (x - x0) * frequency;

                for (int y = 0; y < height; y++)
                {
                    int y0 = (y / period) * period;
                    int y1 = (y0 + period) % height;

                    float verticalBlend = (y - y0) * frequency;

                    float top = this.Interpolate(baseNoise[x0, y0], baseNoise[x1, y0], horizontalBlend);
                    float bottom = this.Interpolate(baseNoise[x0, y1], baseNoise[x1, y1], horizontalBlend);

                    smoothNoise[x, y] = this.Interpolate(top, bottom, verticalBlend);
                }
            }

            return smoothNoise;
        }
        /// <summary>
        /// Generates a perlin noise array.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="persistance">The persistance.</param>
        /// <param name="initialOctave">The initial octave.</param>
        /// <param name="octaveCount">The octave count.</param>
        public float[,] GeneratePerlinNoise(int width, int height, float persistance, int initialOctave, int octaveCount)
        {
            return this.GeneratePerlinNoise(this.GenerateBaseNoise(width, height, 1), persistance, initialOctave, octaveCount);
        }
        /// <summary>
        /// Generates a perlin noise array.
        /// </summary>
        /// <param name="baseNoise">The base noise.</param>
        /// <param name="persistance">The persistance.</param>
        /// <param name="initialOctave">The initial octave.</param>
        /// <param name="octaveCount">The octave count.</param>
        public float[,] GeneratePerlinNoise(float[,] baseNoise, float persistance, int initialOctave, int octaveCount)
        {
            int width = baseNoise.GetLength(0);
            int height = baseNoise.GetLength(1);

            float[][,] smoothNoise = new float[initialOctave + octaveCount][,];

            for (int i = 0; i < octaveCount; i++)
            {
                smoothNoise[initialOctave + i] = GenerateSmoothNoise(baseNoise, initialOctave + i);
            }

            float[,] perlinNoise = new float[width, height];

            float amplitude = 1f;
            float totalAmplitude = 0.0f;

            for (int octave = octaveCount - 1; octave >= initialOctave; octave--)
            {
                amplitude *= persistance;
                totalAmplitude += amplitude;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        perlinNoise[i, j] += smoothNoise[octave][i, j] * amplitude;
                    }
                }
            }
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    perlinNoise[i, j] /= totalAmplitude;
                }
            }
            return perlinNoise;
        }
        /// <summary>
        /// Generates a base noise array.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="multiplier">The multiplier.</param>
        public float[,] GenerateBaseNoise(int width, int height, float multiplier)
        {
            float[,] noise = new float[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    noise[i, j] = this._random.NextFloat() % 1 * multiplier;
                }
            }

            return noise;
        }
        /// <summary>
        /// Interpolates between two values.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="alpha">The amount.</param>
        private float Interpolate(float x0, float x1, float alpha)
        {
            return x0 * (1 - alpha) + alpha * x1;
        }
        #endregion
    }
}
