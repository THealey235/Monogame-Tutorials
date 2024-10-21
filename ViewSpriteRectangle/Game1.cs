using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using ViewSpriteRectangle.Sprites;

namespace ViewSpriteRectangle
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private KeyboardState _currentKey;
        private KeyboardState _previousKey;

        private bool _showBorders = false;
        private List<Sprite> _sprites;

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



            _sprites = new List<Sprite>()
            {
                new Sprite(_graphics.GraphicsDevice, Content.Load<Texture2D>("Apple"))
                {
                    Position = new Vector2(100, 100),
                },
                new Sprite(_graphics.GraphicsDevice, Content.Load<Texture2D>("Monster"))
                {
                    Position = new Vector2(100, 200),
                },
                new Player(_graphics.GraphicsDevice, Content.Load<Texture2D>("NPC"))
                {
                    Position = new Vector2(200, 300),
                },
            };
        }

        protected override void Update(GameTime gameTime)
        {
            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            if(_previousKey.IsKeyDown(Keys.F3) && _currentKey.IsKeyUp(Keys.F3))
                _showBorders = !_showBorders;

            foreach(var sprite in _sprites)
                sprite.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (var sprite in _sprites)
            {
                sprite.ShowRectangle = _showBorders;
                sprite.Draw(gameTime, _spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
