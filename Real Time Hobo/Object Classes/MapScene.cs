///
/// AUTHOR: Daniel Waterston
/// Last edit: 27/04/2015
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Real_Time_Hobo
{
    ///<summary> A Class for the background that handles images and scene changes</summary>
    class MapScene
    {
        //A reference to the game for loading in content
        private static Game1 m_gameRef;
        //A array of all the sprites for the backrounds, loaded in at the start for efficacy
        private static Texture2D[] spriteSets;
        //The maximum number of maps in the game
        private static ushort mapCount = 5;
        //The cordinates of the current scene
        private ushort m_cordinate;
        //The directions you can go from this point in the Map
        bool MapUp = false, MapLeft = false, MapDown = false, MapRight = false;
        ///<summary>Deafult constructor for the Map Scene</summary>
        ///<param name="a_row"> What row the current area of the map is on (The X location)</param>
        ///<param name="a_col"> What collum the current area of the map is on (The Y location)</param>
        public MapScene(ushort a_cordinate)
        {
            m_cordinate = a_cordinate;
            CalculateTransitions();
        }
        private void CalculateTransitions()
        {
            short upNum = (short)(Direction.Up), downNum = (short)(Direction.Down), leftNum = (short)(Direction.Left), rightNum = (short)(Direction.Right);
            if ((m_cordinate + upNum) >= 0)
                MapUp = true;
            else
                MapUp = false;
            if ((m_cordinate + downNum) <= mapCount)
                MapDown = true;
            else
                MapDown = false;
            if ((m_cordinate + leftNum) >= 0)
                MapLeft = true;
            else
                MapLeft = false;
            if ((m_cordinate + rightNum) <= mapCount)
                MapRight = true;
            else
                MapRight = false;
        }
        public static void Initialize(Game1 a_game, ushort a_mapCount)
        {
            m_gameRef = a_game;
            mapCount = a_mapCount;
            spriteSets = new Texture2D[a_mapCount];
            //Loops through and loads in all sprites for the maps
            for (ushort I = 0; I < a_mapCount; ++I )
            {
                string strCor = (I < 10) ? "0" + I.ToString() : I.ToString();
                spriteSets[I] = m_gameRef.Content.Load<Texture2D>("Map Sprites/" + "Map" + strCor);
            }

        }
        ///<summary>Atempts to transition to a new map</summary>
        ///<param name="a_direction">The direction to transition to</param>
        ///<returns>If the map sucesfully transitions it'll return true, otherwise it'll return false</returns>
        public bool Transition(Direction a_direction)
        {
            short tranVal = (short)a_direction;
            //Alters the coordinate IF the coordinate would stay in bounds, otherwise returns false
            switch (tranVal)
            {
                case -1: if (MapLeft){m_cordinate -= 1; }  else { return false;} break;
                case 1:  if (MapRight){ m_cordinate += 1;} else { return false;} break;
                case 3:  if (MapDown){m_cordinate +=3;}    else { return false;} break;
                case -3: if (MapUp) { m_cordinate -= 3;}   else  { return false;} break;
                default: return false;
            }
            CalculateTransitions();
            return true;
        }
        public void Update()
        {
        }
        public void Draw()
        {
            m_gameRef.BatchRef.Draw(spriteSets[m_cordinate], new Vector2(0, 0), Globals.DayNightCycle);
        }
        public ushort MapLoc
        {
            get { return m_cordinate; }
        }
    }
}
