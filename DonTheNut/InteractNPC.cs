using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonTheNut
{
    class InteractNPC
    {
        MC emcee;
        SkeletonBrain brainy;
        List<ItemSlot> droppedItems;

        public InteractNPC(MC mc, SkeletonBrain skelly, List<ItemSlot> droppedItemsList)
        {
            emcee = mc;
            brainy = skelly;
            droppedItems = droppedItemsList;
        }
        private void ColideAndBump(CharacterStats body, CharacterStats secondBody)
        {
            body.moveSpeed = 0f;
            secondBody.moveSpeed = 0f;
            if (body.moveDirection.X == 1)
            {
                if (body.PositionLocation.X < secondBody.HamsterBall.Center.X)
                    body.PositionLocation.X += -2;
                else
                    body.PositionLocation.X += 2;
            }
            else if (body.moveDirection.X == -1)
            {
                if (body.PositionLocation.X > secondBody.HamsterBall.Center.X)
                    body.PositionLocation.X += 2; 
                else
                    body.PositionLocation.X += -2;
            }
            if (body.moveDirection.Y == 1)
            {
                if (body.PositionLocation.Y < secondBody.HamsterBall.Center.Y)
                    body.PositionLocation.Y += -2;
            }
            if (body.moveDirection.Y == -1)
            {
                if (body.PositionLocation.Y > secondBody.HamsterBall.Center.Y)
                    body.PositionLocation.Y += 2;
            }
        }
        public void LineofSightDetection(MC playerCharacter, SkeletonBrain npc)
        {
            npc.distance = playerCharacter.PositionLocation - npc.PositionLocation;
            npc.distance.X = Math.Abs(npc.distance.X);
            npc.distance.Y = Math.Abs(npc.distance.Y);
            npc.distance.Z = Math.Abs(npc.distance.Z);

            if (npc.lineofsightBall.Intersects(playerCharacter.HamsterBall) && !npc.outsidetheHamsterBall.Intersects(playerCharacter.HamsterBall))
            {
                if(!npc.pathbools[0])
                    npc.MoveToLocation(playerCharacter.PositionLocation);
            }
            else
            {
                if(!npc.isDead)
                    npc.setToIdle();
            }
        }
        public bool IsInRangeOfItem(CharacterStats player, ItemSlot droppedItem)
        {
            return player.HamsterBall.Intersects(droppedItem.pickupRange);
        }
        public void actionUpdate(MC player)
        {
            if (player.actionCheck)
            {
                for (int i = 0; i < droppedItems.Count; i++)
                {
                    if (IsInRangeOfItem(player, droppedItems[i]))
                    {
                        player.AddToInventory(droppedItems[i].item, droppedItems[i].quantity);
                        droppedItems.Remove(droppedItems[i]);
                    }
                }
            }
        }
        public void ThrownItemCollision(Consumables thrownItem, CharacterStats guyWhoGotHit)
        {
            if (thrownItem.sphereOfInfluence.Intersects(guyWhoGotHit.HamsterBall))
            {
                guyWhoGotHit.gotHitCheck = true;
                guyWhoGotHit.takenDamage = true;
                //guyWhoGotHit.animatedSprite.setFrame(1);
                thrownItem.useItem(guyWhoGotHit);
            }
        }
        public void CollisionCourse(CharacterStats firstBody, CharacterStats secondBody)
        {
            //body collision detection            
            if (firstBody.HamsterBall.Intersects(secondBody.HamsterBall))
            {
                bool issamefaction = false;

                if (!secondBody.isDead)
                {
                    ColideAndBump(firstBody, secondBody);
                    if (firstBody.faction == secondBody.faction && firstBody.faction != Faction.Playable && secondBody.faction != Faction.Playable)
                    {
                        //issamefaction = true;
                        SkeletonBrain tempSkel;

                        if (firstBody.distance.X > secondBody.distance.X)
                        {
                            tempSkel = (SkeletonBrain)firstBody;
                            if (firstBody.PositionLocation.X > secondBody.PositionLocation.X)
                            {
                                if (!tempSkel.pathbools[0])
                                    tempSkel.destinationfloats[0] = tempSkel.PositionLocation.Y + 20;
                            }
                            else
                            {
                                if (!tempSkel.pathbools[0])
                                    tempSkel.destinationfloats[0] = tempSkel.PositionLocation.Y - 20;
                            }
                            tempSkel.pathbools[0] = true;
                        }
                        else
                        {
                            tempSkel = (SkeletonBrain)secondBody;
                            if (firstBody.PositionLocation.X > secondBody.PositionLocation.X)
                            {
                                //if (!tempSkel.pathbools[0])
                                //tempSkel.destinationfloats[0] = tempSkel.PositionLocation.X + 4;
                            }
                            else
                            {
                                //if (!tempSkel.pathbools[0])
                                //tempSkel.destinationfloats[0] = tempSkel.PositionLocation.X - 4;
                            }
                            //tempSkel.pathbools[0] = true;
                        }


                        if (firstBody.distance.Y > secondBody.distance.Y)
                        {
                            tempSkel = (SkeletonBrain)firstBody;
                            if (firstBody.PositionLocation.Y > secondBody.PositionLocation.Y)
                            {
                                //tempSkel.MoveToLocation(tempSkel.PositionLocation + new Vector3(0, 4, 0));
                            }
                            else
                            {
                                //tempSkel.MoveToLocation(tempSkel.PositionLocation + new Vector3(0, -4, 0));
                            }
                        }
                        else
                        {
                            tempSkel = (SkeletonBrain)secondBody;
                            if (firstBody.PositionLocation.Y > secondBody.PositionLocation.Y)
                            {
                                //tempSkel.MoveToLocation(tempSkel.PositionLocation + new Vector3(0, -4, 0));
                            }
                            else if (firstBody.PositionLocation.Y == secondBody.PositionLocation.Y)
                            {
                                //tempSkel.MoveToLocation(tempSkel.PositionLocation + new Vector3(1, 0, 0));
                            }
                            else
                            {
                                //tempSkel.MoveToLocation(tempSkel.PositionLocation + new Vector3(0, 4, 0));
                            }
                        }
                    }
                    else
                        issamefaction = false;
                    //if(issamefaction)
                    //tempSkel.MoveToLocation(new Vector3(tempSkel.destinationfloats[0], 0, 0));
                    //ColideAndBump(secondBody);
                    //if(firstBody.PositionLocation > secondBody.HamsterBall.Center)
                }
                else
                {
                    firstBody.DefaultWalkSpeed();
                }
            }
            else
                firstBody.moveSpeed = firstBody.defaultMoveSpeed;

            //attack collision detection
            AttackCollision(secondBody, emcee);
            AttackCollision(emcee, secondBody);
            
        }
        public void AttackCollision(CharacterStats attacker, CharacterStats attacked)
        {
            if (attacker.attackCheck)
            {
                if (attacker.attackBall.Intersects(attacked.HamsterBall))
                {
                    attacker.attackHitCheck = true;
                    attacked.gotHitCheck = true;
                    //if (!Wheelhouse.takenDamage)
                    if (!attacker.dealtDamage)
                    {
                        if (!attacked.isInvulnerable)
                        {
                            if (attacked.blockCheck)
                            {
                                attacked.calcTakenDamage(10, 5);
                                attacked.takenDamage = true;
                                attacked.consumeStamina(10);
                                if(!attacked.hasStamina())
                                {
                                    attacked.guardBreak = true;
                                }
                            }
                            else
                            {
                                attacked.calcTakenDamage(10, 0);
                                attacked.takenDamage = true;
                            }

                        }
                        attacker.dealtDamage = true;
                        //Wheelhouse.takenDamage = true;
                    }

                }
                else
                {
                    attacked.gotHitCheck = false;
                    attacker.attackHitCheck = false;
                }
            }
        }
    }
}
