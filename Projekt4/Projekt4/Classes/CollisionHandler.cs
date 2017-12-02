using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Projekt4_byYakupY
{
    class CollisionHandler
    {

        public static void PlayerCollision(Player one)
        {
            // Kollision med objekt
            foreach (GameObjects g in Manager.gameObjs
                .OfType<GameObjects>())
            {
                if (g is Brick && g.PlatformBounds().Intersects(one.FeetBox())
                    && one.Gravity >= 0)
                {
                    one.IsOnGround = true;
                    one.Gravity = 0;
                    one.PositionY = one.PreviousSpot.Y;
                }
                if (g is Wall && g.Bounds().Intersects(one.BodyBox()))
                {
                    one.PositionX = one.PreviousSpot.X;
                }
                if (g is Wall && g.Bounds().Intersects(one.BodyBox()))
                {
                    one.PositionY = one.PreviousSpot.Y;
                    one.Gravity = 0;
                }
            }

            SoundEffectInstance dead = Manager.death.CreateInstance();
            // Kollision med npc
            foreach (Animated a in Manager.gameObjs.
                OfType<Animated>())
            {
                if (a is Enemy && a.BodyBox().Intersects(one.BodyBox()) &&
                    one.IsActive && !a.Hit)
                {
                    one.IsActive = false;
                    dead.Play();
                    Player.energy--;
                }
                else if (a is Enemy && a.BodyBox().Intersects(one.BodyBox()) 
                    && a.IsActive && a.Hit)
                {
                    a.IsActive = false;
                    SuperClass.score += 500;
                }
                if (a is Items && a.BodyBox().Intersects(one.BodyBox()) &&
                    a.IsActive)
                {
                    a.IsActive = false;
                    one.itemPickedUp = true;
                }
            }
        }


        public static void EnemyCollision(Enemy enemy)
        {
            foreach (GameObjects g in Manager.gameObjs
                .OfType<GameObjects>())
            {
                // Om krock med plattform låt enemy stanna på den!
                if (g is Brick && g.PlatformBounds().Intersects(enemy.FeetBox())
                    && enemy.Gravity >= 0)
                {
                    enemy.IsOnGround = true;
                    enemy.Gravity = 0;
                    enemy.PositionY = enemy.PreviousSpot.Y;
                }

                // Kolla vilket håll enemy går och skicka enemy åt andra
                // hållet om krock med vägg
                if (g is Wall && g.Bounds().Intersects(enemy.BodyBox())
                    && enemy.Direction == -2)
                {
                    enemy.PositionX = enemy.PreviousSpot.X;
                    enemy.Gravity = 0.2f;
                    enemy.Direction = 2;
                }
                else if (g is Wall && g.Bounds().Intersects(enemy.BodyBox())
                    && enemy.Direction == 2)
                {
                    enemy.PositionX = enemy.PreviousSpot.X;
                    enemy.Gravity = 0.2f;
                    enemy.Direction = -2;
                }
            }

        }

        public static void BubbleCollision(Bubble bub)
        {
            foreach (GameObjects g in Manager.gameObjs
                .OfType<GameObjects>())
            {
                if (g is Wall && g.Bounds().Intersects(bub.BodyBox()))
                {
                    bub.Direction = 0;
                    bub.PositionY = bub.PreviousSpot.Y;
                    bub.DirectionUp = 0;
                    bub.BubFinalDest = true;
                }
                if (g is Wall && g.Bounds().Intersects(bub.BodyBox()))
                {

                }
            }
            foreach (Animated a in Manager.gameObjs
                    .OfType<Animated>())
            {
                if (a is Enemy && a.BodyBox().Intersects(bub.BodyBox()) && bub.IsActive)
                {
                    a.Hit = true;
                    a.PositionX = bub.PositionX;
                    a.PositionY = bub.PositionY;
                }

                if (a is Player && a.BodyBox().Intersects(bub.BodyBox()) && bub.BubFinalDest)
                {
                    BubbleHandler.RemoveBubbles(bub);
                    SuperClass.score += 100;
                }
            }
        }

        public static bool PixelCollision(SuperClass o1, SuperClass o2)
        {
            if (!o1.HitBox.Intersects(o2.HitBox))
                return false;

            float CollisionLeft = MathHelper.Max(o1.Dest.X, o2.Dest.X);
            float CollisionRight = MathHelper.Max(o2.Dest.Y, o2.Dest.Y);
            float CollisionTop = MathHelper.Min(o1.Dest.Right, o2.Dest.Right);
            float CollisionBottom = MathHelper.Min(o1.Dest.Bottom, o2.Dest.Bottom);

            for (int column = (int)CollisionLeft; column < CollisionRight; column++)
                for (int row = (int)CollisionTop; row < CollisionBottom; row++)
                {
                    if (o1.GetPixel(column, row).A > 127
                        && o2.GetPixel(column, row).A > 127)
                        return true;
                }
            return false;

        }
    }
}
