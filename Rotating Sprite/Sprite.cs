using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rotating_Sprite
{
    internal class Sprite
    {
        private Texture2D _texture;
        private float _rotation;

        public Vector2 Position;
        public Vector2 Origin;

        public float RotationVelocity = 3f;
        public float LinerVelocity = 4f;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, 1f, SpriteEffects.None, 0f);
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _rotation -= MathHelper.ToRadians(RotationVelocity);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _rotation += MathHelper.ToRadians(RotationVelocity);
            }

            var direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(90) - _rotation), -(float)Math.Sin(MathHelper.ToRadians(90) - _rotation));
        
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Position += direction * LinerVelocity;
            }
        }
    }
}
