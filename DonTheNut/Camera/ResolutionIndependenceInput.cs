using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;


namespace DonTheNut
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ResolutionIndependenceInput
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        ContentManager _content;
        private ResolutionIndependentRenderer _resolutionIndependence;
        private Camera2D _camera;
        private SpriteFont _debugFont;
        private InputHelper _inputHelper;
        private Vector2 _screenMousePos;
        private Vector2 _mouseDrawPos;
        private float _rotationDiff = 0.02f;
        private float _zoomDiff = 0.02f;


        public ResolutionIndependenceInput(Game game, ContentManager Content, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            _graphics = graphics;
            _spriteBatch = spriteBatch;
            _content = Content;
            _content.RootDirectory = "Content";
            
            _resolutionIndependence = new ResolutionIndependentRenderer(game);
            _camera = new Camera2D(_resolutionIndependence);
            _camera.Zoom = 1f;
            
            _camera.Position = new Vector2(_resolutionIndependence.virtualWidth / 2, _resolutionIndependence.virtualHeight / 2);
            
            _inputHelper = new InputHelper();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent()
        {
            

            InitializeResolutionIndependence(_graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height);

            _mouseDrawPos.X = 700;
            _mouseDrawPos.Y = 40f;

            // TODO: use this.Content to load your game content here
        }

        private void InitializeResolutionIndependence(int realScreenWidth, int realScreenHeight)
        {
            _resolutionIndependence.virtualWidth = 1366;
            _resolutionIndependence.virtualHeight = 768;
            _resolutionIndependence.screenWidth = realScreenWidth;
            _resolutionIndependence.screenHeight = realScreenHeight;
            _resolutionIndependence.Initialize();

            _camera.RecalculateTransformationMatrices();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            _inputHelper.Update();

            _screenMousePos = _resolutionIndependence.ScaleMouseToScreenCoordinates(_inputHelper.MousePosition);

            if (_inputHelper.IsCurPress(Keys.Add))
            {
                if (_inputHelper.IsCurPress(Keys.LeftShift))
                    _camera.Rotation += _rotationDiff;
                else
                    _camera.Zoom += _zoomDiff;
            }
            else if (_inputHelper.IsCurPress(Keys.Subtract))
            {
                if (_inputHelper.IsCurPress(Keys.LeftShift))
                    _camera.Rotation -= _rotationDiff;
                else
                    _camera.Zoom -= _zoomDiff;
            }
            else if (_inputHelper.IsNewPress(Keys.R))
            {
                _camera.Zoom = 1;
                _camera.Rotation = 0;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            _resolutionIndependence.BeginDraw();
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, _camera.GetViewTransformationMatrix());
            _spriteBatch.End();

            // TODO: Add your drawing code here
        }

        public void ScreenSizeChanged(System.Drawing.Size size)
        {
            InitializeResolutionIndependence((int)size.Width, (int)size.Height);
        }
    }
}
