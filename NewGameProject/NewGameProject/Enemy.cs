using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NewGameProject
{
    class Enemy : Character
    {
        protected int health;

        public int Health
        {
            get { return health; }
        }

        public bool IsAlive
        {
            get { return health > 0; }
        }

        public Enemy(Texture2D texture, BetterRect rectangle) : base(texture, rectangle)
        {
            
        }
    }
}
