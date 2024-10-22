using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterGame.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceShooterGame.Managers
{
    public class EnemyManager
    {
        private float _timer;
        private List<Texture2D> _textures;

        public bool CanAdd { get; set; }
        public Bullet Bullet { get; set; }
        public int MaxEnemies { get; set; }
        public float SpawnTimer { get; set; }

        public EnemyManager(ContentManager contentManager)
        {
            _textures = new List<Texture2D>()
            {
                contentManager.Load<Texture2D>("Ships/Enemy_1"),
                contentManager.Load<Texture2D>("Ships/Enemy_2"),
            };

            MaxEnemies = 10;

            SpawnTimer = 2.5f;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            CanAdd = false;

            if(_timer > SpawnTimer)
            {
                CanAdd = true;

                _timer = 0f;
            }
        }

        public Enemy GetEnemy()
        {
            var texture = _textures[Game1.Random.Next(0, _textures.Count)];

            return new Enemy(texture)
            {
                Colour = Color.Red,
                Bullet = Bullet,
                Health = 5,
                Layer = 0.2f,
                Position = new Vector2(Game1.ScreenWidth + texture.Width, Game1.Random.Next(0, Game1.ScreenHeight)),
                Speed = 2f + (float)Game1.Random.NextDouble(),
                ShootTimer = 1.4f + (float)Game1.Random.NextDouble(),
            };
        }
    }
}
