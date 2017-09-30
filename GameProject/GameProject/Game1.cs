using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Common;
using FarseerPhysics.Collision;
using FarseerPhysics.Controllers;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;
using System.Collections.Generic;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics;

namespace GameProject
{

    enum GameState { Start, MainMenu, Game, GameOver, Pause }
    enum MenuState { Play, Exit }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont debugFont;

        Texture2D chicleTexture;
        Texture2D floorTexture;
        Texture2D blockTexture;

        Player chicle;
        List<LevelObject> levelObjects;
        LevelObject exampleBlock;

        World world;
        Body botBody;
        GameState currState;

        int choice;
        MenuState myState;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            world = new World(new Vector2(0, 9.8f));
            currState = GameState.Game;
            levelObjects = new List<LevelObject>();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            chicleTexture = Content.Load<Texture2D>("TempGum");
            floorTexture = Content.Load<Texture2D>("tempBrick");
            blockTexture = Content.Load<Texture2D>("Brick_Block");

            chicle = new Player(world, chicleTexture);
            debugFont = Content.Load<SpriteFont>("DebugFont");

            

            exampleBlock = new LevelObject(world, blockTexture);
            levelObjects.Add(exampleBlock);

            float screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            float screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            botBody = BodyFactory.CreateRectangle(world, screenWidth, 10f, 10f, ConvertUnits.ToSimUnits(new Vector2(0, 400f)));
            chicle.Body.Position = ConvertUnits.ToSimUnits(new Vector2(20, 20));
            chicle.Y = botBody.Position.Y - (chicle.Texture.Height * .1f) - 1;
            botBody.IsStatic = true;

            choice = 0;
            myState = MenuState.Play;


            world.ContactManager.OnBroadphaseCollision += MyOnBroadphaseCollision;
           ;
            // TODO: use this.Content to load your game content here
        }
        public void MyOnBroadphaseCollision(ref FixtureProxy fp1, ref FixtureProxy fp2)
        {
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            // Update logic
            KeyboardState kb = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            switch (currState)
            {
                case GameState.Start:

                    foreach (Keys key in Keys.GetValues(typeof(Keys)))
                    {
                        if (kb.IsKeyDown(key))
                        {
                            currState = GameState.MainMenu;
                        }
                    }
                    break;

                case GameState.MainMenu:


                    switch (choice)
                    {
                        case 0:
                            myState = MenuState.Play;
                            break;
                        case 1:
                            myState = MenuState.Exit;
                            break;
                    }
                    if (kb.IsKeyDown(Keys.Up))
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
                    if (kb.IsKeyDown(Keys.Down))
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
                    if (kb.IsKeyDown(Keys.Enter))
                    {
                        switch (choice)
                        {
                            case 0:
                                currState = GameState.Game;
                                break;
                            case 1:
                                Exit();
                                break;
                        }
                    }

                    break;
                                    
                case GameState.Game:
                    chicle.Move(kb);
                    world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
                    
                    
                    break;

                case GameState.GameOver:

                    break;

                case GameState.Pause:

                    break;
            }

            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (currState)
            {
                case GameState.Start:

                    break;
                case GameState.MainMenu:

                    break;

                case GameState.Game:

                    spriteBatch.Draw(floorTexture, position: ConvertUnits.ToDisplayUnits(new Vector2(botBody.Position.X, botBody.Position.Y)), scale: new Vector2(.5f));
                    spriteBatch.Draw(chicle.Texture, position: ConvertUnits.ToDisplayUnits(chicle.Body.Position), scale: new Vector2(.1f));

                    spriteBatch.DrawString(debugFont, ConvertUnits.ToDisplayUnits(chicle.Body.Position.X) + "   " + ConvertUnits.ToDisplayUnits(chicle.Body.Position.Y) + "   ", new Vector2(300, 200), Color.White);

                    foreach (LevelObject lo in levelObjects)
                    {
                        spriteBatch.Draw(lo.Texture, position: ConvertUnits.ToDisplayUnits(lo.Body.Position), scale: new Vector2(.1f));
                        spriteBatch.DrawString(debugFont, ConvertUnits.ToDisplayUnits(lo.Body.Position.X) + "   " + ConvertUnits.ToDisplayUnits(lo.Body.Position.Y) + "   " , new Vector2(200, 200), Color.White);
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
    }
}
