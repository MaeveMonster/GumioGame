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
    class Character
    {
        protected Texture2D texture;
        protected Rectangle rectangle;
        protected Vector2 velocity;
        protected Vector2 position;
        bool jumped;

        public Texture2D Texture
        {
            get { return texture; }
        }

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public Character(Texture2D  texture, Rectangle rectangle)
        {
            this.texture = texture;
            this.rectangle = rectangle;
            jumped = true;
        }

       
    }
}
