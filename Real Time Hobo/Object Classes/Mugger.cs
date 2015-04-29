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
    class Mugger
    {
        #region VARIABLES

            ///<summary> A static reference to Game1</summary>
            static Game1 game;
            ///<summary>A static sprite for mugger to draw with</summary>
            static Texture2D muggerSprite;
            ///<summary>The current frame of the sprite thats drawing</summary>
            Rectangle m_frameBounds;
            ///<summary>The current dimensions of the sprite thats drawing</summary>
            Rectangle m_spriteBounds;
            ///<summary>The mugger position</summary>
            Vector2 m_position;
            ///<summary>The speed and direction of the mugger</summary>
            Vector2 m_velocity;
            ///The position of the current player
            Vector2 m_playerPosition;
            ///<summary>The number of pixels by which too offset the UVs each frame</summary>
            Vector2 m_frameOffset;
            ///<summary>The sprite orgin</summary>
            Vector2 m_origin;
            ///<summary>whether or not the sprite is fliped</summary>
            SpriteEffects m_turned;
            ///<summary>A ticker that controls how qucikly the animation is played</summary>
            ushort m_regulatorTick = 0;
            ///<summary>A ticker that controls the animation time</summary>
            ushort m_frameTick = 0;
            ///<summary>The number of bottles the mugger currently has</summary>
            ushort m_bottleCount = 0;
            ///<summary>The health of  the mugger</summary>
            int m_health = 60;
            ///<summary>Is the mugger attacking or not<summary>
            public bool m_attacking = false;
            ///<summary>Is this guy dead?<summary>
            public bool Dead = false;
        #endregion
        #region FUNCTIONS
            public Mugger(Vector2 a_Position)
            {
                m_frameOffset = new Vector2(524, 502);
                m_frameBounds = new Rectangle(0, 0, (int)m_frameOffset.X, (int)m_frameOffset.Y);
                m_position = a_Position;
                m_velocity = new Vector2(1,1);
                m_playerPosition = m_position;
                Random rand = new Random();
                m_bottleCount = (ushort)rand.Next(5,10);
                m_spriteBounds.Height = m_spriteBounds.Width = 250;
                m_origin = new Vector2(m_spriteBounds.Width, m_spriteBounds.Height);
            }
            ///<summary>Loads the muggers sprite and a reference to Game1</summary>
            /// <param name="a_gameRef">A reference to Game1 for loading and rendering</param>
            public static void Initialize(Game1 a_gameRef)
            {
                game = a_gameRef;
                muggerSprite = a_gameRef.Content.Load<Texture2D>("Object Sprites/Mugger_Sprites");
            }
            public int CompareBottles(ushort a_playerBottles)
            {
                int returnValue = ((int)m_bottleCount - (int)a_playerBottles);
                if (returnValue < 0)
                    m_health += returnValue;
                return returnValue;
            }
            ///<summary>Moves the UVs and moves the mugger towards the mouse</summary>
            public void Update()
            {
                m_regulatorTick++;
                m_velocity = m_playerPosition - (m_position + new Vector2(-110, 0));
                float length = m_velocity.Length();

                if(m_regulatorTick > 8)
                {
                    m_frameTick++;
                    if(m_frameTick < 3)
                        m_frameBounds.X += (int)m_frameOffset.X;
                    else
                    {
                        m_frameTick = 0;
                        m_frameBounds.X = 0;
                    } 
                    m_regulatorTick = 0;
                }
                if (length > 2)
                {
                    m_position += m_velocity;
                    m_velocity.Normalize();
                    m_velocity *= 2;
                    m_attacking = true;
                    m_frameBounds.Y = 0;
                }
                else
                {
                    m_frameBounds.Y = (int)m_frameOffset.Y;
                    m_attacking = true;
                }
                if(m_health < 0)
                    Dead = true;
                if (m_velocity.X < 0)
                    m_turned = SpriteEffects.FlipHorizontally;
                else
                    m_turned = SpriteEffects.None;
                m_spriteBounds.X = (int)m_position.X;
                m_spriteBounds.Y = (int)m_position.Y;
            }
            ///<summary>Draws the mugger</summary>
            public void Draw()
            {
                game.BatchRef.Draw(muggerSprite, m_spriteBounds, m_frameBounds, Globals.DayNightCycle, 0, m_origin, m_turned, 0);
            }
        #endregion
        #region PROPERIES
            public Vector2 Position
            {
                get { return m_position; }
            }
            public Vector2 PlayerPos
            {
                set { m_playerPosition = value; }
            }
        #endregion
    }
}
