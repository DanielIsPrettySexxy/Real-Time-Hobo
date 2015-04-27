using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Real_Time_Hobo.State_Classes
{
    public class MenuState : State
    {
        static Game1 game;
        static SpriteBatch spriteBatch;

        private static Texture2D m_menuBackground;
        private static Texture2D m_menuTitle;
        private static Texture2D m_startButton;
        private static Texture2D m_exitButton;

        private Rectangle m_startButtonRectangle;
        private Rectangle m_backgroundRectangle;
        private Rectangle m_exitButtonRectangle;

        private int frames = 0;
        private int m_destination = -80;
        private int m_backgroundX = -50;

        /// <summary>
        /// Constructor
        /// </summary>
        public MenuState()
        {
            m_startButtonRectangle = new Rectangle(430, 280, 220, 80);
            m_backgroundRectangle = new Rectangle(m_backgroundX, 0, 1400, 720);
            m_exitButtonRectangle = new Rectangle(435, 380, 220, 80);
        }
        public static void Initialize(Game1 a_Game1)
        {
            game = a_Game1;
            m_menuBackground = game.Content.Load<Texture2D>("Menu Sprites/menu_background");
            m_menuTitle = game.Content.Load<Texture2D>("Menu Sprites/menu_title");
            m_startButton = game.Content.Load<Texture2D>("Menu Sprites/menu_start");
            m_exitButton = game.Content.Load<Texture2D>("Menu Sprites/exit_button");

            spriteBatch = game.BatchRef;
        }

        /// <summary>
        /// Update Function
        /// </summary>
        public void Update()
        {
            if(frames == 3)
            {
                if(m_backgroundRectangle.X > m_destination)
                {
                    m_backgroundRectangle.X -= 1;
                    if(m_backgroundRectangle.X == m_destination)
                        m_destination = m_backgroundX + 50;
                }      
                else if(m_backgroundRectangle.X < m_destination)
                {
                    m_backgroundRectangle.X += 1;
                    if (m_backgroundRectangle.X == m_destination)
                        m_destination = m_backgroundX - 80;
                }
                frames = 0;
            }
            if (m_startButtonRectangle.Contains(Globals.m_mousePosition) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                StateManager.Pop();
                StateManager.Push(game.GameRef);
            }
            if (m_exitButtonRectangle.Contains(Globals.m_mousePosition) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                game.Exit();
            }
            frames++;
        }

        /// <summary>
        /// Draw Function
        /// </summary>
        public void Draw()
        {
            spriteBatch.Draw(m_menuBackground, m_backgroundRectangle, Color.White);
            spriteBatch.Draw(m_menuTitle, new Rectangle(-120, 0, 1260, 250), Color.White);
            spriteBatch.Draw(m_startButton, m_startButtonRectangle, Color.White);
            spriteBatch.Draw(m_exitButton, m_exitButtonRectangle, Color.White);

            if(m_startButtonRectangle.Contains(Globals.m_mousePosition))
                spriteBatch.Draw(m_startButton, m_startButtonRectangle, Color.Red);
            
            if(m_exitButtonRectangle.Contains(Globals.m_mousePosition))
                spriteBatch.Draw(m_exitButton, m_exitButtonRectangle, Color.Red);
        }
    }

}
