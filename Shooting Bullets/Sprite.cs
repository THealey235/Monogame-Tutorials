﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooting_Bullets
{
    internal class Sprite : ICloneable
    {
        protected Texture2D _texture;
        protected float _rotation;
        protected KeyboardState _currentKey;
        protected KeyboardState _previousKey;

        public Vector2 Position;
        public Vector2 Origin;

        public Vector2 Direction;
        public float RotationVelocity = 3f;
        public float LinearVelocity = 4f;

        public Sprite Parent;

        public float LifeSpan = 0f;

        public bool IsRemoved = false;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, 1f, SpriteEffects.None, 0f);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
