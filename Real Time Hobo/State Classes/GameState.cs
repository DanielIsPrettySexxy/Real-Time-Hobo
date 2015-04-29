/// AUTHOR: Daniel Waterston
/// Last edit: 28/04/2015
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Real_Time_Hobo.State_Classes;
using Real_Time_Hobo.Object_Classes;
using Microsoft.Xna.Framework;

namespace Real_Time_Hobo.State_Classes
{
    /// <summary>
    /// The type of garbage cans available 
    /// </summary>
    public enum TrashType : short
    {
        Food = 0,
        Materials = 1,
        Bottles = 2,
        Random = 3
    }
    public class GameState : IState
    {
        ///<summary>A reference to game1</summary>
        private static Game1 m_game;
        ///<summary>This is the players charcter that they use to navigate and intereact with the world</summary>
        private  Hobo m_player;
        ///<summary>The total number of maps the player can navigate through</summary>
        const ushort m_mapCount = 5;
        ///<summary>The background of the game</summary>
        private MapScene m_map;
        ///<summary>Whether the player is going up, down, left or right to the next map</summary>
        private Direction m_mapTransition;
        ///<summary>The Garbage can (if there is one) on the map</summary>
        private GatherArea m_garbagePlace;
        ///<summary>Minimum and maximum resource value</summary>
        ushort m_minRes, m_maxRes;
        ///<summary>The mugger to hit the player</summary>
        Mugger m_mugger;
        ///<summary>Mugger tick</summary>
        uint m_mugTick = 0;
        ///<summary>Controls random values for resources</summary>
        Random m_resRand;
        private bool m_isItTwo, m_isItOne;
        HomeBase m_base;
        GUI m_gui;
        
        public GameState()
        {
            m_player = new Hobo();
            m_map = new MapScene(0);
            m_garbagePlace = new GatherArea(TrashType.Bottles, new Rectangle(75, 95, 650, 700));
            m_resRand = new Random();
            m_mugger = new Mugger(new Vector2(20,20));
            m_base = new HomeBase();
            m_gui = new GUI(m_player);
        }
        public static void Initialize(Game1 a_mainGame)
        {
            m_game = a_mainGame;
            Mugger.Initialize(a_mainGame);
            GatherArea.Initialize(a_mainGame);
            Hobo.Initialize(a_mainGame);
            MapScene.Initialize(m_game, m_mapCount);
            HomeBase.Initialize(a_mainGame);
            GUI.Initialize(a_mainGame);
        }
        public void Update()
        {
            m_base.Update();
            if (m_map.MapLoc == 1)
                m_isItTwo = true;
            else
                m_isItTwo = false;
            if (m_map.MapLoc == 0)
                m_isItOne = true;
            else
                m_isItOne = false;
            if (m_garbagePlace.CheckCollision(m_player.m_position))
            {
                ushort randValue = (ushort)m_resRand.Next(m_minRes, m_maxRes);
                m_player.AddResource(m_garbagePlace.Trash,randValue);
                m_garbagePlace.Pillaged = true;
            }
            m_player.Update();
            if (m_player.m_position.Y > (Globals.ScreenBoundaries.Y + 50))
                if (!(m_map.Transition(Direction.Down)))
                    m_player.m_position.Y = Globals.ScreenBoundaries.Y - 50;
                else
                    m_player.m_position.Y = Globals.ScreenBoundaries.Y - 200;

            if (m_player.m_position.X > (Globals.ScreenBoundaries.X + 50))
                if (!(m_map.Transition(Direction.Right)))
                    m_player.m_position.X = Globals.ScreenBoundaries.X - 50;
                else
                    m_player.m_position.X = 50;

            if (m_player.m_position.X < 0)
                if (!(m_map.Transition(Direction.Left)))
                    m_player.m_position.X = 50;
                else
                    m_player.m_position.X = Globals.ScreenBoundaries.X - 50;
            if (m_isItTwo)
            {
                if (m_mugger.m_attacking)
                    m_player.Fighting = true;
                else
                    m_player.Fighting = false;

                if (!m_mugger.Dead)
                {
                    m_mugTick++;
                    m_mugger.Update();
                    m_mugger.PlayerPos = m_player.m_position;
                    if(m_mugTick > 24)
                    {
                        m_mugTick = 0;
                        m_mugger.CompareBottles(m_player.Bottles);
                    }
                }
            }
            m_map.Update();
        }
        public void Draw()
        {
            m_map.Draw();
            if (!m_mugger.Dead && m_isItTwo)
            {
                m_mugger.Draw();
            }
            if(m_isItOne)
                m_base.Draw();
            m_garbagePlace.Draw();
            m_player.Draw();
            m_gui.Draw();
            
        }
    }
}