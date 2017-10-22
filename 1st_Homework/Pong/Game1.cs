using System.Collections.Generic;
using _3rd_Assigment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /// <summary >
        /// Bottom paddle object
        /// </summary >
        public Paddle PaddleBottom { get; private set; }
        /// <summary >
        /// Top paddle object
        /// </summary >
        public Paddle PaddleTop { get; private set; }
        /// <summary >
        /// Ball object
        /// </summary >
        public Ball Ball { get; private set; }
        /// <summary >
        /// Background image
        /// </summary >
        public Background Background { get; private set; }
        /// <summary >
        /// Sound when ball hits an obstacle .
        /// SoundEffect is a type defined in Monogame framework
        /// </summary >
        public SoundEffect HitSound { get; private set; }
        /// <summary >
        /// Background music . Song is a type defined in Monogame framework
        /// </summary >
        public Song Music { get; private set; }
        /// <summary >
        /// Generic list that holds Sprites that should be drawn on screen
        /// </summary >
        private IGenericList<Sprite> SpritesForDrawList = new GenericList<Sprite>();

        public GenericList<Wall> Walls { get; set; }
        public GenericList<Wall> Goals { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 600,
                PreferredBackBufferWidth = 400
            };
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
            // TODO: Add your initialization logic here


            // Screen bounds details . Use this information to set up game objects positions.
            var screenBounds = GraphicsDevice.Viewport.Bounds;
            PaddleBottom = new Paddle(GameConstants.PaddleDefaultWidth,
            GameConstants.PaddleDefaulHeight, GameConstants.PaddleDefaulSpeed);
            PaddleBottom.X = screenBounds.Width / 2f - PaddleBottom.Width / 2f;
            PaddleBottom.Y = screenBounds.Bottom - PaddleBottom.Height;
            PaddleTop = new Paddle(GameConstants.PaddleDefaultWidth,
                GameConstants.PaddleDefaulHeight, GameConstants.PaddleDefaulSpeed);
            PaddleTop.X = screenBounds.Width / 2f - PaddleTop.Width / 2f;
            PaddleTop.Y = screenBounds.Top;
            Ball = new Ball(GameConstants.DefaultBallSize, GameConstants.DefaultInitialBallSpeed, 
                GameConstants.DefaultBallBumpSpeedIncreaseFactor)
            {
                X = screenBounds.Width / 2f,
                Y = screenBounds.Height / 2f
            };
            Background = new Background(screenBounds.Width, screenBounds.Height);
            // Add our game objects to the sprites that should be drawn collection .
            SpritesForDrawList.Add(Background);
            SpritesForDrawList.Add(PaddleBottom);
            SpritesForDrawList.Add(PaddleTop);
            SpritesForDrawList.Add(Ball);


            // Adds walls and goals to the game. Walls are horizontal borders, and goals vertical.
            Walls = new GenericList<Wall>()
            {
                // try with 100 for default wall size !
                new Wall ( - GameConstants.WallDefaultSize ,0 ,
                    GameConstants.WallDefaultSize , screenBounds.Height ) ,
                new Wall ( screenBounds.Right ,0 , GameConstants.WallDefaultSize ,
                    screenBounds.Height ), 
            };
            Goals = new GenericList<Wall>()
            {
                new Wall (0 , screenBounds.Height , screenBounds.Width ,
                    GameConstants.WallDefaultSize ) ,
                new Wall ( screenBounds.Top , - GameConstants.WallDefaultSize ,
                    screenBounds.Width , GameConstants.WallDefaultSize ), 
            };


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

            // TODO: use this.Content to load your game content here

            // Set textures
            Texture2D paddleTexture = Content.Load<Texture2D>("paddle");
            PaddleBottom.Texture = paddleTexture;
            PaddleTop.Texture = paddleTexture;
            Ball.Texture = Content.Load<Texture2D>("ball");
            Background.Texture = Content.Load<Texture2D>("background");
            // Load sounds
            // Start background music
            HitSound = Content.Load<SoundEffect>("hit");

            Music = Content.Load<Song>("music");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Music);
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
            var touchState = Keyboard.GetState();

            if (touchState.IsKeyDown(Keys.Left))
            {
                PaddleBottom.X = PaddleBottom.X - (float)(PaddleBottom.Speed *
                                                          gameTime.ElapsedGameTime.TotalMilliseconds);

                PaddleBottom.X = MathHelper.Clamp(PaddleBottom.X, 0, graphics.GraphicsDevice.Viewport.Width -
                                                                               PaddleBottom.Width);
            }
            if (touchState.IsKeyDown(Keys.Right))
            {
                PaddleBottom.X = PaddleBottom.X + (float)(PaddleBottom.Speed *
                                                          gameTime.ElapsedGameTime.TotalMilliseconds);

            }

            if (touchState.IsKeyDown(Keys.A))
            {
                PaddleTop.X = PaddleTop.X - (float)(PaddleTop.Speed *
                                                          gameTime.ElapsedGameTime.TotalMilliseconds);

            }
            if (touchState.IsKeyDown(Keys.D))
            {
                PaddleTop.X = PaddleTop.X + (float)(PaddleTop.Speed *
                                                          gameTime.ElapsedGameTime.TotalMilliseconds);

            }

            PaddleBottom.X = MathHelper.Clamp(PaddleBottom.X, 0, graphics.GraphicsDevice.Viewport.Width -
                                                                    PaddleBottom.Width);
            PaddleTop.X = MathHelper.Clamp(PaddleTop.X, 0, graphics.GraphicsDevice.Viewport.Width -
                                                                    PaddleTop.Width);


            var ballPositionChange = Ball.Direction *
                                     (float)(gameTime.ElapsedGameTime.TotalMilliseconds * Ball.Speed);
            Ball.X += ballPositionChange.X;
            Ball.Y += ballPositionChange.Y;


            //checking Collision

            // If ball has collision with paddles ( with appropriate movement direction !!)
            // Reverse Y direction of the ball
            // Increase the ball speed by bump speed increase factor
            if (CollisionDetector.Overlaps(Ball, PaddleTop) || CollisionDetector.Overlaps(Ball,PaddleBottom))
            {
                Ball.Direction = Ball.Direction * (new Vector2(1, -1));
                Ball.Speed *= Ball.BumpSpeedIncreaseFactor;
                if (Ball.Speed > Ball.MaxSpeed) Ball.Speed = Ball.MaxSpeed;
            }


            // Ball - side walls
            // If ball has collision with any of the side wall
            // Reverse X direction of the ball
            // Increase the ball speed by bump speed increase factor
            foreach (Wall w in Walls)
            {
                if (CollisionDetector.Overlaps(Ball, w))
                {
                    Ball.Direction = Ball.Direction * (new Vector2(-1, 1));
                    Ball.Speed *= Ball.BumpSpeedIncreaseFactor;
                    if (Ball.Speed > Ball.MaxSpeed) Ball.Speed = Ball.MaxSpeed;
                    break;
                    
                }
            }


            // If ball has collision with winning walls ( goals )
            // Move ball to the center
            // Reset ball speed
            // Play hit sound with : HitSound . Play ();
            foreach (Wall g in Goals)
            {
                if (CollisionDetector.Overlaps(Ball, g))
                {

                    Ball.X = graphics.GraphicsDevice.Viewport.Width / 2f;
                    Ball.Y = graphics.GraphicsDevice.Viewport.Height / 2f;
                    Ball.Speed = GameConstants.DefaultInitialBallSpeed;
                    HitSound.Play();
                    
                    break;
                }
            }


            



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
            // Start drawing .
            spriteBatch.Begin();
            for (int i = 0; i < SpritesForDrawList.Count; i++)
            {
                SpritesForDrawList.GetElement(i).DrawSpriteOnScreen(spriteBatch);
            }
            // End drawing .
            // Send all gathered details to the graphic card in one batch .
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
