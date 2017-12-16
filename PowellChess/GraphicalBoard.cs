using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;
using System;

namespace PowellChess
{

    /// <summary>
    /// Constants for the graphical display.
    /// </summary>
    public class DisplaySettings
    {
        public const int squareSize = 72;
        public const int edgePadding = 72;
    }


    /// <summary>
    /// Used to control the display, click handling and graphical manipulations of squares.
    /// </summary>
    public class Square
    {
        /// <summary>
        /// Propertires used for drawing square.
        /// </summary>
        public Rectangle rect;
        public Texture2D texture;
        public Color color = Color.White;

        public int piece = 0;
        public bool highlighted = false;

        private int boardIndex;
        

        public Square(int x, int y, Texture2D t, int bi)
        {
            rect = new Rectangle(x, y, DisplaySettings.squareSize, DisplaySettings.squareSize);
            texture = t;
            boardIndex = bi;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rect, color);
            /*
            if (highlighted)
            {

            }
            */
            /*
            if (pieces != 0)
            {
            
            }
            */
        }
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GraphicalBoard : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Square[] squares = new Square[64];

        private LogicalBoard logicalBoard;


        // input handling
        private MouseState mouseState;
        private MouseState lastMouseState;

        // sprite variables for squares and pieces
        private Texture2D lightSquare;
        private Texture2D darkSquare;
        private Texture2D highlightSquare;

        private Texture2D whiteKing;

        private Dictionary<int, Texture2D> pieceSprites;

        public GraphicalBoard()
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
            IsMouseVisible = true;

            // create logical board
            logicalBoard = new LogicalBoard();

            // create and load squares
            int boardIndex = 0;
            int xPos;
            int yPos;

            // spans across one row at a time starting with 8th row
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    xPos = x * DisplaySettings.squareSize + DisplaySettings.edgePadding;
                    yPos = y * DisplaySettings.squareSize + DisplaySettings.edgePadding;

                    if ((x + y) % 2 == 0)
                    {
                        squares[boardIndex] = new Square(xPos, yPos, lightSquare, boardIndex);
                    } else {
                        squares[boardIndex] = new Square(xPos, yPos, darkSquare, boardIndex);
                    }

                    boardIndex++;
                }
            }
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
            lightSquare = Content.Load<Texture2D>("light_square");
            darkSquare = Content.Load<Texture2D>("dark_square");
            highlightSquare = Content.Load<Texture2D>("highlight_square");
            pieceSprites[2] = Content.Load<Texture2D>("white_king");
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
            
            //HighlightHoverSquare();
            //TODO handle click events

            base.Update(gameTime);
        }

        /// <summary>
        /// Highlights any square that the mouse is hovering over.
        /// </summary>
        protected void HighlightHoverSquare()
        {

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

            // TODO move some of this mouse stuff out of the draw method
            // Used to highlight squares that the mouse is hovering over
            lastMouseState = mouseState;
            mouseState = Mouse.GetState();
            Point mousePosition = new Point(mouseState.X, mouseState.Y);

            spriteBatch.Begin();

            // draw board squares
            for (int si = 0; si < 64; si++)
            {
                squares[si].Draw(spriteBatch);
            }

            /*
            int xPos;
            int yPos;
            int boardIndex = 0;
            Vector2 tempVect = new Vector2(0, 0);
            Rectangle rect = new Rectangle(0, 0, squareSize, squareSize);


            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {

                    xPos = x * squareSize + edgePadding;
                    yPos = y * squareSize + edgePadding;
                    tempVect.X = xPos;
                    tempVect.Y = yPos;

                    rect.X = xPos;
                    rect.Y = yPos;

                    if ((x + y) % 2 == 0)
                    {
                        spriteBatch.Draw(lightSquare, tempVect, Color.White);
                    } else {
                        spriteBatch.Draw(darkSquare, tempVect, Color.White);
                    }

                    if (rect.Contains(mousePosition))
                    {
                        spriteBatch.Draw(highlightSquare, tempVect, Color.White);
                    }

                    //TODO ensure no negatives
                    // TDOO ensure board index is not negative
                    if (boardState[boardIndex] != 0)
                    {

                        //TODO verify that x + y is in keys
                        spriteBatch.Draw(pieceSprites[boardState[boardIndex]], tempVect, Color.White);
                    }
                    boardIndex++;
                }
            }
            */

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
