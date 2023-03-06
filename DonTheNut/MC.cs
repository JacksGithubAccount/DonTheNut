using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonTheNut
{
    class MC : CharacterStats
    {
        private Texture2D spriteMCidle;
        private Texture2D spriteMCwalk;
        private Texture2D spriteMCrun;
        private Texture2D spriteMCattack1;
        private MenuScreen menuScreen;                   
        private KeyboardState oldState;

        Vector3 currentPositionforItemThrow = Vector3.Zero;
        bool isLeftforItemThrow;

        public bool actionCheck = false;
        
        public MC(ContentManager content, MenuScreen menuscreen)
        {            
            this.content = content;
            menuScreen = menuscreen;
            walkSpeed = 100f;
            runSpeed = 150f;
            blockSpeed = 50f;
            isPlayable = true;
            faction = Faction.Playable;
            PositionLocation = new Vector3(400, 200, 0);
            moveDirection = new Vector3(0, 0, 0);
            moveSpeed = 100f;
            defaultMoveSpeed = 100f;
            HamsterBall = new BoundingSphere();            
            HamsterBall.Radius = 15;
            attackBall = new BoundingSphere();
            attackBall.Radius = 21;
            inventory = new List<ItemSlot>();            
        }

        public void MoveDirection(Vector3 moveDirection)
        {
            this.moveDirection += moveDirection;
        }
        public Vector3 GetMoveDirection()
        {
            return moveDirection;
        }
        public void AddToInventory(Items item, int amount)
        {
            if (inventory.Exists(x => (x.item == item)))
            {
                inventory.Find(x => (x.item == item)).quantity += amount;
            }
            else
                inventory.Add(new ItemSlot(item, amount));
            menuScreen.messageCall(new ItemSlot(item, amount));
            menuScreen.displayMessageBox = true;
        }
        public void RemoveFromInventory(Items item)
        {
            //this won't work for removing an item that there are multiple copies of
            if (inventory.Exists(x => (x.item == item)))
            {
                inventory.RemoveAll(x => x.item == item);
            }
        }
        public void DefaultEquipmentLoadout()
        {
            GearList = new List<GearSlot>();
            GearItemList = new List<GearSlot>();
            GearList.Add(mainhand1 = new GearSlot(BodyPart.mainHand1, (Equipment)inventory.Find(x => x.item.ID == 2001).item));
            inventory.Find(x => x.item.ID == 2001).item.isEquipped = true;
            GearList[0].isSelected = true;
            GearList.Add(offhand1 = new GearSlot(BodyPart.offHand1, (Equipment)null));
            GearList[1].isSelected = true;
            GearList.Add(mainhand2 = new GearSlot(BodyPart.mainHand2, (Equipment)null));
            GearList.Add(offhand2 = new GearSlot(BodyPart.offHand2, (Equipment)null));
            GearList.Add(head = new GearSlot(BodyPart.head, (Equipment)inventory.Find(x => x.item.ID == 4001).item));
            inventory.Find(x => x.item.ID == 4001).item.isEquipped = true;
            GearList.Add(arms = new GearSlot(BodyPart.arms, (Equipment)inventory.Find(x => x.item.ID == 5001).item));
            inventory.Find(x => x.item.ID == 5001).item.isEquipped = true;
            GearList.Add(body = new GearSlot(BodyPart.body, (Equipment)null));            
            GearList.Add(legs = new GearSlot(BodyPart.legs, (Equipment)null));
            GearList.Add(necklace = new GearSlot(BodyPart.necklace, (Equipment)null));
            GearList.Add(ring = new GearSlot(BodyPart.ring, (Equipment)null));
            GearList.Add(arrow1 = new GearSlot(BodyPart.arrow1, (ItemSlot)null));
            GearList.Add(arrow2 = new GearSlot(BodyPart.arrow2, (ItemSlot)null));
            GearItemList.Add(item1 = new GearSlot(BodyPart.item1, inventory.Find(x => x.item.ID == 1101)));
            inventory.Find(x => x.item.ID == 1101).item.isEquipped = true;
            GearItemList[0].isSelected = true;
            selectedToolBarConsumable = (Consumables)GearItemList[0].consumable.item;
            GearItemList.Add(item2 = new GearSlot(BodyPart.item2, (ItemSlot)null));
            GearItemList.Add(item3 = new GearSlot(BodyPart.item3, (ItemSlot)null));
            //mainhand1.equipment = (Equipment)null;
        }
        public bool EquipEquipment(ItemSlot itemToEquip, BodyPart bodyPart)
        {
            bool alreadyequipped = false;
            Equipment tempEquip;
            ItemSlot tempConsum = itemToEquip;
            if (itemToEquip.item.isEquipped) // removes item if there is already something equipped to that slot
            {
                switch (itemToEquip.item.itemType)
                {
                    case ItemType.Equipment:
                        if(itemToEquip.item.itemSubType != ItemSubtype.Ammunition)
                            RemoveEquipment(GearList.Find(x => x.equipment == itemToEquip.item).bodyPart);
                        else
                            RemoveEquipment(GearList.Find(x => x.consumable == itemToEquip).bodyPart);
                        break;
                    case ItemType.Consumable:
                        //if(GearItemList.Find(x => x.bodyPart == bodyPart).consumable != null)
                            RemoveEquipment(GearItemList.Find(x => x.consumable == itemToEquip).bodyPart);
                        break;
                }
            }
            itemToEquip.item.isEquipped = true;            
            switch (itemToEquip.item.itemType) //equips the selected equipment
            {
                case ItemType.Equipment:
                    if (itemToEquip.item.itemSubType != ItemSubtype.Ammunition)
                    {
                        tempEquip = GearList.Find(x => x.bodyPart == bodyPart).equipment;
                        if (tempEquip != null)
                        {
                            tempEquip.isEquipped = false;
                        }
                        GearList.Find(x => x.bodyPart == bodyPart).equipment = (Equipment)itemToEquip.item;
                    }
                    else
                    {
                        tempConsum = GearList.Find(x => x.bodyPart == bodyPart).consumable;
                        if (tempConsum != null)
                        {
                            tempConsum.item.isEquipped = false;
                        }
                        GearList.Find(x => x.bodyPart == bodyPart).consumable = itemToEquip;
                    }
                    break;
                case ItemType.Consumable:
                    tempConsum = GearItemList.Find(x => x.bodyPart == bodyPart).consumable;
                    if (tempConsum != null)
                    {                        
                        tempConsum.item.isEquipped = false;
                    }
                    GearItemList.Find(x => x.bodyPart == bodyPart).consumable = itemToEquip;
                    break;
            }                                                       
            updateGearStats();
          
            return alreadyequipped;
        }
        public void RemoveEquipment(BodyPart bodyPart)
        {
            Equipment tempEquip;
            ItemSlot tempConsum;

            if ((int)bodyPart < 10) //equipment gears
            {
                tempEquip = GearList.Find(x => x.bodyPart == bodyPart).equipment;
                if (tempEquip != null)
                {
                    tempEquip.isEquipped = false;
                }
                GearList.Find(x => x.bodyPart == bodyPart).equipment = null;
            }
            else if ((int)bodyPart >= 10 && (int)bodyPart <= 12) //items consumables
            {
                GearSlot tempHolder = GearItemList.Find(x => x.bodyPart == bodyPart);
                if (tempHolder != null)
                {
                    if (tempHolder.consumable != null)
                    {
                        tempHolder.consumable = GearItemList.Find(x => x.bodyPart == bodyPart).consumable;
                        tempHolder.consumable.item.isEquipped = false;
                    }                  
                }
                GearItemList.Find(x => x.bodyPart == bodyPart).consumable = null;
            }else if ((int) bodyPart == 13 || (int)bodyPart == 14) //ammunition
            {
                GearSlot tempHolder = GearList.Find(x => x.bodyPart == bodyPart);
                if (tempHolder != null)
                {
                    if (tempHolder.consumable != null)
                    {
                        tempHolder.consumable = GearList.Find(x => x.bodyPart == bodyPart).consumable;
                        tempHolder.consumable.item.isEquipped = false;
                    }
                }
                GearList.Find(x => x.bodyPart == bodyPart).consumable = null;
            }
            updateGearStats();
        }
        public Items GetEquipment(BodyPart bodyPart)
        {
            if ((int)bodyPart < 10 && GearList.Find(x => x.bodyPart == bodyPart).equipment != null)
            {
                return GearList.Find(x => x.bodyPart == bodyPart).equipment;
            }
            else if ((int)bodyPart >= 10 && GearItemList.Find(x => x.bodyPart == bodyPart).consumable != null)
            {
                return GearItemList.Find(x => x.bodyPart == bodyPart).consumable.item;
            }
            return null;
        }
        //public void AddToInventory(ItemSlot item)
        //{
        //    if(inventory.Contains(item))
        //    {
        //        inventory.Find(x => (x == item)).quantity += item.quantity;
        //    }
        //    else
        //        inventory.Add(item);
        //}
        public void LoadContent()
        {
            font = content.Load<SpriteFont>("font");
            healthBar = content.Load<Texture2D>("healthbarplayer");
            healthBarOuter = content.Load<Texture2D>("healthbarouterplayer");
            staminaBar = content.Load<Texture2D>("UI/staminabarplayer");
            spriteMCidle = content.Load<Texture2D>("idle");
            spriteMCwalk = content.Load<Texture2D>("walk");
            spriteMCrun = content.Load<Texture2D>("run");
            spriteMCattack1 = content.Load<Texture2D>("attack1");
            spriteHit = content.Load<Texture2D>("PlayerHit");
            spriteDead = content.Load<Texture2D>("PlayerDead");
            spriteRoll = content.Load<Texture2D>("PlayerRoll");
            spriteBackstep = content.Load<Texture2D>("player/PlayerBackstep");
            spriteBlock = content.Load<Texture2D>("player/PlayerBlock");
            spriteBlockWalk = content.Load<Texture2D>("player/PlayerBlockWalk");
            spriteBlockHit = content.Load<Texture2D>("player/PlayerBlockHit");
            spriteDrinkPotion = content.Load<Texture2D>("player/PlayerDrinkPotion");
            spriteThrowItem = content.Load<Texture2D>("player/PlayerThrowItem");
            animatedSprite = new AnimatedSprite(spriteMCidle, 1, 6);
            hamsterBox = new Rectangle((int)PositionLocation.X, (int)PositionLocation.Y, animatedSprite.Texture.Width / animatedSprite.Columns, animatedSprite.Texture.Height);
        }
        public void Update(GameTime gameTime, KeyboardState keyState, Inputter inputter)
        {
            healthpercentdisplay = 44 * getHealthPercent();
            staminapercentdisplay = 44 * getStaminaPercent();
            updateStamina();
            actionCheck = false;
            animatedSprite.Update();
            if (!isDead)
            {
                menuActions(keyState);
                // handles the keyboard the input
                if (takenDamage == true && !animatedSprite.waitToFinish)
                {
                    if (blockCheck)
                    {
                        if (guardBreak)
                        {
                            blockCheck = false;
                            animatedSprite.setNewAnimation(spriteHit, 1, 3);
                            animatedSprite.waitToFinish = true;
                        }
                        else
                        {                            
                            animatedSprite.setNewAnimation(spriteBlockHit, 1, 3);
                            animatedSprite.waitToFinish = true;
                        }
                        attackCheck = false;
                    }
                    else
                    {
                        attackCheck = false;
                        animatedSprite.setNewAnimation(spriteHit, 1, 3);
                        animatedSprite.waitToFinish = true;
                    }
                }
                //disables all action if attacking, getting hit, drinking, or rolling
                if (!attackCheck && !takenDamage && !rollCheck && !backstepCheck && !drinkCheck && !throwCheck)
                {
                    if (inputter.isActionPressed(Actions.Run))//(keyState.IsKeyDown(Keys.R))
                    {
                        if (hasStamina())
                        {
                            runCheck = true;
                            moveSpeed = runSpeed;
                        }
                    }
                    else if (!inputter.isActionPressed(Actions.Run))
                    {
                        if (!blockCheck)
                        {
                            runCheck = false;
                            animatedSprite.setRowColumn(1, 6);
                            animatedSprite.setTexture(spriteMCidle);
                            moveSpeed = walkSpeed;
                        }
                        else
                        {
                            moveSpeed = blockSpeed;
                        }
                    }
                    if (inputter.isActioninputtedbyType(Actions.SwitchMain, InputType.Press))
                    {
                        if (GearList.Find(x => x.bodyPart == BodyPart.mainHand1).isSelected)
                        {
                            GearList.Find(x => x.bodyPart == BodyPart.mainHand1).isSelected = false;
                            GearList.Find(x => x.bodyPart == BodyPart.mainHand2).isSelected = true;
                        }
                        else
                        {
                            GearList.Find(x => x.bodyPart == BodyPart.mainHand1).isSelected = true;
                            GearList.Find(x => x.bodyPart == BodyPart.mainHand2).isSelected = false;
                        }
                    }
                    if (inputter.isActioninputtedbyType(Actions.SwitchOff, InputType.Press))
                    {
                        if (GearList.Find(x => x.bodyPart == BodyPart.offHand1).isSelected)
                        {
                            GearList.Find(x => x.bodyPart == BodyPart.offHand1).isSelected = false;
                            GearList.Find(x => x.bodyPart == BodyPart.offHand2).isSelected = true;
                        }
                        else
                        {
                            GearList.Find(x => x.bodyPart == BodyPart.offHand1).isSelected = true;
                            GearList.Find(x => x.bodyPart == BodyPart.offHand2).isSelected = false;
                        }
                    }
                    if (inputter.isActionPressed(Actions.MoveUp) && runCheck || inputter.isActionPressed(Actions.MoveDown) && runCheck || inputter.isActionPressed(Actions.MoveRight) && runCheck || inputter.isActionPressed(Actions.MoveLeft) && runCheck)
                    {
                        animatedSprite.setRowColumn(1, 6);
                        animatedSprite.setTexture(spriteMCrun);
                    }
                    if (inputter.isActionPressed(Actions.MoveUp) && !runCheck || inputter.isActionPressed(Actions.MoveDown) && !runCheck || inputter.isActionPressed(Actions.MoveRight) && !runCheck || inputter.isActionPressed(Actions.MoveLeft) && !runCheck)
                    {
                        animatedSprite.setRowColumn(1, 6);
                        animatedSprite.setTexture(spriteMCwalk);
                    }
                    if (inputter.isActionPressed(Actions.MoveUp))
                    {
                        PositionLocation.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        moveDirection.Y = -1;
                    }
                    if (inputter.isActionPressed(Actions.MoveDown))
                    {
                        PositionLocation.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        moveDirection.Y = 1;
                    }
                    if (inputter.isActionPressed(Actions.MoveLeft))
                    {
                        PositionLocation.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        isLeft = true;
                        moveDirection.X = -1;
                    }
                    if (inputter.isActionPressed(Actions.MoveRight))
                    {
                        PositionLocation.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        isLeft = false;
                        moveDirection.X = 1;
                    }
                    if (!inputter.isActionPressed(Actions.MoveUp) && !inputter.isActionPressed(Actions.MoveDown))
                        moveDirection.Y = 0;
                    if (!inputter.isActionPressed(Actions.MoveRight) && !inputter.isActionPressed(Actions.MoveLeft))
                        moveDirection.X = 0;
                    if (inputter.isActionPressed(Actions.Block))
                    {
                        if (hasStamina())
                        {
                            blockCheck = true;
                            setStaminaRecoveryRate(0.25f);
                            if (inputter.isActioninputtedbyType(Actions.Block, InputType.Press))//(keyState.IsKeyDown(Keys.LeftShift) && oldState.IsKeyUp(Keys.LeftShift))
                            {
                                //animatedSprite.setNewAnimation(spriteBlock, 1, 3);
                                animatedSprite.holdFrame = true;
                                moveSpeed = 60f;
                            }
                            if (inputter.isActionPressed(Actions.MoveUp) || inputter.isActionPressed(Actions.MoveDown) || inputter.isActionPressed(Actions.MoveRight) || inputter.isActionPressed(Actions.MoveLeft))
                            {
                                animatedSprite.setTexture(spriteBlockWalk);
                                animatedSprite.setRowColumn(1, 6);
                            }
                            else
                            {
                                if (animatedSprite.GetCurrentFrame() > 2)
                                    animatedSprite.resetFrame();
                                animatedSprite.setTexture(spriteBlock);
                                animatedSprite.setRowColumn(1, 3);
                            }
                        }                     
                    }
                    else if (!inputter.isActionPressed(Actions.Block))
                    {
                        blockCheck = false;
                        animatedSprite.holdFrame = false;
                        setStaminaRecoveryRate(0.5f);
                        if (runCheck)
                            moveSpeed = 150f;
                        else
                            moveSpeed = 100f;
                    }
                    if (inputter.isActioninputtedbyType(Actions.Attack, InputType.Press))//(oldState.IsKeyUp(Keys.Z) && keyState.IsKeyDown(Keys.Z))
                    {
                        if (hasStamina())
                        {
                            attackCheck = true;
                            animatedSprite.setNewAnimation(spriteMCattack1, 1, 6);
                            animatedSprite.waitToFinish = true;
                            consumeStamina(40);
                        }
                    }
                    if (inputter.isActioninputtedbyType(Actions.Interact, InputType.Press))
                    {
                        actionCheck = true;
                    }
                    if(inputter.isActioninputtedbyType(Actions.Dodge, InputType.Press))
                    {
                        if (hasStamina())
                        {
                            if (moveDirection == new Vector3(0, 0, 0))
                            {
                                backstepCheck = true;
                                animatedSprite.setNewAnimation(spriteBackstep, 1, 5);
                                animatedSprite.waitToFinish = true;
                            }
                            else
                            {
                                rollCheck = true;
                                animatedSprite.setNewAnimation(spriteRoll, 1, 6);
                                animatedSprite.waitToFinish = true;
                            }
                            consumeStamina(35);
                        }
                    }
                    if(inputter.isActioninputtedbyType(Actions.UseItem, InputType.Press))
                    {
                        if (hasStamina())
                        {
                            for (int i = 0; i < GearItemList.Count; i++)
                            {
                                if (GearItemList[i].isSelected)
                                {
                                    if (GearItemList[i].consumable != null)
                                    {                                        
                                        useItem(GearItemList[i].consumable);
                                    }
                                }
                            }
                        }
                    }

                }
                if (inputter.isActioninputtedbyType(Actions.SwitchItem, InputType.Press))
                {
                    for (int i = 0; i < GearItemList.Count; i++)
                    {
                        if (GearItemList[i].isSelected)
                        {
                            GearItemList[i].isSelected = false;
                            if (i + 1 == GearItemList.Count)
                            {
                                GearItemList[0].isSelected = true;
                                selectedToolBarConsumable = (Consumables)GearItemList[0].consumable.item;
                                break;
                            }
                            else
                            {
                                GearItemList[i + 1].isSelected = true;
                                if (GearItemList[i + 1].consumable != null)
                                    selectedToolBarConsumable = (Consumables)GearItemList[i + 1].consumable.item;
                                else
                                    selectedToolBarConsumable = null;
                                break;
                            }
                        }
                    }
                }
                if (drinkCheck)
                {
                    if (hasStamina())
                    {
                        if (!resetAnimationCheck)
                        {
                            animatedSprite.resetFrame();
                            resetAnimationCheck = true;
                        }
                        animatedSprite.setTexture(spriteDrinkPotion);
                        animatedSprite.waitToFinish = true;
                        if(animatedSprite.GetCurrentFrame() == 5)
                        {
                            useItem();
                        }
                    }
                }
                if(throwCheck && hasStamina())
                {
                    if (!resetAnimationCheck)
                    {
                        animatedSprite.resetFrame();
                        resetAnimationCheck = true;
                    }
                    animatedSprite.setTexture(spriteThrowItem);
                    animatedSprite.waitToFinish = true;
                    if (animatedSprite.GetCurrentFrame() == 5)
                    {
                        useItem();
                        currentPositionforItemThrow = PositionLocation;
                        isLeftforItemThrow = isLeft;
                    }
                }
                if (rollCheck)
                {
                    if(animatedSprite.GetCurrentFrame() == 1 || animatedSprite.GetCurrentFrame() == 0)
                    {
                        isInvulnerable = true;
                        PositionLocation += new Vector3(4, 4, 4) * moveDirection;
                    }
                    if (animatedSprite.GetCurrentFrame() == 4 || animatedSprite.GetCurrentFrame() == 3 || animatedSprite.GetCurrentFrame() == 2)
                    {
                        isInvulnerable = true;
                        PositionLocation += new Vector3(2, 2, 2) * moveDirection;
                    }
                    if(animatedSprite.GetCurrentFrame() == 5 || animatedSprite.GetCurrentFrame() == 6)
                    {
                        isInvulnerable = false;
                        PositionLocation += new Vector3(1, 1, 1) * moveDirection;
                    }

                    
                }
                if (backstepCheck)
                {
                    if (animatedSprite.GetCurrentFrame() == 1 || animatedSprite.GetCurrentFrame() == 0)
                    {
                        isInvulnerable = true;
                        if(!isLeft)
                            PositionLocation.X += -3;
                        else
                            PositionLocation.X += 3;
                    }
                    if (animatedSprite.GetCurrentFrame() == 3 || animatedSprite.GetCurrentFrame() == 2)
                    {
                        isInvulnerable = true;
                        if (!isLeft)
                            PositionLocation.X += -1;
                        else
                            PositionLocation.X += 1;
                    }
                    if (animatedSprite.GetCurrentFrame() == 4 || animatedSprite.GetCurrentFrame() == 5)
                    {
                        isInvulnerable = false;
                        if (!isLeft)
                            PositionLocation.X += -1;
                        else
                            PositionLocation.X += 1;
                    }
                }
                if (attackCheck && animatedSprite.GetCurrentFrame() == 3)
                {
                    attackBall.Radius = 21;
                    if (isLeft)
                        attackBall.Center = PositionLocation - new Vector3(30, 20, 0);
                    else
                        attackBall.Center = PositionLocation + new Vector3(30, -20, 0);
                }                
                //sets animation back to idle after an animation that needs waiting plays out
                if (!animatedSprite.isRunning() && !blockCheck)
                {
                    animatedSprite.waitToFinish = false;
                    attackCheck = false;
                    dealtDamage = false;
                    takenDamage = false;
                    rollCheck = false;
                    backstepCheck = false;
                    guardBreak = false;
                    drinkCheck = false;
                    throwCheck = false;
                    resetAnimationCheck = false;
                    animatedSprite.setRowColumn(1, 6);
                    animatedSprite.setTexture(spriteMCidle);
                }
                else if(!animatedSprite.isRunning() && blockCheck)
                {
                    animatedSprite.waitToFinish = false;
                    attackCheck = false;
                    dealtDamage = false;
                    takenDamage = false;
                    rollCheck = false;
                    backstepCheck = false;
                    guardBreak = false;
                    drinkCheck = false;
                    throwCheck = false;
                    isStaminaRecovering = true;
                    resetAnimationCheck = false;
                    animatedSprite.setNewAnimation(spriteBlock, 1, 3);
                }
                if (currentConsumable != null)
                {
                    //updates positions for thrown items
                    if (currentConsumable.thrownItems.Count > 0)
                    {                        
                        List<Consumables> tempConsum = currentConsumable.thrownItems.FindAll(x => x.isThrown == true);
                        for (int i = 0; i < tempConsum.Count; i++)
                        {
                            if (tempConsum[i].isThrown)
                            {
                                if (!tempConsum[i].isMidThrow)
                                {
                                    tempConsum[i].throwItem(gameTime, currentPositionforItemThrow, isLeftforItemThrow);
                                    tempConsum[i].isMidThrow = true;
                                }
                                else
                                {
                                    tempConsum[i].throwItem(gameTime, currentPositionforItemThrow);
                                }
                            }
                            if (tempConsum[i].isDoneThrow)
                            {
                                currentConsumable.thrownItems.Remove(tempConsum[i]);
                            }
                            if( tempConsum[i].position.X < 0 || tempConsum[i].position.Y < 0 || tempConsum[i].position.X > menuScreen.graphics.GraphicsDevice.Viewport.Width || tempConsum[i].position.Y > menuScreen.graphics.GraphicsDevice.Viewport.Height)
                            {
                                currentConsumable.thrownItems.Remove(tempConsum[i]);
                            }
                        }

                    }
                }                
                //sets animation to idle if there animation has finished and no keys are pressed
                if (keyState.GetPressedKeys().Length == 0 && animatedSprite.waitToFinish == false)
                {
                    animatedSprite.setRowColumn(1, 6);
                    animatedSprite.setTexture(spriteMCidle);
                }
                
                if (runCheck)
                {
                    if(moveDirection.Length() > 0)
                        consumeStamina(1);
                    if (!hasStamina())
                    {
                        runCheck = false;
                        animatedSprite.setRowColumn(1, 6);
                        animatedSprite.setTexture(spriteMCidle);
                        moveSpeed = 100f;
                    }
                }
                if (!attackCheck)
                {
                    attackBall.Radius = 0;
                    attackBall.Center = PositionLocation - new Vector3(10, 15, 0);
                }
                HamsterBall.Center = PositionLocation - new Vector3(10, 15, 0);
                oldState = keyState;
                if (checkHealth() <= 0)
                {
                    isDead = true;
                    animatedSprite.setNewAnimation(spriteDead, 1, 4);
                    animatedSprite.waitToFinish = true;
                }
            }
            //actionCheck = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //if(animatedSprite.Texture == spriteMCattack1 && !isLeft)
            //    animatedSprite.Draw(spriteBatch, new Vector3(PositionLocation.X - 15, PositionLocation.Y - 17, 0), isLeft);
            
            //else if(animatedSprite.Texture == spriteMCattack1 && isLeft)
            //    animatedSprite.Draw(spriteBatch, new Vector3(PositionLocation.X - 35, PositionLocation.Y - 17, 0), isLeft);
            //else
            animatedSprite.Draw(spriteBatch, PositionLocation, isLeft);
            if (currentConsumable != null)
            {
                for (int i = 0; i < currentConsumable.thrownItems.Count; i++)
                {
                    Consumables tempConsum = (Consumables)currentConsumable.thrownItems[i];
                    tempConsum.drawItem(spriteBatch);
                }
            }
            
            
            
        }


        public void menuActions(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape))
            {
                menuScreen.menuCall = true;
                menuScreen.MenuCall();
            }
            if (keyState.IsKeyDown(Keys.I) && oldState.IsKeyUp(Keys.I))
            {
                menuScreen.menuCall = true;
            }
        }
    }
}
