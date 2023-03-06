using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//from http://blog.roboblob.com/2013/07/27/solving-resolution-independent-rendering-and-2d-camera-using-monogame/comment-page-1/ 


namespace DonTheNut
{
    class ResolutionIndependentRenderer
    {
        private readonly Game _game;
        private Viewport _viewport;
        private float _ratioX;
        private float _ratioY;

        public int virtualHeight;
        public int virtualWidth;
        public int screenWidth;
        public int screenHeight;

        public bool RenderingToScreenIsFinished;
        private static Matrix _scaleMatrix;
        private bool _dirtyMatrix = true;

        private Vector2 _virtualMousePosition = new Vector2();

        public Color BackgroundColor = Color.Orange;

        public ResolutionIndependentRenderer(Game game)
        {
            _game = game;
            virtualWidth = 1366;
            virtualHeight = 768;
            screenWidth = 1024;
            screenHeight = 768;
        }

        public void Initialize()
        {
            SetupVirtualScreenViewport();

            _ratioX = (float)_viewport.Width / virtualWidth;
            _ratioY = (float)_viewport.Height / virtualHeight;

            _dirtyMatrix = true;
        }
        public void SetupFullViewport()
        {
            var vp = new Viewport();
            vp.X = vp.Y = 0;
            vp.Width = screenWidth;
            vp.Height = screenHeight;
            _game.GraphicsDevice.Viewport = vp;
            _dirtyMatrix = true;
        }
        public void BeginDraw()
        {
            // Start by reseting viewport to (0,0,1,1)
            SetupFullViewport();
            // Clear to Black
            _game.GraphicsDevice.Clear(BackgroundColor);
            // Calculate Proper Viewport according to Aspect Ratio
            SetupVirtualScreenViewport();
            // and clear that
            // This way we are gonna have black bars if aspect ratio requires it and
            // the clear color on the rest
        }
        public Matrix GetTransformationMatrix()
        {
            if (_dirtyMatrix)
                RecreateScaleMatrix();
            return _scaleMatrix;
        }
        private void RecreateScaleMatrix()
        {
            Matrix.CreateScale((float)screenWidth / virtualWidth, (float)screenWidth / virtualWidth, 1f, out _scaleMatrix);
            _dirtyMatrix = false;
        }
        public Vector2 ScaleMouseToScreenCoordinates(Vector2 screenPosition)
        {
            var realX = screenPosition.X - _viewport.X;
            var realY = screenPosition.Y - _viewport.Y;
            _virtualMousePosition.X = realX / _ratioX;
            _virtualMousePosition.Y = realY / _ratioY;
            return _virtualMousePosition;
        }
        public void SetupVirtualScreenViewport()
        {
            var targetAspectRatio = virtualWidth / (float)virtualHeight;
            // figure out the largest area that fits in this resolution at the desired aspect ratio
            var width = screenWidth;
            var height = (int)(width / targetAspectRatio + .5f);

            if (height > screenHeight)
            {
                height = screenHeight;
                // PillarBox
                width = (int)(height * targetAspectRatio + .5f);
            }
            // set up the new viewport centered in the backbuffer
            _viewport = new Viewport
            {
                X = (screenWidth / 2) - (width / 2),
                Y = (screenHeight / 2) - (height / 2),
                Width = width,
                Height = height

            };
            _game.GraphicsDevice.Viewport = _viewport;
        }
    }
}
