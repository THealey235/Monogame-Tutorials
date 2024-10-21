using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StaticBackground.Core;

namespace StaticBackground
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Camera _camera;
        private Texture2D _playerTexture;
        private Texture2D _bgTexture;
        private Vector2 _playerPosition;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _camera = new Camera();

            _playerTexture = Content.Load<Texture2D>("NPC");
            _bgTexture = Content.Load<Texture2D>("Background");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                _playerPosition.Y -= 3f;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                _playerPosition.Y += 3f;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                _playerPosition.X -= 3f;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                _playerPosition.X += 3f;

            _camera.Follow(_playerPosition);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin();

            _spriteBatch.Draw(_bgTexture, new Vector2(0, 0), Color.White);

            _spriteBatch.End();

            _spriteBatch.Begin(transformMatrix: _camera.Transform);

            _spriteBatch.Draw(_playerTexture, _playerPosition, Color.White);
            _spriteBatch.Draw(_playerTexture, new Vector2(0, 0), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
