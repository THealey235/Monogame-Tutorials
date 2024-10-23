using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snow_Particles.Sprites;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow_Particles.Emitters
{
    public class SnowEmitter : Component
    {
        private float _generateTimer;

        private float _globalVelocityTimer;

        protected Sprite _particlePrefab;

        protected List<Sprite> _particles;

        //how often a particle is produced
        public float GenerateSpeed = 0.005f;

        //how often the global velocity to the particles 
        public float GlobalVelocitySpeed = 1;

        public int MaxParicles = 1000;

        public SnowEmitter(Sprite particlePrefab)
        {
            _particlePrefab = particlePrefab;

            _particles = new List<Sprite>();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var particle in _particles)
                particle.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            _generateTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _globalVelocityTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            AddParticle();

            if(_globalVelocityTimer > GlobalVelocitySpeed)
            {
                _globalVelocityTimer = 0f;

                ApplyGlobalVelocity();
            }

            foreach (var particle in _particles)
                particle.Update(gameTime);

            RemoveFinishedParticles();
        }

        private void RemoveFinishedParticles()
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                if (_particles[i].IsRemoved)
                {
                    _particles.RemoveAt(i);
                    i--;
                }
            }
        }

        private void ApplyGlobalVelocity()
        {
            var xSway = (float)Game1.Random.Next(-2, 2);

            foreach (var particle in _particles)
                particle.Velocity.X = (xSway * particle.Scale) / 50;
        }

        private void AddParticle()
        {
            if (_generateTimer > GenerateSpeed)
            {
                _generateTimer = 0f;

                if (_particles.Count < MaxParicles)
                {
                    _particles.Add(GenerateParitcle());
                }
            }
        }

        protected Sprite GenerateParitcle()
        {
            var sprite = _particlePrefab.Clone() as Sprite;

            var xPosition = Game1.Random.Next(0, Game1.ScreenWidth);
            var ySpeed = Game1.Random.Next(10, 100) / 100f;

            //starts slightly off the screen
            sprite.Position = new Vector2(xPosition, -sprite.Rectangle.Height);
            sprite.Opacity = (float)Game1.Random.NextDouble();
            sprite.Rotation = MathHelper.ToRadians(Game1.Random.Next(0, 360));
            sprite.Scale = (float)Game1.Random.NextDouble() + Game1.Random.Next(0, 3);
            sprite.Velocity = new Vector2(0, ySpeed);

            return sprite;
        }
    }
}
