using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShooterGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooterGame.Sprites
{
    internal class Player : Ship
    {
        private float _shootTimer = 0f;

        public bool IsDead
        {
            get
            {
                return Health <= 0;
            }
        }

        public Input Input;

        public Score Score;

        public Player(Texture2D texture) : base(texture)
        {
            Speed = 3f;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsDead) 
                return;

            var velocity = Vector2.Zero;
            _rotation = 0;

            if (Keyboard.GetState().IsKeyDown(Input.Up))
            {
                velocity.Y -= Speed;
                _rotation = MathHelper.ToRadians(-15);
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
            {
                velocity.Y += Speed;
                _rotation = MathHelper.ToRadians(15);
            }

            if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                velocity.X -= Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                velocity.X += Speed;
            }

            _shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //You may hold down space but it does not spam
            if(Keyboard.GetState().IsKeyDown(Input.Shoot) && _shootTimer > 0.25f)
            {
                Shoot(Speed * 2);
                _shootTimer = 0f;
            }

            Position += velocity;


            //Keeps ship in bounds
            Position = Vector2.Clamp(Position, new Vector2(80, 0), new Vector2(Game1.ScreenWidth / 4, Game1.ScreenHeight));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsDead) return;

            base.Draw(gameTime, spriteBatch);
        }

        public override void OnCollide(Sprite sprite)
        {
            if(IsDead) return;

            if (sprite is Bullet && sprite.Parent is Enemy)
                Health--;

            if (sprite is Enemy)
                Health -= 3;
        }
    }
}
