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
            PositionByPixel.X = 50 + Tile.TileWidth/4;
            PositionByPixel.Y = 50 + Tile.TileHeight/4;
            mTexture = content.Load<Texture2D>(@"Textures\background");
        }


        //Draw the sprite to the screen
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mTexture, PositionByPixel, new Rectangle(0, 0, Tile.TileWidth/2, Tile.TileHeight/2), Color.White);
        }

        // Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.
        public void Update(GameTime time)
        {
            
        }
    }
}

