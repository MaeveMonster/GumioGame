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
    class EnemyTeeth : Enemy
    {
        int widthOfSingleSprite;
        EnemyTeethState currentState;
        SpriteEffects flip;

        public EnemyTeeth(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {
            widthOfSingleSprite = texture.Width / 2;
            flip = SpriteEffects.None;
        }

        public void Move(Player player)
        {
           // rectangle.X += (int)velocity.X;

            if (player.Rectangle.Left > rectangle.Right)
            {
                //velocity.X = 3f; 
                currentState = EnemyTeethState.FaceRight;
            }
            else if (player.Rectangle.Right < rectangle.Left)
            {
                //velocity.X = -3f;
                currentState = EnemyTeethState.FaceLeft;
            }
            else
            {
                //velocity.X = 0f;
            }

            if (currentState == EnemyTeethState.FaceLeft) flip = SpriteEffects.FlipHorizontally;
            else flip = SpriteEffects.None;
        }

        public void DrawEnemyTeethChomping(SpriteBatch sb, int currentFrame)
        {
            int frameToDraw = currentFrame;
            if (currentFrame > 2) frameToDraw = 1;
            sb.Draw(texture, destinationRectangle: rectangle, sourceRectangle: new Rectangle((frameToDraw - 1) * widthOfSingleSprite, 0, widthOfSingleSprite, texture.Height), color: Color.White, rotation: 0.0f, origin: Vector2.Zero, scale: new Vector2(1f), effects: flip, layerDepth: 0.0f);
        }
    }
}
