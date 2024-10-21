using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewSpriteRectangle.Sprites
{
    public class Sprite : Component
    {
        protected Texture2D _rectangleTexture;

        protected Texture2D _texture;

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height); }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);

            if(ShowRectangle)
            {
                if (_rectangleTexture != null)
                    spriteBatch.Draw(_rectangleTexture, Position, Color.Red);
            }
        }

        public bool ShowRectangle {  get; set; }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            ShowRectangle = false;
        }

        public Sprite(GraphicsDevice graphics, Texture2D texture)
            :this(texture)
        {
            SetRectangleTexture(graphics, texture);
        }

        private void SetRectangleTexture(GraphicsDevice graphics, Texture2D texture)
        {

            var colours = new List<Color>();

           for (int y = 0; y < _texture.Height; y++)
            {
                for (int x = 0; x < _texture.Width; x++)
                {
                    if(x == 0 || //left side
                       y == 0 || //top side
                       x == texture.Width-1 ||  //right side
                       y == texture.Height - 1) //bottom side
                    {
                        colours.Add(new Color(255, 255, 255, 255));
                    }
                    else
                    {
                        colours.Add(new Color(0, 0, 0, 0));

                    }
                }
            }

           _rectangleTexture = new Texture2D(graphics, texture.Width, texture.Height);
            _rectangleTexture.SetData<Color>(colours.ToArray());
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
