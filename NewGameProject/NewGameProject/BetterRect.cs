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
    class BetterRect
    {
        private Rectangle rect;
        //Vector2 acceleration;
        Vector2 velocity;
        private bool canCollide;
        //float friction = 2;

        public Rectangle Rect
        {
            get { return rect; }
        }

        public int X
        {
            get { return rect.X; }
        }

        public int Y
        {
            get { return rect.Y; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public float MoveX
        {
            get { return velocity.X; }
            set { velocity = new Vector2(value, velocity.Y); }
        }

        public float MoveY
        {
            get { return velocity.Y; }
            set { velocity = new Vector2(velocity.X, value); }
        }

        public bool CanCollide
        {
            get { return canCollide; }
            set { canCollide = value; }
        }

        public BetterRect(Rectangle rec)
        {
            rect = rec;
            canCollide = false;
        }

        public void AddForce(float force, Vector2 direction)
        {
            velocity = new Vector2(velocity.X + (direction.X * force), velocity.Y + (direction.Y * force));
        }

        //Uses the velocity to change position of the object, accounts for collisions.
        public void Move(float time, List<Rectangle> rectz)
        {

            if(velocity.X > 10)
            {
                velocity = new Vector2(10, velocity.Y);
            }
            if (velocity.X < -10)
            {
                velocity = new Vector2(-10, velocity.Y);
            }
            if (velocity.Y > 10)
            {
                velocity = new Vector2(velocity.X, 10);
            }
            if (velocity.X < -10)
            {
                velocity = new Vector2(velocity.X, -10);
            }


            //velocity = new Vector2((acceleration.X), (acceleration.Y));
            //velocity.X = (acceleration.X * time);
            //velocity.Y = (acceleration.Y * time);
            //if (acceleration.X > 0 && acceleration.X < friction || acceleration.X < 0 && acceleration.X > friction)
            //{
            //    acceleration.X = 0;
            //    acceleration = new Vector2(0, acceleration.Y);
            //}
            //else if(acceleration.X > 0)
            //{
            //    acceleration.X -= friction;
            //    acceleration = new Vector2(acceleration.X - friction, acceleration.Y);
            //}
            //else
            //{
            //    acceleration.X += friction;
            //    acceleration = new Vector2(acceleration.X + friction, acceleration.Y);
            //}

            //if (acceleration.Y > 0)
            //{
            //    acceleration.Y -= friction;
            //}
            //else
            //{
            //    acceleration.Y += friction;
            //}


            //Moves it if there's no collision
            if (!Collides(rectz, new Rectangle((int)(rect.X + velocity.X), (int)(rect.Y + velocity.Y), rect.Width, rect.Height)))
            {
                rect = new Rectangle((int)(rect.X + velocity.X), (int)(rect.Y + velocity.Y), rect.Width, rect.Height);
            }
            
            else
            {
                int nextX = Rect.X;
                int nextY = Rect.Y;
                bool finished = false;

                while (!finished)
                {
                    if(velocity.X > 0 && velocity.Y > 0)
                    {
                        if(!Collides(rectz, new Rectangle(nextX + 1, nextY + 1, rect.Width, rect.Height)))
                        {
                            nextX++;
                            nextY++;
                        }
                        else if (!Collides(rectz, new Rectangle(nextX + 1, nextY, rect.Width, rect.Height)))
                        {
                            nextX++;
                        }
                        else if (!Collides(rectz, new Rectangle(nextX, nextY +1, rect.Width, rect.Height)))
                        {
                            nextY++;
                        }
                        else
                        {
                            if (CanCollide) velocity = new Vector2(0, 0);
                            finished = true;
                        }
                    }
                    else if(velocity.X > 0 && velocity.Y < 0)
                    {
                        if (!Collides(rectz, new Rectangle(nextX + 1, nextY - 1, rect.Width, rect.Height)))
                        {
                            nextX++;
                            nextY--;
                        }
                        else if (!Collides(rectz, new Rectangle(nextX + 1, nextY, rect.Width, rect.Height)))
                        {
                            nextX++;
                        }
                        else if (!Collides(rectz, new Rectangle(nextX, nextY - 1, rect.Width, rect.Height)))
                        {
                            nextY--;
                        }
                        else
                        {
                            finished = true;
                        }
                    }
                    else if(velocity.X < 0 && velocity.Y > 0)
                    {
                        if (!Collides(rectz, new Rectangle(nextX - 1, nextY + 1, rect.Width, rect.Height)))
                        {
                            nextX--;
                            nextY++;
                        }
                        else if (!Collides(rectz, new Rectangle(nextX - 1, nextY, rect.Width, rect.Height)))
                        {
                            nextX--;
                        }
                        else if (!Collides(rectz, new Rectangle(nextX, nextY + 1, rect.Width, rect.Height)))
                        {
                            nextY++;
                        }
                        else
                        {
                            finished = true;
                        }
                    }
                    else
                    {
                        if (!Collides(rectz, new Rectangle(nextX - 1, nextY - 1, rect.Width, rect.Height)))
                        {
                            nextX--;
                            nextY--;
                        }
                        else if (!Collides(rectz, new Rectangle(nextX - 1, nextY, rect.Width, rect.Height)))
                        {
                            nextX--;
                        }
                        else if (!Collides(rectz, new Rectangle(nextX, nextY - 1, rect.Width, rect.Height)))
                        {
                            nextY--;
                        }
                        else
                        {
                            finished = true;
                        }
                    }
                }
                if (nextX != rect.X + velocity.X) velocity = new Vector2(0, velocity.Y);
                if (nextY != rect.Y + velocity.Y) velocity = new Vector2(velocity.X, 0);
                rect = new Rectangle(nextX, nextY, rect.Width, rect.Height);
                
                
                //int nextX = (int)(Rect.X + velocity.X);
                //int nextY = (int)(Rect.Y + velocity.Y);
                
                //while (!Collides(rectz, new Rectangle((nextX), (rect.Y), rect.Width, rect.Height)))
                //{
                //    if (velocity.X > 0)
                //    {
                //        nextX--;
                //    }
                //    else if (velocity.X != 0)
                //    {
                //        nextX++;
                //    }
                //}
                //while(!Collides(rectz, new Rectangle((rect.X), (nextY), rect.Width, rect.Height)))
                //{
                //    if (velocity.Y > 0)
                //    {
                //        nextY--;
                //    }
                //    else if (velocity.Y != 0)
                //    {
                //        nextY++;
                //    }
                //}
                //    rect = new Rectangle(nextX, nextY, rect.Width, rect.Height);

                //Sets speed limit
                
            }

        }

        //Checks if the given rectangle collides with any on the list and returns the appropriate bool.
        public bool Collides(List<Rectangle> rectz, Rectangle temp)
        {
            for (int i = 0; i < rectz.Count; i++)
            {
                if(temp.Intersects(rectz[i]) && temp != rectz[i]) return true;
            }
            return false;
        }
    }
}
