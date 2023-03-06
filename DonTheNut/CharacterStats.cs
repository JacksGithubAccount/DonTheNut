using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DonTheNut
{
    enum Faction
    {
        Playable,
        Undead,
    }
    class CharacterStats
    {
        public string name = "Don";
        public int maxHealth;
        public int health;
        public int maxStamina;
        public float stamina;
        public int mana;
        public int strength;
        public int dexterity;
        public int vitality;
        public int intelligence;
        public int wisdom;
        public int charisma;
        public int luck;
        public int level = 1;

        public Faction faction;

        public double maxWeight;
        public double weight;
        public double weightPercent;
        public int pAttackMain1;
        public int pAttackMain2;
        public int pAttackOff1;
        public int pAttackOff2;
        public int mAttackMain1;
        public int mAttackMain2;
        public int mAttackOff1;
        public int mAttackOff2;
        public int pDefense;
        public int mDefense;
        public int fireRes;
        public int iceRes;
        public int lightningRes;

        public List<GearSlot> GearList;
        public List<GearSlot> GearItemList;
        public GearSlot mainhand1;
        public GearSlot mainhand2;
        public bool isUsingMain1 = true;
        public GearSlot offhand1;
        public GearSlot offhand2;
        public bool isUsingOff1 = true;
        public GearSlot head;
        public GearSlot body;
        public GearSlot arms;
        public GearSlot legs;
        public GearSlot necklace;
        public GearSlot ring;
        public GearSlot arrow1;
        public GearSlot arrow2;
        public bool isUsingArrow1 = true;
        public GearSlot item1;
        public GearSlot item2;
        public GearSlot item3;

        public AnimatedSprite animatedSprite;
        public List<ItemSlot> inventory;
        public ItemSlot currentConsumable;
        public Texture2D spriteIdle;
        public Texture2D spriteWalk;
        public Texture2D spriteRun;
        public Texture2D spriteHit;
        public Texture2D spriteDead;
        public Texture2D spriteRoll;
        public Texture2D spriteBackstep;
        public Texture2D spriteBlock;
        public Texture2D spriteBlockWalk;
        public Texture2D spriteBlockHit;
        public Texture2D spriteDrinkPotion;
        public Texture2D spriteThrowItem;

        public Rectangle hamsterBox;

        public SpriteFont font;
        public float moveSpeed { get; set; }
        public float defaultMoveSpeed;
        public float walkSpeed;
        public float runSpeed;
        public float blockSpeed;
        public Vector3 moveDirection;
        public Vector3 PositionLocation;
        public Vector3 PositionOffset;
        public Vector3 distance;
        public bool[] pathbools = new bool[9];
        public float[] destinationfloats = new float[9];
        public bool[] behaviorbools = new bool[9]; // run, walk, block, 
        public bool behaviorConfirmed = false;

        public float healthpercentdisplay = 22;
        public float staminapercentdisplay = 22;
        public ContentManager content;
        public Texture2D healthBar;
        public Texture2D healthBarOuter;
        public Texture2D staminaBar;
        public float staminaRecoveryRate = 0.5f;
        public bool isStaminaRecovering = true;
        public Consumables selectedToolBarConsumable;

        public BoundingSphere HamsterBall;
        public BoundingSphere attackBall;
        public BoundingSphere lineofsightBall;
        public BoundingSphere outsidetheHamsterBall;

        public bool isPlayable = false;

        public bool attackCheck = false;
        public bool runCheck = false;
        public bool walkCheck = false;
        public bool rollCheck = false;
        public bool backstepCheck = false;
        public bool blockCheck = false;
        public bool idleCheck = false;
        public bool drinkCheck = false;
        public bool throwCheck = false;
        public bool attackHitCheck { get; set; }
        public bool dealtDamage = false;
        public bool isDead = false;
        public bool takenDamage = false;
        public bool isInvulnerable = false;
        public bool guardBreak = false;
        public bool gotHitCheck { get; set; }
        public bool isLeft = false;
        public bool isChase = false;

        public bool resetAnimationCheck = false;

        public CharacterStats()
        {
            GenerateStats(1);
        }
        public CharacterStats(int seed)
        {
            GenerateStats(seed);            
        }
        public void updateGearStats()
        {
            List<GearSlot> tempnonWepList = GearList.FindAll(x => x.equipment != null);
            weight = 0;
            for (int i = 0; i < tempnonWepList.Count; i++)
            {
                weight += tempnonWepList[i].equipment.weight;
            }
            tempnonWepList.Remove(mainhand1);
            tempnonWepList.Remove(mainhand2);
            tempnonWepList.Remove(offhand1);
            tempnonWepList.Remove(offhand2);
            tempnonWepList.Remove(arrow1);
            tempnonWepList.Remove(arrow2);
            //List<GearSlot> tempWepList = new List<GearSlot>();
            //tempWepList.Add(mainhand1);
            //tempWepList.Add(mainhand2);
            //tempWepList.Add(offhand1);
            //tempWepList.Add(offhand2);
            int temppatk = 0;
            int tempmatk = 0;
            pDefense = vitality;
            mDefense = wisdom;
            fireRes = dexterity / 2;
            iceRes = vitality / 2;
            lightningRes = wisdom / 2;
            weightPercent = Math.Round(weight / maxWeight * 100,2);

            for (int i = 0; i < tempnonWepList.Count; i++)
            {
                temppatk =+tempnonWepList[i].equipment.pAttack;
                tempmatk =+tempnonWepList[i].equipment.mAttack;
                pDefense += tempnonWepList[i].equipment.pDefense;
                mDefense += tempnonWepList[i].equipment.mDefense;
                fireRes += tempnonWepList[i].equipment.fireRes;
                iceRes += tempnonWepList[i].equipment.iceRes;
                lightningRes += tempnonWepList[i].equipment.lightningRes;                
            }
            int temppatkholder = strength + temppatk;
            int tempmatkholder = intelligence + tempmatk;
            pAttackMain1 = updateWeaponStats(mainhand1.equipment, temppatkholder, true);
            pAttackMain2 = updateWeaponStats(mainhand2.equipment, temppatkholder, true);
            pAttackOff1 = updateWeaponStats(offhand1.equipment, temppatkholder, true);
            pAttackOff2 = updateWeaponStats(offhand2.equipment, temppatkholder, true);
            mAttackMain1 = updateWeaponStats(mainhand1.equipment, tempmatkholder, false);
            mAttackMain2 = updateWeaponStats(mainhand2.equipment, tempmatkholder, false);
            mAttackOff1 = updateWeaponStats(offhand1.equipment, tempmatkholder, false);
            mAttackOff2 = updateWeaponStats(offhand2.equipment, tempmatkholder, false);
        }
        private int updateWeaponStats(Equipment wep, int attackBeforeWep, bool isPhys)
        {
            if (wep != null)
                if(isPhys)
                    return wep.pAttack + attackBeforeWep;
                else
                    return wep.mAttack + attackBeforeWep;
            else
                return attackBeforeWep;
        }
        public void DefaultWalkSpeed()
        {
            if (blockCheck)
                this.moveSpeed = this.blockSpeed;
            else if (runCheck)
                this.moveSpeed = this.runSpeed;
            else
                this.moveSpeed = this.walkSpeed;
        }
        public BoundingSphere getHamsterBall()
        {
            return HamsterBall;
        }
        public void GenerateStats(int seed)
        {
            maxHealth = 100;
            health = 100;
            maxStamina = 100;
            stamina = 100;
            mana = 20;
            strength = 10;
            dexterity = 10;
            vitality = 10;
            intelligence = 10;
            wisdom = 10;
            charisma = 10;
            luck = 10;
            maxWeight = vitality + wisdom / 2 + 40; // put in update stats method for level ups
        }
        public void calcTakenDamage(int inflicterATK, int receiverDEF)
        {
            if (inflicterATK >= receiverDEF)
                health -= inflicterATK - receiverDEF;
        }
        public void calcHeal(int recoverAmount)
        {
            if (health + recoverAmount > maxHealth)
                health = maxHealth;
            else
                health += recoverAmount;
        }
        public int checkHealth()
        {
            return health;
        }
        public float getHealthPercent()
        {
            return (float)health / (float)maxHealth;
        }
        public float getStaminaPercent()
        {
            return (float)stamina / (float)maxStamina;
        }
        public void consumeStamina(int amount)
        {
            stamina -= amount;
            isStaminaRecovering = false;
        }
        public void updateStamina()
        {
            if(stamina < maxStamina && isStaminaRecovering)
            {
                stamina += staminaRecoveryRate;
            }
        }
        public void setStaminaRecoveryRate(float rate)
        {
            isStaminaRecovering = true;
            staminaRecoveryRate = rate;
        }
        public bool hasStamina()
        {
            return stamina > 0;
        }
        //public void useItem(Consumables consumable)
        public void useItem(ItemSlot consumable) //called to select item to use
        {
            currentConsumable = consumable;
            if (currentConsumable.quantity != 0)
            {
                if(currentConsumable.item.itemSubType == ItemSubtype.Potion)
                    drinkCheck = true;
                if (currentConsumable.item.itemSubType == ItemSubtype.Bomb)
                    throwCheck = true;
            }
        }
        public void dropItem(ItemSlot consumable)
        {
            currentConsumable = consumable;
        }
        public void useItem() // actually uses the item
        {
            Consumables tempConsum = currentConsumable.totalItems[0];
            if (currentConsumable.item.itemSubType == ItemSubtype.Potion)
            {                
                if (tempConsum.itemEffect1 == ItemEffect.Health || tempConsum.itemEffect2 == ItemEffect.Health || tempConsum.itemEffect3 == ItemEffect.Health)
                {
                    //calcHeal((int)tempConsum.itemPower);
                    tempConsum.useItem(this);
                }
            }
            if (currentConsumable.item.itemSubType == ItemSubtype.Bomb)
            {
                tempConsum.isThrown = true;
            }
            //subtracts from quantity and removes if none is left
            currentConsumable.SubtractItemQuantity(1);
            if (currentConsumable.quantity <= 0)
            {
                inventory.Remove(currentConsumable);
                if (GearItemList.Find(x => x.consumable == currentConsumable) != null)
                {
                    GearItemList.Find(x => x.consumable == currentConsumable).consumable.item.isEquipped = false;
                    GearItemList.Find(x => x.consumable == currentConsumable).consumable = null;
                    selectedToolBarConsumable = null;
                }
            }
        }
        
    }
}
