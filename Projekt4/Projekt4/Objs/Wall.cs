using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Projekt4_byYakupY
{
    class Wall : GameObjects
    {
        public Wall(Texture2D texture, Vector2 position,
                    Point size)
            : base(texture, position, size)
        {

        }
    }
}
