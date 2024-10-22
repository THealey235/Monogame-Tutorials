using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooterGame.Sprites
{
    public class Enemy : Ship
    {
        private float _timer;

        public float ShootTimer = 1.75f;
        public Enemy(Texture2D texture) : base(texture)
        {
            Speed = 2f;
        }

        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= ShootTimer)
            {
                Shoot(-5f); //to the left
                _timer = 0;
            }

            Position += new Vector2(-Speed, 0);

            //if the enemy is off the left side of the screen
            if(Position.X < -_texture.Width)
                IsRemoved = true;
        }

        public override void OnCollide(Sprite sprite)
        {
            //if it crashes into a player
            if(sprite is Player && !((Player)sprite).IsRemoved)
            {
                ((Player)sprite).Score.Value++;

                IsRemoved = true;
            }

            if(sprite is Bullet && sprite.Parent is Player)
            {
                Health--;

                if (Health <= 0)
                {
                    IsRemoved = true;
                    ((Player)sprite.Parent).Score.Value++;
                }
            }
        }
    }
}
