///
/// AUTHOR: Daniel Waterston
/// Last edit: 1/04/2015
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Real_Time_Hobo
{
    /// <summary>
    /// A Class for the background that handles images and scene changes
    /// </summary>
    class MapScenes
    {
        //The actual background texture for each scene
        readonly Texture2D background;
        //The cordinates of the current scene
        readonly uint[] cordinates;
        //The directions you can go from this point in the Map
        MapScenes MapUp, MapLeft, MapDown, MapRight;
        /// <summary>
        /// Deafult constructor for the Map Scene
        /// </summary>
        /// <param name="a_row"> What row the current area of the map is on (The X location)</param>
        /// <param name="a_col"> What collum the current area of the map is on (The Y location)</param>
        public MapScenes(uint a_row, uint a_col)
        {
            cordinates[0] = a_row;
            cordinates[1] = a_col;
        }
        public void Update()
        {
        }
        public void Draw()
        {
        
        }
    }
}
