using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Parallaxing.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallaxing.Misc
{
    public class ScrollingBackground : Component
    {

        //for items that will be moving constantly i.e. clouds
        private bool _constantSpeed;

        private float _layer;

        private float _scrollingSpeed;

        //Backgrounds may be made of multiple sprites
        private List<Sprite> _sprites;

        private readonly Player _player;

        private float _speed;

        public float Layer
        {
            get { return _layer; }
            set 
            {
                _layer = value;

                foreach (var sprite in _sprites)
                    sprite.Layer = _layer;
            }
        }

        public ScrollingBackground(Texture2D texture, Player player, float scrollingSpeed, bool constantSpeed = false)
            :this(new List<Texture2D>() { texture, texture}, player, scrollingSpeed, constantSpeed)
        {

        }

        public ScrollingBackground(List<Texture2D> textures, Player player, float scrollingSpeed, bool constantSpeed = false)
        {
            _player = player;

            _sprites = new List<Sprite>();

            _scrollingSpeed = scrollingSpeed;

            _constantSpeed = constantSpeed;

            for(int i = 0; i < textures.Count; i++)
            {
                var texture = textures[i];

                _sprites.Add(new Sprite(texture)
                {
                    //-1 for no tear between 2 sprites
                    Position = new Vector2((i * texture.Width) - 1 , Game1.ScreenHeight - texture.Height),
                });
                
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var sprite in _sprites)
                sprite.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            ApplySpeed(gameTime);

            CheckPositon();
        }
        private void ApplySpeed(GameTime gameTime)
        {
            _speed = (float)(_scrollingSpeed * gameTime.ElapsedGameTime.TotalSeconds);

            if (!_constantSpeed || _player.Velocity.X != 0)
                _speed *= _player.Velocity.X;

            foreach (var sprite in _sprites)
                sprite.Position.X -= _speed; //-= so it goes left when we go right
        }

        private void CheckPositon()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite = _sprites[i];

                //if it has gone off the lefts side of the screen
                if(sprite.Rectangle.Right <= 0)
                {
                    var index = (i - 1 < 0) ? _sprites.Count - 1 : i - 1;

                    sprite.Position.X = _sprites[index].Rectangle.Right - (_speed * 2);                    
                }
            }
        }

    }
}
