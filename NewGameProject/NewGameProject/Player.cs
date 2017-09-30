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
        //protected Vector2 velocity;
        protected bool jumped;
        protected Hand hand;
        protected KeyboardState previous;


        public int Score
        {
            get { return score; }
        }

        public int Lives
        {
            get { return lives; }
        }

        public Hand Hand
        {
            get { return hand; }
            set { hand = value; }
        }

        //public Vector2 Velocity
        //{
        //    get { return velocity; }
        //    set { velocity = value; }
        //}

        public Player(Texture2D texture, BetterRect rectangle) : base(texture, rectangle)
        {
            jumped = true;
            previous = Keyboard.GetState();
        }

        public void Move(GameTime gt, KeyboardState ks, List<Rectangle> rectz)
        {
            //position += velocity;
            if (ks.IsKeyDown(Keys.D))
            {
                rectangle.AddForce(0.1f, new Vector2(1, 0));
                //velocity.X = 3f;
                //cam.position.X += velocity.X;
            }
            else if (ks.IsKeyDown(Keys.A))
            {
                rectangle.AddForce(0.1f, new Vector2(-1, 0));
                //velocity.X = -3f;
                //cam.position.X += velocity.X;
            }
            //else
            //{
            //    //velocity.X = 0f;
            //}
            if (ks.IsKeyDown(Keys.Space) && !jumped)
            {
                rectangle.AddForce(0.1f, new Vector2(0, -1));
                //position.Y -= 10f;
                //velocity.Y = -5f;
                jumped = true;
            }

            //if (position.Y + texture.Height * .1f >= 400)
            //{
            //    jumped = false;
            //}

            if (!jumped)
            {
                //velocity.Y = 0f;
                rectangle.MoveY = 0;
            }
            rectangle.Move((float)(gt.ElapsedGameTime.TotalSeconds), rectz);
            //position = new Vector2(rectangle.Rect.X, rectangle.Rect.Y);
        }

        public void Stick()
        {
            float divisor = (float)Math.Sqrt(((Mouse.GetState().X - (float)Rectangle.Rect.X) * (Mouse.GetState().X - (float)Rectangle.Rect.X)) + ((Mouse.GetState().Y - (float)Rectangle.Rect.Y) * (Mouse.GetState().Y - (float)Rectangle.Rect.Y)));
            hand = new Hand(new BetterRect(new Rectangle(rectangle.X, rectangle.Y, 2, 2)), this, new Vector2((Mouse.GetState().X - (float)Rectangle.Rect.X) / divisor, (Mouse.GetState().Y - (float)Rectangle.Rect.Y) / divisor));
            
        }
        //hand.Rectangle.Velocity = new Vector2((Mouse.GetState().X - (float)Rectangle.Rect.X)/divisor, (Mouse.GetState().Y - (float)Rectangle.Rect.Y)/divisor);

        public void ApplyGravity(GameTime gt, List<Rectangle> rectz)
        {
            if (jumped)
            {
                //float i = 1;
                //velocity.Y += .15f * i;
                rectangle.AddForce(0.01f, new Vector2(0, 1));
                //if (rectangle.Rect.Contains(rectangle.X, rectangle.Y + rectangle.Rect.Height + 1) || rectangle.Rect.Contains(rectangle.X + (int)(rectangle.Rect.Width / 2), rectangle.Y + rectangle.Rect.Height + 1) || rectangle.Rect.Contains(rectangle.X + rectangle.Rect.Width, rectangle.Y + rectangle.Rect.Height + 1))
                //{
                //    jumped = false;
                //}
                if (rectangle.Collides(rectz, rectangle.Rect) || rectangle.Collides(rectz, new Rectangle(rectangle.Rect.X, rectangle.Rect.Y + 5, rectangle.Rect.Width, rectangle.Rect.Height)))
                {
                    jumped = false;
                }
            }
            rectangle.Move((float)(gt.ElapsedGameTime.TotalSeconds), rectz);
        }

        public void Update(GameTime gt, KeyboardState ks, List<Rectangle> rectz)
        {
            Move(gt, ks, rectz);
            if(!rectangle.Collides(rectz, new Rectangle(rectangle.Rect.X, rectangle.Rect.Y + 5, rectangle.Rect.Width, rectangle.Rect.Height)))
            {
                jumped = true;
            }
            ApplyGravity(gt, rectz);
            if(ks.IsKeyDown(Keys.Q) && previous.IsKeyUp(Keys.Q))
            {
                Hand = null;
                Stick();
            }
            if (Hand != null) Hand.Update();
        }

    }
}
