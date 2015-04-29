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
    class HomeBase
    {

        ///<summary> A static reference to Game1</summary>
        static Game1 game;

        static private float m_resourceNumber;

        static Texture2D m_baseTexture;
        static Texture2D m_stage1Base;
        static Texture2D m_stage2Base;
        static Texture2D m_stage3Base;
        static Texture2D m_stage4Base;
        uint Tick = 0;
        public HomeBase()
        {
            m_resourceNumber = 0;
        }

        public static void Initialize(Game1 a_gameRef)
        {
            game = a_gameRef;
            m_baseTexture = a_gameRef.Content.Load<Texture2D>("Map Sprites/Map00");
            m_stage1Base = a_gameRef.Content.Load<Texture2D>("Map Sprites/Bases/Base1");
            m_stage2Base = a_gameRef.Content.Load<Texture2D>("Map Sprites/Bases/Base2");
            m_stage3Base = a_gameRef.Content.Load<Texture2D>("Map Sprites/Bases/Base3");
            m_stage4Base = a_gameRef.Content.Load<Texture2D>("Map Sprites/Bases/Base4");
        }
        public void Update()
        {
            Tick++;
            if(Tick > 50)
            { 
            if (Keyboard.GetState().IsKeyDown(Keys.M))
                m_resourceNumber += 5;
            Tick = 0;
            }
        }
        public void Draw()
        {
            game.BatchRef.Draw(m_baseTexture, new Rectangle(0, 0, 1080, 720), Globals.DayNightCycle);


            if(m_resourceNumber < 5)
            {
                game.BatchRef.Draw(m_stage1Base, new Rectangle(0, 0, 1080, 720), Globals.DayNightCycle);
            }
            
            else if(m_resourceNumber >= 5 && m_resourceNumber <= 15)
            {
                game.BatchRef.Draw(m_stage2Base, new Rectangle(0, 0, 1080, 720), Globals.DayNightCycle);
            }
            else if (m_resourceNumber >= 15 && m_resourceNumber <= 20)
            {
                game.BatchRef.Draw(m_stage3Base, new Rectangle(0, 0, 1080, 720), Globals.DayNightCycle);
            }
            else if (m_resourceNumber >= 20)
            {
                game.BatchRef.Draw(m_stage4Base, new Rectangle(0, 0, 1080, 720), Globals.DayNightCycle);
            }
        }
        public float ResourceNumber
        {
            set { m_resourceNumber = value; }
        }
    }
}
