using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SimpleQLearningExample
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D[] textures;
        SpriteFont font;
        Map map;
        QLearning qLearning;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 700;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            textures = new Texture2D[5];
            map = new Map();
            qLearning = new QLearning();
            qLearning.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textures[0] = Content.Load<Texture2D>(@"Textures/circle");
            textures[1] = Content.Load<Texture2D>(@"Textures/square01");
            textures[2] = Content.Load<Texture2D>(@"Textures/target");
            textures[3] = Content.Load<Texture2D>(@"Textures/wall");
            textures[4] = Content.Load<Texture2D>(@"Textures/yellowSquare");

            font = Content.Load<SpriteFont>(@"Fonts/font");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                //this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            qLearning.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here

            spriteBatch.Begin();
            map.Drawn(spriteBatch, textures);
            qLearning.Drawn(spriteBatch, textures);
            qLearning.DrawnDebug(spriteBatch, textures, font);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
