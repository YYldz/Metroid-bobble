using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Projekt4_byYakupY
{
    class Bubble : Animated
    {
        private double bubDirtimer = 1500;

        private double bubDeathTime = 7500;
        public double BubDeathTime
        {
            get { return bubDeathTime; }
            set { bubDeathTime = value; }
        }

        private bool bubFinalDest;
        public bool BubFinalDest
        {
            get { return bubFinalDest; }
            set { bubFinalDest = value; }
        }

        private int directionUp;
        public int DirectionUp
        {
            get { return directionUp; }
            set { directionUp = value; }
        }

        private double bubDeathAnim = 180;
    
        public Bubble(Texture2D texture, Vector2 position,
        Point size, int dir)
            : base(texture, position, size)
        {
            spriteRec = new Rectangle(0, 0, 26, 26);
            spWidth = 26;
            picFrames = 4;
            isActive = true;
            this.direction = dir;

        }



        #region SpelLoopen

        public override void Update(GameTime gameTime)
        {
            previousSpot = position;
            if (isActive)
            {
                SpriteAnimator(gameTime);
                if (spriteRec.X == 26)
                {
                    picFrames = 2;
                }

                position.X += direction;

                bubDirtimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                if (bubDirtimer <= 0)
                {
                    position.Y += directionUp;
                    direction = 0;
                    directionUp = -3;
                    bubDeathTime -= gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (bubFinalDest && bubDeathTime <= 0)
                    {
                        bubDeathAnim -= gameTime.ElapsedGameTime.TotalMilliseconds;
                        spriteRec.Y = 26;
                        picFrames = 4;

                        if (bubDeathAnim <= 0)
                        {
                            BubbleHandler.RemoveBubbles(this);
                            isActive = false;
                            Enemy.hit = false;
                        }
                    }
                }
            }

            CollisionHandler.BubbleCollision(this);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, BodyBox(),
                            spriteRec, Color.White, 0,
                            Vector2.Zero, 0, 1);
        }

        #endregion
    }
}
