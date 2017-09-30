using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGameProject
{
    class Hand
    {
        private BetterRect rectangle;
        private Player owner;

        public BetterRect Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public Hand(BetterRect rect, Player chicle, Vector2 velocity)
        {
            rectangle = rect;
            rectangle.Velocity = velocity * 2;
            rectangle.CanCollide = true;
            owner = chicle;
        }

        public void Update()
        {
            if(rectangle.Velocity == new Vector2(0, 0))
            {
                float divisor = (float)Math.Sqrt(((rectangle.X - owner.Rectangle.X) * (rectangle.X - owner.Rectangle.X)) + ((rectangle.Y - owner.Rectangle.Y) * (rectangle.Y - owner.Rectangle.Y)));
                owner.Rectangle.Velocity += new Vector2((rectangle.X - owner.Rectangle.X)/divisor, (rectangle.Y - owner.Rectangle.Y)/divisor);
            }
        }
    }
}
