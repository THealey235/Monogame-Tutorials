using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlayerAttributes.Models;
using PlayerAttributes.Sprites;
using System.Collections.Generic;

namespace PlayerAttributes
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player _player;

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

            var hatAttributes = new Attributes()
            {
                HealthPoint = 3,
                Speed = 0,
            };

            var jumperAttributes = new Attributes()
            {
                HealthPoint = 2,
                Speed = 2,
            };

            var trouserAttributes = new Attributes()
            {
                HealthPoint = 10,
                Speed = 3,
            };

            _player = new Player(Content.Load<Texture2D>("Player2"))
            {
                Position = new Vector2(100, 200),
                BaseAttributes = new Attributes()
                {
                    HealthPoint = 10,
                    Speed = 3,
                },
                AttributeModifiers = new List<Attributes>()
                {
                    hatAttributes,
                    jumperAttributes,
                    trouserAttributes,
                },
            };
        }

        protected override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _player.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
