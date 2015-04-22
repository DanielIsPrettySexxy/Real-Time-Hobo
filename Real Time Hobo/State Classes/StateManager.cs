using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Real_Time_Hobo
{
    /// <summary>
    /// A game state interface class to and polymorphisim for the game manager to handle
    /// </summary>
    interface State
    {
        /// <summary>
        /// Updates the current state
        /// </summary>
        void Update();
        /// <summary>
        /// Draws the current state
        /// </summary>
        void Draw();
    }
    class StateManager
    {
        /// <summary>
        /// The GameManager instance is the actual stack that the gamestates run on 
        /// </summary>
        private static Stack<State> GameManager;
        /// <summary>
        /// Pushes the argument state on to the top of the stack
        /// </summary>
        /// <param name="a_State"> The current state to push onto the stack</param>
        /// <returns>Returns true if it was sucessfully pushed and false if the state is null</returns>
        public static bool Push(State a_State)
        {
            if(a_State != null)
            {
                GameManager.Push(a_State);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Initalise the stack DO BEFORE USING THIS CLASS AT ALL 
        /// </summary>
        public static void Initalise()
        {
            GameManager = new Stack<State>();
        }
        /// <summary>
        /// Pops the state on top of the stack off
        /// </summary>
        /// <returns>Returns true if it was sucessful and false if the stack is empty</returns>
        public static bool Pop()
        {
            if (GameManager.Count > 0)
            {
                GameManager.Pop();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Updates the state currently on the top of the stack
        /// </summary>
        public static void Update()
        {
            GameManager.Peek().Update();
        }
        /// <summary>
        /// Draws the state currently at the top of the stack
        /// </summary>
        public static void Draw()
        {
            GameManager.Peek().Draw();
        }
        /// <summary>
        /// Clears the stack of all states currently on it
        /// </summary>
        ~StateManager()
        {
            GameManager.Clear();
        }
    }
}
