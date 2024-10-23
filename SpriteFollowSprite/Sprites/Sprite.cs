using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SpriteFollowSprite.Sprites
{
    public class Sprite : Component
    {
        #region Members

        protected float _layer {  get; set; }
        protected Vector2 _origin { get; set; }
        protected Vector2 _position { get; set; }
        protected float _rotation { get; set; }

        protected Texture2D _texture;

        #endregion

        #region Properites

        public Color Colour { get; set; }

        //The sprite that we want to follow
        public Sprite FollowTarget { get; set; }

        public float FollowDistance { get; set; }

        public bool IsRemoved { get; set; }

        public Vector2 Direction { get; set; }

        public float RotationVelocity = 3f;

        public float LinearVelocity = 4f;

        public float Layer
        {
            get { return _layer; }
            set
            {
                _layer = value;
            }
        }

        public Vector2 Origin
        {
            get { return _origin; }
            set
            {
                _origin = value;
            }
        }

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
            }
        }

        public Rectangle Rectangle
        {
            get 
            {
                return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, _texture.Width, _texture.Height);
            }
            
        }

        public float Rotaotion
        {
            get { return _rotation; }
            set
            {
                _rotation = value;
            }
        }

        #endregion

        #region Methods

        public Sprite(Texture2D texture)
        {
            _texture = texture;

            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            Colour = Color.White;
        }

        public Sprite SetFollowTarget(Sprite followTarget, float followDistance)
        {
            FollowTarget = followTarget;

            FollowDistance = followDistance;

            return this;
        }

        protected void Follow()
        {
            if(FollowTarget == null) { return; }

            var distance = FollowTarget.Position - this.Position;
            _rotation = (float)Math.Atan2(distance.Y, distance.X);

            Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

            var currentDistance = Vector2.Distance(this.Position, FollowTarget.Position);

            if (currentDistance > FollowDistance)
            {
                //we want to move at a speed that does not get us too close
                var t = MathHelper.Min((float)Math.Abs(currentDistance- FollowDistance), LinearVelocity); 
                var velocity = Direction * t;

                Position += velocity;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Colour, _rotation, Origin, 1f, SpriteEffects.None, 0.1f);
        }

        public override void Update(GameTime gameTime)
        {
            Follow();
        }
        #endregion
    }
}
