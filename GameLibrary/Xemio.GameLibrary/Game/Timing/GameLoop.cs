using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Game.Timing
{
    public class GameLoop : IGameLoop
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GameLoop"/> class.
        /// </summary>
        public GameLoop()
        {
            this._invoker = new ThreadInvoker();
            this._handlers = new List<IGameHandler>();

            this.Precision = PrecisionLevel.High;
            this.LagCompensation = LagCompensation.ExecuteMissedTicks;
        }
        #endregion

        #region Fields
        private Task _loopTask;
        private Stopwatch _gameTime;

        private readonly ThreadInvoker _invoker;
        private readonly List<IGameHandler> _handlers;

        private double _renderTime;
        private double _tickTime;

        private bool _requestRender;
        private double _unprocessedTicks;

        private double _elapsedTickTime;
        private double _elapsedRenderTime;

        private int _fpsCount;
        private double _lastFpsMeasure;

        private double _lastTick;
        private double _lastRender;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GameLoop"/> is active.
        /// </summary>
        public bool Active { get; private set; }
        /// <summary>
        /// Gets the current frame index.
        /// </summary>
        public long FrameIndex { get; private set; }
        /// <summary>
        /// Gets or sets the precision. The precision level determines the amount of "Thread.Sleep"
        /// used to wait for the next frame. The higher the precision level, the lower the amount
        /// of Thread.Sleep used.
        /// </summary>
        public PrecisionLevel Precision { get; set; }
        /// <summary>
        /// Gets or sets the lag compensation. If set to ExecuteMissedTicks, the game loop will
        /// always run at the same tick frequency.
        /// </summary>
        public LagCompensation LagCompensation { get; set; }
        /// <summary>
        /// Gets the frames per second.
        /// </summary>
        public int FramesPerSecond { get; private set; }
        /// <summary>
        /// Gets the frame time (render time + tick time).
        /// </summary>
        public double FrameTime
        {
            get { return this._tickTime + this._renderTime; }
        }
        /// <summary>
        /// Gets the tick time.
        /// </summary>
        public double TickTime
        {
            get { return this._tickTime; }
        }
        /// <summary>
        /// Gets the render time.
        /// </summary>
        public double RenderTime
        {
            get { return this._renderTime; }
        }
        /// <summary>
        /// Gets the target tick time.
        /// </summary>
        public double TargetTickTime { get; set; }
        /// <summary>
        /// Gets the target frame time.
        /// </summary>
        public double TargetFrameTime { get; set; }
        #endregion
        
        #region Event Methods
        /// <summary>
        /// Called when the game should be updated.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        protected virtual void OnTick(float elapsed)
        {
            Stopwatch tickWatch = Stopwatch.StartNew();

            //Increment the frame index, since a frame has passed. A
            //frame can only pass inside a tick call, since render isn't called
            //as frequent as tick
            this.FrameIndex++;

            foreach (IGameHandler gameHandler in this._handlers)
            {
                gameHandler.Tick(elapsed);
            }

            tickWatch.Stop();
            this._tickTime = tickWatch.Elapsed.TotalMilliseconds;
        }
        /// <summary>
        /// Called when the game should be rendered.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        protected virtual void OnRender(float elapsed)
        {
            Stopwatch renderWatch = Stopwatch.StartNew();

            foreach (IGameHandler gameHandler in this._handlers)
            {
                gameHandler.Render();
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
            if (!this.Active)
            {
                this.Active = true;
                this._loopTask = Task.Factory.StartNew(InternalLoop);
            }
        }
        /// <summary>
        /// Stops the game loop.
        /// </summary>
        public void Stop()
        {
            if (this.Active)
            {
                this.Active = false;
                this._loopTask.Wait();
            }
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
        /// Resets all important fields.
        /// </summary>
        private void ResetFields()
        {
            this._unprocessedTicks = 0;
            this._elapsedRenderTime = 0;
            this._elapsedTickTime = 0;
            this._fpsCount = 0;
            this._tickTime = 0;
            this._renderTime = 0;
        }
        /// <summary>
        /// A function representing the internal game loop logic.
        /// </summary>
        private void InternalLoop()
        {
            this._gameTime = Stopwatch.StartNew();
            this._requestRender = true;

            this.ResetFields();

            this._lastFpsMeasure = this._gameTime.Elapsed.TotalMilliseconds;
            this._lastTick = this._gameTime.Elapsed.TotalMilliseconds;
            this._lastRender = this._gameTime.Elapsed.TotalMilliseconds;
            
            while (this.Active)
            {
                this.HandleRenderRequest();
                this.HandleUnprocessedTicks();

                this.CheckRenderRequest();
                this.UpdateFramesPerSecond();
            }
        }
        /// <summary>
        /// Determines whether the specified time has elapsed.
        /// </summary>
        /// <param name="pointInTime">The point in time.</param>
        /// <param name="elapsed">The elapsed.</param>
        private bool IsElapsed(double pointInTime, double elapsed)
        {
            return this._gameTime.Elapsed.TotalMilliseconds - pointInTime >= elapsed;
        }
        /// <summary>
        /// Handles render requests.
        /// </summary>
        private void HandleRenderRequest()
        {
            if (this._requestRender)
            {
                this._fpsCount++;

                this._elapsedRenderTime = this._gameTime.Elapsed.TotalMilliseconds - this._lastRender;
                this._lastRender = this._gameTime.Elapsed.TotalMilliseconds;

                this._invoker.Invoke(() => this.OnRender((float)this._elapsedRenderTime));
                this._requestRender = false;
            }
        }
        /// <summary>
        /// Handles all unprocessed ticks.
        /// </summary>
        private void HandleUnprocessedTicks()
        {
            //Time since the last handled tick.
            double elapsed = this._gameTime.Elapsed.TotalMilliseconds - this._lastTick;

            this._elapsedTickTime += elapsed;
            this._unprocessedTicks += elapsed / this.TargetTickTime;
            this._lastTick = this._gameTime.Elapsed.TotalMilliseconds;

            //If there are unprocessed ticks, call OnTick.
            if (this._unprocessedTicks >= 1)
            {
                int tickCount = (int)this._unprocessedTicks;

                //Subtract unprocessed ticks as an integer, since we want
                //to keep digits for the next tick. (Example: unprocessedTicks = 3.11, => tickCount = 3)
                this._unprocessedTicks -= tickCount;

                switch (this.LagCompensation)
                {
                    case LagCompensation.None:
                        this._invoker.Invoke(() => this.OnTick((float)this._elapsedTickTime));
                        break;
                    case LagCompensation.ExecuteMissedTicks:
                        //Handle unprocessed ticks by calling the OnTick method as often
                        //as needed for the elapsed tick time.
                        //Example: elapsedTickTime = 32ms, TargetFrameTime = 16ms => call OnTick twice.

                        float tickElapsed = (float)(this._elapsedTickTime / tickCount);
                        for (int i = 0; i < tickCount; i++)
                        {
                            this._invoker.Invoke(() => this.OnTick(tickElapsed));
                        }
                        break;
                }

                this._elapsedTickTime = 0;
                this.ManagePrecisionLevel();
            }
        }
        /// <summary>
        /// Checks for render requests.
        /// </summary>
        private void CheckRenderRequest()
        {
            if (this.IsElapsed(this._lastRender, this.TargetFrameTime))
            {
                this._requestRender = true;
            }
        }
        /// <summary>
        /// Updates the timing properties for FramesPerSecond.
        /// </summary>
        private void UpdateFramesPerSecond()
        {
            if (this.IsElapsed(this._lastFpsMeasure, 1000.0))
            {
                this.FramesPerSecond = this._fpsCount;

                this._lastFpsMeasure = this._gameTime.Elapsed.TotalMilliseconds;
                this._fpsCount = 0;
            }
        }
        /// <summary>
        /// Manages the precision level.
        /// </summary>
        private void ManagePrecisionLevel()
        {
            double frameTime = this._renderTime + this._tickTime;
            int timeTillNextFrame = (int)(this.TargetFrameTime - frameTime);

            if (timeTillNextFrame < 0)
                return;

            switch (this.Precision)
            {
                case PrecisionLevel.Highest:
                    //Use busy waiting for the game loop
                    break;
                case PrecisionLevel.High:
                    Thread.Sleep(timeTillNextFrame / 8);
                    break;
                case PrecisionLevel.Normal:
                    Thread.Sleep(timeTillNextFrame / 4);
                    break;
                case PrecisionLevel.Low:
                    Thread.Sleep(timeTillNextFrame / 2);
                    break;
                case PrecisionLevel.Lowest:
                    Thread.Sleep(timeTillNextFrame);
                    break;
            }
        }
        #endregion
    }
}
