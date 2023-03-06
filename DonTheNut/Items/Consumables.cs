using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonTheNut
{
    class Consumables : Items
    {
        public int useAmount;
        public float itemPower;
        public ItemEffect itemEffect1;
        public ItemEffect itemEffect2;
        public ItemEffect itemEffect3;
        bool alreadyLeft = false;
        public bool isThrown = false;
        public bool isMidThrow = false;
        public bool isDoneThrow = false;

        public BoundingSphere sphereOfInfluence;
        public Rectangle hamsterBox;


        float throwSpeed = 200f;
        Vector3 initialPosition = Vector3.Zero;
        Vector3 initialVelocity = new Vector3(30, -40, 0); // Choose values that work for you
        //Vector2 acceleration = new Vector2(0, -9.8f);
        private const float GRAVITY = 9.8f;
        float time = 0;
        public Vector3 position = Vector3.Zero; // Use this when drawing your sprite

        public Consumables(int idnumber, string name, int money, int stackableAmount, ItemSubtype subType, Texture2D menuImage, Texture2D mainGameImage, Texture2D toolBarImage, int usage, int power, ItemEffect effect) 
            : base(idnumber, name, money,stackableAmount, subType, menuImage, mainGameImage, toolBarImage)
        {
            itemType = ItemType.Consumable;
            useAmount = usage;
            itemPower = power;
            itemEffect1 = effect;
            sphereOfInfluence = new BoundingSphere();
            sphereOfInfluence.Radius = 10;
            sphereOfInfluence.Center = position + new Vector3(mainGameImage.Width / 2, mainGameImage.Height / 2, 0);
            hamsterBox = new Rectangle((int)position.X, (int)position.Y, itemMainGameImage.Width, itemMainGameImage.Height);
        }
        public Consumables(int idnumber, string name, int money, int stackableAmount, ItemSubtype subType, Texture2D menuImage, Texture2D mainGameImage, Texture2D toolBarImage, int usage, int power, ItemEffect effect, ItemEffect effect2)
            :this(idnumber, name, money, stackableAmount, subType, menuImage, mainGameImage, toolBarImage, usage, power, effect)
        {            
            itemEffect2 = effect2;
        }
        public Consumables(int idnumber, string name, int money, int stackableAmount, ItemSubtype subType, Texture2D menuImage, Texture2D mainGameImage, Texture2D toolBarImage, int usage, int power, ItemEffect effect, ItemEffect effect2, ItemEffect effect3)
            : this(idnumber, name, money, stackableAmount, subType, menuImage, mainGameImage, toolBarImage, usage, power, effect, effect2)
        {            
            itemEffect3 = effect3;
        }
        public Consumables(Consumables consumables)
            :base(consumables)
        {
            itemType = ItemType.Consumable;
            useAmount = consumables.useAmount;
            itemPower = consumables.itemPower;
            itemEffect1 = consumables.itemEffect1;
            itemEffect2 = consumables.itemEffect2;
            itemEffect3 = consumables.itemEffect3;
        }
        public void useItem(CharacterStats AffectedPerson)
        {
            if(this.itemSubType == ItemSubtype.Potion)
            {
                AffectedPerson.calcHeal((int)this.itemPower);
            }if(this.itemSubType == ItemSubtype.Bomb)
            {
                //AffectedPerson.gotHitCheck = true;
                AffectedPerson.calcTakenDamage((int)this.itemPower, AffectedPerson.pDefense);
                //AffectedPerson.takenDamage = true;
                this.isDoneThrow = true;
            }
        }
        public void useItem(CharacterStats[] AffectedPeople)
        {

        }        
        public void throwItem(GameTime gameTime, Vector3 startingPosition, bool isLeft)            
        {
            if (isLeft && !alreadyLeft)
            {
                initialVelocity.X *= -1;
                alreadyLeft = true;
            }
            throwItem(gameTime, startingPosition);
        }
        public void throwItem(GameTime gameTime, Vector3 startingPosition)
        {
            initialPosition = new Vector3(startingPosition.X, startingPosition.Y, startingPosition.Z);

            time += (float)gameTime.ElapsedGameTime.Milliseconds / 90.0f;//(float)gameTime.ElapsedGameTime.TotalSeconds;
           
            position = initialPosition + initialVelocity * time;
            position.Y = position.Y - 0.5f * -GRAVITY * time * time;

            sphereOfInfluence.Radius = 10;
            sphereOfInfluence.Center = position - new Vector3(itemMainGameImage.Width, itemMainGameImage.Height, 0);
        }
        public void drawItem(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(itemMainGameImage, new Vector2(position.X, position.Y), Color.White);
        }
    }
}
