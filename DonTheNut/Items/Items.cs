using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonTheNut
{
    class Items
    {
        public int ID;
        public string name;
        public int monetaryValue;
        public int stack;
        //public int quantity;
        public ItemType itemType;
        public ItemSubtype itemSubType;
        public Texture2D itemMenuImage;
        public Texture2D itemMainGameImage;
        public Texture2D itemToolBarImage;
        public bool isEquipped = false;
        public string shortDescription = "short description place holder";
        public string longDescription = "long description place holder";
        public Items(int idnumber, string name, int money, int stackableAmount, ItemSubtype subType, Texture2D menuImage, Texture2D mainGameImage, Texture2D toolBarImage)
        {
            ID = idnumber;
            this.name = name;
            monetaryValue = money;
            stack = stackableAmount;            
            itemSubType = subType;
            itemMenuImage = menuImage;
            itemMainGameImage = mainGameImage;
            itemToolBarImage = toolBarImage;
        }
        public Items(Items item)
        {
            ID = item.ID;
            this.name = item.name;
            monetaryValue = item.monetaryValue;
            stack = item.stack;
            itemSubType = item.itemSubType;
            itemMenuImage = item.itemMenuImage;
            itemMainGameImage = item.itemMainGameImage;
            itemToolBarImage = item.itemToolBarImage;
        }
    }
}
