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
    class LevelObject
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

        public LevelObject(World world, Texture2D texture)
        {
            this.texture = texture;
            //changing texture.Width and texture.Height to numbers makes it run without falling, but the player cannot jump
            body = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(texture.Width * .1f) , ConvertUnits.ToSimUnits(texture.Height * .1f) , 20f, ConvertUnits.ToSimUnits(new Vector2(250f, 300f)));
            body.BodyType = BodyType.Static;
            Vertices vertices = PolygonTools.CreateRectangle(ConvertUnits.ToSimUnits(texture.Width) , ConvertUnits.ToSimUnits(texture.Height));
            PolygonShape rectangle = new PolygonShape(vertices, 20f);
            //Rectangle rect = new Rectangle(5, 5, 5, 5);
            fixture = FixtureFactory.AttachRectangle(ConvertUnits.ToSimUnits(texture.Width), ConvertUnits.ToSimUnits(texture.Height), 20f, ConvertUnits.ToSimUnits(new Vector2(250f, 300f)), body);
            //fixture = body.CreateFixture(new EdgeShape(new Vector2(0f,0f), new Vector2(1f,1f)));

        }
    }
}
