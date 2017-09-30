using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NewGameProject
{
    enum GameState { Start, MainMenu, Game, GameOver, Pause }
    enum MenuState { Play, Exit }
    enum MoveState { }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera cam;

        Texture2D playerTexture;
        Texture2D enemyTeethTexture; //No texture yet
        Texture2D floorTexture;
        Texture2D backgroundTexture;
        Texture2D cloudTexture;
        Texture2D handTexture;

        Vector2 backRectOne;
        Vector2 backRectTwo;

        GameState currentGameState;
        MenuState currentMenuState;
        int choice;

        Player chicle;
        LevelObject floor;
        List<Enemy> enemies;
        List<LevelObject> levelObjects;
        List<Rectangle> rectz;

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
            rectz = new List<Rectangle>();

            backRectOne = new Vector2(0, 0);
            backRectTwo = new Vector2(0, 0);
            cam = new Camera(GraphicsDevice.Viewport);
            

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
            //enemyTeethTexture = Content.Load<Texture2D>("enemyTeethTexture");
            floorTexture = Content.Load<Texture2D>("floorTexture");
            backgroundTexture = Content.Load<Texture2D>("backgroundTexture");
            cloudTexture = Content.Load<Texture2D>("cloudTexture");
            handTexture = Content.Load<Texture2D>("tempHand");



            chicle = new Player(playerTexture, new BetterRect(new Rectangle(50, 50, 60, 60)));
            floor = new LevelObject(floorTexture, new BetterRect(new Rectangle(0, 400, 2000, floorTexture.Height)));
            levelObjects.Add(floor);
            //LevelObject obs = new LevelObject(floorTexture, new BetterRect(new Rectangle(200, 380, 2000, floorTexture.Height)));
            //levelObjects.Add(obs);

            foreach(Enemy en in enemies)
            {
                rectz.Add(en.Rectangle.Rect);
            }
            for (int i = 0; i < levelObjects.Count; i++)
            {
                rectz.Add(levelObjects[i].Rectangle.Rect);
            }

            currentGameState = GameState.Game;
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
                    switch (choice)
                    {
                        case 0:
                            currentMenuState = MenuState.Play;
                            break;
                        case 1:
                            currentMenuState = MenuState.Exit;
                            break;
                    }
                    if (ks.IsKeyDown(Keys.Up))
                    {
                        if (choice == 0)
                        {
                            choice = 1;
                        }
                        else
                        {
                            choice--;
                        }
                    }
                    if (ks.IsKeyDown(Keys.Down))
                    {
                        if (choice == 1)
                        {
                            choice = 0;
                        }
                        else
                        {
                            choice++;
                        }
                    }
                    if (ks.IsKeyDown(Keys.Enter))
                    {
                        switch (choice)
                        {
                            case 0:
                                currentGameState = GameState.Game;
                                break;
                            case 1:
                                Exit();
                                break;
                        }
                    }

                    break;

                case GameState.Game:

                    chicle.Update(gameTime, ks, rectz);
                    cam.Scroll(chicle, GraphicsDevice);
                    
                    break;

                case GameState.GameOver:

                    break;

                case GameState.Pause:

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

                    break;
                case GameState.MainMenu:

                    break;

                case GameState.Game:
                    spriteBatch.Draw(backgroundTexture, cam.position, new Rectangle((int)(cam.Position.X * .1f), (int)cam.Position.Y, backgroundTexture.Width, backgroundTexture.Height), Color.White);

                    spriteBatch.Draw(cloudTexture, position: new Vector2((int)(cam.Position.X * .25f), (int)cam.Position.Y), color: Color.White, scale: new Vector2(.4f));

                    spriteBatch.Draw(chicle.Texture, destinationRectangle: chicle.Rectangle.Rect);
                    if (chicle.Hand != null) spriteBatch.Draw(handTexture, chicle.Hand.Rectangle.Rect, Color.White);
                    foreach (LevelObject lo in levelObjects)
                    {
                        //spriteBatch.Draw(lo.Texture, position: new Vector2(lo.Rectangle.X, lo.Rectangle.Y), scale: new Vector2(1f));
                        spriteBatch.Draw(lo.Texture, lo.Rectangle.Rect, Color.White);
                    }

                    foreach (Enemy en in enemies)
                    {
                        spriteBatch.Draw(en.Texture, en.Rectangle.Rect, Color.White);                      
                    }

                    break;

                case GameState.GameOver:
                    break;

                case GameState.Pause:
                    break;
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void CloudScroll(Vector2 v1, Vector2 v2, Vector2 v3)
        {
            if (cam.Position.X > GraphicsDevice.Viewport.Width / 2.5f)
            {

            }
        }
    }
}
