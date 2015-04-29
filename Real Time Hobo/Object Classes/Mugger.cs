﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Real_Time_Hobo.State_Classes;

namespace Real_Time_Hobo.Object_Classes
{
    class Mugger
    {
          #region VARIABLES

            ///<summary> A static reference to Game1</summary>
            static Game1 game;
            ///<summary>A static sprite for hobo to draw with</summary>
            static Texture2D muggerSprite;
            ///<summary>The current frame of the sprite thats drawing</summary>
            Rectangle m_frameBounds;
            ///<summary>The hobos position</summary>
            Vector2 m_position;
            ///<summary>The speed and direction of the hobo</summary>
            Vector2 m_velocity;
            ///<summary>The number of pixels by which too offset the UVs each frame</summary>
            Vector2 m_frameOffset;
            ///<summary>A ticker that controls the animation time</summary>
            ushort m_frameTick = 0;
            ///<summary>The number of bottles the hobo currently has</summary>
            ushort m_bottleCount = 1;

        #endregion
        #region FUNCTIONS
            public Mugger()
            {
                m_frameOffset = new Vector2(367,474);
                m_frameBounds = new Rectangle(0, 0, (int)m_frameOffset.X, (int)m_frameOffset.Y);
                m_position = new Vector2(0,0);
                m_velocity = new Vector2(1,1);
            }
            /// <summary>
            /// Loads the hobos sprite and a reference to Game1
            /// </summary>
            /// <param name="a_gameRef">A reference to Game1 for loading and rendering</param>
            public static void Initialize(Game1 a_gameRef)
            {
                game = a_gameRef;
                muggerSprite = a_gameRef.Content.Load<Texture2D>("Object Sprites/Hobo_Sprites");
            }
            ///<summary>Adds a resource to the hobo based on the type given</summary>
            /// <param name="a_resType">The type of resource to add</param>
            /// <param name="a_num">The number of resources to add</param>
            public void AddResource(TrashType a_resType, ushort a_num)
            {
                switch(a_resType)
                {
                    case TrashType.Bottles : m_bottleCount += a_num; break;
                    case TrashType.Random : 
                    {
                        Random rand = new Random();
                        switch (rand.Next(1,3))
                        {
                            case 1 :m_bottleCount += a_num; break;
                            default: break;
                        }
                        break;
                    }
                    default: break;
                }
            }
            ///<summary>Moves the UVs and moves the hobo towards the mouse</summary>
            public void Update()
            {
                m_frameTick++;
                m_position += m_velocity;

                if(m_frameTick < 3)
                    m_frameBounds.X += (int)m_frameOffset.X;
                else
                {
                    m_frameTick = 0;
                    m_frameBounds.X = 0;
                }
                m_velocity = Globals.MousePosition - m_position;
                m_velocity.Normalize();
                m_velocity *= 3;
            }
            ///<summary>Draws the hobo</summary>
            public void Draw()
            {
                game.BatchRef.Draw(muggerSprite, m_position, m_frameBounds, Color.White);
            }
        #endregion
        #region PROPERIES
            public Vector2 Position
            {
                get { return m_position; }
            }
        #endregion
    }
}