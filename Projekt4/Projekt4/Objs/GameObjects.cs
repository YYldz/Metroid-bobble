using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Projekt4_byYakupY
{
    class GameObjects : SuperClass
    {
        public GameObjects(Texture2D texture, Vector2 position,
            Point size)
            : base(texture, position, size)
        {

        }

        #region SpelLoopen

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, boundingBox, Color.White);
        }

        #endregion

        public Rectangle Bounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, size.X, size.Y);
        }

        public Rectangle PlatformBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, size.X, 10);
        }
    }


}
