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
    class Camera
    {
        private Viewport viewport;
        private Vector2 origin;
        public Vector2 position;

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            origin = new Vector2(viewport.Width / 2, viewport.Height / 2);
            position = new Vector2(0, 0);
        }

        public Matrix CameraMatrix()
        {
            return
                Matrix.CreateTranslation(new Vector3(-position, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(origin, 0.0f));
        }

        public void Scroll(Player player, GraphicsDevice gd)
        {
            
                position.X = player.Rectangle.Rect.X;
            
        }
    }
}
