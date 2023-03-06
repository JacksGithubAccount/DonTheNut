using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DonTheNut
{
    class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        private Texture2D defaultTexture;
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        public bool waitToFinish { get; set; }
        public bool holdFrame { get; set; }
        private Vector2 spritePositionOffset;
        private Vector2 isLeftOffset;


        private int currentUpdate;
        public int updatesPerFrame = 6;//Higher means slower.

    public AnimatedSprite(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            defaultTexture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }

        public bool isRunning()
        {
            if (currentFrame == totalFrames -1)
                return false;
            else return true;
        }
        public void resetFrame()
        {
            currentFrame = 0;
        }
        public void setFrame(int frameNumber)
        {
            currentFrame = frameNumber;
        }
        public int GetCurrentFrame()
        {
            return currentFrame;
        }
        public void setRowColumn(int row, int column)
        {
            Rows = row;
            Columns = column;
            totalFrames = Rows * Columns;
        }
        public void setNewAnimation(Texture2D texture, int row, int column)
        {
            resetFrame();
            setRowColumn(row, column);
            setTexture(texture);
        }
        public void setTexture(Texture2D texture)
        {
            Texture = texture;
            if (texture.Width > defaultTexture.Width && texture.Height > defaultTexture.Height)
            {
                spritePositionOffset = new Vector2(15, 17);
                isLeftOffset = new Vector2(20, 0);
            }
            else
            {
                spritePositionOffset = new Vector2(0, 0);
                isLeftOffset = new Vector2(0, 0);
            }
        }
        public void Update()
        {
            currentUpdate++;
            if (currentUpdate == updatesPerFrame)
            {
                currentUpdate = 0;
                if (waitToFinish)
                {
                    if (currentFrame != totalFrames -1)
                        currentFrame++;
                }
                else
                {
                    currentFrame++;
                    if (currentFrame == totalFrames -1 || currentFrame == totalFrames)
                        resetFrame();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector3 location,bool isLeft)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;
            var effects = SpriteEffects.FlipHorizontally;
            
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            //Rectangle destinationRectangle = new Rectangle((int)location.X - (int)spritePositionOffset.X, (int)location.Y - (int)spritePositionOffset.Y, width, height);

            if (isLeft)
            {
                Rectangle destinationRectangle = new Rectangle((int)location.X - (int)spritePositionOffset.X - (int)isLeftOffset.X, (int)location.Y - (int)spritePositionOffset.Y, width, height);
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, effects, 1f);
            }
            if (!isLeft)
            {
                Rectangle destinationRectangle = new Rectangle((int)location.X - (int)spritePositionOffset.X, (int)location.Y - (int)spritePositionOffset.Y, width, height);
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);                                   
            }
        }
    }
}
