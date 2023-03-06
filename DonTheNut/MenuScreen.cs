using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DonTheNut
{
    class MenuScreen
    {
        public GameState currentGameState;
        private GameState oldGameState;
        Inputter inputter;
        ItemSlot displayedItem;
        List<ItemSlot> inventory;
        List<ItemSlot> inventoryDisplaySelection;
        MC player;
        private int inventorySortPlacement = 0;
        private int inventoryArrowPlacement = 0;
        private int itemAmountSelection = 0;
        private int intToTrackTimeByUpdate;
        public GraphicsDeviceManager graphics;
        private SpriteFont font;
        private Texture2D titleScreen;
        private Texture2D settingScreen;
        private Texture2D menuScreen;
        private Texture2D inventoryScreen;
        private Texture2D equipmentScreen;
        private Texture2D equipmentSlot;
        private Texture2D gearSelectionScreen;
        private Texture2D itemDescriptionScreen;
        private Texture2D popupSelectionScreen;
        private Texture2D messageBox;
        private Texture2D remapInputScreen;
        private Texture2D titleScreenArrow;
        private AnimatedSprite titleScreenArrowSprite;
        //player HUD
        Vector2 healthBarOuterPosition = new Vector2(0, 0);
        Vector2 healthBarPosition = new Vector2(8, 0);
        Vector2 staminaBarOuterPosition = new Vector2(0, 10);
        Vector2 staminaBarPosition = new Vector2(8, 10);
        private Texture2D itemToolBarTexture;
        Vector2 itemToolBarPosition = new Vector2(20 , 30);
        Vector2 itemToolBarOffset = new Vector2(2, 2);

        private Vector2 positionArrow;
        private Vector2 positionNewGame;
        private Vector2 positionContinue;
        private Vector2 positionSettings;
        private Vector2 positionExit;
        private Vector2 positionFSYes;
        private Vector2 positionFSNo;
        private Vector2 positionSettingsBack;
        private Vector2 positionInventoryMenu;
        private Vector2 positionEquipmentMenu;
        private Vector2 positionSettingsMenu;
        private Vector2 positionExitMenu;
        private Vector2 positionConsumablesInventory;
        private Vector2 positionEquipmentInventory;
        private Vector2 positionMaterialsInventory;
        private Vector2 positionKeyItemsInventory;
        List<Vector2> inventorySlotPositions;
        Vector2[] settingsTopSlotPositions;
        Vector2[,] settingsLeftSlotPositions;
        Vector2[,] settingsRightSlotPositions;
        Vector2[,] remapMenuSlotPositions;
        //popup vars
        private Vector2 popupPosition;
        Vector2[] popupPositionSlots;
        Vector2[] popupCenterSlots;
        bool[] popupSlotBools = new bool[4];
        int popupArrowPlacement = 0;
        Vector2 popupScreenBottomOffset;
        Vector2 popupCenterScreen;
        Vector2 messageBoxPosition;
        private Vector2 popupArrowOffset;
        //
        private Vector2 arrowOffset;
        private Vector3 itemDropLocation;
        //titlemenubools
        private bool selectNewGame = true;
        private bool selectContinue = false;
        private bool selectOptions = false;
        private bool selectExit = false;
        //settingsbools
        private bool selectGeneral = false;
        private bool selectDisplay = false;
        private bool selectAudio = false;
        private bool selectControls = false;
        private bool settingsMenuLeft = false;
        private bool settingMenuRight = false;
        private bool selectFullScreen = false;
        private bool selectBorderless = false;
        private bool selectWindowed = false;
        private bool selectFScreen = false;
        private bool selectBack = false;
        private bool selectRemap = false;
        //remapbools
        private bool selectActionConfirm = false;
        private bool selectActionCancel = false;
        private bool selectActionAttack = false;
        private bool selectActionBlock = false;
        private bool selectActionRun = false;
        private bool selectActionDodge = false;
        private bool selectActionUseItem = false;
        private bool selectActionInteract = false;
        private bool selectActionOpenMenu = false;
        private bool selectActionInventory = false;
        private bool selectActionEquipment = false;
        private bool selectActionMoveUp = false;
        private bool selectActionMoveDown = false;
        private bool selectActionMoveLeft = false;
        private bool selectActionMoveRight = false;
        bool[] remapBoolList = new bool[16];
        int remapBoolListTrackerX = 0;
        int remapBoolListTrackerY = 0;
        bool[] remapBoolList2 = new bool[4];
        private bool remapConfirmPopup = false;
        private bool remapDupeKey = false;
        Actions actions;
        Actions dupeActions;
        //equipmentvars /260x 30,150y +102x 345y items
        List<Vector2> equipmentStatsSlots;
        List<string> equipmentStatsString = new List<string>();
        Vector2[,] equipmentGearSlots;
        string[,] equipmentGearString;
        Vector2[] equipmentItemSlots;
        string[] equipmentItemString;
        bool[,] equipmentGearBool= new bool[5,2];
        bool[] equipmentItemBool = new bool[5];
        int equipmentArrowPlacementX = 0;
        int equipmentArrowPlacementY = 0;
        bool gearSelectionScreenBool = false;
        List<Vector2> gearSelectionPositions;
        int gearselectiontracker = 0;
        List<ItemSlot> gearDisplayedSelections;
        Vector2 gearSelectionStringDisplay;
        Vector2 gearSelectionScreenStartPosition;
        Vector2 itemDesNameLocation;
        Vector2 itemDesPictureLocation;
        List<Vector2> itemDesStatsLocation;
        List<string> itemDetailsString;
        Items equipmentToGetDetailsFrom;
        Vector2 itemDesScreenOffset;
        bool itemDesBool = false;
        //mainmenubools
        private bool selectInventory = false;
        private bool selectEquipment = false;
        private bool selectYes = false;
        private bool selectNo = false;
        private bool selectConsumables = false;
        private bool selectMaterials = false;
        private bool selectKeyItems = false;
        private bool selectInventorySpace = false;
        private bool selectUse = false;
        private bool selectDrop = false;
        private bool selectDropMenu = false;
        private bool selectDiscard = false;
        private bool selectDiscardMenu = false;
        private bool selectClose = false;
        private bool selectNumber = false;
        private bool selectConfirm = false;
        private bool selectCancel = false;
        
        
        public bool dropItem = false;
        public bool haspickedup = false;
        public ItemSlot droppedItem;
        public bool displayMessageBox = false;

        public bool isinMenu = true;
        //used to call the menu from another class
        public bool menuCall = false;
        //used to stop the screen from activating the selected option when Z/enter is pressed and changing screens
        private bool screenTransition = false;
        private bool screenTransition2 = false;
        //used to make a small pop up confirming selection in menus
        private bool popupSelectionMenu = false;
        private bool popupSelectionMenu2 = false;

        ContentManager Content;
        private KeyboardState oldState;

        public MenuScreen(ContentManager content, GraphicsDeviceManager _graphics, Inputter inputer)
        {
            Content = content;
            graphics = _graphics;
            inputter = inputer;
            currentGameState = GameState.TitleScreen;            
            positionNewGame = new Vector2(190, 170);
            positionContinue = new Vector2(190, 255);
            positionSettings = new Vector2(190, 340);
            positionExit = new Vector2(190, 415);
            positionFSYes = new Vector2(425, 250);
            positionFSNo = new Vector2(600, 250);
            positionSettingsBack = new Vector2(230, 400);
            positionInventoryMenu = new Vector2(25, 10);
            positionEquipmentMenu = new Vector2(25, 60);
            positionSettingsMenu = new Vector2(25, 360);
            positionExitMenu = new Vector2(25, 410);
            positionConsumablesInventory = new Vector2(80, 30);
            positionEquipmentInventory = new Vector2(240, 30);
            positionMaterialsInventory = new Vector2(400, 30);
            positionKeyItemsInventory = new Vector2(560, 30);
            //generates positions for the inventory screen
            inventoryDisplaySelection = new List<ItemSlot>();
            inventorySlotPositions = new List<Vector2>();
            int[] xarray = new int[3];
            xarray[0] = 80; xarray[1] = 350; xarray[2] = 620;
            for (int y = 160; y <= 440; y += 40)
            {
                for (int x = 0; x <= 2; x++)
                {
                    inventorySlotPositions.Add(new Vector2(xarray[x], y));
                }
            }
            //generates positions for the settings screen
            settingsTopSlotPositions = new Vector2[5]; // 90,20
            int xp = 0;
            for (int x = 90; x <= 730; x += 160)
            {
                settingsTopSlotPositions[xp] = new Vector2(x, 20);
                xp++;
            }
            settingsLeftSlotPositions = new Vector2[12, 4]; // { { new Vector2(70, 140), new Vector2(210, 140) } }
            xp = 0;
            int yp = 0;
            for (int y = 140; y <= 420; y+= 40)
            {
                for(int x = 70; x <= 490; x += 140)
                {
                    settingsLeftSlotPositions[yp, xp] = new Vector2(x, y);
                    xp++;
                }
                xp = 0;
                yp++;
            }

            //generates positions for remap menu
            remapMenuSlotPositions = new Vector2[4,16];
            xp = 0;
            yp = 0;
            for (int y = 30; y < 670; y += 40)
            {
                for(int x = 100; x <= 520; x += 140)
                {
                    remapMenuSlotPositions[xp, yp] = new Vector2(x,y);
                    xp++;
                }
                xp = 0;
                yp++;
            }            

            //           
            arrowOffset = new Vector2(60, 20);
            popupArrowOffset = new Vector2(-50, -20);
            popupScreenBottomOffset = new Vector2(0, -187);            
            popupPosition = new Vector2(0, 0);
            popupPositionSlots = new Vector2[4];
            popupCenterSlots = new Vector2[4];
            messageBoxPosition = new Vector2( 10, 200);
            positionArrow = positionNewGame;            
        }
        private void equipmentMenuLoad()
        {
            int xp;
            int yp;
            //generates slots for equipment /260x 30,150y +102x 345y items
            gearDisplayedSelections = new List<ItemSlot>();
            equipmentStatsSlots = new List<Vector2>();
            for (int y = 30; y < 500; y += 20)
            {
                equipmentStatsSlots.Add(new Vector2(30, y));
            }
            equipmentGearSlots = new Vector2[5, 2];
            equipmentItemSlots = new Vector2[5];
            xp = 0;
            yp = 0;
            for (int y = 30; y < 270; y += 120)
            {
                for (int x = 260; x < 770; x += 102)
                {
                    equipmentGearSlots[xp, yp] = new Vector2(x, y);
                    if (equipmentItemSlots.Length <= 5)
                        equipmentItemSlots[xp] = new Vector2(x, 345);
                    xp++;
                }
                xp = 0;
                yp++;
            }
            gearSelectionPositions = new List<Vector2>();
            for (int y = 60; y <= 300; y += 120)
            {
                for (int x = 25; x < 535; x += 102)
                {
                    gearSelectionPositions.Add(new Vector2(x, y));
                }
            }
            gearSelectionStringDisplay = gearSelectionScreenStartPosition + new Vector2(75, 25);
            equipmentGearString = new string[5, 2] { { "Main Hand 1", "Off Hand 1" }, { "Main Hand 2", "Off Hand 2" }, { "Hat", "Gloves" }, { "Body", "Shoes" }, { "Necklace", "Ring" } };
            equipmentItemString = new string[5] { "Item slot 1", "Item slot 2", "Item slot 3", "Arrow slot 1", "Arrow slot 2" };
            itemDesScreenOffset = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - itemDescriptionScreen.Width / 2, 0);
            itemDesNameLocation = new Vector2(40, 25) + itemDesScreenOffset;
            itemDesPictureLocation = new Vector2(60, 60) + itemDesScreenOffset;
            itemDesStatsLocation = new List<Vector2>(); //30, 220            
            for (int x = 30; x < 300; x += 157)
            {
                for (int y = 220; y < 460; y += 30)
                {
                    itemDesStatsLocation.Add(new Vector2(x, y) + itemDesScreenOffset);
                }
            }
        }
        public void getPlayer(MC _player)
        {
            player = _player;
            inventory = _player.inventory;
            updateEquipmentScreenStatStrings();
        }
        private void updateEquipmentScreenStatStrings()
        {
            equipmentStatsString.Clear();
            equipmentStatsString.Add(player.name);
            equipmentStatsString.Add("Level " + player.level);
            equipmentStatsString.Add("Health: " + player.health + "/" + player.maxHealth);
            equipmentStatsString.Add("Mana: " + player.mana);
            equipmentStatsString.Add("Stamina: " + player.stamina + "/" + player.maxStamina);
            equipmentStatsString.Add("Main 1 Phys Atk: " + player.pAttackMain1);
            equipmentStatsString.Add("Main 1 Magic Atk: " + player.mAttackMain1);
            equipmentStatsString.Add("Main 2 Phys Atk: " + player.pAttackMain2);
            equipmentStatsString.Add("Main 2 Magic Atk: " + player.mAttackMain2);
            equipmentStatsString.Add("Off 1 Phys Atk: " + player.pAttackOff1);
            equipmentStatsString.Add("Off 1 Magic Atk: " + player.mAttackOff1);
            equipmentStatsString.Add("Off 1 Phys Atk: " + player.pAttackOff2);
            equipmentStatsString.Add("Off 2 Magic Atk: " + player.mAttackOff2);
            equipmentStatsString.Add("Physical Defence: " + player.pDefense);
            equipmentStatsString.Add("Magic Defence: " + player.mDefense);
            equipmentStatsString.Add("Fire Defence: " + player.fireRes);
            equipmentStatsString.Add("Ice Defence: " + player.iceRes);
            equipmentStatsString.Add("Lightning Def: " + player.lightningRes);
            equipmentStatsString.Add("Weight: " + player.weight + "/" + player.maxWeight + "(" + player.weightPercent + "%" + ")");
        }
        public void WriteItemDetails(Items item)
        {
            itemDetailsString = new List<string>();
            itemDetailsString.Add(item.itemType.ToString());
            itemDetailsString.Add(item.itemSubType.ToString());
            itemDetailsString.Add("");
            if(item.itemType == ItemType.Equipment)
            {
                Equipment equipment = (Equipment)item;
                itemDetailsString.Add("Physical Attack: " + equipment.pAttack.ToString());
                itemDetailsString.Add("Magical Attack: " + equipment.mAttack.ToString());
                itemDetailsString.Add("Physical Defense: " + equipment.pDefense.ToString());
                itemDetailsString.Add("Magical Defense: " + equipment.mDefense.ToString());
                itemDetailsString.Add("");
                itemDetailsString.Add("");
                itemDetailsString.Add("");
                itemDetailsString.Add("");
                itemDetailsString.Add("Fire Defense: " + equipment.fireRes.ToString());
                itemDetailsString.Add("Ice Defense: " + equipment.iceRes.ToString());
                itemDetailsString.Add("Lightning Defense: " + equipment.lightningRes.ToString());
                itemDetailsString.Add("Weight: " + equipment.weight.ToString());
            }
            if(item.itemType == ItemType.Consumable)
            {
                Consumables consumable = (Consumables)item;
                itemDetailsString.Add(consumable.shortDescription);
                itemDetailsString.Add(consumable.longDescription);
            }
        }
        public void Load()
        {
            font = Content.Load<SpriteFont>("font");
            titleScreen = Content.Load<Texture2D>("UI/TitleScreen");
            settingScreen = Content.Load<Texture2D>("UI/SettingsScreen");
            menuScreen = Content.Load<Texture2D>("UI/MenuScreen");
            inventoryScreen = Content.Load<Texture2D>("UI/InventoryScreen");
            equipmentScreen = Content.Load<Texture2D>("UI/EquipmentScreen");
            equipmentSlot = Content.Load<Texture2D>("UI/EquipmentSlot");
            gearSelectionScreen = Content.Load<Texture2D>("UI/GearSelectionScreen");
            itemDescriptionScreen = Content.Load<Texture2D>("UI/ItemDescriptionScreen");
            popupSelectionScreen = Content.Load<Texture2D>("UI/PopupSelectionScreen");
            remapInputScreen = Content.Load<Texture2D>("UI/RemapInputScreen");
            messageBox = Content.Load<Texture2D>("UI/MessageScreen");
            titleScreenArrow = Content.Load<Texture2D>("UI/TitleScreenArroww");
            titleScreenArrowSprite = new AnimatedSprite(titleScreenArrow, 1, 3)
            {
                updatesPerFrame = 12
            };
            itemToolBarTexture = Content.Load<Texture2D>("UI/itemToolBarSlot");
            popupCenterScreen = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - popupSelectionScreen.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2 - popupSelectionScreen.Height / 2);
            popupCenterSlots[0] = popupCenterScreen + new Vector2(40, 30);
            popupCenterSlots[1] = popupCenterScreen + new Vector2(70, 70);
            popupCenterSlots[2] = popupCenterScreen + new Vector2(60, 110);
            popupCenterSlots[3] = popupCenterScreen + new Vector2(60, 150);
            gearSelectionScreenStartPosition = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - gearSelectionScreen.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2 - gearSelectionScreen.Height / 2);
            equipmentMenuLoad();
        }
        public void moveArrow(Vector2 newPosition, ref bool settotrue, ref bool settofalse)
        {
            positionArrow = newPosition;
            settofalse = false;
            settotrue = true;
        }
        public void moveArrow(Vector2 newPosition)
        {
            positionArrow = newPosition;
        }
        public void MenuCall()
        {
            selectInventory = true;
            isinMenu = true;
            currentGameState = GameState.MenuScreen;
            positionArrow = positionInventoryMenu;
        }
        public void PopupCall(ref bool settotrue, ref bool settofalse)
        {
            popupPosition = positionArrow;
            if (popupPosition.Y + popupSelectionScreen.Height > graphics.GraphicsDevice.Viewport.Height)
                popupPosition = positionArrow + popupScreenBottomOffset;

            popupSelectionMenu = true;
            popupArrowPlacement = 0;
            popupPositionSlots[0] = new Vector2(popupPosition.X + 60, popupPosition.Y + 30);// -50 -127
            popupPositionSlots[1] = new Vector2(popupPosition.X + 60, popupPosition.Y + 70);
            popupPositionSlots[2] = new Vector2(popupPosition.X + 60, popupPosition.Y + 110);
            popupPositionSlots[3] = new Vector2(popupPosition.X + 60, popupPosition.Y + 150);

            if (currentGameState == GameState.MenuScreen)
            {
                if (popupPosition.Y + popupSelectionScreen.Height < graphics.GraphicsDevice.Viewport.Height)
                    moveArrow(popupPositionSlots[1] + popupArrowOffset, ref settotrue, ref settofalse);
                else
                    moveArrow(popupPositionSlots[1] + popupArrowOffset + popupScreenBottomOffset, ref settotrue, ref settofalse);
            }
            else if (currentGameState == GameState.Inventory || currentGameState == GameState.Equipment)
            {

                if (popupPosition.Y + popupSelectionScreen.Height < graphics.GraphicsDevice.Viewport.Height)
                    moveArrow(new Vector2(popupPosition.X + 10, popupPosition.Y + 10), ref settotrue, ref settofalse);
                else
                    moveArrow(new Vector2(popupPosition.X + 10, popupPosition.Y - 107), ref settotrue, ref settofalse);
            }
        }
        public void messageCall(ItemSlot item)
        {
            displayedItem = item;
        }
        public void Update(Game game, KeyboardState keyState, MC player)
        {
            titleScreenArrowSprite.Update();
            if (currentGameState == GameState.TitleScreen)
            {
                oldGameState = currentGameState;
                if (inputter.isActioninputtedbyType(Actions.MoveDown, InputType.Press))
                {
                    if (selectNewGame)
                        moveArrow(positionContinue, ref selectContinue, ref selectNewGame);
                    else if (selectContinue)
                        moveArrow(positionSettings, ref selectOptions, ref selectContinue);
                    else if (selectOptions)
                        moveArrow(positionExit, ref selectExit, ref selectOptions);
                }
                if (inputter.isActioninputtedbyType(Actions.MoveUp, InputType.Press))
                {
                    if (selectContinue)
                        moveArrow(positionNewGame, ref selectNewGame, ref selectContinue);
                    else if (selectOptions)
                        moveArrow(positionContinue, ref selectContinue, ref selectOptions);
                    else if (selectExit)
                        moveArrow(positionSettings, ref selectOptions, ref selectExit);
                }
                if (inputter.isActioninputtedbyType(Actions.Confirm, InputType.Release))
                {
                    if (selectNewGame)
                    {
                        isinMenu = false;
                        menuCall = false;
                        currentGameState = GameState.GameRunning;
                    }
                    else if (selectOptions)
                    {
                        currentGameState = GameState.SettingsScreen;//425 250
                        screenTransition = true;
                        moveArrow(settingsTopSlotPositions[0] - arrowOffset, ref selectGeneral, ref selectOptions);
                    }
                    if (selectExit)
                        game.Exit();
                }
            }
            if (currentGameState == GameState.SettingsScreen)
            {
                if (inputter.isActioninputtedbyType(Actions.MoveRight, InputType.Press))
                {
                    if (!settingsMenuLeft && !settingMenuRight)
                    {
                        if (selectGeneral)
                            moveArrow(settingsTopSlotPositions[1] - arrowOffset, ref selectDisplay, ref selectGeneral);
                        else if (selectDisplay)
                            moveArrow(settingsTopSlotPositions[2] - arrowOffset, ref selectAudio, ref selectDisplay);
                        else if (selectAudio)
                            moveArrow(settingsTopSlotPositions[3] - arrowOffset, ref selectControls, ref selectAudio);
                        else if (selectControls)
                            moveArrow(settingsTopSlotPositions[4] - arrowOffset, ref selectBack, ref selectControls);                        
                    }else if (settingMenuRight)
                    {
                        if (selectWindowed)
                            moveArrow(settingsLeftSlotPositions[0, 2] - arrowOffset, ref selectBorderless, ref selectWindowed);
                        else if (selectBorderless)
                            moveArrow(settingsLeftSlotPositions[0, 3] - arrowOffset, ref selectFScreen, ref selectBorderless);
                    }
                }
                if (inputter.isActioninputtedbyType(Actions.MoveLeft, InputType.Press))
                {
                    if (!settingsMenuLeft && !settingMenuRight)
                    {
                        if (selectBack)
                            moveArrow(settingsTopSlotPositions[3] - arrowOffset, ref selectControls, ref selectBack);
                        else if (selectControls)
                            moveArrow(settingsTopSlotPositions[2] - arrowOffset, ref selectAudio, ref selectControls);
                        else if (selectAudio)
                            moveArrow(settingsTopSlotPositions[1] - arrowOffset, ref selectDisplay, ref selectAudio);
                        else if (selectDisplay)
                            moveArrow(settingsTopSlotPositions[0] - arrowOffset, ref selectGeneral, ref selectDisplay);
                    }else if (settingMenuRight)
                    {
                        if (selectFScreen)
                            moveArrow(settingsLeftSlotPositions[0, 2] - arrowOffset, ref selectBorderless, ref selectFScreen);
                        else if (selectBorderless)
                            moveArrow(settingsLeftSlotPositions[0, 1] - arrowOffset, ref selectWindowed, ref selectBorderless);                        
                    }
                }
                if (inputter.isActioninputtedbyType(Actions.MoveDown, InputType.Press))
                {
                   
                }
                if (inputter.isActioninputtedbyType(Actions.MoveUp, InputType.Press))
                {

                }
                if (inputter.isActioninputtedbyType(Actions.Cancel, InputType.Release))
                {
                    if (settingMenuRight)
                    {
                        settingMenuRight = false;
                        settingsMenuLeft = true;
                        if (selectDisplay)
                        {
                            moveArrow(settingsLeftSlotPositions[0, 0] - arrowOffset, ref selectFullScreen, ref selectWindowed);
                            selectFullScreen = false;
                            selectBorderless = false;
                        }                       
                    }
                    else if (settingsMenuLeft)
                    {
                        settingsMenuLeft = false;
                        if (selectDisplay)
                        {
                            moveArrow(settingsTopSlotPositions[1] - arrowOffset, ref selectDisplay, ref selectFullScreen);
                        }
                        if (selectControls)
                        {
                            moveArrow(settingsTopSlotPositions[3] - arrowOffset, ref selectControls, ref selectRemap);
                        }
                    }
                    else
                    {
                        currentGameState = oldGameState;
                        if (oldGameState == GameState.TitleScreen)
                            moveArrow(positionNewGame, ref selectNewGame, ref selectBack);
                        else if (oldGameState == GameState.MenuScreen)
                            moveArrow(positionSettingsMenu, ref selectOptions, ref selectBack);
                    }
                }
                if ((inputter.isActioninputtedbyType(Actions.Confirm, InputType.Release)) && !screenTransition)
                {
                    //screenTransition = true;
                    if (selectDisplay && !settingsMenuLeft && !settingMenuRight)
                    {
                        moveArrow(settingsLeftSlotPositions[0, 0] - arrowOffset, ref selectFullScreen, ref selectNo);
                        settingsMenuLeft = true;
                    }
                    else if (selectControls && !settingsMenuLeft && !settingMenuRight)
                    {
                        moveArrow(settingsLeftSlotPositions[0, 0] - arrowOffset, ref selectRemap, ref selectNo);
                        settingsMenuLeft = true;
                    }
                    else if (selectFullScreen && !settingMenuRight)
                    {
                        moveArrow(settingsLeftSlotPositions[0, 1] - arrowOffset, ref selectWindowed, ref selectFullScreen);
                        settingMenuRight = true;
                        settingsMenuLeft = false;
                    }
                    else if (selectWindowed)
                    {
                        graphics.HardwareModeSwitch = true;
                        graphics.IsFullScreen = false;
                        graphics.ApplyChanges();
                    }
                    else if (selectFScreen)
                    {
                        graphics.HardwareModeSwitch = true;
                        graphics.IsFullScreen = true;
                        graphics.ApplyChanges();
                    }
                    else if (selectBorderless)
                    {
                        graphics.HardwareModeSwitch = false;
                        graphics.IsFullScreen = true;
                        graphics.ApplyChanges();
                        //graphics.ToggleFullScreen();
                    }
                    else if (selectRemap)
                    {
                        currentGameState = GameState.RemapInput;//40 30
                        screenTransition = true;
                        settingsMenuLeft = false;
                        //moveArrow(remapMenuSlotPositions[0, 1] - arrowOffset, ref selectActionConfirm, ref selectRemap);
                        moveArrow(remapMenuSlotPositions[0, 1] - arrowOffset, ref remapBoolList[0], ref selectRemap);
                        remapBoolListTrackerX = 0;
                        remapBoolListTrackerY = 0;
                    }
                    else if (selectBack)
                    {
                        currentGameState = oldGameState;
                        if (oldGameState == GameState.TitleScreen)
                            moveArrow(positionNewGame, ref selectNewGame, ref selectBack);
                        else if (oldGameState == GameState.MenuScreen)
                            moveArrow(positionSettingsMenu, ref selectOptions, ref selectBack);
                    }
                }
            }
            if (currentGameState == GameState.RemapInput)
            {
                if (!remapDupeKey)
                {
                    if ((inputter.isActioninputtedbyType(Actions.MoveDown, InputType.Press)) && remapBoolListTrackerY < remapBoolList.Length - 2 && !remapConfirmPopup)
                    {
                        if (remapBoolList[remapBoolListTrackerY])
                        {
                            moveArrow(remapMenuSlotPositions[remapBoolListTrackerX, remapBoolListTrackerY + 2] - arrowOffset, ref remapBoolList[remapBoolListTrackerY + 1], ref remapBoolList[remapBoolListTrackerY]);
                            remapBoolListTrackerY++;
                        }
                        //if (selectActionConfirm)
                        //    moveArrow(remapMenuSlotPositions[0, 2] - arrowOffset, ref selectActionCancel, ref selectActionConfirm);
                        //if(selectActionCancel)
                        //    moveArrow(remapMenuSlotPositions[0, 3] - arrowOffset, ref selectActionAttack, ref selectActionCancel);
                        //if(selectActionAttack)
                        //    moveArrow(remapMenuSlotPositions[0, 4] - arrowOffset, ref selectActionBlock, ref selectActionAttack);
                        //if(selectActionBlock)
                        //    moveArrow(remapMenuSlotPositions[0, 5] - arrowOffset, ref selectActionCancel, ref selectActionConfirm);
                    }
                    if ((inputter.isActioninputtedbyType(Actions.MoveUp, InputType.Press)))
                    {
                        if (remapBoolListTrackerY > 0 && !remapConfirmPopup)
                        {
                            moveArrow(remapMenuSlotPositions[remapBoolListTrackerX, remapBoolListTrackerY] - arrowOffset, ref remapBoolList[remapBoolListTrackerY - 1], ref remapBoolList[remapBoolListTrackerY]);
                            remapBoolListTrackerY--;
                        }
                    }
                    if ((inputter.isActioninputtedbyType(Actions.MoveRight, InputType.Press)) && remapBoolListTrackerX < 3 && !remapConfirmPopup)
                    {
                        moveArrow(remapMenuSlotPositions[remapBoolListTrackerX + 1, remapBoolListTrackerY + 1] - arrowOffset, ref remapBoolList2[remapBoolListTrackerX + 1], ref remapBoolList2[remapBoolListTrackerX]);
                        remapBoolListTrackerX++;
                    }
                    if ((inputter.isActioninputtedbyType(Actions.MoveLeft, InputType.Press)) && remapBoolListTrackerX > 0 && !remapConfirmPopup)
                    {
                        moveArrow(remapMenuSlotPositions[remapBoolListTrackerX - 1, remapBoolListTrackerY + 1] - arrowOffset, ref remapBoolList2[remapBoolListTrackerX - 1], ref remapBoolList2[remapBoolListTrackerX]);
                        remapBoolListTrackerX--;
                    }
                }
                if (inputter.isActioninputtedbyType(Actions.Cancel, InputType.Release))
                {
                    if (!remapConfirmPopup && !remapDupeKey)
                    {
                        currentGameState = GameState.SettingsScreen;//425 250
                        //screenTransition = true;
                        moveArrow(settingsTopSlotPositions[0] - arrowOffset, ref selectGeneral, ref remapBoolList[remapBoolListTrackerY]);
                    }else if (remapDupeKey)
                    {
                        remapDupeKey = false;
                    }
                }
                if (inputter.isActioninputtedbyType(Actions.Confirm, InputType.Release) && !screenTransition2)
                {                    
                    if(remapBoolList2[1] && !remapDupeKey) // the 1 is for keyboard buttons
                    {
                        actions = (Actions)remapBoolListTrackerY;
                        remapConfirmPopup = true;
                    }
                    else if (remapDupeKey)
                    {
                        remapDupeKey = false;
                    }
                    
                }
                if (inputter.isAnyButtonInputTyped(InputType.Release) && !remapConfirmPopup)
                {
                    screenTransition2 = false;
                }
                if (remapConfirmPopup && keyState.GetPressedKeyCount() != 0 && !screenTransition)
                {
                    Keys inputKey = keyState.GetPressedKeys()[0];
                    dupeActions = inputter.doesKeyExistinControls(inputKey, actions);
                    if (keyState.IsKeyDown(inputKey) && oldState.IsKeyDown(inputKey))
                    {
                        screenTransition = true;                        
                        if (dupeActions == actions)
                        {
                            inputter.remap(inputKey, actions);
                        }
                        else
                        {
                            remapDupeKey = true;
                        }
                        
                        remapConfirmPopup = false;
                        selectActionConfirm = true;
                    }
                    screenTransition2 = true;
                }                              
            }
            if (currentGameState == GameState.MenuScreen)
            {
                oldGameState = currentGameState;
                if (inputter.isActioninputtedbyType(Actions.MoveDown, InputType.Press))
                {
                    if (selectInventory)
                        moveArrow(positionEquipmentMenu, ref selectEquipment, ref selectInventory);
                    else if (selectEquipment)
                        moveArrow(positionSettingsMenu, ref selectOptions, ref selectEquipment);
                    else if (selectOptions)
                        moveArrow(positionExitMenu, ref selectExit, ref selectOptions);
                    if (popupSelectionMenu && selectYes)
                        moveArrow(popupPositionSlots[2] + popupArrowOffset, ref selectNo, ref selectYes);
                    //moveArrow(new Vector2(popupPosition.X + 10, popupPosition.Y - 97), ref selectNo, ref selectYes);

                }
                if (inputter.isActioninputtedbyType(Actions.MoveUp, InputType.Press))
                {
                    if (selectExit)
                        moveArrow(positionSettingsMenu, ref selectOptions, ref selectExit);
                    else if (selectEquipment)
                        moveArrow(positionInventoryMenu, ref selectInventory, ref selectEquipment);
                    else if (selectOptions)
                        moveArrow(positionEquipmentMenu, ref selectEquipment, ref selectOptions);
                    if (popupSelectionMenu && selectNo)
                        moveArrow(popupPositionSlots[1] + popupArrowOffset, ref selectYes, ref selectNo);                    
                }
                if (inputter.isActioninputtedbyType(Actions.Cancel, InputType.Press) && !menuCall)
                {
                    isinMenu = false;
                    currentGameState = GameState.GameRunning;
                }
                if (inputter.isActioninputtedbyType(Actions.Confirm, InputType.Release) && !screenTransition)
                {
                    screenTransition = true;
                    if (selectInventory)
                    {
                        currentGameState = GameState.Inventory;
                        moveArrow(positionConsumablesInventory - arrowOffset, ref selectConsumables, ref selectInventory);
                    }
                    else if (selectEquipment)
                    {
                        currentGameState = GameState.Equipment;
                        gearselectiontracker = 0;
                        equipmentArrowPlacementX = 0;
                        equipmentArrowPlacementY = 0;
                        moveArrow(equipmentGearSlots[0, 0] - arrowOffset, ref equipmentGearBool[0, 0], ref selectEquipment);
                    }
                    else if (selectOptions)
                    {
                        currentGameState = GameState.SettingsScreen;//425 250                        
                        moveArrow(settingsTopSlotPositions[0] - arrowOffset, ref selectGeneral, ref selectOptions);
                    }
                    else if (selectExit)
                    {
                        PopupCall(ref selectNo, ref selectExit);
                    }
                    else if (selectYes)
                    {
                        currentGameState = GameState.TitleScreen;
                        moveArrow(positionNewGame, ref selectNewGame, ref selectYes);
                    }
                    else if (selectNo)
                    {
                        popupSelectionMenu = false;
                        moveArrow(positionExitMenu, ref selectExit, ref selectNo);
                    }
                }
            }
            if (currentGameState == GameState.Inventory)
            {
                if (inputter.isActioninputtedbyType(Actions.MoveRight, InputType.Press))
                {
                    if (selectConsumables && !selectInventorySpace && !popupSelectionMenu)
                        moveArrow(positionEquipmentInventory - arrowOffset, ref selectEquipment, ref selectConsumables);
                    else if (selectEquipment && !selectInventorySpace && !popupSelectionMenu)
                        moveArrow(positionMaterialsInventory - arrowOffset, ref selectMaterials, ref selectEquipment);
                    else if (selectMaterials && !selectInventorySpace && !popupSelectionMenu)
                        moveArrow(positionKeyItemsInventory - arrowOffset, ref selectKeyItems, ref selectMaterials);
                    else if (selectInventorySpace)
                    {
                        if(inventoryArrowPlacement < inventorySlotPositions.Count - 1 && inventoryArrowPlacement < inventoryDisplaySelection.Count - 1)
                            moveArrow(inventorySlotPositions[inventoryArrowPlacement += 1] - arrowOffset);
                    }
                    if (popupSelectionMenu2)
                    {
                        if (itemAmountSelection < inventoryDisplaySelection[inventoryArrowPlacement].quantity)
                            itemAmountSelection++;
                    }
                }
                else if (inputter.isActioninputtedbyType(Actions.MoveLeft, InputType.Press))
                {
                    if (selectKeyItems && !selectInventorySpace && !popupSelectionMenu)
                        moveArrow(positionMaterialsInventory - arrowOffset, ref selectMaterials, ref selectKeyItems);
                    else if (selectEquipment && !selectInventorySpace && !popupSelectionMenu)
                        moveArrow(positionConsumablesInventory - arrowOffset, ref selectConsumables, ref selectEquipment);
                    else if (selectMaterials && !selectInventorySpace && !popupSelectionMenu)
                        moveArrow(positionEquipmentInventory - arrowOffset, ref selectEquipment, ref selectMaterials);
                    else if (selectInventorySpace)
                    {
                        if (inventoryArrowPlacement > 0 )
                            moveArrow(inventorySlotPositions[inventoryArrowPlacement -= 1] - arrowOffset);
                    }
                    if (popupSelectionMenu2)
                    {
                        if (itemAmountSelection > 0)
                            itemAmountSelection--;
                    }
                }
                if (inputter.isActioninputtedbyType(Actions.MoveUp, InputType.Press))
                {
                    if (selectInventorySpace)
                    {
                        if (inventoryArrowPlacement > 2)
                            moveArrow(inventorySlotPositions[inventoryArrowPlacement -= 3] - arrowOffset);
                        else
                        {
                            inventoryArrowPlacement = 0;
                            moveArrow(inventorySlotPositions[inventoryArrowPlacement] - arrowOffset);
                        }
                    }else if (popupSelectionMenu)
                    {
                        if (selectClose)
                            moveArrow(popupPositionSlots[2] + popupArrowOffset, ref selectDiscard, ref selectClose);
                        else if (selectDrop)
                            moveArrow(popupPositionSlots[0] + popupArrowOffset, ref selectUse, ref selectDrop);
                        else if (selectDiscard)
                            moveArrow(popupPositionSlots[1] + popupArrowOffset, ref selectDrop, ref selectDiscard);
                    }
                    if (popupSelectionMenu2)
                    {
                        if (selectCancel)
                            moveArrow(popupCenterSlots[2] + popupArrowOffset, ref selectConfirm, ref selectCancel);
                        else if (selectConfirm)
                            moveArrow(popupCenterSlots[1] + popupArrowOffset - new Vector2(10, 0), ref selectNumber, ref selectConfirm);
                    }
                }
                else if (inputter.isActioninputtedbyType(Actions.MoveDown, InputType.Press))
                {
                    if (selectInventorySpace)
                    {
                        if (inventoryArrowPlacement < inventorySlotPositions.Count - 4 && inventoryArrowPlacement < inventoryDisplaySelection.Count - 3)
                            moveArrow(inventorySlotPositions[inventoryArrowPlacement += 3] - arrowOffset);
                        else
                        {
                            inventoryArrowPlacement = inventoryDisplaySelection.Count - 1;
                            moveArrow(inventorySlotPositions[inventoryArrowPlacement] - arrowOffset);
                        }
                    }else if (popupSelectionMenu)
                    {
                        if(selectUse)
                            moveArrow(popupPositionSlots[1] + popupArrowOffset, ref selectDrop, ref selectUse);
                        else if(selectDrop)
                            moveArrow(popupPositionSlots[2] + popupArrowOffset, ref selectDiscard, ref selectDrop);
                        else if(selectDiscard)
                            moveArrow(popupPositionSlots[3] + popupArrowOffset, ref selectClose, ref selectDiscard);
                    }if (popupSelectionMenu2)
                    {
                        if(selectNumber)
                            moveArrow(popupCenterSlots[2] + popupArrowOffset, ref selectConfirm, ref selectNumber);
                        else if (selectConfirm)
                            moveArrow(popupCenterSlots[3] + popupArrowOffset, ref selectCancel, ref selectConfirm);
                    }
                }
                if (inputter.isActioninputtedbyType(Actions.Cancel, InputType.Press))
                {
                    if (selectInventorySpace && !popupSelectionMenu && !popupSelectionMenu2)
                    {
                        if (selectConsumables)
                            moveArrow(positionConsumablesInventory - arrowOffset, ref selectConsumables, ref selectInventorySpace);
                        else if (selectEquipment)
                            moveArrow(positionEquipmentInventory - arrowOffset, ref selectEquipment, ref selectInventorySpace);
                        else if (selectMaterials)
                            moveArrow(positionMaterialsInventory - arrowOffset, ref selectMaterials, ref selectInventorySpace);
                        else if (selectKeyItems)
                            moveArrow(positionKeyItemsInventory - arrowOffset, ref selectKeyItems, ref selectInventorySpace);
                    }
                    else if (popupSelectionMenu && !popupSelectionMenu2)
                    {
                        moveArrow(inventorySlotPositions[inventoryArrowPlacement], ref selectInventorySpace, ref popupSelectionMenu);
                        selectUse = false;
                        selectDrop = false;
                        selectDiscard = false;
                        selectClose = false;
                    }
                    else if (popupSelectionMenu2)
                    {
                        moveArrow(popupPositionSlots[0], ref popupSelectionMenu, ref popupSelectionMenu2);
                        selectNumber = false;
                        selectConfirm = false;
                        selectCancel = false;
                    }
                    else
                    {
                        currentGameState = oldGameState;
                        moveArrow(positionInventoryMenu, ref selectInventory, ref selectMaterials);
                        selectEquipment = false;
                        selectConsumables = false;
                        selectKeyItems = false;
                        selectInventorySpace = false;
                        popupSelectionMenu = false;
                    }
                }
                if (inputter.isActioninputtedbyType(Actions.Confirm, InputType.Release) && !screenTransition)
                {
                    screenTransition = true;
                    if (selectConsumables && !selectInventorySpace && !popupSelectionMenu)
                    {
                        moveArrow(inventorySlotPositions[inventoryArrowPlacement] - arrowOffset);
                        selectInventorySpace = true;
                    }
                    else if (selectInventorySpace)
                    {
                        PopupCall(ref selectUse, ref selectInventorySpace);
                    }
                    else if (selectUse)
                    {
                        if (inventoryDisplaySelection[inventoryArrowPlacement].quantity >= 0)
                        {
                            player.useItem(inventoryDisplaySelection[inventoryArrowPlacement]);
                            //inventoryDisplaySelection[inventoryArrowPlacement].SubtractItemQuantity(1);
                            //if(inventoryDisplaySelection[inventoryArrowPlacement].quantity <= 0)
                            //{
                            //    inventory.Remove(inventoryDisplaySelection[inventoryArrowPlacement]);
                            //}
                            popupSelectionMenu = false;
                            moveArrow(inventorySlotPositions[inventoryArrowPlacement] - arrowOffset, ref selectInventorySpace, ref selectClose);
                        }                        
                        selectInventorySpace = false;                     
                        isinMenu = false;
                        currentGameState = GameState.GameRunning;
                    }
                    else if (selectDrop)
                    {
                        popupSelectionMenu2 = true;
                        selectDropMenu = true;
                        moveArrow(popupCenterSlots[1] + popupArrowOffset - new Vector2(10,0), ref selectNumber, ref selectDrop);
                    }else if (selectDiscard)
                    {
                        popupSelectionMenu2 = true;
                        selectDiscardMenu = true;
                        moveArrow(popupCenterSlots[1] + popupArrowOffset - new Vector2(10, 0), ref selectNumber, ref selectDiscard);
                    }
                    else if (selectClose)
                    {
                        popupSelectionMenu = false;
                        moveArrow(inventorySlotPositions[inventoryArrowPlacement] - arrowOffset, ref selectInventorySpace, ref selectClose);
                    }else if (selectConfirm)
                    {
                        if (selectDropMenu)
                        {
                            haspickedup = false;
                            dropItem = true;                            
                            itemDropLocation = player.PositionLocation;
                            droppedItem = new ItemSlot(inventoryDisplaySelection[inventoryArrowPlacement].item, itemAmountSelection);
                            droppedItem.SetLocation(player.PositionLocation + droppedItem.offset);
                        }
                        inventoryDisplaySelection[inventoryArrowPlacement].SubtractItemQuantity(itemAmountSelection);
                        if (inventoryDisplaySelection[inventoryArrowPlacement].quantity <= 0)
                        {
                            inventory.Remove(inventoryDisplaySelection[inventoryArrowPlacement]);
                        }
                        itemAmountSelection = 0;
                        popupSelectionMenu2 = false;
                        selectDropMenu = false;
                        selectDiscardMenu = false;
                        moveArrow(popupPositionSlots[0] + popupArrowOffset, ref selectUse, ref selectConfirm);
                        popupSelectionMenu = false;
                        moveArrow(inventorySlotPositions[inventoryArrowPlacement] - arrowOffset, ref selectInventorySpace, ref selectUse);
                    }
                    else if (selectCancel)
                    {
                        itemAmountSelection = 0;
                        popupSelectionMenu2 = false;
                        selectDropMenu = false;
                        selectDiscardMenu = false;
                        moveArrow(popupPositionSlots[0] + popupArrowOffset, ref selectUse, ref selectCancel);
                    }
                }
            }
            if (currentGameState == GameState.Equipment)
            {                
                if (inputter.isActioninputtedbyType(Actions.MoveUp, InputType.Press))
                {
                    if (!popupSelectionMenu && !gearSelectionScreenBool)
                    {
                        if (equipmentArrowPlacementY > 0 && !equipmentItemBool[equipmentArrowPlacementX])
                        {
                            equipmentArrowPlacementY--;
                            moveArrow(equipmentGearSlots[equipmentArrowPlacementX, equipmentArrowPlacementY] - arrowOffset, ref equipmentGearBool[equipmentArrowPlacementX, equipmentArrowPlacementY], ref equipmentGearBool[equipmentArrowPlacementX, equipmentArrowPlacementY + 1]);
                        }
                        else if (equipmentArrowPlacementY >= 1 && !popupSelectionMenu)
                        {
                            //equipmentArrowPlacementY--;
                            moveArrow(equipmentGearSlots[equipmentArrowPlacementX, equipmentArrowPlacementY] - arrowOffset, ref equipmentGearBool[equipmentArrowPlacementX, equipmentArrowPlacementY], ref equipmentItemBool[equipmentArrowPlacementX]);
                        }
                    }
                    else if (popupSelectionMenu && !gearSelectionScreenBool)
                    {
                        if (popupArrowPlacement > 0)
                        {
                            popupArrowPlacement--;
                            moveArrow(popupPositionSlots[popupArrowPlacement] + popupArrowOffset, ref popupSlotBools[popupArrowPlacement], ref popupSlotBools[popupArrowPlacement +1]);
                        }
                    }
                    else if (gearSelectionScreenBool && !popupSelectionMenu)
                    {
                        if (gearselectiontracker > 4)
                        {
                            moveArrow(gearSelectionPositions[gearselectiontracker-=5] + gearSelectionScreenStartPosition - arrowOffset);
                        }
                        else
                        {
                            gearselectiontracker = 0;
                            moveArrow(gearSelectionPositions[gearselectiontracker] + gearSelectionScreenStartPosition - arrowOffset);
                        }
                    }
                }
                if (inputter.isActioninputtedbyType(Actions.MoveDown, InputType.Press))
                {
                    if (!popupSelectionMenu && !gearSelectionScreenBool)
                    {
                        if (equipmentArrowPlacementY < 1)
                        {
                            equipmentArrowPlacementY++;
                            moveArrow(equipmentGearSlots[equipmentArrowPlacementX, equipmentArrowPlacementY] - arrowOffset, ref equipmentGearBool[equipmentArrowPlacementX, equipmentArrowPlacementY], ref equipmentGearBool[equipmentArrowPlacementX, equipmentArrowPlacementY - 1]);
                        }
                        else if (equipmentArrowPlacementY >= 1)
                        {
                            moveArrow(equipmentItemSlots[equipmentArrowPlacementX] - arrowOffset, ref equipmentItemBool[equipmentArrowPlacementX], ref equipmentGearBool[equipmentArrowPlacementX, equipmentArrowPlacementY]);
                        }
                    }
                    else if (popupSelectionMenu && !gearSelectionScreenBool)
                    {
                        if (popupArrowPlacement < popupPositionSlots.Length  - 1)
                        {
                            popupArrowPlacement++;
                            moveArrow(popupPositionSlots[popupArrowPlacement] + popupArrowOffset, ref popupSlotBools[popupArrowPlacement], ref popupSlotBools[popupArrowPlacement - 1]);
                        }
                    }
                    else if (gearSelectionScreenBool && !popupSelectionMenu)
                    {
                        if (gearselectiontracker < gearSelectionPositions.Count - 5 && gearselectiontracker < gearDisplayedSelections.Count - 5)
                        {
                            moveArrow(gearSelectionPositions[gearselectiontracker+=5] + gearSelectionScreenStartPosition - arrowOffset);
                        }
                        else if(gearselectiontracker < gearSelectionPositions.Count)
                        {
                            gearselectiontracker = gearDisplayedSelections.Count - 1;
                            moveArrow(gearSelectionPositions[gearselectiontracker] + gearSelectionScreenStartPosition - arrowOffset);
                        }
                    }

                }
                if (inputter.isActioninputtedbyType(Actions.MoveLeft, InputType.Press))
                {
                    if (!popupSelectionMenu && !gearSelectionScreenBool)
                    {
                        if (equipmentArrowPlacementX > 0 && !equipmentItemBool[equipmentArrowPlacementX])
                        {
                            equipmentArrowPlacementX--;
                            moveArrow(equipmentGearSlots[equipmentArrowPlacementX, equipmentArrowPlacementY] - arrowOffset, ref equipmentGearBool[equipmentArrowPlacementX, equipmentArrowPlacementY], ref equipmentGearBool[equipmentArrowPlacementX + 1, equipmentArrowPlacementY]);
                        }
                        else if (equipmentArrowPlacementX > 0 && equipmentItemBool[equipmentArrowPlacementX])
                        {
                            equipmentArrowPlacementX--;
                            moveArrow(equipmentItemSlots[equipmentArrowPlacementX] - arrowOffset, ref equipmentItemBool[equipmentArrowPlacementX], ref equipmentItemBool[equipmentArrowPlacementX + 1]);
                        }
                    }
                    if (gearSelectionScreenBool)
                    {
                        if (gearselectiontracker > 0)
                        {
                            moveArrow(gearSelectionPositions[gearselectiontracker-=1] + gearSelectionScreenStartPosition - arrowOffset);
                        }
                    }
                }
                if (inputter.isActioninputtedbyType(Actions.MoveRight, InputType.Press))
                {
                    if (!popupSelectionMenu && !gearSelectionScreenBool)
                    {
                        if (equipmentArrowPlacementX < 4 && !equipmentItemBool[equipmentArrowPlacementX])
                        {
                            equipmentArrowPlacementX++;
                            moveArrow(equipmentGearSlots[equipmentArrowPlacementX, equipmentArrowPlacementY] - arrowOffset, ref equipmentGearBool[equipmentArrowPlacementX, equipmentArrowPlacementY], ref equipmentGearBool[equipmentArrowPlacementX - 1, equipmentArrowPlacementY]);
                        }
                        else if (equipmentArrowPlacementX < 4 && equipmentItemBool[equipmentArrowPlacementX])
                        {
                            equipmentArrowPlacementX++;
                            moveArrow(equipmentItemSlots[equipmentArrowPlacementX] - arrowOffset, ref equipmentItemBool[equipmentArrowPlacementX], ref equipmentItemBool[equipmentArrowPlacementX - 1]);
                        }
                    }
                    if (gearSelectionScreenBool)
                    {
                        if (gearselectiontracker < gearSelectionPositions.Count && gearselectiontracker < gearDisplayedSelections.Count - 1)
                        {
                            moveArrow(gearSelectionPositions[gearselectiontracker+=1] + gearSelectionScreenStartPosition - arrowOffset);
                        }
                    }
                }
                if (inputter.isActioninputtedbyType(Actions.Cancel, InputType.Release))
                {
                    if (popupSelectionMenu || gearSelectionScreenBool || itemDesBool)
                    {
                        popupSelectionMenu = false;
                        gearSelectionScreenBool = false;
                        itemDesBool = false;
                        if (equipmentItemBool[equipmentArrowPlacementX])
                            moveArrow(equipmentItemSlots[equipmentArrowPlacementX] - arrowOffset, ref equipmentItemBool[equipmentArrowPlacementX], ref popupSlotBools[popupArrowPlacement]);
                        else if (!equipmentItemBool[equipmentArrowPlacementX])
                            moveArrow(equipmentGearSlots[equipmentArrowPlacementX, equipmentArrowPlacementY] - arrowOffset, ref equipmentGearBool[equipmentArrowPlacementX, equipmentArrowPlacementY], ref popupSlotBools[popupArrowPlacement]);
                    }
                    else
                    {
                        currentGameState = oldGameState;
                        moveArrow(positionInventoryMenu, ref selectInventory, ref equipmentGearBool[equipmentArrowPlacementX, equipmentArrowPlacementY]);
                        equipmentItemBool[equipmentArrowPlacementX] = false;
                    }

                }
                if(inputter.isActioninputtedbyType(Actions.Confirm, InputType.Release) && !screenTransition)
                {
                    int bodypartintconversion = equipmentArrowPlacementX;
                    if (equipmentArrowPlacementY == 1)
                        bodypartintconversion += 5;
                    if(equipmentItemBool[equipmentArrowPlacementX])
                        bodypartintconversion += 5;
                    if (!popupSelectionMenu && !gearSelectionScreenBool)
                    {
                        popupArrowPlacement = 0;
                        PopupCall(ref popupSlotBools[popupArrowPlacement], ref selectExit);
                    }
                    else if (popupSlotBools[0]) //equip
                    {
                        gearselectiontracker = 0;
                        moveArrow(gearSelectionPositions[0] + gearSelectionScreenStartPosition - arrowOffset, ref gearSelectionScreenBool, ref popupSlotBools[0]);
                        popupSelectionMenu = false;
                    }
                    else if (popupSlotBools[1]) //remove
                    {
                        player.RemoveEquipment((BodyPart)bodypartintconversion);

                        if (bodypartintconversion >= 10 && bodypartintconversion <= 12)
                            if (player.GearItemList.Find(x => x.bodyPart == (BodyPart)bodypartintconversion).isSelected)
                                player.selectedToolBarConsumable = null;

                        updateEquipmentScreenStatStrings();
                        popupSelectionMenu = false;
                        if (!equipmentItemBool[equipmentArrowPlacementX])
                            moveArrow(equipmentGearSlots[equipmentArrowPlacementX, equipmentArrowPlacementY] - arrowOffset, ref equipmentGearBool[equipmentArrowPlacementX, equipmentArrowPlacementY], ref popupSlotBools[1]);
                        else if (equipmentItemBool[equipmentArrowPlacementX])
                            moveArrow(equipmentItemSlots[equipmentArrowPlacementX] - arrowOffset, ref equipmentItemBool[equipmentArrowPlacementX], ref popupSlotBools[1]);
                    }
                    else if (popupSlotBools[2]) //details
                    {
                        if (player.GetEquipment((BodyPart)bodypartintconversion) != null)
                        {
                            //if (player.GetEquipment((BodyPart)bodypartintconversion).itemType == ItemType.Equipment)
                            equipmentToGetDetailsFrom = player.GetEquipment((BodyPart)bodypartintconversion);
                            WriteItemDetails(equipmentToGetDetailsFrom);
                            moveArrow(-arrowOffset, ref itemDesBool, ref popupSelectionMenu);
                        }
                    }
                    else if (gearSelectionScreenBool)
                    {
                        if (gearDisplayedSelections.Count > 0)
                        {
                            player.EquipEquipment(gearDisplayedSelections[gearselectiontracker], (BodyPart)bodypartintconversion);

                            if (bodypartintconversion >= 10 && bodypartintconversion <= 12)
                                if (player.GearItemList.Find(x => x.bodyPart == (BodyPart)bodypartintconversion).isSelected)
                                    player.selectedToolBarConsumable = (Consumables)gearDisplayedSelections[gearselectiontracker].item;
                        }
                        updateEquipmentScreenStatStrings();
                        if (!equipmentItemBool[equipmentArrowPlacementX])
                            moveArrow(equipmentGearSlots[equipmentArrowPlacementX, equipmentArrowPlacementY] - arrowOffset, ref equipmentGearBool[equipmentArrowPlacementX, equipmentArrowPlacementY], ref gearSelectionScreenBool);
                        else if (equipmentItemBool[equipmentArrowPlacementX])
                            moveArrow(equipmentItemSlots[equipmentArrowPlacementX] - arrowOffset, ref equipmentItemBool[equipmentArrowPlacementX], ref gearSelectionScreenBool);
                    }
                }
            }
            menuCall = false;
            screenTransition = false;
            //screenTransition2 = false;
            oldState = keyState;
        }
        public void updateOutofMenu(Game game, KeyboardState keyState, MC player)
        {
            if (currentGameState == GameState.GameRunning)
            {
                oldGameState = currentGameState;
                if (displayMessageBox)
                {
                    intToTrackTimeByUpdate++;
                    if (intToTrackTimeByUpdate >= 300)
                    {
                        displayMessageBox = false;
                        intToTrackTimeByUpdate = 0;
                    }
                }
                if (inputter.isActioninputtedbyType(Actions.OpenMenu, InputType.Press) && !screenTransition)
                {
                    //MenuCall();
                    screenTransition = true;
                }
                if (inputter.isActioninputtedbyType(Actions.Inventory, InputType.Press))
                {
                    isinMenu = true;
                    currentGameState = GameState.Inventory;
                }
            }
            menuCall = false;
            screenTransition = false;
            //screenTransition2 = false;
            oldState = keyState;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isinMenu || menuCall)
            {
                if (currentGameState == GameState.TitleScreen)
                {
                    spriteBatch.Draw(titleScreen, new Vector2(0, 0), Color.White);
                    //_spriteBatch.Draw(titleScreenArrow, new Vector2(0, 0), Color.White);

                }
                else if (currentGameState == GameState.SettingsScreen)
                {
                    spriteBatch.Draw(settingScreen, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(font, "General", settingsTopSlotPositions[0], Color.Black);
                    spriteBatch.DrawString(font, "Display", settingsTopSlotPositions[1], Color.Black);
                    spriteBatch.DrawString(font, "Audio", settingsTopSlotPositions[2], Color.Black);
                    spriteBatch.DrawString(font, "Controls", settingsTopSlotPositions[3], Color.Black);
                    spriteBatch.DrawString(font, "Exit", settingsTopSlotPositions[4], Color.Black);
                    if (selectDisplay)
                    {
                        spriteBatch.DrawString(font, "Fullscreen: ", settingsLeftSlotPositions[0, 0], Color.Black);
                        spriteBatch.DrawString(font, "Windowed", settingsLeftSlotPositions[0, 1], Color.Black);
                        spriteBatch.DrawString(font, "Borderless", settingsLeftSlotPositions[0, 2], Color.Black);
                        spriteBatch.DrawString(font, "Fullscreen", settingsLeftSlotPositions[0, 3], Color.Black);
                    }
                    else if (selectControls)
                    {
                        spriteBatch.DrawString(font, "Remap Controls", settingsLeftSlotPositions[0, 0], Color.Black);
                    }
                }
                else if (currentGameState == GameState.RemapInput)
                {
                    spriteBatch.Draw(remapInputScreen, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(font, "Actions", remapMenuSlotPositions[0, 0], Color.Black);
                    spriteBatch.DrawString(font, "Keyboard", remapMenuSlotPositions[1, 0], Color.Black);
                    spriteBatch.DrawString(font, "Mouse", remapMenuSlotPositions[2, 0], Color.Black);
                    spriteBatch.DrawString(font, "Controller", remapMenuSlotPositions[3, 0], Color.Black);
                    spriteBatch.DrawString(font, "Confirm", remapMenuSlotPositions[0, 1], Color.Black);
                    spriteBatch.DrawString(font, "Cancel", remapMenuSlotPositions[0, 2], Color.Black);
                    spriteBatch.DrawString(font, "Attack", remapMenuSlotPositions[0, 3], Color.Black);
                    spriteBatch.DrawString(font, "Block", remapMenuSlotPositions[0, 4], Color.Black);
                    spriteBatch.DrawString(font, "Run", remapMenuSlotPositions[0, 5], Color.Black);
                    spriteBatch.DrawString(font, "Dodge", remapMenuSlotPositions[0, 6], Color.Black);
                    spriteBatch.DrawString(font, "Use Item", remapMenuSlotPositions[0, 7], Color.Black);
                    spriteBatch.DrawString(font, "Interact", remapMenuSlotPositions[0, 8], Color.Black);
                    spriteBatch.DrawString(font, "Open Menu", remapMenuSlotPositions[0, 9], Color.Black);
                    spriteBatch.DrawString(font, "Inventory", remapMenuSlotPositions[0, 10], Color.Black); //end of screen here
                    spriteBatch.DrawString(font, "Equipment", remapMenuSlotPositions[0, 11], Color.Black);
                    spriteBatch.DrawString(font, "Up", remapMenuSlotPositions[0, 12], Color.Black);
                    spriteBatch.DrawString(font, "Down", remapMenuSlotPositions[0, 13], Color.Black);
                    spriteBatch.DrawString(font, "Left", remapMenuSlotPositions[0, 14], Color.Black);
                    spriteBatch.DrawString(font, "Right", remapMenuSlotPositions[0, 15], Color.Black);
                    spriteBatch.DrawString(font, inputter.getKeyforAction(Actions.Confirm).ToString(), remapMenuSlotPositions[1, 1], Color.Black);
                    spriteBatch.DrawString(font, inputter.getKeyforAction(Actions.Cancel).ToString(), remapMenuSlotPositions[1, 2], Color.Black);
                    spriteBatch.DrawString(font, inputter.getKeyforAction(Actions.Attack).ToString(), remapMenuSlotPositions[1, 3], Color.Black);
                    spriteBatch.DrawString(font, inputter.getKeyforAction(Actions.Block).ToString(), remapMenuSlotPositions[1, 4], Color.Black);
                    spriteBatch.DrawString(font, inputter.getKeyforAction(Actions.Run).ToString(), remapMenuSlotPositions[1, 5], Color.Black);
                    spriteBatch.DrawString(font, inputter.getKeyforAction(Actions.Dodge).ToString(), remapMenuSlotPositions[1, 6], Color.Black);
                    spriteBatch.DrawString(font, inputter.getKeyforAction(Actions.UseItem).ToString(), remapMenuSlotPositions[1, 7], Color.Black);
                    spriteBatch.DrawString(font, inputter.getKeyforAction(Actions.Interact).ToString(), remapMenuSlotPositions[1, 8], Color.Black);
                    spriteBatch.DrawString(font, inputter.getKeyforAction(Actions.OpenMenu).ToString(), remapMenuSlotPositions[1, 9], Color.Black);
                    spriteBatch.DrawString(font, inputter.getKeyforAction(Actions.Inventory).ToString(), remapMenuSlotPositions[1, 10], Color.Black);
                    spriteBatch.DrawString(font, inputter.getKeyforAction(Actions.Equipment).ToString(), remapMenuSlotPositions[1, 11], Color.Black);
                    spriteBatch.DrawString(font, inputter.getKeyforAction(Actions.MoveUp).ToString(), remapMenuSlotPositions[1, 12], Color.Black);
                    spriteBatch.DrawString(font, inputter.getKeyforAction(Actions.MoveDown).ToString(), remapMenuSlotPositions[1, 13], Color.Black);
                    spriteBatch.DrawString(font, inputter.getKeyforAction(Actions.MoveLeft).ToString(), remapMenuSlotPositions[1, 14], Color.Black);
                    spriteBatch.DrawString(font, inputter.getKeyforAction(Actions.MoveRight).ToString(), remapMenuSlotPositions[1, 15], Color.Black);
                    if (remapConfirmPopup)
                    {
                        spriteBatch.Draw(messageBox, new Vector2(graphics.GraphicsDevice.Viewport.Height / 2 - messageBox.Height / 2, graphics.GraphicsDevice.Viewport.Width / 2 - messageBox.Width / 2), Color.White);
                        spriteBatch.DrawString(font, "Press Key", new Vector2(graphics.GraphicsDevice.Viewport.Height / 2 - messageBox.Height / 2 + 10, graphics.GraphicsDevice.Viewport.Width / 2 - messageBox.Width / 2 + 10), Color.Black);
                        spriteBatch.DrawString(font, "for " + actions.ToString(), new Vector2(graphics.GraphicsDevice.Viewport.Height / 2 - messageBox.Height / 2 + 10, graphics.GraphicsDevice.Viewport.Width / 2 - messageBox.Width / 2 + 30), Color.Black);

                    }
                    if (remapDupeKey)
                    {
                        spriteBatch.Draw(messageBox, new Vector2(graphics.GraphicsDevice.Viewport.Height / 2 - messageBox.Height / 2, graphics.GraphicsDevice.Viewport.Width / 2 - messageBox.Width / 2), Color.White);
                        spriteBatch.DrawString(font, "This is already", new Vector2(graphics.GraphicsDevice.Viewport.Height / 2 - messageBox.Height / 2 + 10, graphics.GraphicsDevice.Viewport.Width / 2 - messageBox.Width / 2 + 10), Color.Black);
                        spriteBatch.DrawString(font, "assigned to " + dupeActions.ToString(), new Vector2(graphics.GraphicsDevice.Viewport.Height / 2 - messageBox.Height / 2 + 10, graphics.GraphicsDevice.Viewport.Width / 2 - messageBox.Width / 2 + 30), Color.Black);
                    }
                }
                else if (currentGameState == GameState.MenuScreen)
                {
                    spriteBatch.Draw(menuScreen, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(font, "Inventory", positionInventoryMenu + arrowOffset, Color.Black);
                    spriteBatch.DrawString(font, "Equipment", positionEquipmentMenu + arrowOffset, Color.Black);

                    spriteBatch.DrawString(font, "Settings", positionSettingsMenu + arrowOffset, Color.Black);
                    spriteBatch.DrawString(font, "Title Screen", positionExitMenu + arrowOffset, Color.Black);
                }
                else if (currentGameState == GameState.Inventory)
                {
                    spriteBatch.Draw(inventoryScreen, new Vector2(0, 0), Color.White); //80 30
                    spriteBatch.DrawString(font, "Consumables", positionConsumablesInventory, Color.Black);
                    spriteBatch.DrawString(font, "Equipment", positionEquipmentInventory, Color.Black);
                    spriteBatch.DrawString(font, "Materials", positionMaterialsInventory, Color.Black);
                    spriteBatch.DrawString(font, "Key Items", positionKeyItemsInventory, Color.Black);
                    //prints out the inventory
                    if (selectConsumables)
                    {
                        inventoryDisplaySelection.Clear();
                        for (int i = 0; i < inventory.Count; i++)
                        {
                            if (inventory[i].item.itemType == ItemType.Consumable)
                            {
                                inventoryDisplaySelection.Add(inventory[i]);
                                spriteBatch.DrawString(font, inventory[i].quantity + " " + inventory[i].item.name, inventorySlotPositions[inventorySortPlacement], Color.Black);
                                inventorySortPlacement++;
                            }
                        }
                    }
                    if (selectEquipment)
                    {
                        inventoryDisplaySelection.Clear();
                        for (int i = 0; i < inventory.Count; i++)
                        {
                            if (inventory[i].item.itemType == ItemType.Equipment)
                            {
                                inventoryDisplaySelection.Add(inventory[i]);
                                spriteBatch.DrawString(font, inventory[i].quantity + " " + inventory[i].item.name, inventorySlotPositions[inventorySortPlacement], Color.Black);
                                inventorySortPlacement++;
                            }
                        }
                    }
                    inventorySortPlacement = 0;
                }
                else if (currentGameState == GameState.Equipment)
                {
                    int equipmentdisplaytracker = 0;
                    gearDisplayedSelections.Clear();
                    spriteBatch.Draw(equipmentScreen, new Vector2(0, 0), Color.White);
                    for (int i = 0; i < equipmentStatsString.Count; i++)
                    {
                        spriteBatch.DrawString(font, equipmentStatsString[i], equipmentStatsSlots[i], Color.Black);
                    }
                    int gearint = 0;
                    for (int x = 0; x < 5; x++) 
                    {
                        for (int y = 0; y < 2; y++)
                        {
                            spriteBatch.DrawString(font, equipmentGearString[x,y], equipmentGearSlots[x, y], Color.Black);
                            spriteBatch.Draw(equipmentSlot, equipmentGearSlots[x,y], Color.White);
                            if(player.GearList[gearint].equipment != null)
                                spriteBatch.Draw(player.GearList[gearint].equipment.itemMenuImage, equipmentGearSlots[x, y] + new Vector2(7, 23), Color.White);
                            gearint++;
                        }
                        spriteBatch.DrawString(font, equipmentItemString[x], equipmentItemSlots[x], Color.Black);
                        spriteBatch.Draw(equipmentSlot, equipmentItemSlots[x], Color.White);
                        if(x < player.GearItemList.Count && player.GearItemList[x].consumable != null)
                        {
                            //if(player.GearItemList[x].consumable.quantity > 0)
                                spriteBatch.Draw(player.GearItemList[x].consumable.item.itemMenuImage, equipmentItemSlots[x] + new Vector2(7, 23), Color.White);
                        }                        
                    }
                    if (player.GearList[player.GearList.Count - 2].consumable != null)
                        //if (player.GearList[player.GearList.Count - 2].consumable.quantity > 0)
                            spriteBatch.Draw(player.GearList[player.GearList.Count - 2].consumable.item.itemMenuImage, equipmentItemSlots[3] + new Vector2(7, 23), Color.White);
                    if (player.GearList[player.GearList.Count - 1].consumable != null)
                        //if (player.GearList[player.GearList.Count - 1].consumable.quantity > 0)
                            spriteBatch.Draw(player.GearList[player.GearList.Count - 1].consumable.item.itemMenuImage, equipmentItemSlots[4] + new Vector2(7, 23), Color.White);
                    if (gearSelectionScreenBool)
                    {
                        spriteBatch.Draw(gearSelectionScreen, gearSelectionScreenStartPosition, Color.White);
                        for (int i = 0; i < gearSelectionPositions.Count; i++)
                        {                            
                            spriteBatch.Draw(equipmentSlot, gearSelectionScreenStartPosition + gearSelectionPositions[i], Color.White);
                            if (i < inventory.Count)
                            {
                                if (equipmentGearBool[0, 0] || equipmentGearBool[1, 0])//{ "Main Hand 1","Off Hand 1" }, { "Main Hand 2", "Off Hand 2" }, { "Hat","Gloves"}, { "Body", "Shoes" },{ "Necklace", "Ring" }
                                {
                                    if (inventory[i].item.itemSubType == ItemSubtype.Weapon)
                                    {
                                        gearDisplayedSelections.Add(inventory[i]);
                                        spriteBatch.DrawString(font, inventory[i].item.name, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker], Color.Black);
                                        spriteBatch.Draw(inventory[i].item.itemMenuImage, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker] + new Vector2(7, 23), Color.White);
                                        equipmentdisplaytracker++;
                                    }
                                }
                                else if (equipmentGearBool[0,1] || equipmentGearBool[1, 1])
                                {
                                    if (inventory[i].item.itemSubType == ItemSubtype.OffHand)
                                    {
                                        gearDisplayedSelections.Add(inventory[i]);
                                        spriteBatch.DrawString(font, inventory[i].item.name, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker], Color.Black);
                                        spriteBatch.Draw(inventory[i].item.itemMenuImage, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker] + new Vector2(7, 23), Color.White);
                                        equipmentdisplaytracker++;
                                    }
                                }
                                else if (equipmentGearBool[2, 0])
                                {
                                    if (inventory[i].item.itemSubType == ItemSubtype.Head)
                                    {
                                        gearDisplayedSelections.Add(inventory[i]);
                                        spriteBatch.DrawString(font, inventory[i].item.name, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker], Color.Black);
                                        spriteBatch.Draw(inventory[i].item.itemMenuImage, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker] + new Vector2(7, 23), Color.White);
                                        equipmentdisplaytracker++;
                                    }
                                }
                                else if (equipmentGearBool[2, 1])
                                {
                                    if (inventory[i].item.itemSubType == ItemSubtype.Arms)
                                    {
                                        gearDisplayedSelections.Add(inventory[i]);
                                        spriteBatch.DrawString(font, inventory[i].item.name, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker], Color.Black);
                                        spriteBatch.Draw(inventory[i].item.itemMenuImage, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker] + new Vector2(7, 23), Color.White);
                                        equipmentdisplaytracker++;
                                    }
                                }
                                else if (equipmentGearBool[3, 0])
                                {
                                    if (inventory[i].item.itemSubType == ItemSubtype.Body)
                                    {
                                        gearDisplayedSelections.Add(inventory[i]);
                                        spriteBatch.DrawString(font, inventory[i].item.name, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker], Color.Black);
                                        spriteBatch.Draw(inventory[i].item.itemMenuImage, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker] + new Vector2(7, 23), Color.White);
                                        equipmentdisplaytracker++;
                                    }
                                }
                                else if (equipmentGearBool[3, 1])
                                {
                                    if (inventory[i].item.itemSubType == ItemSubtype.Legs)
                                    {
                                        gearDisplayedSelections.Add(inventory[i]);
                                        spriteBatch.DrawString(font, inventory[i].item.name, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker], Color.Black);
                                        spriteBatch.Draw(inventory[i].item.itemMenuImage, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker] + new Vector2(7, 23), Color.White);
                                        equipmentdisplaytracker++;
                                    }
                                }
                                else if (equipmentGearBool[4, 0])
                                {
                                    if (inventory[i].item.itemSubType == ItemSubtype.Necklace)
                                    {
                                        gearDisplayedSelections.Add(inventory[i]);
                                        spriteBatch.DrawString(font, inventory[i].item.name, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker], Color.Black);
                                        spriteBatch.Draw(inventory[i].item.itemMenuImage, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker] + new Vector2(7, 23), Color.White);
                                        equipmentdisplaytracker++;
                                    }
                                }
                                else if (equipmentGearBool[4, 1])
                                {
                                    if (inventory[i].item.itemSubType == ItemSubtype.Ring)
                                    {
                                        gearDisplayedSelections.Add(inventory[i]);
                                        spriteBatch.DrawString(font, inventory[i].item.name, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker], Color.Black);
                                        spriteBatch.Draw(inventory[i].item.itemMenuImage, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker] + new Vector2(7, 23), Color.White);
                                        equipmentdisplaytracker++;
                                    }
                                }
                                else if (equipmentItemBool[0] || equipmentItemBool[1] || equipmentItemBool[2])
                                {
                                    if (inventory[i].item.itemType == ItemType.Consumable)
                                    {
                                        gearDisplayedSelections.Add(inventory[i]);
                                        spriteBatch.DrawString(font, inventory[i].quantity + " "+ inventory[i].item.name, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker], Color.Black);
                                        spriteBatch.Draw(inventory[i].item.itemMenuImage, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker] + new Vector2(7, 23), Color.White);
                                        equipmentdisplaytracker++;
                                    }
                                }
                                else if (equipmentItemBool[3] || equipmentItemBool[4])
                                {
                                    if (inventory[i].item.itemSubType == ItemSubtype.Ammunition)
                                    {
                                        gearDisplayedSelections.Add(inventory[i]);
                                        spriteBatch.DrawString(font, inventory[i].quantity + " " + inventory[i].item.name, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker], Color.Black);
                                        spriteBatch.Draw(inventory[i].item.itemMenuImage, gearSelectionScreenStartPosition + gearSelectionPositions[equipmentdisplaytracker] + new Vector2(7, 23), Color.White);
                                        equipmentdisplaytracker++;
                                    }
                                }
                            }
                        }
                    }
                    if (itemDesBool)
                    {
                        spriteBatch.Draw(itemDescriptionScreen, itemDesScreenOffset, Color.White);
                        spriteBatch.DrawString(font, equipmentToGetDetailsFrom.name, itemDesNameLocation, Color.Black);
                        spriteBatch.Draw(equipmentToGetDetailsFrom.itemMenuImage, itemDesPictureLocation, Color.White);
                        for (int i = 0; i < itemDetailsString.Count; i++)
                        {
                            if (itemDetailsString[i] != null)
                                spriteBatch.DrawString(font, itemDetailsString[i], itemDesStatsLocation[i], Color.Black);
                        
                        }
                    }
                    //if (equipmentisalreadyequippedelsewhere)
                    //{
                    //    spriteBatch.Draw(messageBox, gearSelectionScreenStartPosition, Color.White);
                    //    spriteBatch.DrawString(font, "This item is already equipped elsewhere. Equip here anyways?", gearSelectionScreenStartPosition)
                    //}
                    equipmentdisplaytracker = 0;
                }
                if (popupSelectionMenu)
                {
                    spriteBatch.Draw(popupSelectionScreen, popupPosition, Color.White);
                    if (currentGameState == GameState.MenuScreen)
                    {
                        spriteBatch.DrawString(font, "Do you want to return" + Environment.NewLine + "to the title screen?", new Vector2(popupPosition.X + 16, popupPosition.Y + 20), Color.Black);
                        spriteBatch.DrawString(font, "Yes", popupPositionSlots[1], Color.Black);
                        spriteBatch.DrawString(font, "No", popupPositionSlots[2], Color.Black);
                    }
                    else if (currentGameState == GameState.Inventory)
                    {
                        spriteBatch.DrawString(font, "Use", popupPositionSlots[0], Color.Black);
                        spriteBatch.DrawString(font, "Drop", popupPositionSlots[1], Color.Black);
                        spriteBatch.DrawString(font, "Discard", popupPositionSlots[2], Color.Black);
                        spriteBatch.DrawString(font, "Close", popupPositionSlots[3], Color.Black);
                    }
                    else if (currentGameState == GameState.Equipment)
                    {
                        spriteBatch.DrawString(font, "Equip", popupPositionSlots[0], Color.Black);
                        spriteBatch.DrawString(font, "Remove", popupPositionSlots[1], Color.Black);
                        spriteBatch.DrawString(font, "Details", popupPositionSlots[2], Color.Black);
                        spriteBatch.DrawString(font, "Close", popupPositionSlots[3], Color.Black);

                    }
                }
                if (popupSelectionMenu2)
                {
                    spriteBatch.Draw(popupSelectionScreen, popupCenterScreen, Color.White);
                    spriteBatch.DrawString(font, "Select amount: ", popupCenterSlots[0], Color.Black);
                    spriteBatch.DrawString(font, "< " + itemAmountSelection.ToString() + " >", popupCenterSlots[1], Color.Black);
                    spriteBatch.DrawString(font, "Confirm", popupCenterSlots[2], Color.Black);
                    spriteBatch.DrawString(font, "Cancel", popupCenterSlots[3], Color.Black);
                }
                titleScreenArrowSprite.Draw(spriteBatch, new Vector3(positionArrow.X, positionArrow.Y, 0), false);//255/ 340 / 410
            }
            else
            {
                if(currentGameState == GameState.GameRunning)
                {
                    //draws player HUD
                    if (displayMessageBox)
                    {
                        spriteBatch.Draw(messageBox, messageBoxPosition, Color.White);
                        spriteBatch.DrawString(font, "You have acquired: ", messageBoxPosition + new Vector2(10, 10), Color.Black);
                        spriteBatch.DrawString(font, displayedItem.quantity + " " + displayedItem.item.name, messageBoxPosition + new Vector2(10, 30), Color.Black);
                    }                   
                    spriteBatch.Draw(player.healthBarOuter, healthBarOuterPosition, Color.White);
                    spriteBatch.Draw(player.healthBar, healthBarPosition, new Rectangle(0, 0, (int)player.healthpercentdisplay, 48), Color.White);
                    spriteBatch.Draw(player.healthBarOuter, staminaBarOuterPosition, Color.White);
                    spriteBatch.Draw(player.staminaBar, staminaBarPosition, new Rectangle(0, 0, (int)player.staminapercentdisplay, 48), Color.White);
                    //spriteBatch.DrawString(font, player.checkHealth().ToString(), new Vector2(100, 100), Color.White);
                    spriteBatch.Draw(itemToolBarTexture, itemToolBarPosition, Color.White);
                    if(player.selectedToolBarConsumable != null)
                        spriteBatch.Draw(player.selectedToolBarConsumable.itemToolBarImage, itemToolBarPosition + itemToolBarOffset, Color.White);
                }
            }
        }

    }
}
