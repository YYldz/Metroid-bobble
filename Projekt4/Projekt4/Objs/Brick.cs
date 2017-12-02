using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Projekt4_byYakupY
{
    class Brick: GameObjects
    {
        public Brick(Texture2D texture, Vector2 position,
            Point size)
            : base(texture, position, size)
        {

        }
        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds(), Color.White);
        }
    }
}
