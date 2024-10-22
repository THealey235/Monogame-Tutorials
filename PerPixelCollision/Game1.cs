using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PerPixelCollision.Sprites;
using System.Collections.Generic;

namespace PerPixelCollision
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

            var shipTexture = Content.Load<Texture2D>("Player2");

            var bulletPrefab = new Bullet(Content.Load<Texture2D>("Bullet"));

            _sprites = new List<Sprite>()
            {
                new Ship(shipTexture)
                {
                    Bullet = bulletPrefab,
                    Position = new Vector2(100, 100),
                    Colour = Color.Green,
                },
                new Sprite(shipTexture)
                {
                    Position = new Vector2(200, 200),
                    Colour = Color.Red,
                },
                new Sprite(Content.Load<Texture2D>("Enemy_1"))
                {
                    Position = new Vector2(300, 100),
                    Colour = Color.Red,
                },
            };
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites)
                sprite.Update(gameTime);

            PostUpdate(gameTime);

            base.Update(gameTime);
        }

        protected void PostUpdate(GameTime gameTime)
        {
            //1. Check collision 
            foreach(var spriteA in _sprites)
            {
                foreach (var spriteB in _sprites)
                {
                    if (spriteA == spriteB)
                        continue;

                    if (spriteA.Intersects(spriteB))
                        spriteA.OnCollide(spriteB);
                }
            }

            //2. Add Children to the list of _sprites and clear
            int count = _sprites.Count;
            for (int i = 0; i < count; i++)
            {
                foreach (var child in _sprites[i].Children)
                    _sprites.Add(child);

                _sprites[i].Children.Clear();
            }

            //3. Remove all "IsRemoved sprites
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
                sprite.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
