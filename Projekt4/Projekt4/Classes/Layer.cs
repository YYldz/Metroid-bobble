using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Projekt4_byYakupY
{
    class Layer
    {
        protected Vector2 position;
        private float speed;
        protected Texture2D texture;

        public Layer(Vector2 position, float speed, Texture2D texture)
        {
            this.position = position;
            this.speed = speed;
            this.texture = texture;
        }

        public void Update()
        {
            position.X -= speed;
            if (position.X < -texture.Width)
                position.X += texture.Width;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            if (position.X + texture.Width < 950)
                spriteBatch.Draw(texture, position +
                    new Vector2(texture.Width, 0), Color.White);
        }
    }
}
