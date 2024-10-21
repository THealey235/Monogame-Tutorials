using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprite_Fonts.Sprites;
using System;
using System.Collections.Generic;

namespace Sprite_Fonts
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Random Random;

        public static int ScreenWidth;
        public static int ScreenHeight;

        private List<Sprite> _sprites;

        private SpriteFont _font;

        private float _timer;

        private Texture2D _appleTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Random = new Random();

            ScreenHeight = _graphics.PreferredBackBufferHeight;
            ScreenWidth = _graphics.PreferredBackBufferWidth;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var playerTexture = Content.Load<Texture2D>("Monster");

            _sprites = new List<Sprite>()
            {
                new Player(playerTexture)
                {
                    Input = new Models.Input()
                    {
                        Left = Keys.A,
                        Right = Keys.D,
                        Up = Keys.W,
                        Down = Keys.S,
                    },
                    Position = new Vector2(100, 100),
                    Color = Color.Blue,
                    Speed = 5f,
                },
                new Player(playerTexture)
                {
                    Input = new Models.Input()
                    {
                        Left = Keys.Left,
                        Right = Keys.Right,
                        Up = Keys.Up,
                        Down = Keys.Down,
                    },
                    Position = new Vector2(ScreenWidth - 100 - playerTexture.Width, 100),
                    Color = Color.Green,
                    Speed = 5f,
                }
            };

            _font = Content.Load<SpriteFont>("Font");
            _appleTexture = Content.Load<Texture2D>("Apple");

        }

        protected override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var sprite in _sprites)
                sprite.Update(gameTime, _sprites);

            PostUpdate();

            SpawnApple();

            base.Update(gameTime);
        }

        private void SpawnApple()
        {
            if (_timer > 1)
            {
                _timer = 0;

                var xPos = Random.Next(0, ScreenWidth - _appleTexture.Width);
                var yPos = Random.Next(0, ScreenHeight - _appleTexture.Height);

                _sprites.Add(new Sprite(_appleTexture)
                {
                    Position = new Vector2(xPos, yPos),
                }
                );
            }
        }

        private void PostUpdate()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);

            var fontY = 10;
            var i = 0;
            foreach (var sprite in _sprites)
            {
                if (sprite is Player)
                    _spriteBatch.DrawString(_font, string.Format("Player {0}: {1}", ++i, ((Player)sprite).Score), new Vector2(10, fontY += 20), Color.Black);
                {
                    
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
