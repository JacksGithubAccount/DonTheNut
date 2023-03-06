using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DonTheNut
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Inputter inputter;

        private ItemGenerator generateItemizer;
        
        private Texture2D background;
        private MenuScreen menuScreen;

        //ResolutionIndependenceInput rii;
        private MC mc;
        private SkeletonBrain skellyJelly;
        private List<SkeletonBrain> Skellies;
        private InteractNPC interaction;

        List<Rectangle> allObjects;
        List<BoundingSphere> allSpheres;
        List<BoundingBox> allBoxes;

        List<CharacterStats> allPlayers;
        List<CharacterStats> allNPCs;
        List<Consumables> allThrownItems;
        List<Items> allItems;
        List<ItemSlot> droppedItems;

        Quadtree quadTree;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;            
            //Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;  // set this value to the desired width of your window //1920×1080
            _graphics.PreferredBackBufferHeight = 480;   // set this value to the desired height of your window //800 x 480
            _graphics.ApplyChanges();
            inputter = new Inputter();
            quadTree = new Quadtree(0, new Rectangle(0, 0, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height));
            //allObjects = new List<Rectangle>();
            allItems = new List<Items>();
            droppedItems = new List<ItemSlot>();
            generateItemizer = new ItemGenerator(Content);
            generateItemizer.GenerateItems(ref allItems);
            menuScreen = new MenuScreen(Content, _graphics, inputter);
            mc = new MC(Content, menuScreen);// get the newest state    
            mc.AddToInventory(allItems.Find(x => (x.ID == 1001)), 1);
            mc.AddToInventory(allItems.Find(x => (x.ID == 1002)), 2);
            mc.AddToInventory(allItems.Find(x => (x.ID == 1003)), 4);
            mc.AddToInventory(allItems.Find(x => (x.ID == 1004)), 2);
            mc.AddToInventory(allItems.Find(x => (x.ID == 1005)), 3);
            mc.AddToInventory(allItems.Find(x => (x.ID == 2001)), 1);
            mc.AddToInventory(allItems.Find(x => (x.ID == 4001)), 1);
            mc.AddToInventory(allItems.Find(x => (x.ID == 5001)), 1);
            mc.AddToInventory(allItems.Find(x => (x.ID == 3001)), 1);
            mc.AddToInventory(allItems.Find(x => (x.ID == 1006)), 1);
            mc.AddToInventory(allItems.Find(x => (x.ID == 1007)), 8);
            mc.AddToInventory(allItems.Find(x => (x.ID == 6001)), 1);
            mc.AddToInventory(allItems.Find(x => (x.ID == 9001)), 11);
            mc.AddToInventory(allItems.Find(x => (x.ID == 1101)), 113);
            mc.DefaultEquipmentLoadout();
            mc.updateGearStats();
            menuScreen.getPlayer(mc);
            
            skellyJelly = new SkeletonBrain(Content);
            Skellies = new List<SkeletonBrain>();
            interaction = new InteractNPC(mc, skellyJelly, droppedItems);
            Skellies.Add(skellyJelly);
            Skellies.Add(new SkeletonBrain(Content, 200, 200));

            //allObjects.Add(mc.hamsterBox);
            //allObjects.Add(skellyJelly.hamsterBox);
            allSpheres = new List<BoundingSphere>();
            allSpheres.Add(mc.HamsterBall);
            allSpheres.Add(skellyJelly.HamsterBall);

            allPlayers = new List<CharacterStats>();
            allNPCs = new List<CharacterStats>();
            allThrownItems = new List<Consumables>();

            allPlayers.Add(mc);
            foreach (SkeletonBrain skelbros in Skellies)
            {
                allNPCs.Add(skelbros);
            }
            

            List<ItemSlot> tempitemlist = mc.inventory.FindAll(x => x.item.itemSubType == ItemSubtype.Bomb);
            foreach (ItemSlot item in tempitemlist)
            {
                Consumables tempConsum = (Consumables)item.item;
                allSpheres.Add(tempConsum.sphereOfInfluence);
                foreach (Consumables thrownItem in item.thrownItems)
                {
                    allThrownItems.Add(thrownItem);
                }
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //rii = new ResolutionIndependenceInput(this, Content, _graphics, _spriteBatch);
            
            menuScreen.Load();
            //rii.LoadContent();
            // TODO: use this.Content to load your game content here
            mc.LoadContent();
            foreach (SkeletonBrain skelbros in Skellies)
            {
                skelbros.Load();
            }
            //skellyJelly.Load();
            background = Content.Load<Texture2D>("Solar-Eclipse-2014");            
        }

        protected override void Update(GameTime gameTime)
        {           
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here  
            inputter.update(Keyboard.GetState());
            if (menuScreen.isinMenu || menuScreen.menuCall)
            {
                menuScreen.Update(this, Keyboard.GetState(), mc);
                if (menuScreen.dropItem)
                {
                    if (!menuScreen.haspickedup)
                    {
                        droppedItems.Add(menuScreen.droppedItem);
                        menuScreen.haspickedup = true;
                    }
                }
            }
            else
            {
                //quadTree.clear();
                //for (int i = 0; i < allObjects.Count; i++)
                //{
                //    quadTree.insert(allObjects[i]);
                //}
                //quadtreeUpdate();
                //rii.Update(gameTime);
                mc.Update(gameTime, Keyboard.GetState(), inputter);

                //CharacterStats tempholder;

                //tempholder = circle;
                foreach (CharacterStats player in allPlayers)
                {
                    foreach (CharacterStats ball in allNPCs)
                    {
                        interaction.CollisionCourse(player, ball);
                        interaction.LineofSightDetection((MC)player, (SkeletonBrain)ball);
                        foreach (CharacterStats circle in allNPCs)
                        {
                            if (circle != ball)
                                interaction.CollisionCourse(circle, ball);
                        }
                        //foreach(Consumables thrownItem in allThrownItems)
                        //{
                        //    interaction.ThrownItemCollision(thrownItem, ball);
                        //}
                        foreach (Consumables thrownItem in mc.inventory.Find(x => x.item.ID == 1101).thrownItems)
                        {
                            interaction.ThrownItemCollision(thrownItem, ball);
                        }
                    }
                }
                
                                                   
                interaction.actionUpdate(mc);
                foreach (SkeletonBrain skelbros in Skellies)
                {
                    skelbros.Update(gameTime);
                }
                //skellyJelly.Update(gameTime);
                menuScreen.updateOutofMenu(this, Keyboard.GetState(), mc);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Clear the screen
            GraphicsDevice.Clear(Color.Pink);
            //rii.Draw(gameTime);
            _spriteBatch.Begin();            
            if (!menuScreen.isinMenu)
            {
                _spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
                if (menuScreen.dropItem)
                {
                    if(droppedItems.Count > 1)
                    {
                        for(int i = 0; i < droppedItems.Count; i++)
                        {
                            _spriteBatch.Draw(droppedItems[i].item.itemMainGameImage, new Vector2(droppedItems[i].location.X, droppedItems[i].location.Y), Color.White);
                        }
                    }else if (droppedItems.Count == 1)
                        _spriteBatch.Draw(droppedItems[0].item.itemMainGameImage, new Vector2(droppedItems[0].location.X, droppedItems[0].location.Y), Color.White);
                }
                mc.Draw(_spriteBatch);
                foreach (SkeletonBrain skelbros in Skellies)
                {
                    skelbros.Draw(_spriteBatch);
                }
                //skellyJelly.Draw(_spriteBatch);
            }

                menuScreen.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void quadtreeUpdate()
        {
            List<Object> returnObjects = new List<Object>();
            for (int i = 0; i < allObjects.Count; i++)
            {
                returnObjects.Clear();
                quadTree.retrieve(returnObjects, allObjects[i]);

                for (int x = 0; x < returnObjects.Count; x++)
                {
                    // Run collision detection algorithm between objects
                    
                }
            }


                    // Run collision detection algorithm between objects
                    //interaction.CollisionCourse(mc,skellyJelly);
                
            
        }
    }
}