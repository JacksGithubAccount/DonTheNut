using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonTheNut
{
    class ItemSlot
    {
        public Items item;
        public List<Consumables> totalItems;
        public int quantity;
        public Vector3 location;
        public Vector3 offset;
        public BoundingSphere pickupRange;
        public List<Consumables> thrownItems;

        public ItemSlot(Items item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
            totalItems = new List<Consumables>();
            thrownItems = new List<Consumables>();           
            pickupRange = new BoundingSphere();
            if(item != null)
                offset = new Vector3(this.item.itemMenuImage.Width / 2, this.item.itemMenuImage.Height / 2, 0);
            if (item.itemType == ItemType.Consumable)
            {
                for (int i = 0; i < quantity; i++)
                {
                    totalItems.Add(new Consumables((Consumables)item));
                }
            }
        }
        public ItemSlot(Items item, int quantity, Vector3 location)
            :this(item,quantity)
        {
            this.location = location;
            pickupRange.Center = location - offset;
            pickupRange.Radius = 10;
        }
        public void AddMoreItem(int amount)
        {
            quantity += amount;
            for (int i = 0; i <= amount; i++)
            {
                totalItems.Add(new Consumables((Consumables)item));
            }
        }
        public void SubtractItemQuantity(int amount)
        {
            quantity -= amount;
            if (totalItems[0].isThrown)
            {
                thrownItems.Add(totalItems[0]);                
            }
            totalItems.Remove(totalItems[0]);
        }
        public void SetLocation(Vector3 location)
        {
            this.location = location;
            pickupRange.Center = location - offset;
            pickupRange.Radius = 10;
        }
    }
}
