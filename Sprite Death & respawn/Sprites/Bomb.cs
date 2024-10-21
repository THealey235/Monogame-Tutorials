using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Sprite_Death.Sprites
{
    public class Bomb : Sprite
    {
        public Bomb(Texture2D texture) : base(texture)
        {
            Position = new Vector2(Game1.Random.Next(0, Game1.ScreenWidth - _texture.Width), -_texture.Height);
            Speed = Game1.Random.Next(3, 10);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Position.Y += Speed;

            //If hit the bottom of the window
            if(Rectangle.Bottom >= Game1.ScreenHeight)
            {
                IsRemoved = true;
            }
        }
    }
}
