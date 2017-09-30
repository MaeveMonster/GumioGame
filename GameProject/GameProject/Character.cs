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
using FarseerPhysics;

namespace GameProject
{
    class Character
    {
        protected Texture2D texture;
        protected Fixture fixture;
        protected Body body;
               
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Fixture Fixture
        {
            get { return fixture; }
        }

        public Body Body
        {
            get { return body; }
        }

        public float X
        {
            get { return body.Position.X; }
            set { body.Position = new Vector2(value, body.Position.Y); }
        }

        public float Y
        {
            get { return body.Position.Y; }
            set { body.Position = new Vector2(body.Position.X, value); }
        }

        public Character(World world, Texture2D texture)
        {
            this.texture = texture;
            //body = BodyFactory.CreateRectangle(world, texture.Width, texture.Height, 2f, new Vector2(20f, 20f));
            //body.BodyType = BodyType.Dynamic;
            //Vertices vertices = PolygonTools.CreateRectangle(texture.Width / 2, texture.Height / 2);
            //PolygonShape rectangle = new PolygonShape(vertices, 2f);
            //fixture = body.CreateFixture(rectangle);

            body = BodyFactory.CreateCircle(world, 2f, 2f, ConvertUnits.ToSimUnits(new Vector2(20f, 200.0f)));
            body.BodyType = BodyType.Dynamic;
            body.SleepingAllowed = false;
            CircleShape circle = new CircleShape(2f, 2f);
            circle.Density = 0.1f;
            fixture = body.CreateFixture(circle);
        }
    }
}
