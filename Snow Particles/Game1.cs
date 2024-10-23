using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snow_Particles.Controls;
using Snow_Particles.Emitters;
using Snow_Particles.Sprites;
using System;

namespace Snow_Particles
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int ScreenHeight = 2160;
        public static int ScreenWidth = 3840;

        private Button _button;

        public static Random Random;

        private SnowEmitter _snowEmitter;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.IsFullScreen = true;
            _graphics.HardwareModeSwitch = false; //borderless fullscreen
            _graphics.ApplyChanges();

            IsMouseVisible = true;

            Random = new Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _snowEmitter = new SnowEmitter(new Sprite(Content.Load<Texture2D>("Particles/Snow")))
            {
                MaxParicles = 10000,
            };

            var buttonTexture = Content.Load<Texture2D>("Button");
            var buttonFont = Content.Load<SpriteFont>("Font");

            _button = new Button(buttonTexture, buttonFont)
            {
                Text = "Quit",
                Position = new Vector2(buttonTexture.Width + 20, ScreenHeight - buttonTexture.Height - 60),
                Click = new EventHandler(Button_Quit_Clicked),
                Layer = 0.1f
            };
        }

        private void Button_Quit_Clicked(object sender, EventArgs e)
        {
            Exit();
        }

        protected override void Update(GameTime gameTime)
        {
            _snowEmitter.Update(gameTime);

            _button.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MidnightBlue);

            _spriteBatch.Begin();

            _snowEmitter.Draw(gameTime, _spriteBatch);
            _button.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
