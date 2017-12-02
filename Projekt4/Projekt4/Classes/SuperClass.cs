using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Projekt4_byYakupY
{
    abstract class SuperClass
    {
        #region Variables
        // Dessa variabler kommer att användas av
        // alla klasser
        protected Texture2D texture;
        protected Vector2 position;
        public float PositionY{
            get { return position.Y; }
            set { position.Y = value; }
        }
        public float PositionX
        {
            get { return position.X; }
            set { position.X = value; }
        }
        public static int score = 0;


        protected Point size;
        protected Rectangle boundingBox;
        
        public static int nrofRows = 19;
        public static int nrofCols = 15;
        public static int tSize = 50;
        

        #endregion

        public SuperClass(Texture2D texture, Vector2 position,
            Point size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            boundingBox = new Rectangle((int)position.X,
                (int)position.Y, size.X, size.Y);
        }
        #region SpelLoopen

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        #endregion

        #region PixelCollision



        // Används för pixlperfekt kollision.
        public virtual Rectangle Source{
            get 
            { 
                return new Rectangle(0, 0, size.X, size.Y); 
            }
        }

        public virtual Rectangle Dest{
            get
            {
                return new Rectangle((int)position.X,
                    (int)position.Y, size.X, size.Y);
            }
        }

        public virtual Rectangle HitBox { get { return Dest; } }

        Color[] colorData;
        public void SetColorData()
        {
            colorData = new Color[size.X * size.Y];
            texture.GetData(colorData);
        }

        public Color GetPixel(int col, int row)
        {
            int c = col - Dest.X + Source.X;
            int r = row - Dest.Y + Source.Y;
            return colorData[r * texture.Width + c];
        }

        #endregion
    }
}
