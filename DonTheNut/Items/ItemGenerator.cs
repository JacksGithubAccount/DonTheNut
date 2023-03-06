using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonTheNut
{
    class ItemGenerator
    {
        ContentManager content;
        Texture2D healingPotionToolTexture;
        Texture2D healingPotionGameTexture;
        Texture2D healingPotionMenuTexture;
        Texture2D RockPunchToolTexture;
        Texture2D RockPunchMenuTexture;
        Texture2D RockPunchGameTexture;
        Texture2D handAxeToolImage;
        Texture2D handAxeGameImage;
        Texture2D handAxeMenuImage;
        public ItemGenerator(ContentManager content)
        {
            this.content = content;
        }
        public void Load()
        {
            healingPotionToolTexture = content.Load<Texture2D>("items/consumables/HealingPotion/HealingPotionToolBar");
            healingPotionMenuTexture = content.Load<Texture2D>("items/consumables/HealingPotion/HealingPotionMenu");
            healingPotionGameTexture = content.Load<Texture2D>("items/consumables/HealingPotion/HealingPotionMainGame");
            RockPunchToolTexture = content.Load<Texture2D>("items/consumables/RockPunch/RockPunchTool");
            RockPunchMenuTexture = content.Load<Texture2D>("items/consumables/RockPunch/RockPunchMenu");
            RockPunchGameTexture = content.Load<Texture2D>("items/consumables/RockPunch/RockPunchMainGame");
            handAxeToolImage = content.Load<Texture2D>("items/equipment/HandAxe/HandAxeImageTool");
            handAxeMenuImage = content.Load<Texture2D>("items/equipment/HandAxe/HandAxeImageMenu");
            handAxeGameImage = content.Load<Texture2D>("items/equipment/HandAxe/HandAxeImageMainGame");
        }
        public void GenerateItems(ref List<Items> allItems)
        {
            Load();
            allItems.Add(new Equipment(0001, "Nothing", 1, 0, ItemSubtype.Nothing, healingPotionMenuTexture, healingPotionGameTexture, healingPotionToolTexture, 0, 0, 0, 0, 0, 0, 0, 0));
            allItems.Add(new Consumables(1001, "Healing Potion", 1, 20, ItemSubtype.Potion, healingPotionMenuTexture, healingPotionGameTexture, healingPotionToolTexture, 1, 10, ItemEffect.Health));
            allItems.Add(new Consumables(1002, "Super Healing Potion", 1, 20, ItemSubtype.Potion, healingPotionMenuTexture, healingPotionGameTexture, healingPotionToolTexture, 1, 30, ItemEffect.Health));
            allItems.Add(new Consumables(1003, "Mega Healing Potion", 1, 20, ItemSubtype.Potion, healingPotionMenuTexture, healingPotionGameTexture, healingPotionToolTexture, 1, 70, ItemEffect.Health));
            allItems.Add(new Equipment(2001, "Hand Axe", 1, 1, ItemSubtype.Weapon, handAxeMenuImage, handAxeGameImage, handAxeToolImage, 10, 10, 0, 0, 0, 0, 0, 0));
            allItems.Add(new Consumables(1004, "Giga Healing Potion", 1, 20, ItemSubtype.Potion, healingPotionMenuTexture, healingPotionGameTexture, healingPotionToolTexture, 1, 100, ItemEffect.Health));
            allItems.Add(new Consumables(1005, "Ultra Healing Potion", 1, 20, ItemSubtype.Potion, healingPotionMenuTexture, healingPotionGameTexture, healingPotionToolTexture, 1, 130, ItemEffect.Health));
            allItems.Add(new Equipment(3001, "Clothes", 2, 1, ItemSubtype.Body, healingPotionMenuTexture, healingPotionGameTexture, healingPotionToolTexture, 5, 0, 0, 3, 2, 0, 10, 5));
            allItems.Add(new Equipment(4001, "Hat", 1, 1, ItemSubtype.Head, healingPotionMenuTexture, healingPotionGameTexture, healingPotionToolTexture, 1, 0, 0, 1, 1, 0, 3, 1));
            allItems.Add(new Equipment(5001, "Gloves", 1, 1, ItemSubtype.Arms, healingPotionMenuTexture, healingPotionGameTexture, healingPotionToolTexture, 1, 0, 0, 1, 0, 0, 3, 1));
            allItems.Add(new Equipment(6001, "Shoes", 2, 1, ItemSubtype.Legs, healingPotionMenuTexture, healingPotionGameTexture, healingPotionToolTexture, 3, 0, 0, 2, 2, 0, 3, 7));
            allItems.Add(new Consumables(1006, "Mana Potion", 50, 10, ItemSubtype.Potion, healingPotionMenuTexture, healingPotionGameTexture, healingPotionToolTexture, 1, 20, ItemEffect.Mana));
            allItems.Add(new Consumables(1007, "Mana-er Potion", 50, 10, ItemSubtype.Potion, healingPotionMenuTexture, healingPotionGameTexture, healingPotionToolTexture, 1, 50, ItemEffect.Mana));
            allItems.Add(new Consumables(9999, "Super Mega Ultra Potion", 1, 1, ItemSubtype.Potion, healingPotionMenuTexture, healingPotionGameTexture, healingPotionToolTexture, 1, 999, ItemEffect.Health, ItemEffect.Mana, ItemEffect.Stamina));
            allItems.Add(new Equipment(9001, "Wooden Arrow", 1, 99, ItemSubtype.Ammunition, healingPotionMenuTexture, healingPotionGameTexture, healingPotionToolTexture, 0.1, 1, 0, 0, 0, 0, 0, 0));
            allItems.Add(new Consumables(1101, "Rock Punch", 30, 20, ItemSubtype.Bomb, RockPunchMenuTexture, RockPunchGameTexture, RockPunchToolTexture, 1, 20, ItemEffect.Health));
        }        
    }
}
