using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace Projekt4_byYakupY
{
    class Manager
    {
        public static SuperClass[,] gameObjs;
        public static List<Bubble> gameObjList = new List<Bubble>();
        public static Texture2D bTex;
        Layer L1, L2, L3;
        Texture2D L1Tex, L2Tex, L3Tex;

        SpriteFont myFont;
        // Sounds
        public static SoundEffect iceBeam, jump, death;

        public static bool start;
        public static bool hasLost;

        private int rows;
        //private int indexCount = 0;
        //private int nrofLevels = 3;

        //List<string> levelFiles = new List<string>();
        //static readonly string[] Levels = new string[] { 
        //    "Level1.txt", 
        //    "Level2.txt", 
        //    "Level3.txt"};

        // Texturer för bana
        Texture2D brickTex1, brickTex2, brickTex3, brickTex6;
        // Texturer för animerade npc/pc
        Texture2D player1Tex, player2Tex, enemyTex1, enemyTex2;
        // Texturer för bonus
        Texture2D itemTex1;

        Vector2 layPos;

        // Olika bakgrundslåtar för spelet
        Song brinstar;
        Song kraid;
        bool songstart = false;

        public Manager()
        {
            gameObjs = new SuperClass[SuperClass.nrofRows,
                SuperClass.nrofCols];
            rows = 0;
            // Lägg till element i arrayen som står för
            // filnamnet på de olika banorna.
        }

        public void LoadContent(ContentManager content)
        {
            // Maptextures
            brickTex1 = content.Load<Texture2D>(@"Objects/tex1");
            //brickTex2 = content.Load<Texture2D>(@"Objects/tex2.png");
            //brickTex3 = content.Load<Texture2D>("tex3.png");
            brickTex6 = content.Load<Texture2D>(@"Objects/tex6");

            // Pc/Npc textures
            player1Tex = content.Load<Texture2D>(@"Animated/Samus");
            bTex = content.Load<Texture2D>(@"Animated/Shot");
            enemyTex1 = content.Load<Texture2D>(@"Animated/enemy");

            // Item textures
            itemTex1 = content.Load<Texture2D>(@"Animated/qu");
            // Sounds
            brinstar = content.Load<Song>(@"Sounds/03 - Brinstar");
            kraid = content.Load<Song>(@"Sounds/07 - Kraid");
            iceBeam = content.Load<SoundEffect>(@"Sounds/ice_beam");
            jump = content.Load<SoundEffect>(@"Sounds/jump");
            death = content.Load<SoundEffect>(@"Sounds/samus_die");

            // Layers
            L1Tex = content.Load<Texture2D>(@"para1");
            L2Tex = content.Load<Texture2D>(@"para2");
            L3Tex = content.Load<Texture2D>(@"para3");


            L1 = new Layer(layPos, 0.5f, L1Tex);
            L2 = new Layer(layPos, 0.7f, L2Tex);
            L3 = new Layer(layPos, 1f, L3Tex);
            //SoundEffectInstance beam = Manager.iceBeam.CreateInstance();

            // Misc.
            myFont = content.Load<SpriteFont>(@"font");

            // Loopa bakgrundsmusiken och sätt volym på den
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.8f;
            
            CreateLevel();
        }

        #region FileReaders
        //Metoder som används för att läsa filer hamnar här

        private string ReadFromFile(string fileName)
        {
            TextReader txtreader = new StreamReader(fileName);
            string txtcontent = "";

            while (true)
            {
                string row = txtreader.ReadLine();
                if (row == null)
                    break;
                else
                {
                    txtcontent += row;
                    rows++;
                }
            }
            txtreader.Close();
            return txtcontent;
        }

        private void CreateLevel()
        {
            string txtcontent =
                ReadFromFile(@".../debug/Levels/Level1.txt");

            int counter = 0;

            for (int cols = 0;
                cols < SuperClass.nrofCols; cols++)
            {
                for (int rows = 0;
                    rows < SuperClass.nrofRows; rows++)
                {
                    if (txtcontent.Substring
                        (counter, 1) == "b")
                    {
                        gameObjs[rows, cols] =
                            new Brick(brickTex1,
                                new Vector2(
                                    rows * SuperClass.tSize,
                                    cols * SuperClass.tSize),
                                    new Point(SuperClass.tSize,
                                        SuperClass.tSize));
                    }

                    if (txtcontent.Substring
                        (counter, 1) == "w")
                    {
                        gameObjs[rows, cols] =
                            new Wall(brickTex1,
                                new Vector2(
                                rows * SuperClass.tSize,
                                cols * SuperClass.tSize),
                                new Point(SuperClass.tSize,
                                    SuperClass.tSize));
                    }

                    if (txtcontent.Substring
                        (counter, 1) == "1")
                    {
                        gameObjs[rows, cols] =
                            new Player(player1Tex,
                                new Vector2
                                    (rows * SuperClass.tSize,
                                    cols * SuperClass.tSize),
                                    new Point(SuperClass.tSize,
                                        SuperClass.tSize));

                    }
                    if (txtcontent.Substring
                        (counter, 1) == "e")
                    {
                        gameObjs[rows, cols] =
                            new Enemy(enemyTex1,
                                new Vector2
                                (rows * SuperClass.tSize,
                                cols * SuperClass.tSize),
                                new Point(SuperClass.tSize,
                                    SuperClass.tSize));
                    }
                    if (txtcontent.Substring
                        (counter, 1) == "?")
                    {
                        gameObjs[rows, cols] =
                            new Items(itemTex1,
                                new Vector2
                                (rows * SuperClass.tSize,
                                cols * SuperClass.tSize),
                                new Point(SuperClass.tSize,
                                    SuperClass.tSize));
                    }

                    counter++;
                }
            }
        }

        #endregion



        #region MovementSystems/Collision

        #endregion

        public void Update(GameTime gameTime)
        {
            if (!start)
            {
                L1.Update();
                L2.Update();
                L3.Update();
            }

            if (!start && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                start = true;
            }

            if (hasLost)
            {
                start = false;

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    hasLost = false;
                    Player.energy = 3;
                    CreateLevel(); 
                }
            }

            if (!songstart && start && !hasLost)
            {
                MediaPlayer.Play(brinstar);
                songstart = true;
            }
            foreach (SuperClass g in gameObjs)
            {
                if (g != null)
                {
                    g.Update(gameTime);
                }
            }
            foreach (Bubble bub in gameObjList.OfType<Bubble>())
            {
                bub.Update(gameTime);
                if (!bub.IsActive)
                {
                    break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!start && !hasLost)
            {
                string hello = "Press Enter to Begin!";
                spriteBatch.DrawString(myFont, hello, new Vector2(300, 300), Color.White);
                L3.Draw(spriteBatch);
                L1.Draw(spriteBatch);
                L2.Draw(spriteBatch);
                
            }
            else if (hasLost)
            {
                string lost = "You have died! Press Enter to restart.";
                spriteBatch.DrawString(myFont, lost, new Vector2(300, 300), Color.White);
            }
            else if (start)
            {
                foreach (SuperClass g in gameObjs)
                {
                    if (g != null)
                    {
                        g.Draw(spriteBatch);
                    }
                }
                foreach (Bubble bub in gameObjList.OfType<Bubble>())
                {
                    bub.Draw(spriteBatch);
                }
                string pts = "Score: " + SuperClass.score;
                string life = "Energy: " + Player.energy;

                spriteBatch.DrawString(myFont, pts, new Vector2(6, 6), Color.Yellow);
                spriteBatch.DrawString(myFont, life, new Vector2(800, 6), Color.Wheat);
            }
        }
    }
}
