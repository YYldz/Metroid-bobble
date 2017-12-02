using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Projekt4_byYakupY
{
    class Animated : SuperClass
    {
        #region Variables
        protected double frameTimer = 75;
        protected int frameinterval = 75;
        protected int frame;
        protected int spWidth;
        protected Vector2 previousSpot;
        public Vector2 PreviousSpot
        {
            get { return previousSpot; }
            private set { }
        }
        protected Rectangle spriteRec;
        protected int picFrames;
        protected float gravity;
        public float Gravity
        {
            get { return gravity; }
            set { gravity = value; }
        }
        protected bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public bool shoot;
        public bool Shoot
        {
            get { return shoot; }
            set { shoot = value; }
        }
        public static bool hit;
        public bool Hit
        {
            get { return hit; }
            set { hit = value; }
        }
        protected bool isOnGround;
        public bool IsOnGround
        {
            get { return isOnGround; }
            set { isOnGround = value; }
        }
        protected int direction;
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        #endregion

        public Animated(Texture2D texture, Vector2 position,
            Point size)
            : base(texture, position, size)
        {

        }

        #region SpelLoopen

        public void SpriteAnimator(GameTime gameTime)
        {
            // SpriteAnimator
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameinterval;
                frame++;
                spriteRec.X = (frame % picFrames) * spWidth;
            }
        }



        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }
        #endregion

        #region animatedBounds
        public Rectangle FeetBox()
        {
            return new Rectangle((int)position.X, (int)position.Y + (SuperClass.tSize - 2), size.X - 8, 2);
        }
        public Rectangle HeadBox()
        {
            return new Rectangle((int)position.X, (int)position.Y, size.X, 2);
        }
        public Rectangle BodyBox()
        {
            return new Rectangle((int)position.X, (int)position.Y, size.X, size.Y);
        }
        #endregion
    }
}
