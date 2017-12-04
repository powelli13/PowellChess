using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;
using System;

namespace PowellChess
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        LogicalBoard logicalBoard;

        // board display constants
        const int squareSize = 72;
        const int edgePadding = 72;

        // sprite variables for squares and pieces
        private Texture2D lightSquare;
        private Texture2D darkSquare;

        private Texture2D whiteKing;

        private Dictionary<int, Texture2D> pieceSprites;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 960;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            
            base.Initialize();

            // make standard mouse cursor visible
            this.IsMouseVisible = true;

            // creat logical board
            logicalBoard = new LogicalBoard();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pieceSprites = new Dictionary<int, Texture2D>();

            // sprites for board squares and pieces
            lightSquare = this.Content.Load<Texture2D>("light_square");
            darkSquare = this.Content.Load<Texture2D>("dark_square");
            pieceSprites[2] = this.Content.Load<Texture2D>("white_king");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO verify length of boardstate
            int[] boardState = logicalBoard.RetrieveBoardState();
            GraphicsDevice.Clear(Color.LightGray);

            spriteBatch.Begin();

            // draw board squares
            int xPos;
            int yPos;
            int boardIndex = 0;

            // spans across one row at a time starting with 8th row
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {

                    xPos = x * squareSize + edgePadding;
                    yPos = y * squareSize + edgePadding;

                    if ((x + y) % 2 == 0)
                    {
                        spriteBatch.Draw(lightSquare, new Vector2(xPos, yPos), Color.White);
                    } else {
                        spriteBatch.Draw(darkSquare, new Vector2(xPos, yPos), Color.White);
                    }

                    //TODO ensure no negatives
                    // TDOO ensure board index is not negative
                    if (boardState[boardIndex] != 0)
                    {

                        //TODO verify that x + y is in keys
                        spriteBatch.Draw(pieceSprites[boardState[boardIndex]], new Vector2(xPos, yPos), Color.White);
                    }
                    /*else if (boardState[x + y] == -1) {
                        Console.WriteLine()
                    }*/
                    boardIndex++;
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
