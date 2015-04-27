///
/// AUTHOR: Daniel Waterston
/// Last edit: 27/04/2015
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Real_Time_Hobo.State_Classes;

namespace Real_Time_Hobo.State_Classes
{
    public class GameState : State
    {
        /// <summary>
        /// A reference to game1
        /// </summary>
        private static Game1 game;
        /// <summary>
        /// This is the players charcter that they use to navigate and intereact with the world
        /// </summary>
        private  Hobo player;
        /// <summary>
        /// The total number of maps the player can navigate through
        /// </summary>
        const ushort mapCount = 4;
        /// <summary>
        /// The background of the game
        /// </summary>
        private MapScene map;
        /// <summary>
        /// Whether the player is going up, down, left or right to the next map
        /// </summary>
        private Direction mapTransition;
        public GameState()
        {
            player = new Hobo();
            map = new MapScene(0);
        }
        public static void Initialize(Game1 a_mainGame)
        {
            game = a_mainGame;
            Hobo.Initialize(a_mainGame);
            MapScene.Initialize(game, mapCount);
        }
        public void Update()
        {
            player.Update();
            map.Update();
        }
        public void Draw()
        {
            map.Draw();
            player.Draw();
        }
    }
}