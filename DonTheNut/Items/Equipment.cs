using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonTheNut
{
    class Equipment : Items
    {        
        public double weight;
        public int pAttack;
        public int mAttack;
        public int pDefense;
        public int mDefense;
        public int fireRes;
        public int iceRes;
        public int lightningRes;      

        public Equipment(int idnumber, string name, int money, int stackableAmount, ItemSubtype subType, Texture2D menuImage, Texture2D mainGameImage, Texture2D toolBarImage, double weight, int physAtk, int magAtk, int physDef, int magDef, int fRes, int iRes, int lRes)
            :base(idnumber,name,money,stackableAmount,subType,menuImage,mainGameImage,toolBarImage)
        {
            itemType = ItemType.Equipment;
            this.weight = weight;
            pAttack = physAtk;
            mAttack = magAtk;
            pDefense = physDef;
            mDefense = magDef;
            fireRes = fRes;
            iceRes = iRes;
            lightningRes = lRes;
        }
    }
}
