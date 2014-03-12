using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
{
    class Object
    {
        int db_id;
        string Title;
        string Graphic;
        private Texture2D mTexture;
        public Vector2 PositionByTile = Vector2.Zero;
        public Vector2 PositionByPixel;
        Vector2 scale = new Vector2(0.5f, 0.5f);


        public void LoadContent(ContentManager content, string name)
        {
            PositionByPixel.X = 50 + BoardLocation.TileWidth/4;
            PositionByPixel.Y = 50 + BoardLocation.TileHeight/4;
            mTexture = content.Load<Texture2D>(@"Textures\spaceship");
        }


        //Draw the sprite to the screen
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mTexture, PositionByPixel, new Rectangle(0, 0, BoardLocation.TileWidth/2, BoardLocation.TileHeight/2), Color.White, 0.0f, scale, 0.0f, SpriteEffects.None, 0);
        }

        // Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.
        public void Update(GameTime time, Vector2 theSpeed, Vector2 theDirection)
        {
            
        }
    }
}

