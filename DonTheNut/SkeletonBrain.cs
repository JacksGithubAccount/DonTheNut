using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonTheNut
{
    class SkeletonBrain : CharacterStats
    {
        private Texture2D skeletonIdle;
        private Texture2D skeletonHit;
        private Texture2D skeletonDyingClose;
        private Texture2D skeletonAttackSwing;

        private ContentManager Content;

        bool pathComplete1 = false;
        bool pathComplete2 = true;               

        
        GameTime gametime;
        public SkeletonBrain(ContentManager Content)
        {
            this.Content = Content;
            PositionLocation = new Vector3(200, 100, 0);
            PositionOffset = new Vector3(10, 15, 0);
            faction = Faction.Undead;
            //skeletonSprite = new AnimatedSprite(skeletonIdle, 1, 3);            
            //bones = new CharacterStats(2);
            HamsterBall = new BoundingSphere();            
            HamsterBall.Radius = 10;
            outsidetheHamsterBall = new BoundingSphere();
            HamsterBall.Radius = 11;
            moveSpeed = 40f;
            defaultMoveSpeed = 40f;
            walkSpeed = 40f;
            runSpeed = 60f;
            blockSpeed = 20f;
            lineofsightBall = new BoundingSphere();
            lineofsightBall.Radius = 120;
            lineofsightBall.Center = PositionLocation;
        }
        public SkeletonBrain(ContentManager Content, float positionX, float positionY) 
            : this(Content)
        {
            PositionLocation = new Vector3(positionX, positionY, 0);
        }
        public void setToIdle()
        {
            //moveSpeed = defaultMoveSpeed;
            moveDirection = new Vector3(0, 0, 0);
            if (!idleCheck && animatedSprite.Columns <= 3 && animatedSprite.GetCurrentFrame() > 3)
            {
                animatedSprite.resetFrame();
                idleCheck = true;
            }
            animatedSprite.setTexture(skeletonIdle);
            animatedSprite.setRowColumn(1, 3);
        }
        public void MoveToLocation(Vector3 destination)
        {
            if (!isDead && !attackCheck && !gotHitCheck)
            {
                isChase = true;
                if (!walkCheck)
                {
                    
                    if (blockCheck)
                    {
                        moveSpeed = blockSpeed;
                        animatedSprite.setNewAnimation(spriteBlockWalk, 1, 6);
                    }
                    else
                    {
                        moveSpeed = defaultMoveSpeed;
                        animatedSprite.resetFrame();
                        animatedSprite.setTexture(spriteWalk);
                        animatedSprite.setRowColumn(1, 6);
                    }
                    walkCheck = true;
                }                
                if (gametime != null)
                {
                    if (PositionLocation.X > destination.X)
                    {
                        moveDirection.X = -1;
                        PositionLocation.X -= moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (PositionLocation.X < destination.X)
                    {
                        moveDirection.X = 1;
                        PositionLocation.X += moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
                    }
                    if (PositionLocation.Y > destination.Y)
                    {
                        moveDirection.Y = -1;
                        PositionLocation.Y -= moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (PositionLocation.Y < destination.Y)
                    {
                        moveDirection.Y = 1;
                        PositionLocation.Y += moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
                    }
                }
            }
            
        }        
        public void Attack()
        {
            if (!attackCheck && !gotHitCheck)
            {
                isChase = false;
                attackCheck = true;
                animatedSprite.setNewAnimation(skeletonAttackSwing, 1, 6);
                moveDirection = new Vector3(0, 0, 0);
                animatedSprite.waitToFinish = true;
            }
            
        }
        public void Load()
        {
            skeletonIdle = Content.Load<Texture2D>("SkeletonIdle");
            skeletonHit = Content.Load<Texture2D>("SkeletonHit");
            skeletonDyingClose = Content.Load<Texture2D>("SkeletonDyingClose");
            skeletonAttackSwing = Content.Load<Texture2D>("SkeletonAttackSwing");
            spriteWalk = Content.Load<Texture2D>("SkeletonWalk");
            spriteRun = Content.Load<Texture2D>("enemies/skeleton/SkeletonRun");
            spriteBlock = Content.Load<Texture2D>("enemies/skeleton/SkeletonBlock");
            spriteBlockWalk = Content.Load<Texture2D>("enemies/skeleton/SkeletonBlockWalk");
            healthBar = Content.Load<Texture2D>("healthbar");
            healthBarOuter = Content.Load<Texture2D>("healthbarouter");
            animatedSprite = new AnimatedSprite(skeletonIdle, 1, 3);
            animatedSprite.updatesPerFrame = 12;
            font = Content.Load<SpriteFont>("font");
            hamsterBox = new Rectangle((int)PositionLocation.X, (int)PositionLocation.Y, animatedSprite.Texture.Width / animatedSprite.Columns, animatedSprite.Texture.Height);
        }
        public void Update(GameTime gameTime)
        {
            gametime = gameTime;
            healthpercentdisplay = 16 * getHealthPercent();
            HamsterBall.Center = PositionLocation - PositionOffset;
            outsidetheHamsterBall.Center = PositionLocation - PositionOffset;
            lineofsightBall.Center = PositionLocation - PositionOffset;
            if (!isDead)
            {
                if (moveDirection.X == -1)
                    isLeft = true;
                else if (moveDirection.X == 1)
                    isLeft = false;
                if (gotHitCheck == true)
                {
                    if (attackCheck)
                        animatedSprite.waitToFinish = false;
                    attackCheck = false;
                    if (!animatedSprite.waitToFinish)
                        animatedSprite.resetFrame();
                    animatedSprite.setTexture(skeletonHit);
                    animatedSprite.setRowColumn( 1, 6);
                    animatedSprite.waitToFinish = true;
                }

                if (animatedSprite.waitToFinish == false && distance.X < 30 && distance.X > -30 && distance.Y < 30 && distance.Y > -30)
                    attackCheck = false;//Attack();
                if (pathbools[0])
                {
                    MoveToLocation(new Vector3(PositionLocation.X, destinationfloats[0], 0));
                    if (PositionLocation.Y <= destinationfloats[0])
                    {
                        pathbools[0] = false;
                    }
                }
                if (attackCheck && animatedSprite.GetCurrentFrame() == 3)
                {
                    attackBall.Radius = 21;
                    if (isLeft)
                        attackBall.Center = PositionLocation - new Vector3(30, 20, 0);
                    else
                        attackBall.Center = PositionLocation + new Vector3(3, -20, 0);
                }
                if(moveDirection.Y == 0 && moveDirection.X == 0)
                    walkCheck = false;
                if (isChase || gotHitCheck || walkCheck)
                    idleCheck = false;
                if (isChase)
                {
                    if (!behaviorConfirmed)
                    {
                        Random random = new Random();
                        behaviorbools[random.Next(0, 9)] = true;
                        behaviorConfirmed = true;
                    }
                    if (behaviorbools[0])
                    {
                        runCheck = true;//
                        animatedSprite.setTexture(spriteRun);
                        animatedSprite.setRowColumn(1, 6);
                        moveSpeed = runSpeed;
                        if (attackCheck)
                        {
                            behaviorConfirmed = false;
                            walkCheck = false;
                            runCheck = false;
                        }
                    }else if (behaviorbools[1])
                    {

                    }
                    else if (behaviorbools[2])
                    {
                        blockCheck = true;
                        //animatedSprite.setNewAnimation(spriteBlock, 1, 3);
                    }
                    else
                    {
                        behaviorConfirmed = false; //test, turn off when done
                    }

                }
                if (!animatedSprite.isRunning())// && !isChase
                {                    
                    animatedSprite.waitToFinish = false;
                    attackCheck = false;
                    dealtDamage = false;
                    isChase = false;
                    gotHitCheck = false;
                    blockCheck = false;
                    behaviorConfirmed = false;
                    setToIdle();
                }

                if (checkHealth() <= 0)
                {
                    isDead = true;
                    animatedSprite.setNewAnimation(skeletonDyingClose, 1, 6);
                    animatedSprite.waitToFinish = true;
                    //HamsterBall.Radius = 0;
                    //HamsterBall.Deconstruct(out HamsterBall.Center,out HamsterBall.Radius);
                }
                if (!attackCheck)
                {
                    attackBall.Radius = 0;
                    attackBall.Center = PositionLocation - new Vector3(10, 15, 0);
                }
            }
            animatedSprite.Update();
        }
        
        public void Draw(SpriteBatch _spriteBatch)
        {
                animatedSprite.Draw(_spriteBatch, PositionLocation, isLeft);

            //_spriteBatch.Begin();
            _spriteBatch.Draw(healthBarOuter, new Vector2((int)PositionLocation.X + 14, (int)PositionLocation.Y - 5), Color.White);
            _spriteBatch.Draw(healthBar, new Vector2((int)PositionLocation.X + 17, (int)PositionLocation.Y - 5), new Rectangle(0, 0, (int)healthpercentdisplay, 48), Color.White);
            //_spriteBatch.DrawString(font, checkHealth().ToString(), new Vector2(100, 100), Color.White);
            //_spriteBatch.End();
        }
    }
}
