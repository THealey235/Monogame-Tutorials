using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlayerAttributes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerAttributes.Sprites
{
    public class Player : Sprite
    {
        //only changes on level-up and similar
        public Attributes BaseAttributes { get; set; }

        //Extra atribues which can be gained from different sources such as spells/items
        public List<Attributes> AttributeModifiers { get; set; }

        public Attributes TotalAttributes
        {
            get
            {
                return BaseAttributes + AttributeModifiers.Sum();
            }
        }


        public Player(Texture2D texture) : base(texture)
        {
            BaseAttributes = new Attributes();

            AttributeModifiers = new List<Attributes>();
        }

        public override void Update(GameTime gameTime)
        {
            var speed = TotalAttributes.Speed;

            var velocity = new Vector2();

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                velocity.X = speed;
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
                velocity.X = -speed;

            Position += velocity;
        }
    }
}
