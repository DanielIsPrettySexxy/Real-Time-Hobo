/// AUTHOR: Daniel Waterston
/// Last edit: 28/04/2015
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Real_Time_Hobo.State_Classes;
using Microsoft.Xna.Framework.Graphics;

namespace Real_Time_Hobo.Object_Classes
{
    class GatherArea
    {
        ///<summary> The Type of garbage this varaiable is</summary>
        TrashType m_myType;
        ///<summary> A rectangle for countaining the hitbox for the gathering areas to get items</summary>
        Rectangle m_hitBox;
        ///<summary>A bool controlling weather or not this area has been serched</summary>
        bool m_pillaged = false;
        ///<summary>The sprite for the garbage can</summary>
        private static Texture2D sprite;
        ///<summary>A reference to the game</summary>
        private static Game1 gameRef;
        public GatherArea(TrashType a_garbageType, Rectangle a_hitBox)
        {
            if ((uint)a_garbageType <= 3)
                m_myType = a_garbageType;
            else
                m_myType = TrashType.Random;
            m_hitBox = a_hitBox;
        }
        public bool CheckCollision(Vector2 a_playerPos)
        {
            if (a_playerPos.X > m_hitBox.X && a_playerPos.X < (m_hitBox.X + m_hitBox.Width))
                if (a_playerPos.Y > m_hitBox.Y && a_playerPos.Y < (m_hitBox.Y + m_hitBox.Height))
                    return true;
            return false;
        }
        public static void Initialize(Game1 a_gameRef)
        {
            sprite = a_gameRef.Content.Load<Texture2D>("Object Sprites/Garbage Can");
            gameRef = a_gameRef;
        }
        public void Draw()
        {
          //  gameRef.BatchRef.Draw(sprite,m_hitBox,Color.White);
        }
        public TrashType Trash
        {
            get { return m_myType; }
            set { m_myType = value; }
        }
        public Rectangle HitBox
        {
            set { HitBox = value; }
        }
        public bool Pillaged
        {
            get { return m_pillaged;}
            set {m_pillaged = value;}
        }
    }
}
