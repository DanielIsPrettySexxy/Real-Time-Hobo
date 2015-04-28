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
    /// <summary>
    /// A Class for the background that handles images and scene changes
    /// </summary>
    class MapScene
    {
        //A reference to the game for loading in content
        private static Game1 gameRef;
        //A array of all the sprites for the backrounds, loaded in at the start for efficacy
        private static Texture2D[] spriteSets;
        //The maximum number of maps in the game
        private static ushort mapCount;
        //The cordinates of the current scene
        ushort cordinate;
        //The directions you can go from this point in the Map
        bool MapUp = false, MapLeft = false, MapDown = false, MapRight = false;
        ///<summary>Deafult constructor for the Map Scene</summary>
        ///<param name="a_row"> What row the current area of the map is on (The X location)</param>
        ///<param name="a_col"> What collum the current area of the map is on (The Y location)</param>
        public MapScene(ushort a_cordinate)
        {
            cordinate = a_cordinate;
            CalculateTransitions();
        }
        private void CalculateTransitions()
        {
            if ((cordinate + Direction.Up) >= 0)
                MapUp = true;
            if ((cordinate + (ushort)Direction.Down) >= mapCount)
                MapDown = true;
            if ((cordinate + Direction.Left) >= 0)
                MapLeft = true;
            if ((cordinate + (ushort)Direction.Right) >= mapCount)
                MapRight = true;
        }
        public static void Initialize(Game1 a_game, ushort a_mapCount)
        {
            gameRef = a_game;
            mapCount = a_mapCount;
            spriteSets = new Texture2D[a_mapCount];
            //Loops through and loads in all sprites for the maps
            for (ushort I = 0; I < a_mapCount; ++I )
            {
                string strCor = (I < 10) ? "0" + I.ToString() : I.ToString();
                spriteSets[I] = gameRef.Content.Load<Texture2D>("Map Sprites/" + "Map" + strCor);
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
                case -1: if (MapLeft){cordinate -= 1; }  else { return false;} break;
                case 1:  if (MapRight){ cordinate += 1;} else { return false;} break;
                case 3:  if (MapDown){cordinate +=3;}    else { return false;} break;
                case -3: if (MapUp) { cordinate -= 3;}  else  { return false;} break;
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
            gameRef.BatchRef.Draw(spriteSets[cordinate],new Vector2(0,0),Color.White);
        }
        public ushort MapLoc
        {
            get { return cordinate; }
        }
    }
}
