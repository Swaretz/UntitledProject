using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Net.Mime;
using System.Xml;

namespace UntitledProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        
        SimplexWorld worl;
        Effect effect;
        GameInterface gameInterface;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            _graphics.PreferredBackBufferWidth = 1500;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 750;   // set this value to the desired height of your window
            
            _graphics.ApplyChanges();

            worl = new SimplexWorld(2000, 2000, 5, 5, 1500, 750, GraphicsDevice);
            gameInterface = new GameInterface(GraphicsDevice);
            

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            effect = Content.Load<Effect>("TreeMask");
            //font = Content.Load<SpriteFont>("Immortal");
            //treeFive = Content.Load<Texture2D>("Tree5");
            //settlement = Content.Load<Texture2D>("Settlement");
            
            // TODO: use this.Content to load your game content here
        }

        float fade=0.0f;

        MouseState mouseState = Mouse.GetState();
        protected override void Update(GameTime gameTime)
        {
            fade += 0.1f;
            fade = fade % 1.0f;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            worl.Update(mouseState);
            gameInterface.Update(worl, mouseState);
            worl.changed = false;
            
            mouseState = Mouse.GetState();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);

            worl.Render(_spriteBatch);
            gameInterface.Render(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
