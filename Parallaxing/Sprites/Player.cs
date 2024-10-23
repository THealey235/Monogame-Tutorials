using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallaxing.Sprites
{
    public class Player : Sprite
    {
        public Vector2 Velocity;

        public Player(Texture2D texture) : base(texture)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Velocity.X = 3f;
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
                Velocity.X = -3f;
            else
                Velocity.X = 0f;
        }
    }
}
