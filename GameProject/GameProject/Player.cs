using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Common;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Controllers;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;


namespace GameProject
{
    class Player : Character
    {
        protected int lives;

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public Player(World world, Texture2D texture) : base(world, texture)
        {
            lives = 3;
        }

        public void Move(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.Space))
            {
                if (body.LinearVelocity.Y == 0)
                   // this.Fixture.Body.ApplyForce(new Vector2(0, -100000f));
                    Fixture.Body.ApplyLinearImpulse(new Vector2(0, -600));

            }
            if (state.IsKeyDown(Keys.A))
            {
                this.X -= 1.5f;
                
            }
            if (state.IsKeyDown(Keys.D))
            {
                this.X += 1.5f;
            }
        }

        public void Stick(MouseState state)
        {
            if (state.LeftButton == ButtonState.Pressed)
            {
                
            }
            if (state.RightButton == ButtonState.Pressed)
            {

            }
        }
    }
}
