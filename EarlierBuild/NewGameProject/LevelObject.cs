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
    class LevelObject
    {
        protected Texture2D texture;
        protected Rectangle rectangle;

        protected bool leftCollision;
        protected bool rightCollision;
        protected bool bottomCollision;
        protected bool topCollision;

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

        public bool BottomCollision
        {
            get { return bottomCollision; }
            set { bottomCollision = value; }
        }

        public bool TopCollision
        {
            get { return topCollision; }
            set { topCollision = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Rectangle Rectangle
        {
            get { return rectangle; }
        }

        public LevelObject(Texture2D texture, Rectangle rectangle)
        {
            this.texture = texture;
            this.rectangle = rectangle;
        }
    }
}
