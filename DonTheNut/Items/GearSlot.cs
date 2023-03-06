using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonTheNut
{
    class GearSlot
    {
        public BodyPart bodyPart;
        public Equipment equipment;
        //public Consumables consumable;
        public ItemSlot consumable;
        public Vector3 location;
        public Vector3 offset;
        public BoundingSphere pickupRange;
        public bool isSelected = false;

        public GearSlot(BodyPart _bodyPart, Equipment _equipment)
        {
            bodyPart = _bodyPart;
            equipment = _equipment;
            if (equipment != null)
            {
                pickupRange = new BoundingSphere();
                offset = new Vector3(this.equipment.itemMenuImage.Width / 2, this.equipment.itemMenuImage.Height / 2, 0);
            }
        }
        public GearSlot(BodyPart _bodyPart, ItemSlot _consumable)
        {
            bodyPart = _bodyPart;
            consumable = _consumable;
            if (consumable != null)
            {
                pickupRange = new BoundingSphere();
                offset = new Vector3(this.consumable.item.itemMenuImage.Width / 2, this.consumable.item.itemMenuImage.Height / 2, 0);
            }
        }
        public GearSlot(BodyPart _bodyPart, Equipment _equipment, Vector3 location)
            :this(_bodyPart, _equipment)
        {            
            this.location = location;
            pickupRange.Center = location - offset;
            pickupRange.Radius = 10;
        }
        public void SetLocation(Vector3 location)
        {
            this.location = location;
            pickupRange.Center = location - offset;
            pickupRange.Radius = 10;
        }
    }
}
