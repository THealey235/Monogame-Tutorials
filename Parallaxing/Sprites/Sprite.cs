﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallaxing.Sprites
{
    public class Sprite : Component
    {
        protected float _layer;

        protected Texture2D _texture;

        public float Layer
        {
            get { return _layer; }
            set
            {
                _layer = value;
            }
        }

        public Vector2 Position;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }
        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, Layer);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
