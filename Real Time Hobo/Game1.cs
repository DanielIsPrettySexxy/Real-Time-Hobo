using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Real_Time_Hobo.State_Classes;

namespace Real_Time_Hobo
{
    /// <summary>
    /// An Enum that controls the direction of Transitions
    /// </summary>
    public enum Direction
    {
        Up = (short)-3,
        Down = (short)3,
        Left = (short)-1,
        Right = (short)1,
    };

    public static class Globals
    {
        public static Vector2 MousePosition;
        public static Rectangle MouseRectangle;
        public static Vector2 ScreenBoundaries;
        public static uint Seconds;
        public static uint Minutes;
        public static uint Hours;
        public static ushort Days;
        public static Color Day;
        public static Color Night;
        public static Color DayNightCycle;
        public static bool isDayTime;
        public static bool transition;
    }
    /// </summary>
    ///<summary> This is the main type for your game</summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private MenuState menuState;
        private GameState gameState;
        private float m_lerpValue;
        
        public Game1() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Globals.ScreenBoundaries.X = graphics.PreferredBackBufferWidth = 1080;
            Globals.ScreenBoundaries.Y = graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            
            menuState = new MenuState();
            gameState = new GameState();
            IsMouseVisible = true;

            Globals.MousePosition = new Vector2(0, 0);
            Globals.MouseRectangle = new Rectangle(0, 0, 0, 0);

            Globals.Day = new Color(255, 255, 255, 255);
            Globals.Night = new Color(55, 55, 55, 255);
            Globals.DayNightCycle = new Color(Globals.Night, 1);
            Globals.isDayTime = false;
            Globals.transition = false;
            Globals.Hours = 6;
            m_lerpValue = 0.08333f;
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
            Globals.ScreenBoundaries = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            
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
            Globals.MousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Globals.Seconds++;
            if(Globals.Seconds > 60)
            {
                Globals.Seconds = 0;
                Globals.Minutes++;

                if (Globals.transition == true)
                    m_lerpValue += 0.01666666f;
                    
            }
            if(Globals.Minutes > 60)
            {
                Globals.Minutes = 0;
                Globals.Hours++;
            }
            if(Globals.Hours > 24)
            {
                Globals.Days++;
                Globals.Hours = 0;
            }
            if (Globals.Days > 7) ;

            StateManager.Update();

            if (Globals.Hours >= 7)
                Globals.isDayTime = true;
            else if (Globals.Hours >= 17)
                Globals.isDayTime = false;


            if(Globals.Hours >= 0 && Globals.Hours <=6)
            {
                Globals.transition = false;
                m_lerpValue = 0.08333f;
                Globals.DayNightCycle = Globals.Night;
            }
            else if(Globals.Hours >= 6 && Globals.Hours <= 7)
            {
                Globals.transition = true;
                Globals.DayNightCycle = Color.Lerp(Globals.Night, Globals.Day, m_lerpValue);
            }
            else if(Globals.Hours >= 7 && Globals.Hours <= 17)
            {
                Globals.transition = false;
                m_lerpValue = 0.08333f;
                Globals.DayNightCycle = Globals.Day;
            }
            else if (Globals.Hours >= 17 && Globals.Hours <= 18)
            {
                Globals.transition = true;
                Globals.DayNightCycle = Color.Lerp(Globals.Day, Globals.Night, m_lerpValue);
            }
            else if (Globals.Hours >= 18 && Globals.Hours <= 24)
            {
                Globals.transition = false;
                m_lerpValue = 0.08333f;
                Globals.DayNightCycle = Globals.Night;
            }

            base.Update(gameTime);
           // Globals.m_mouseRectangle = new Rectangle(Globals.m_mousePosition.X, Globals.m_mousePosition.Y, 0, 0);

            InputManager.InputManager.UpdateInputs();
        }
        ///<summary>This is called when the game should draw itself.</summary>
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
