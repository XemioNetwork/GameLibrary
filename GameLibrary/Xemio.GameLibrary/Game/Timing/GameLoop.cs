﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Game.Timing
{
    public class GameLoop : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GameLoop"/> class.
        /// </summary>
        public GameLoop()
        {
            this._handlers = new List<IGameHandler>();
        }
        #endregion

        #region Fields
        private Task _loopTask;
        private readonly List<IGameHandler> _handlers;

        private double _renderTime;
        private double _tickTime;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GameLoop"/> is active.
        /// </summary>
        public bool Active { get; private set; }
        /// <summary>
        /// Gets or sets the precision.
        /// </summary>
        public GameLoopPrecision Precision { get; set; }
        /// <summary>
        /// Gets the frames per second.
        /// </summary>
        public int FramesPerSecond { get; private set; }
        /// <summary>
        /// Gets the frame time.
        /// </summary>
        public double FrameTime { get; private set; }
        /// <summary>
        /// Gets the target tick time.
        /// </summary>
        public double TargetTickTime { get; set; }
        /// <summary>
        /// Gets the target frame time.
        /// </summary>
        public double TargetFrameTime { get; set; }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the game should be updated.
        /// </summary>
        public event GameEventHandler Tick;
        /// <summary>
        /// Occurs when the game should render all scenes.
        /// </summary>
        public event GameEventHandler Render;
        #endregion

        #region Event Methods
        /// <summary>
        /// Called when the game should be updated.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void OnTick(float elapsed)
        {
            Stopwatch tickWatch = Stopwatch.StartNew();

            if (this.Tick != null)
            {
                this.Tick(elapsed);
            }
            for (int i = 0; i < this._handlers.Count; i++)
            {
                this._handlers[i].Tick(elapsed);
            }

            tickWatch.Stop();
            this._tickTime = tickWatch.Elapsed.TotalMilliseconds;
        }
        /// <summary>
        /// Called when the game should be rendered.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void OnRender(float elapsed)
        {
            Stopwatch renderWatch = Stopwatch.StartNew();

            if (this.Render != null)
            {
                this.Render(elapsed);
            }
            for (int i = 0; i < this._handlers.Count; i++)
            {
                this._handlers[i].Render();
            }

            renderWatch.Stop();
            this._renderTime = renderWatch.Elapsed.TotalMilliseconds;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Runs the game loop and creates a parallel thread.
        /// </summary>
        public void Run()
        {
            this.Active = true;

            this._loopTask = new Task(this.InternalLoop);
            this._loopTask.Start();
        }
        /// <summary>
        /// Stops the game loop.
        /// </summary>
        public void Stop()
        {
            this.Active = false;
            this._loopTask.Wait();
        }
        /// <summary>
        /// Subscribes and adds the handler to the gameloop.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void Subscribe(IGameHandler handler)
        {
            this._handlers.Add(handler);
        }
        /// <summary>
        /// Unsubscribes the gameloop and removes the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void Unsubscribe(IGameHandler handler)
        {
            this._handlers.Remove(handler);
        }
        /// <summary>
        /// A function representing the internal game loop logic.
        /// </summary>
        private void InternalLoop()
        {
            Stopwatch gameTime = Stopwatch.StartNew();

            bool requestRender = true;

            double unprocessedTicks = 0;

            double elapsedTickTime = 0;
            double elapsedRenderTime = 0;

            double lastFrameCount = gameTime.Elapsed.TotalMilliseconds;
            
            double lastTick = gameTime.Elapsed.TotalMilliseconds;
            double lastRender = gameTime.Elapsed.TotalMilliseconds;

            int frames = 0;
            
            while (this.Active)
            {
                if (requestRender)
                {
                    frames++;

                    elapsedRenderTime = gameTime.Elapsed.TotalMilliseconds - lastRender;
                    lastRender = gameTime.Elapsed.TotalMilliseconds;

                    ThreadInvoker.Invoke(() => this.OnRender((float)elapsedRenderTime));

                    requestRender = false;
                }

                double elapsed = gameTime.Elapsed.TotalMilliseconds - lastTick;

                elapsedTickTime += elapsed;
                unprocessedTicks += elapsed / this.TargetTickTime;
                lastTick = gameTime.Elapsed.TotalMilliseconds;

                int tickCount = (int)unprocessedTicks;
                if (unprocessedTicks >= 1)
                {
                    float tickElapsed = (float)(elapsedTickTime / tickCount);
                    unprocessedTicks -= tickCount;

                    for (int i = 0; i < tickCount; i++)
                    {
                        ThreadInvoker.Invoke(() => this.OnTick(tickElapsed));
                    }

                    elapsedTickTime = 0;
                }

                if (gameTime.Elapsed.TotalMilliseconds - lastRender >= this.TargetFrameTime)
                {
                    requestRender = true;
                }

                if (gameTime.Elapsed.TotalMilliseconds - lastFrameCount >= 1000.0)
                {
                    this.FrameTime = this._tickTime + this._renderTime;
                    this.FramesPerSecond = frames;

                    lastFrameCount = gameTime.Elapsed.TotalMilliseconds;
                    frames = 0;
                }

                this.ManagePrecisionLevel();
            }
        }
        /// <summary>
        /// Manages the precision level.
        /// </summary>
        private void ManagePrecisionLevel()
        {
            switch (this.Precision)
            {
                case GameLoopPrecision.Highest:
                    //Use busy waiting for the game loop
                    break;
                case GameLoopPrecision.High:
                    Thread.Sleep(1);
                    break;
                case GameLoopPrecision.Low:
                    Thread.Sleep(5);
                    break;
            }
        }
        #endregion
    }

    /// <summary>
    /// Represents a game event handler.
    /// </summary>
    /// <param name="elapsed">The elapsed.</param>
    public delegate void GameEventHandler(float elapsed);
}