using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace NewGameProject
{
    enum GameState { Start, MainMenu, Game, GameOver, Pause }
    enum MenuState { Play, Exit }
    enum MoveState { }
    enum ChicleState { FaceLeft, FaceRight, WalkLeft, WalkRight }
    enum EnemyTeethState { FaceLeft, FaceRight, ClosedMouth, OpenMouth }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera cam;
        static Random rand;

        //Texture fields
        Texture2D playerTexture; //No texture yet
        Texture2D enemyTeethTexture; //No texture yet
        Texture2D floorTexture;
        Texture2D backgroundTexture;
        Texture2D cloudTexture;
        Texture2D chicleSheet;
        Texture2D wallTexture;
        Texture2D startScreen;
        Texture2D menuScreen;
        Texture2D playButtonOver;
        Texture2D playButtonUp;
        Texture2D exitButtonOver;
        Texture2D exitButtonUp;
        Texture2D resumeButtonOver;
        Texture2D resumeButtonUp;
        Texture2D menuButtonOver;
        Texture2D menuButtonUp;
        Texture2D healthHeart;
        Texture2D gameOver;
        Texture2D chicleTexture;
        Texture2D chicleArm;
        Texture2D platformTextureOne;
        Texture2D platformTextureTwo;
        Texture2D platformTextureThree;

        Texture2D chicleSpriteSheetZero;

        Texture2D chicleTextureZero;
        Texture2D chicleTextureOne;
        Texture2D chicleTextureTwo;
        Texture2D chicleTextureThree;
        Texture2D chicleTextureFour;
        Texture2D chicleTextureFive;
        Texture2D gameWonScreen;

        Vector2 backRectOne;
        Vector2 backRectTwo;
        LevelObject playButtonRect;
        LevelObject exitButtonRect;
        LevelObject resumeButtonRect;
        LevelObject menuButtonRect;
        LevelObject pausedExitButtonRect;
        


        GameState currentGameState;
        MenuState currentMenuState;
        ChicleState currentChicleState;
        int choice;
        int textureNum;
        int finishLine;
        bool gameWon;
        bool levelGenerated;

        Player chicle;
        LevelObject floor;
        LevelObject wall;
        List<Enemy> enemies;
        List<LevelObject> levelObjects;

        // Animation fields
        double fps;
        double secondsPerFrame;
        double timeCounter; 
        int currentFrame;
        int totalFrames;
        int numSpritesInSheet;
        int widthOfSingleSprite;

        Color col;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            enemies = new List<Enemy>();
            levelObjects = new List<LevelObject>();

            backRectOne = new Vector2(0, 0);
            backRectTwo = new Vector2(0, 0);
            cam = new Camera(GraphicsDevice.Viewport);
            rand = new Random();

            fps = 8;
            secondsPerFrame = 1.0f / fps;
            timeCounter = 0;
            currentFrame = 1;
            totalFrames = 3;
            Load();
            gameWon = false;
            bool levelGenerated = false;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerTexture = Content.Load<Texture2D>("playerTexture");
            enemyTeethTexture = Content.Load<Texture2D>("enemyTeethTexture");
            floorTexture = Content.Load<Texture2D>("floorTexture");
            backgroundTexture = Content.Load<Texture2D>("backgroundTexture");
            cloudTexture = Content.Load<Texture2D>("cloudTexture");
            chicleSheet = Content.Load<Texture2D>("chicleSheet");
            wallTexture = Content.Load<Texture2D>("testFloorTexture");
            startScreen = Content.Load<Texture2D>("GumioStartScreen");
            menuScreen = Content.Load<Texture2D>("GumioMenuScreen");
            playButtonOver = Content.Load<Texture2D>("PlayButtonOver");
            playButtonUp = Content.Load<Texture2D>("PlayButtonUp");
            exitButtonOver = Content.Load<Texture2D>("ExitButtonOver");
            exitButtonUp = Content.Load<Texture2D>("ExitButtonUp");
            resumeButtonOver = Content.Load<Texture2D>("ResumeButtonOver");
            resumeButtonUp = Content.Load<Texture2D>("ResumeButtonUp");
            menuButtonOver = Content.Load<Texture2D>("MenuButtonOver");
            menuButtonUp = Content.Load<Texture2D>("MenuButtonUp");
            healthHeart = Content.Load<Texture2D>("HealthHeart");
            gameOver = Content.Load<Texture2D>("GameOver");
            chicleArm = Content.Load<Texture2D>("chicleArm");
            chicleTextureZero = Content.Load<Texture2D>("PinkBodyNoHat");
            chicleTextureOne = Content.Load<Texture2D>("PinkBodyHatOne");
            chicleTextureTwo = Content.Load<Texture2D>("PinkBodyHatTwo");
            chicleTextureThree = Content.Load<Texture2D>("BlueBodyNoHat");
            chicleTextureFour = Content.Load<Texture2D>("BlueBodyHatOne");
            chicleTextureFive = Content.Load<Texture2D>("BlueBodyHatTwo");
            chicleSpriteSheetZero = Content.Load<Texture2D>("chicleSpriteSheetZero");
            platformTextureOne = Content.Load<Texture2D>("platformTextureOne");
            platformTextureTwo = Content.Load<Texture2D>("platformTextureTwo");
            platformTextureThree = Content.Load<Texture2D>("platformTextureThree");
            gameWonScreen = Content.Load<Texture2D>("gameWonScreen");
        
            // chicleTexture = Content.Load<Texture2D>("SpriteSheetImproved");
            numSpritesInSheet = 4;
            widthOfSingleSprite = chicleSpriteSheetZero.Width / numSpritesInSheet;
            textureNum = 1;
            switch (textureNum)
            {
                case 0:
                    chicle = new Player(chicleTextureZero, new Rectangle(0, 400, (int)(chicleTextureZero.Width * .025), (int)(chicleTextureZero.Height * .025)));
                    break;
                case 1:
                    chicle = new Player(chicleTextureOne, new Rectangle(0, 350, (int)(chicleTextureOne.Width * .05), (int)(chicleTextureOne.Height * .05) ));
                    break;
                case 2:
                    chicle = new Player(chicleTextureTwo, new Rectangle(0, 400, (int)(chicleTextureTwo.Width * .1), (int)(chicleTextureTwo.Height * .1) ));
                    break;
                case 3:
                    chicle = new Player(chicleTextureThree, new Rectangle(0, 400, (int)(chicleTextureThree.Width * .1), (int)(chicleTextureThree.Height * .1) ));
                    break;
                case 4:
                    chicle = new Player(chicleTextureFour, new Rectangle(0, 400, (int)(chicleTextureFour.Width * .1), (int)(chicleTextureFour.Height * .1) ));
                    break;
                case 5:
                    chicle = new Player(chicleTextureFive, new Rectangle(0, 400, (int)(chicleTextureFive.Width * .1), (int)(chicleTextureFive.Height * .1) ));
                    break;
            }

            //chicle = new Player(playerTexture, new Rectangle(0, 400, (int)(playerTexture.Width * .1), (int)(playerTexture.Height * .1)));
            floor = new LevelObject(floorTexture, new Rectangle(-5 , 410, floorTexture.Width * 5, floorTexture.Height));
            wall = new LevelObject(wallTexture, new Rectangle(250, 200, (int)(wallTexture.Width * .5), (int)(wallTexture.Height * .05)));


            playButtonRect = new LevelObject(playButtonUp, new Rectangle(600, 15, (int)(playButtonOver.Width * .25), (int)(playButtonOver.Height * .25)));
            exitButtonRect = new LevelObject(exitButtonUp, new Rectangle(600, 100, (int)(exitButtonOver.Width * .25), (int)(exitButtonOver.Height * .25)));
            resumeButtonRect = new LevelObject(resumeButtonUp, new Rectangle((graphics.PreferredBackBufferWidth / 2) - 75, 100, (int)(resumeButtonOver.Width * .25), (int)(resumeButtonOver.Height * .25)));
            menuButtonRect = new LevelObject(menuButtonUp, new Rectangle((graphics.PreferredBackBufferWidth / 2) - 75, 175, (int)(menuButtonOver.Width * .25), (int)(menuButtonOver.Height * .25)));
            pausedExitButtonRect = new LevelObject(exitButtonUp, new Rectangle((graphics.PreferredBackBufferWidth / 2) - 75, 250, (int)(exitButtonOver.Width * .25), (int)(exitButtonOver.Height * .25)));

            


            //levelObjects.Add(floor);
            //levelObjects.Add(wall);

            try
            {
                StreamReader input = new StreamReader("..\\..\\..\\..\\..\\..\\ColorChoice.txt");
                String tempRead = input.ReadLine();
                input.Close();

                switch (tempRead)
                {
                    case "Blue":
                        col = Color.Blue;
                        break;
                    case "MediumSeaGreen":
                        col = Color.MediumSeaGreen;
                        break;
                    case "Black":
                        col = Color.Black;
                        break;
                    case "Red":
                        col = Color.Red;
                        break;
                    case "Default":
                        col = Color.White;
                        break;
                    case "Orange":
                        col = Color.Orange;
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                col = Color.White;
                Console.WriteLine("Use the external tool if you would like to select a color for the player character.");
            }

            currentGameState = GameState.Start;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState ks = Keyboard.GetState();
            MouseState ms = Mouse.GetState();

            switch (currentGameState)
            {
                case GameState.Start:

                    foreach (Keys key in Keys.GetValues(typeof(Keys)))
                    {
                        if (ks.IsKeyDown(key))
                        {
                            currentGameState = GameState.MainMenu;
                        }
                    }
                    break;

                case GameState.MainMenu:

                    gameWon = true;

                    if (levelGenerated == false)
                    {
                        LevelObject startingPlatform = new LevelObject(platformTextureOne, new Rectangle(0, 410, (int)(platformTextureOne.Width * .25), (int)(platformTextureOne.Height * .25)));
                        for (int i = 0; i < 30; i++)
                        {
                            int spawnEnemy = rand.Next(0, 2);
                            int randPlatformNum = rand.Next(1, 4);
                            LevelObject randPlatform = null;
                            EnemyTeeth teeth = null;
                            if (randPlatformNum == 1)
                            {
                                randPlatform = new LevelObject(platformTextureOne, new Rectangle((i + 1) * 2 * rand.Next(125, 150), rand.Next(50, 400), (int)(platformTextureOne.Width * .25), (int)(platformTextureOne.Height * .1)));
                            }
                            if (randPlatformNum == 2)
                            {
                                randPlatform = new LevelObject(platformTextureTwo, new Rectangle((i + 1) * 2 * rand.Next(125, 150), rand.Next(50, 400), (int)(platformTextureTwo.Width * .25), (int)(platformTextureTwo.Height * .1)));
                            }
                            if (randPlatformNum == 3)
                            {
                                randPlatform = new LevelObject(platformTextureThree, new Rectangle((i + 1) * 2 * rand.Next(125, 150), rand.Next(50, 400), (int)(platformTextureThree.Width * .25), (int)(platformTextureThree.Height * .1)));
                            }
                            if (spawnEnemy == 1)
                            {
                                teeth = new EnemyTeeth(enemyTeethTexture, new Rectangle(randPlatform.Rectangle.X + (int)(randPlatform.Rectangle.Width / 3), randPlatform.Rectangle.Y - (int)(enemyTeethTexture.Height * .2), (int)(enemyTeethTexture.Width * .15), (int)(enemyTeethTexture.Height * .2)));
                                enemies.Add(teeth);
                            }
                            levelObjects.Add(randPlatform);
                            if (i == 29) finishLine = randPlatform.Rectangle.Right - 5;
                        }
                        levelObjects.Add(startingPlatform);
                        levelGenerated = true;
                    }

                    if (ms.X >= playButtonRect.Rectangle.X && ms.X <= playButtonRect.Rectangle.X + playButtonRect.Rectangle.Width)
                    {
                        if (ms.Y >= playButtonRect.Rectangle.Y && ms.Y <= playButtonRect.Rectangle.Y + playButtonRect.Rectangle.Height)
                        {
                            playButtonRect.Texture = playButtonOver;
                            if (ms.LeftButton == ButtonState.Pressed)
                            {
                                currentGameState = GameState.Game;
                            }
                        }
                        else
                        {
                            playButtonRect.Texture = playButtonUp;
                        }
                    }

                    if (ms.X >= exitButtonRect.Rectangle.X && ms.X <= exitButtonRect.Rectangle.X + exitButtonRect.Rectangle.Width)
                    {
                        if (ms.Y >= exitButtonRect.Rectangle.Y && ms.Y <= exitButtonRect.Rectangle.Y + exitButtonRect.Rectangle.Height)
                        {
                            exitButtonRect.Texture = exitButtonOver;
                            if (ms.LeftButton == ButtonState.Pressed)
                            {
                                this.Exit();
                            }
                        }
                        else
                        {
                            exitButtonRect.Texture = exitButtonUp;
                        }
                    }



                    break;

                case GameState.Game:

                    chicle.Move(gameTime, ks); 
                    chicle.Stick(ms, levelObjects, cam);
                    Collisions();
                    cam.Scroll(chicle, GraphicsDevice);

                    timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

                    if (timeCounter >= secondsPerFrame)
                    {
                        currentFrame++;

                        if (currentFrame > totalFrames)
                            currentFrame = 1;

                        timeCounter -= secondsPerFrame;
                    }

                    if (ks.IsKeyDown(Keys.A))
                    {
                        currentChicleState = ChicleState.WalkLeft;
                    }
                    if (ks.IsKeyDown(Keys.D))
                    {
                        currentChicleState = ChicleState.WalkRight;
                    }

                    switch (currentChicleState)
                    {
                        case ChicleState.FaceLeft:
                            if (ks.IsKeyDown(Keys.A)) currentChicleState = ChicleState.WalkRight;
                            break;
                        case ChicleState.FaceRight:
                            if (ks.IsKeyDown(Keys.D)) currentChicleState = ChicleState.WalkRight;
                            break;
                        case ChicleState.WalkLeft:
                            if (!ks.IsKeyDown(Keys.A)) currentChicleState = ChicleState.FaceLeft;
                            break;
                        case ChicleState.WalkRight:
                            if (!ks.IsKeyDown(Keys.D)) currentChicleState = ChicleState.FaceRight;
                            break;
                    }

                    if (ks.IsKeyDown(Keys.Tab))
                    {
                        currentGameState = GameState.Pause;
                    }

                    foreach (Enemy e in enemies)
                    {
                        if (chicle.Rectangle.Intersects(e.Rectangle))
                        {

                            //chicle.Rectangle = new Rectangle(0, 350, (int)(chicleTextureOne.Width * .05), (int)(chicleTextureOne.Height * .05));
                            if (!chicle.Jumped && chicle.Rectangle.Right >= e.Rectangle.Left)
                            {
                                chicle.LeftCollision = false;
                                chicle.RightCollision = false;
                                chicle.Velocity = new Vector2(-15f, 0f);
                                chicle.LeftCollision = false;
                                chicle.RightCollision = false;
                            }
                            else if (!chicle.Jumped && chicle.Rectangle.Left <= e.Rectangle.Right)
                            {
                                chicle.LeftCollision = false;
                                chicle.RightCollision = false;
                                chicle.Velocity = new Vector2(15f, 0f);
                                chicle.LeftCollision = false;
                                chicle.RightCollision = false;
                            }
                            else
                            {
                                chicle.Velocity = new Vector2(-15f, -10f);
                            }
                            

                            chicle.Lives--;
                        }
                        if (e is EnemyTeeth)
                        {
                            EnemyTeeth currentEnemy = (EnemyTeeth)e;
                            currentEnemy.Move(chicle);
                        }
                    }

                    if (chicle.Lives <= 0)
                    {
                        cam.Position = new Vector2(0, 0);
                        currentGameState = GameState.GameOver;
                    }

                    if (chicle.Lives > 0 && chicle.Rectangle.X >= finishLine)
                    {
                        gameWon = true;
                        cam.Position = new Vector2(0, 0);
                        currentGameState = GameState.GameOver;
                    }

                    break;

                case GameState.GameOver:

                    if (ms.X >= menuButtonRect.Rectangle.X && ms.X <= menuButtonRect.Rectangle.X + menuButtonRect.Rectangle.Width)
                    {
                        if (ms.Y >= menuButtonRect.Rectangle.Y && ms.Y <= menuButtonRect.Rectangle.Y + menuButtonRect.Rectangle.Height)
                        {
                            menuButtonRect.Texture = menuButtonOver;
                            if (ms.LeftButton == ButtonState.Pressed)
                            {
                                if (textureNum == 0 || textureNum == 3)
                                {
                                    chicle = new Player(chicle.Texture, new Rectangle(0, 350, (int)(chicle.Texture.Width * .1), (int)(chicle.Texture.Height * .025)));
                                }
                                else
                                {
                                    chicle = new Player(chicle.Texture, new Rectangle(0, 350, (int)(chicle.Texture.Width * .05), (int)(chicle.Texture.Height * .05) ));
                                }
                                cam.Position = new Vector2(0, 0);
                                currentGameState = GameState.MainMenu;
                            }
                        }
                        else
                        {
                            menuButtonRect.Texture = menuButtonUp;
                        }

                        levelGenerated = false;
                        enemies.Clear();
                        levelObjects.Clear();
                    }

                    if (ms.X >= pausedExitButtonRect.Rectangle.X && ms.X <= pausedExitButtonRect.Rectangle.X + pausedExitButtonRect.Rectangle.Width)
                    {
                        if (ms.Y >= pausedExitButtonRect.Rectangle.Y && ms.Y <= pausedExitButtonRect.Rectangle.Y + pausedExitButtonRect.Rectangle.Height)
                        {
                            pausedExitButtonRect.Texture = exitButtonOver;
                            if (ms.LeftButton == ButtonState.Pressed)
                            {
                                this.Exit();
                            }
                        }
                        else
                        {
                            pausedExitButtonRect.Texture = exitButtonUp;
                        }
                    }

                    break;

                case GameState.Pause:

                    if (ms.X >= resumeButtonRect.Rectangle.X && ms.X <= resumeButtonRect.Rectangle.X + resumeButtonRect.Rectangle.Width)
                    {
                        if (ms.Y >= resumeButtonRect.Rectangle.Y && ms.Y <= resumeButtonRect.Rectangle.Y + resumeButtonRect.Rectangle.Height)
                        {
                            resumeButtonRect.Texture = resumeButtonOver;
                            if (ms.LeftButton == ButtonState.Pressed)
                            {
                                currentGameState = GameState.Game;
                            }
                        }
                        else
                        {
                            resumeButtonRect.Texture = resumeButtonUp;
                        }
                    }

                    if (ms.X >= menuButtonRect.Rectangle.X && ms.X <= menuButtonRect.Rectangle.X + menuButtonRect.Rectangle.Width)
                    {
                        if (ms.Y >= menuButtonRect.Rectangle.Y && ms.Y <= menuButtonRect.Rectangle.Y + menuButtonRect.Rectangle.Height)
                        {
                            menuButtonRect.Texture = menuButtonOver;
                            if (ms.LeftButton == ButtonState.Pressed)
                            {
                                currentGameState = GameState.MainMenu;
                                if (textureNum == 0 || textureNum == 3)
                                {
                                    chicle = new Player(chicle.Texture, new Rectangle(0, 350, (int)(chicle.Texture.Width * .025), (int)(chicle.Texture.Height * .025)));
                                }
                                else
                                {
                                    chicle = new Player(chicle.Texture, new Rectangle(0, 350, (int)(chicle.Texture.Width * .05), (int)(chicle.Texture.Height * .05)));
                                }
                                cam.Position = new Vector2(0, 0);
                            }
                        }
                        else
                        {
                            menuButtonRect.Texture = menuButtonUp;
                        }
                    }

                    if (ms.X >= pausedExitButtonRect.Rectangle.X && ms.X <= pausedExitButtonRect.Rectangle.X + pausedExitButtonRect.Rectangle.Width)
                    {
                        if (ms.Y >= pausedExitButtonRect.Rectangle.Y && ms.Y <= pausedExitButtonRect.Rectangle.Y + pausedExitButtonRect.Rectangle.Height)
                        {
                            pausedExitButtonRect.Texture = exitButtonOver;
                            if (ms.LeftButton == ButtonState.Pressed)
                            {
                                this.Exit();
                            }
                        }
                        else
                        {
                            pausedExitButtonRect.Texture = exitButtonUp;
                        }
                    }

                    break;             
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: cam.CameraMatrix());

            switch (currentGameState)
            {
                case GameState.Start:
                    spriteBatch.Draw(startScreen, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                    break;
                case GameState.MainMenu:
                    spriteBatch.Draw(menuScreen, new Rectangle((int)cam.Position.X, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                    spriteBatch.Draw(playButtonRect.Texture, position: new Vector2(cam.Position.X + playButtonRect.Rectangle.X, playButtonRect.Rectangle.Y), scale: new Vector2(.25f));
                    spriteBatch.Draw(exitButtonRect.Texture, position: new Vector2(cam.Position.X + exitButtonRect.Rectangle.X, exitButtonRect.Rectangle.Y), scale: new Vector2(.25f));

                    break;

                case GameState.Game:
                    spriteBatch.Draw(backgroundTexture, cam.position, new Rectangle((int)(cam.Position.X * .05f), (int)cam.Position.Y, backgroundTexture.Width, backgroundTexture.Height), Color.White);

                    for (int i = 25; i < floorTexture.Width * 5; i += 250)
                    {
                        spriteBatch.Draw(cloudTexture, position: new Vector2((int)(cam.Position.X * .25f) + i, (int)cam.Position.Y ), color: Color.White, scale: new Vector2(.3f));
                    }

                    //spriteBatch.Draw(chicle.Texture, position: chicle.Position, scale: new Vector2(.1f));
                   // spriteBatch.Draw(chicle.Texture, position: new Vector2(chicle.Rectangle.X, chicle.Rectangle.Y), scale: new Vector2(.1f));

                    foreach (LevelObject lo in levelObjects)
                    {
                       // spriteBatch.Draw(lo.Texture, position: new Vector2(lo.Rectangle.X, lo.Rectangle.Y), scale: new Vector2(1f));
                        spriteBatch.Draw(lo.Texture, destinationRectangle: new Rectangle(lo.Rectangle.X, lo.Rectangle.Y, lo.Rectangle.Width, (int)(lo.Rectangle.Height * 1.5)), color: Color.White);
                    }

                    //for (int i = 0; i < floorTexture.Width * 5; i += floorTexture.Width)
                    //{
                    //    spriteBatch.Draw(floorTexture, position: new Vector2(floor.Rectangle.X + i, floor.Rectangle.Y), scale: new Vector2(1f));
                    //}


                    foreach (Enemy en in enemies)
                    {
                        if (en is EnemyTeeth)
                        {
                            EnemyTeeth currentEnemy = (EnemyTeeth)en;
                            currentEnemy.DrawEnemyTeethChomping(spriteBatch, currentFrame);
                        }                
                    }

                    for (int i = 0; i < chicle.Lives; i++)
                    {
                        spriteBatch.Draw(healthHeart, position: new Vector2(cam.Position.X + (int)((healthHeart.Width * i) *(.2)), 0), scale: new Vector2(.2f));
                    }

                    if (true)
                    {
                        chicle.DrawChicleSticking(spriteBatch, chicleArm);
                    }

                    switch (currentChicleState)
                    {
                        case ChicleState.FaceLeft:
                            DrawChicleStanding(SpriteEffects.FlipHorizontally);
                            break;
                        case ChicleState.FaceRight:
                            DrawChicleStanding(SpriteEffects.None);
                            break;
                        case ChicleState.WalkLeft:
                            DrawChicleWalking(SpriteEffects.FlipHorizontally);
                            break;
                        case ChicleState.WalkRight:
                            DrawChicleWalking(SpriteEffects.None);
                            break;
                    }

                    break;

                case GameState.GameOver:

                    spriteBatch.Draw(gameWonScreen, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                    if (chicle.Lives == 0 && chicle.Rectangle.X < finishLine)
                    {
                        spriteBatch.Draw(gameOver, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                    }
                    
                    spriteBatch.Draw(menuButtonRect.Texture, position: new Vector2(cam.Position.X + menuButtonRect.Rectangle.X, menuButtonRect.Rectangle.Y), scale: new Vector2(.25f));
                    spriteBatch.Draw(pausedExitButtonRect.Texture, position: new Vector2(cam.Position.X + pausedExitButtonRect.Rectangle.X, pausedExitButtonRect.Rectangle.Y), scale: new Vector2(.25f));
                    break;

                case GameState.Pause:
                    spriteBatch.Draw(backgroundTexture, cam.position, new Rectangle((int)(cam.Position.X * .05f), (int)cam.Position.Y, backgroundTexture.Width, backgroundTexture.Height), Color.White);

                    for (int i = 25; i < floorTexture.Width * 5; i += 250)
                    {
                        spriteBatch.Draw(cloudTexture, position: new Vector2((int)(cam.Position.X * .25f) + i, (int)cam.Position.Y), color: Color.White, scale: new Vector2(.3f));
                    }

                    //spriteBatch.Draw(chicle.Texture, position: chicle.Position, scale: new Vector2(.1f));
                    // spriteBatch.Draw(chicle.Texture, position: new Vector2(chicle.Rectangle.X, chicle.Rectangle.Y), scale: new Vector2(.1f));

                    foreach (LevelObject lo in levelObjects)
                    {
                        // spriteBatch.Draw(lo.Texture, position: new Vector2(lo.Rectangle.X, lo.Rectangle.Y), scale: new Vector2(1f));
                        spriteBatch.Draw(lo.Texture, destinationRectangle: new Rectangle(lo.Rectangle.X, lo.Rectangle.Y, lo.Rectangle.Width, (int)(lo.Rectangle.Height * 1.5)), color: Color.White);
                    }

                    //for (int i = 0; i < floorTexture.Width * 5; i += floorTexture.Width)
                    //{
                    //    spriteBatch.Draw(floorTexture, position: new Vector2(floor.Rectangle.X + i, floor.Rectangle.Y), scale: new Vector2(1f));
                    //}


                    foreach (Enemy en in enemies)
                    {
                        if (en is EnemyTeeth)
                        {
                            EnemyTeeth currentEnemy = (EnemyTeeth)en;
                            currentEnemy.DrawEnemyTeethChomping(spriteBatch, currentFrame);
                        }
                    }

                    for (int i = 0; i < chicle.Lives; i++)
                    {
                        spriteBatch.Draw(healthHeart, position: new Vector2(cam.Position.X + (int)((healthHeart.Width * i) * (.2)), 0), scale: new Vector2(.2f));
                    }

                    if (true)
                    {
                        chicle.DrawChicleSticking(spriteBatch, chicleArm);
                    }

                    switch (currentChicleState)
                    {
                        case ChicleState.FaceLeft:
                            DrawChicleStanding(SpriteEffects.FlipHorizontally);
                            break;
                        case ChicleState.FaceRight:
                            DrawChicleStanding(SpriteEffects.None);
                            break;
                        case ChicleState.WalkLeft:
                            DrawChicleWalking(SpriteEffects.FlipHorizontally);
                            break;
                        case ChicleState.WalkRight:
                            DrawChicleWalking(SpriteEffects.None);
                            break;
                    }

                    spriteBatch.Draw(resumeButtonRect.Texture, position: new Vector2(cam.Position.X + resumeButtonRect.Rectangle.X, resumeButtonRect.Rectangle.Y), scale: new Vector2(.25f));
                    spriteBatch.Draw(menuButtonRect.Texture, position: new Vector2(cam.Position.X + menuButtonRect.Rectangle.X, menuButtonRect.Rectangle.Y), scale: new Vector2(.25f));
                    spriteBatch.Draw(pausedExitButtonRect.Texture, position: new Vector2(cam.Position.X + pausedExitButtonRect.Rectangle.X, pausedExitButtonRect.Rectangle.Y), scale: new Vector2(.25f));

                    break;
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Collisions()
        {
            List<LevelObject> intersectingObjects = new List<LevelObject>();
            foreach (LevelObject lo in levelObjects)
            {
                if (chicle.Rectangle.Intersects(lo.Rectangle))
                {
                    intersectingObjects.Add(lo);
                }
            }

            LevelObject groundingObject = null;
            LevelObject leftingObject = null;
            LevelObject rightingObject = null;
            LevelObject bottingObject = null;
            foreach (LevelObject lo in intersectingObjects)
            {
                if (chicle.Rectangle.Left <= lo.Rectangle.Right && chicle.Rectangle.Right > lo.Rectangle.Right) lo.LeftCollision = true;
                else lo.LeftCollision = false;

                if (chicle.Rectangle.Right >= lo.Rectangle.Left && chicle.Rectangle.Left < lo.Rectangle.Left) lo.RightCollision = true;
                else lo.RightCollision = false;

                if (chicle.Rectangle.Top <= lo.Rectangle.Bottom && chicle.Rectangle.Bottom > lo.Rectangle.Bottom) lo.BottomCollision = true;
                else lo.BottomCollision = false;

                if (chicle.Rectangle.Bottom >= lo.Rectangle.Top && chicle.Rectangle.Top < lo.Rectangle.Top) lo.TopCollision = true;
                else lo.TopCollision = false;       
                
                if (lo.TopCollision) 
                {
                    groundingObject = lo;
                }
              
                if (lo.LeftCollision)
                {
                    leftingObject = lo;
                }

                if (lo.RightCollision)
                {
                    rightingObject = lo;
                }

                if (lo.BottomCollision)
                {
                    bottingObject = lo;
                }
            }

            if (groundingObject != null)
            {
                chicle.Jumped = false;
            }
            else
            {
                chicle.Jumped = true;
            }

            if (leftingObject != null)
            {
                chicle.LeftCollision = true;
            }
            else
            {
                chicle.LeftCollision = false;
            }

            if (rightingObject != null)
            {
                chicle.RightCollision = true;
            }
            else
            {
                chicle.RightCollision = false;
            }

            if (bottingObject != null)
            {
                chicle.TopCollision = true;
            }
            else
            {
                chicle.TopCollision = false;
            }

        }

        private void DrawChicleWalking(SpriteEffects flip)
        {
            spriteBatch.Draw(chicleSpriteSheetZero, destinationRectangle: chicle.Rectangle, sourceRectangle: new Rectangle(currentFrame * widthOfSingleSprite, 0, widthOfSingleSprite, chicleSpriteSheetZero.Height), color: col, rotation: 0.0f, origin: Vector2.Zero, scale: new Vector2(1f), effects: flip, layerDepth: 0.0f);
        }

        private void DrawChicleStanding(SpriteEffects flip)
        {
            spriteBatch.Draw(chicleSpriteSheetZero, destinationRectangle: chicle.Rectangle, sourceRectangle: new Rectangle(0, 0, widthOfSingleSprite, chicleSpriteSheetZero.Height), color: col, rotation: 0.0f, origin: Vector2.Zero, scale: new Vector2(1f), effects: flip, layerDepth: 0.0f);

        }

        public void Load()
        {
            StreamReader input = null;

            try
            {
                input = new StreamReader("..\\WindowsFormApplication1\\WindowsFormApplication1\\obj\\CharacterTransferFiles\\character.txt");
                String line = null;
                while ((line = input.ReadLine()) != null)
                {
                    textureNum = int.Parse(line);
                }
            }
            catch (Exception e)
            {
                //Console.SetWindowSize(320, 84);
                Console.WriteLine("Problem reading: " + e.Message);
            }
            finally
            {
                if (input != null)
                    input.Close();
            }
        }
    }
}
