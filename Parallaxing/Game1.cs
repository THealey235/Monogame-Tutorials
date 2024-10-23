using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Parallaxing.Misc;
using Parallaxing.Sprites;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Parallaxing
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;

        private List<ScrollingBackground> _scrollingBackgrounds;
        private Player _player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var playerTexture = Content.Load<Texture2D>("Player2");

            //Layer value must be between 0f to 1f inclusive

            _player = new Player(playerTexture)
            {
                Position = new Vector2(50, (ScreenHeight / 2) - playerTexture.Height),
                Layer = 1f,
            };

            _scrollingBackgrounds = new List<ScrollingBackground>()
            {
                new ScrollingBackground(Content.Load<Texture2D>("ScrollingBackgrounds/Trees"), _player, 60f)
                {
                    Layer = 0.99f
                },
                new ScrollingBackground(Content.Load<Texture2D>("ScrollingBackgrounds/Floor"), _player, 60f)
                {
                    Layer = 0.98f
                },new ScrollingBackground(Content.Load<Texture2D>("ScrollingBackgrounds/Hills_Front"), _player, 40f)
                {
                    Layer = 0.8f
                },new ScrollingBackground(Content.Load<Texture2D>("ScrollingBackgrounds/Hills_Middle"), _player, 30f)
                {
                    Layer = 0.79f
                },new ScrollingBackground(Content.Load<Texture2D>("ScrollingBackgrounds/Clouds_Fast"), _player, 25f, true)
                {
                    Layer = 0.78f
                },new ScrollingBackground(Content.Load<Texture2D>("ScrollingBackgrounds/Hills_Back"), _player, 0f)
                {
                    Layer = 0.77f
                },new ScrollingBackground(Content.Load<Texture2D>("ScrollingBackgrounds/Clouds_Slow"), _player, 10f, true)
                {
                    Layer = 0.7f
                },new ScrollingBackground(Content.Load<Texture2D>("ScrollingBackgrounds/Sky"), _player, 0f)
                {
                    Layer = 0.1f
                },
            };
        }

        protected override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);

            foreach(var sb in _scrollingBackgrounds)
                sb.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

            _player.Draw(gameTime, _spriteBatch);

            foreach(var sb in _scrollingBackgrounds)
                sb.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
