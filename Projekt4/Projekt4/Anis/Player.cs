using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Projekt4_byYakupY
{
    class Player : Animated
    {
        KeyboardState kState;
        SpriteEffects spriteFx;
        public bool itemPickedUp;

        private double respawnTimer = 1500;
        private Random rand = new Random();
        private int rndNr;
        public static int energy = 3;

        private double shootCD;
        private int cd;

        public Player(Texture2D texture, Vector2 position,
            Point size)
            : base(texture, position, size)
        {
            spriteRec = new Rectangle(0, 0, 40, 34);
            picFrames = 4;
            cd = 400;
            shootCD = cd;
            direction = 3;
            isActive = true;
        }



        public void ItemPickup()
        {
            // Detta gör "?"
            SoundEffectInstance dead = Manager.death.CreateInstance();
            do
            {
                rndNr = rand.Next(1, 6);
            } while (rndNr == 0);

            if (rndNr == 1)
            {
                score -= 1000;
            }
            if (rndNr == 2)
            {
                score += 1000;
            }
            if (rndNr == 3)
            {
                int rndLoc = rand.Next(100, 650);
                position.X = rndLoc;
                position.Y = rndLoc;
            }
            if (rndNr == 4)
            {
                isActive = false;
                dead.Play();
                energy--;
            }
            if (rndNr == 5)
            {
                energy++;
            }
        }

        #region SpelLoopen
        public override void Update(GameTime gameTime)
        {
            SoundEffectInstance jmp = Manager.jump.CreateInstance();
            SoundEffectInstance beam = Manager.iceBeam.CreateInstance();
            
            previousSpot = position;
            kState = Keyboard.GetState();

            spWidth = 40;

                if (gravity < 6 && !isOnGround)
                {
                    gravity += 0.2f;
                }

                // Movement System for player(1)
                if (kState.IsKeyDown(Keys.Left) && isActive)
                {
                    spriteFx = SpriteEffects.FlipHorizontally;
                    position.X -= 2;
                    direction = -3;
                    SpriteAnimator(gameTime);
                }
                else if (kState.IsKeyDown(Keys.Right) && isActive)
                {
                    spriteFx = SpriteEffects.None;
                    position.X += 2;
                    direction = 3;
                    SpriteAnimator(gameTime);
                }

                if (kState.IsKeyDown(Keys.Up) && isOnGround && isActive)
                {
                    // Om Space trycks ned så ändra spritesektion till hoppa
                    // Få player att hoppa
                    jmp.Play();
                    gravity = -7;
                    isOnGround = false;
                }

                // Om en fiende träffar player, starta respawnTimer och
                // spawna player igen på x: 300, y: 50
                if (!isActive)
                {
                    respawnTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (respawnTimer <= 0)
                    {
                        this.position.X = 300;
                        this.position.Y = 50;
                        isActive = true;
                        respawnTimer = 1500;
                    }
                }
                // En timer för att begränsa antalet bubblor.
                shootCD -= gameTime.ElapsedGameTime.TotalMilliseconds;

                if (kState.IsKeyDown(Keys.Space) && isActive)
                {
                    if (shootCD <= 0)
                    {
                        BubbleHandler.AddBubbles(this.position, Manager.bTex, direction);
                        beam.Play();
                        isActive = true;
                        shootCD = cd;
                    }
                }

                if (itemPickedUp)
                {
                    ItemPickup();
                    itemPickedUp = false;
                }

                if (energy == 0)
                {
                    Manager.hasLost = true;
                }

                position.Y += gravity;
                isOnGround = false;

                CollisionHandler.PlayerCollision(this); 
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isActive && energy != 0)
            {
                spriteBatch.Draw(texture, BodyBox(),
                        spriteRec, Color.White, 0,
                        Vector2.Zero,
                        spriteFx, 1);
            }
        }

        #endregion
    }
}
