using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Projekt4_byYakupY
{
    class Enemy : Animated
    {
        private int randDir;

        private double miniJumpTimer = 225;
        private double JumpTimer = 1800;

        Random rand = new Random();

        public Enemy(Texture2D texture, Vector2 position,
            Point size)
            : base(texture, position, size)
        {
            spriteRec = new Rectangle(0, 0, 26, 26);
            spWidth = 26;
            picFrames = 3;
            isActive = true;
            hit = false;
        }

        protected void EnemyMovement()
        {
            /* Här tänker jag att man rullar på en
             * tärning med värdena 1-100. Om den hamnar
             * under 50 så sätts värdet på EnemyDir till -2
             * och annars till 2.
            */
            randDir = rand.Next(1, 2);
            if (randDir == 1)
            {
                direction = -2;
            }
            else if (randDir == 2)
            {
                direction = 2;
            }
        }

        #region SpelLoopen
        public override void Update(GameTime gameTime)
        {
            previousSpot = position;
            SpriteAnimator(gameTime);

            // Räkna ner på jumptimers!
            miniJumpTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            JumpTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (gravity < 6 && !isOnGround)
            {
                gravity += 0.2f;
            }

            if (JumpTimer <= 0 && isOnGround)
            {
                gravity = -7;
                isOnGround = false;
                JumpTimer = 2250;
                miniJumpTimer = 225;
            }
            else if (miniJumpTimer <= 0 && isOnGround)
            {
                gravity = -3;
                isOnGround = false;
                miniJumpTimer = 225;
            }

            while (direction == 0)
            {
                EnemyMovement();
            }

            position.X += direction;
            position.Y += gravity;
            isOnGround = false;

            CollisionHandler.EnemyCollision(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (isActive)
            {
                spriteBatch.Draw(texture, BodyBox(),
            spriteRec, Color.White, 0,
            Vector2.Zero, 0, 1); 
            }
        }

        #endregion
    }
}
