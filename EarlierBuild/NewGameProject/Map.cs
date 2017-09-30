using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGameProject
{
    class Map
    {
        string[,] map = new string[5, 24];
        Random randy = new Random();

        /// <summary>
        /// indexer
        /// </summary>
        /// <param name="i">the row</param>
        /// <param name="j">the column</param>
        /// <returns></returns>
        public string this[int i, int j]
        {
            get
            {
                return map[i, j];
            }
            set
            {
                map[i, j] = value;
            }
        }

        /// <summary>
        /// randomly fills the map with "_" to represent a blank space, or "b" to represent a block
        /// to be implemented: also include "e" for enemy, "p" for player,
        /// </summary>
        public Map()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); i++)
                {
                    int random = randy.Next(0, 1);
                    if (random == 0)
                    {
                        map[i, j] = "_";
                    }
                    else if (random == 1)
                    {
                        map[i, j] = "b";
                    }
                }
            }
        }
        /// <summary>
        /// prints the map to the console
        /// </summary>
        public void Print()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); i++)
                {
                    Console.WriteLine(map[i, j]);
                }
            }
        }
    }
}
