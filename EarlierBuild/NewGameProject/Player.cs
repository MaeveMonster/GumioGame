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
    class Player : Character
    {
        protected int score;
        protected int lives;
        protected Vector2 velocity;
        protected bool jumped;
        protected bool rightCollision;
        protected bool leftCollision;
        protected bool topCollision;
        protected bool bottomCollision;
        protected bool sticking;
        protected int stickPositionX;
        protected int stickPositionY;

        public int Score
        {
            get { return score; }
        }

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public bool Jumped
        {
            get { return jumped; }
            set { jumped = value; }
        }

        public bool Sticking
        {
            get { return sticking; }
        }

        public bool LeftCollision
        {
            get { return leftCollision; }
            set { leftCollision = value; }
        }
        
        public bool RightCollision
        {
            get { return rightCollision; }
            set { rightCollision = value; }
        }

        public bool TopCollision
        {
            get { return topCollision; }
            set { topCollision = value; }
        }

        public Player(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {
            jumped = true;
            lives = 3;
            rightCollision = false;
            leftCollision = false;
        }

        public void Move(GameTime gt, KeyboardState ks)
        {
            //position += velocity;
            rectangle.X += (int)velocity.X;
            rectangle.Y += (int)velocity.Y;
            if (!rightCollision && ks.IsKeyDown(Keys.D))
            {
                velocity.X = 3f;
            }
            else if (!leftCollision && ks.IsKeyDown(Keys.A))
            {
                velocity.X = -3f;
            }
            else 
            {
                velocity.X = 0f;
            }

            if (topCollision && ks.IsKeyDown(Keys.S))
            {
                velocity.Y = 3f;
            }
            else if (topCollision && !ks.IsKeyDown(Keys.S))
            {
                velocity.Y = 0f;
            }

            if (ks.IsKeyDown(Keys.Space) && !jumped)
            {
                // position.Y -= 10f;
                rectangle.Y -= 10;
                velocity.Y = -5f;
                jumped = true;
            }
            if (jumped)
            {
                float i = 1;
                velocity.Y += .15f * i;
            }

            //if (position.Y + texture.Height * .1f >= levelRect.Bottom)
            //{
            //    jumped = false;
            //}

            if (!jumped)
            {
                velocity.Y = 0f;
            }



            if (rectangle.Y >= 500)
            {
                lives -= 1;
                rectangle = new Rectangle(0, 350, rectangle.Width, rectangle.Height);
            }


        }

        public void Stick(MouseState ms, List<LevelObject> levelObjects, Camera cam)
        {
            if (ms.LeftButton == ButtonState.Pressed)
            {
                LevelObject stickObject = null;
                foreach (LevelObject lo in levelObjects)
                {
                    if (ms.Position.X + cam.Position.X >= lo.Rectangle.Left && ms.Position.X + cam.Position.X <= lo.Rectangle.Right && ms.Position.Y >= lo.Rectangle.Top && ms.Position.Y <= lo.Rectangle.Bottom)
                    {
                        sticking = true;
                        stickObject = lo;
                        stickPositionX = ms.Position.X + (int)cam.Position.X;
                        stickPositionY = ms.Position.Y;
                        int xDistance = Math.Abs(rectangle.X - ms.Position.X);
                        int yDistance = rectangle.Y - ms.Position.Y;
                        if (!lo.Rectangle.Intersects(this.rectangle))
                        {
                            if (stickPositionX > rectangle.X)
                            {
                                velocity.X = 3f;
                            }
                            else if (stickPositionX < rectangle.X)
                            {
                                velocity.X = -3f;
                            }
                            else
                            {
                                velocity.X = 0f;
                            }
                            rectangle.X += (int)velocity.X;
                        }
                        if (!lo.Rectangle.Intersects(this.rectangle))
                        {
                            if (stickPositionY > rectangle.Bottom)
                            {
                                velocity.Y = 3f;
                                rectangle.Y += (int)velocity.Y;
                            }
                            else if (stickPositionY < rectangle.Bottom)
                            {
                                velocity.Y = -3f;
                                rectangle.Y += (int)velocity.Y;
                            }
                        }
                        else
                        {
                            jumped = false;
                        }
                    }
                }
            }
            if (ms.Position.X != stickPositionX && ms.Position.Y != stickPositionY) sticking = false;
        }

        public void DrawChicleSticking(SpriteBatch sb, Texture2D arm)
        {
            if (sticking)
            {
                Vector2 source = new Vector2(rectangle.Center.X, rectangle.Center.Y);
                Vector2 destination = new Vector2(stickPositionX, stickPositionY);
                Vector2 direction = destination - source;
                float angle = (float)Math.Atan2(direction.Y, direction.X);
                float distance = Vector2.Distance(source, destination);
                sb.Draw(arm, source, new Rectangle((int)source.X, (int)source.Y, (int)distance, 10), Color.Pink, angle, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            }
        }

    }
}
