using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Real_Time_Hobo.State_Classes;

namespace Real_Time_Hobo
{
    public enum Direction
    {
        Up = (short)-3,
        Down = (short)3,
        Left = (short)-1,
        Right = (short)1,
    };

    public static class Globals
    {
        public static Vector2 m_mousePosition;
        public static Rectangle m_mouseRectangle;
        public static Vector2 m_screenBoundaries;
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private MenuState menuState;
        private GameState gameState;
        private Texture2D m_mockMenu;
        
        public Game1() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            
            menuState = new MenuState();
            gameState = new GameState();
            IsMouseVisible = true;

            Globals.m_mousePosition = new Vector2(0, 0);
            Globals.m_mouseRectangle = new Rectangle(0, 0, 0, 0);
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
            InputManager.InputManager.InitaliseInputManager();
            Globals.m_screenBoundaries = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            
            StateManager.Initialize();
            MenuState.Initialize(this);
            GameState.Initialize(this);

            StateManager.Push(menuState);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            m_mockMenu = Content.Load<Texture2D>("Menu Sprites/MockMenu");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            Globals.m_mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            StateManager.Update();

            base.Update(gameTime);
           // Globals.m_mouseRectangle = new Rectangle(Globals.m_mousePosition.X, Globals.m_mousePosition.Y, 0, 0);

            InputManager.InputManager.UpdateInputs();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            spriteBatch.Begin();

            StateManager.Draw(); 

            spriteBatch.End();
        }

       #region PROPERTIES
        /// <summary>
        /// A property that returns a reference to the current spriteBatch
        /// </summary>
        public SpriteBatch BatchRef
        {
            get { return spriteBatch; }
        }
        /// <summary>
        /// A property that gets a reference to the current Game State
        /// </summary>
        public GameState GameRef
        {
            get { return gameState; }
        }
        /// <summary>
        /// A property that gets a reference to the current Menu State
        /// </summary>
        public MenuState MenuRef
        {
            get { return menuState; }
        }
        #endregion
    }
}
