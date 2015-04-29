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
    public enum TrashType
    {
        Food = 0,
        Materials = 1,
        Bottles = 2,
        Random = 3
    }
    public class GameState : IState
    {
        /// <summary>
        /// A reference to game1
        /// </summary>
        private static Game1 m_game;
        /// <summary>
        /// This is the players charcter that they use to navigate and intereact with the world
        /// </summary>
        private  Hobo m_player;
        /// <summary>
        /// The total number of maps the player can navigate through
        /// </summary>
        const ushort m_mapCount = 4;
        /// <summary>
        /// The background of the game
        /// </summary>
        private MapScene m_map;
        /// <summary>
        /// Whether the player is going up, down, left or right to the next map
        /// </summary>
        private Direction m_mapTransition;
        /// <summary>The Garbage can (if there is one) on the map</summary>
        private GatherArea m_garbagePlace;
        ///<summary>Minimum and maximum resource value</summary>
        ushort m_minRes, m_maxRes;
        ///<summary>Controls random values for resources</summary>
        Random m_resRand;
        public GameState()
        {
            m_player = new Hobo();
            m_map = new MapScene(0);
            m_garbagePlace = new GatherArea(TrashType.Bottles,new Rectangle(20,20,100,100));
            m_resRand = new Random();
        }
        public static void Initialize(Game1 a_mainGame)
        {
            m_game = a_mainGame;
            GatherArea.Initialize(a_mainGame);
            Hobo.Initialize(a_mainGame);
            MapScene.Initialize(m_game, m_mapCount);
        }
        public void Update()
        {
            if (m_garbagePlace.CheckCollision(m_player.Position))
            {
                ushort randValue = (ushort)m_resRand.Next(m_minRes, m_maxRes);
                m_player.AddResource(m_garbagePlace.Trash,randValue);
                m_garbagePlace.Pillaged = true;
            }
            m_player.Update();
            m_map.Update();
        }
        public void Draw()
        {
            m_map.Draw();
            m_garbagePlace.Draw();
            m_player.Draw();
        }
    }
}