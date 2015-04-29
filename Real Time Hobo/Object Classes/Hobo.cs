using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Real_Time_Hobo.State_Classes;

namespace Real_Time_Hobo
{
    ///<summary> The hobo and main player class used to interact with the world</summary>
    class Hobo
    {
        #region VARIABLES
            ///<summary> A static reference to Game1</summary>
            static Game1 game;
            ///<summary>A static sprite for hobo to draw with</summary>
            static Texture2D hoboSprite;
            ///<summary>The current frame of the sprite thats drawing</summary>
            Rectangle m_frameBounds;
            ///<summary>The current bounding box of the sprite</summary>
            Rectangle m_spriteBounds;
            ///<summary>The orgin of the sprite</summary>
            Vector2 m_orgin;
            ///<summary>whether or not the sprite is fliped</summary>
            SpriteEffects m_turned;
            ///<summary>Where the hobo should end up</summary>
            Vector2 m_destination;
            ///<summary>The hobos position</summary>
            public Vector2 m_position;
            ///<summary>The speed and direction of the hobo</summary>
            Vector2 m_velocity;
            ///<summary>The number of pixels by which too offset the UVs each frame</summary>
            Vector2 m_frameOffset;
            ///<summary>A ticker that controls the animation time</summary>
            ushort m_frameTick = 0;
            ///<summary>A ticker that controls how qucikly the animation is played</summary>
            ushort m_regulatorTick = 0;
            ///<summary>The number of bottles the hobo currently has</summary>
            ushort m_bottleCount = 15;
            ///<summary>The number of matirials for bas upgrades the hobo currently has</summary>
            ushort m_matCount = 1;
            ///<summary>The current health of the hobo</summary>
            ushort m_Health = 300;
            public bool Fighting = false;
        #endregion
        #region FUNCTIONS
            public Hobo()
            {
                m_frameOffset = new Vector2(524, 484);
                m_frameBounds = new Rectangle(0, 0, (int)m_frameOffset.X, (int)m_frameOffset.Y);
                m_position = new Vector2(Globals.ScreenBoundaries.X/2, Globals.ScreenBoundaries.Y/2);
                m_destination = m_velocity = new Vector2(1,1);
                m_spriteBounds = new Rectangle((int)m_position.X,(int)m_position.Y,250,250);
                m_orgin = new Vector2(m_spriteBounds.Width,m_spriteBounds.Height);
            }
            /// <summary>
            /// Loads the hobos sprite and a reference to Game1
            /// </summary>
            /// <param name="a_gameRef">A reference to Game1 for loading and rendering</param>
            public static void Initialize(Game1 a_gameRef)
            {
                game = a_gameRef;
                hoboSprite = a_gameRef.Content.Load<Texture2D>("Object Sprites/Hobo_Sprites");
            }
            ///<summary>Adds a resource to the hobo based on the type given</summary>
            /// <param name="a_resType">The type of resource to add</param>
            /// <param name="a_num">The number of resources to add</param>
            public void AddResource(TrashType a_resType, ushort a_num)
            {
                switch(a_resType)
                {
                    case TrashType.Bottles : m_bottleCount += a_num; break;
                    case TrashType.Food    : m_Health += a_num; break;
                    case TrashType.Materials : m_matCount += a_num; break;
                    case TrashType.Random : 
                    {
                        Random rand = new Random();
                        switch (rand.Next(1,3))
                        {
                            case 1 :m_bottleCount += a_num; break;
                            case 2 :m_Health += a_num; break;
                            case 3: m_matCount += a_num; break;
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
                m_regulatorTick++;
                m_position += m_velocity;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    m_destination = Globals.MousePosition;

                m_velocity = m_destination - m_position;
                if (m_velocity.Length() > 3)
                {
                    if (m_regulatorTick > 8)
                    {
                        m_frameTick++;
                        if (m_frameTick < 3)
                            m_frameBounds.X += (int)m_frameOffset.X;
                        else
                        {
                            m_frameTick = 0;
                            m_frameBounds.X = 0;
                        }
                        m_regulatorTick = 0;
                    }
                    m_velocity = m_destination - m_position;
                    m_velocity.Normalize();
                    m_velocity *= 4;
                }
                else
                {
                    m_velocity = new Vector2(0,0);
                    m_destination = m_position;
                    m_frameBounds.X = 0;
                }
                if (Fighting)
                    m_frameBounds.Y = (int)m_frameOffset.Y;
                else
                    m_frameBounds.Y = 0;

                if(m_velocity.X < 0)
                    m_turned = SpriteEffects.None;
                else
                    m_turned = SpriteEffects.FlipHorizontally;

                m_spriteBounds.X = (int)m_position.X;
                m_spriteBounds.Y = (int)m_position.Y;
            }
            ///<summary>Draws the hobo</summary>
            public void Draw()
            {
                game.BatchRef.Draw(hoboSprite, m_spriteBounds, m_frameBounds, Globals.DayNightCycle, 0, m_orgin, m_turned, 0);
            }
        #endregion
        #region PROPERIES
            public ushort Bottles
            {
                get { return m_bottleCount; }
                set { m_bottleCount = value; }
            }
        #endregion
    }
}
