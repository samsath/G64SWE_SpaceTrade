using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
{
    class Board
    {
        SpriteFont font1;
        Vector2 fontPosition;
        Texture2D space;
        Texture2D mercury;
        Texture2D venus;
        Texture2D earth;
        Texture2D mars;
        Texture2D jupiter;
        Texture2D saturn;
        Texture2D uranus;
        Texture2D neptune;
        public const int NumberofTilesWidth = 11;
        public const int NumberofTilesHeight = 11;
        Random random = new Random();
        SpaceShip ship;

        private List<Tile> tiles = new List<Tile>();

        public void LoadContent(ContentManager content)
        {
            font1 = content.Load<SpriteFont>(@"Font\CourierNew");
            space = content.Load<Texture2D>(@"Textures\space");
            mercury = content.Load<Texture2D>(@"Textures\mercury");
            venus = content.Load<Texture2D>(@"Textures\venus");
            earth = content.Load<Texture2D>(@"Textures\earth");
            mars = content.Load<Texture2D>(@"Textures\mars");
            jupiter = content.Load<Texture2D>(@"Textures\jupiter");
            saturn = content.Load<Texture2D>(@"Textures\saturn");
            uranus = content.Load<Texture2D>(@"Textures\uranus");
            neptune = content.Load<Texture2D>(@"Textures\neptune");

            List<Tile> tempList = new List<Tile>();
            for (int i = 0; i < 4; i++)
            {
                tempList.Add(new Tile(space, new Planet("space")));
                tempList.Add(new Tile(space, new Planet("space")));
                tempList.Add(new Tile(mercury, new Planet("mercury")));
                tempList.Add(new Tile(venus, new Planet("venus")));
                tempList.Add(new Tile(earth, new Planet("earth")));
                tempList.Add(new Tile(mars, new Planet("mars")));
                tempList.Add(new Tile(jupiter, new Planet("jupiter")));
                tempList.Add(new Tile(saturn, new Planet("saturn")));
                tempList.Add(new Tile(uranus, new Planet("uranus")));
                tempList.Add(new Tile(neptune, new Planet("neptune")));
            }

            // randomise the list of planets and space
            while (tempList.Count > 0)
            {
                int index = random.Next(tempList.Count);
                tiles.Add(tempList[index]);
                tempList.RemoveAt(index);
            }
        }

        public void Draw(Game1 game, SpriteBatch spriteBatch, SpaceShip myShip)
        {
            ship = myShip;
            int k = 0;
            for (int x = 0; x < NumberofTilesWidth; x++)
            {
                spriteBatch.Draw(tiles.ElementAt(k).getTexture(), new Rectangle(50 + (x * Tile.TileWidth), 50 + (0 * Tile.TileHeight), Tile.TileWidth, Tile.TileHeight), Color.White); k++;
            }
            for (int y = 1; y < NumberofTilesHeight; y++)
            {
                spriteBatch.Draw(tiles.ElementAt(k).getTexture(), new Rectangle(50 + (10 * Tile.TileWidth), 50 + (y * Tile.TileHeight), Tile.TileWidth, Tile.TileHeight), Color.White); k++;
            }
            for (int x = 9; x >= 0; x--)
            {
                spriteBatch.Draw(tiles.ElementAt(k).getTexture(), new Rectangle(50 + (x * Tile.TileWidth), 50 + (10 * Tile.TileHeight), Tile.TileWidth, Tile.TileHeight), Color.White); k++;
            }
            for (int y = 9; y > 0; y--)
            {
                spriteBatch.Draw(tiles.ElementAt(k).getTexture(), new Rectangle(50 + (0 * Tile.TileWidth), 50 + (y * Tile.TileHeight), Tile.TileWidth, Tile.TileHeight), Color.White); k++;
            }

            string ourLocation = "We are at " + tiles.ElementAt(boardLocationToListLocation(ship.getShipLocation())).getPlanet().getName();
            Vector2 FontOrigin = font1.MeasureString(ourLocation) / 2;
            fontPosition = new Vector2(230, 125);
            spriteBatch.DrawString(font1, ourLocation, fontPosition, Color.Blue, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            List<Resource> resource = new List<Resource>();
            resource = tiles.ElementAt(boardLocationToListLocation(ship.getShipLocation())).getPlanet().getResourceList();
            int l = 0;
            foreach (Resource m in resource)
            {
                string myResource = "Available Resources: " + m.getName() + " for " + m.getPrice();
                fontPosition = new Vector2(230, 150 + l);
                spriteBatch.DrawString(font1, myResource, fontPosition, Color.Red, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                l = l + 25;
            }
            //printDiceRolled = "Move remaining is: " + diceRemaining.ToString();
            //fontPosition = new Vector2(100, 35);
            //spriteBatch.DrawString(font1, printDiceRolled, fontPosition, Color.Red, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }

        public int boardLocationToListLocation(Vector2 location)
        {
            if (location.Y == 0) return (int)location.X;
            if (location.X == 10 && location.Y > 0) return ((int)location.X + (int)location.Y);
            if (location.Y == 10 && location.X < 10) return (10 - (int)location.X) + 2 * 10;
            return (10 - (int)location.Y) + 3 * 10;
        }
    }
}
