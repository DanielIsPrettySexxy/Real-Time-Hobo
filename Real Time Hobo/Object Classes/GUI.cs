using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Real_Time_Hobo.State_Classes;

namespace Real_Time_Hobo.Object_Classes
{
    class GUI
    {
        ///<summary> A static reference to Game1</summary>
        static Game1 game;

        static SpriteFont font;

        static Hobo m_hobo;

        static Texture2D m_guiBarTexture; 
        public GUI(Hobo hobo)
        {
            m_hobo = hobo;
        }

        public static void Initialize(Game1 a_gameRef)
        {
            game = a_gameRef;

            m_guiBarTexture = a_gameRef.Content.Load<Texture2D>("GUI/GUI");
            font = a_gameRef.Content.Load<SpriteFont>("font");
        }

        public void Update()
        {

        }

        public void Draw()
        {
            game.BatchRef.Draw(m_guiBarTexture, new Rectangle(0, 0, 1080, 720), Color.White);

            game.BatchRef.DrawString(font, "Bottles:" + m_hobo.Bottles.ToString(), new Vector2(80, 670), Color.White);
        }
    }
}
