using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Real_Time_Hobo.State_Classes
{
    class MenuState : State
    {
        Game1 game;
        SpriteBatch spriteBatch;

        private Texture2D m_menuBackground;
        private Texture2D m_menuTitle;
        private Texture2D m_startButton;
        private Texture2D m_exitButton;

        private Rectangle m_startButtonRectangle;

        /// <summary>
        /// Constructor
        /// </summary>
        public MenuState(Game1 a_Game1)
        {
            game = a_Game1;
        }

        /// <summary>
        /// Initialize Function
        /// </summary>
        public void LoadContent()
        {
            m_menuBackground = game.Content.Load<Texture2D>("Menu Sprites/menu_background");
            m_menuTitle = game.Content.Load<Texture2D>("Menu Sprites/menu_title");
            m_startButton = game.Content.Load<Texture2D>("Menu Sprites/menu_start");
            m_exitButton = game.Content.Load<Texture2D>("Menu Sprites/exit_button");

            m_startButtonRectangle = new Rectangle(0, 250, 1080, 100);

            spriteBatch  = game.spriteBatch;
        }

        /// <summary>
        /// Update Function
        /// </summary>
        public void Update()
        {

        }

        /// <summary>
        /// Draw Function
        /// </summary>
        public void Draw()
        {
            spriteBatch.Draw(m_menuBackground, new Rectangle(0, 0, 1080, 720), Color.White);
            spriteBatch.Draw(m_menuTitle, new Rectangle(-120, 0, 1260, 250), Color.White);
            spriteBatch.Draw(m_startButton, m_startButtonRectangle, Color.White);
            spriteBatch.Draw(m_exitButton, new Rectangle(10, 350, 1080, 100), Color.White);

                
        }
    }

}
