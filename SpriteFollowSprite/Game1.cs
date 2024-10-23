using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteFollowSprite.Sprites;
using System.Collections.Generic;

namespace SpriteFollowSprite
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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

            var texture = Content.Load<Texture2D>("Square");

            var player = new Player(texture)
            {
                Colour = Color.Green,
                Position = new Vector2(100, 100)
            };

            _sprites = new List<Sprite>()
            {
                player,
                //Opt 1:
                new Sprite(texture)
                {
                    Colour= Color.Blue,
                    Position = new Vector2(200, 200)
                }.SetFollowTarget(player, 75f),

                //Opt 2:
                new Sprite(texture)
                {
                    Colour= Color.Orange,
                    Position = new Vector2(400, 200),
                    FollowTarget = player,
                    FollowDistance = 150f,
                },
            };
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites)
                sprite.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (var sprite in _sprites)
                sprite.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
