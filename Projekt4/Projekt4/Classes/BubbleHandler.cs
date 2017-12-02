using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projekt4_byYakupY
{
    class BubbleHandler
    {

        public static void AddBubbles(Vector2 bubblePos, Texture2D bTex, int direction)
        {

            Manager.gameObjList.Add(new Bubble(bTex, bubblePos,
                                    new Point(SuperClass.tSize, SuperClass.tSize), direction));

        }

        public static void RemoveBubbles(Bubble bub)
        {
            bub.IsActive = false;
            Manager.gameObjList.Remove(bub);
        }
    }
}
