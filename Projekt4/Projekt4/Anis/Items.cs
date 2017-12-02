using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Projekt4_byYakupY
{
    class Items : Animated
    {
        private double qmSpawnTimer = 1000;
        private bool spawnOK;

        public Items(Texture2D texture, Vector2 position,
            Point size)
            : base(texture, position, size)
        {
            isActive = true;
            spriteRec = new Rectangle(0, 0, 259, 305);
            spWidth = 259;
            picFrames = 2;
            frameTimer = 125;
            frameinterval = 125;
        }

        public override void Update(GameTime gameTime)
        {
            qmSpawnTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (qmSpawnTimer <= 0)
            {
                spawnOK = true;
            }

            SpriteAnimator(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isActive && spawnOK)
            {
                spriteBatch.Draw(texture, BodyBox(),
                                spriteRec, Color.White, 0,
                                Vector2.Zero, 0, 1);
            }
        }
    }
}
