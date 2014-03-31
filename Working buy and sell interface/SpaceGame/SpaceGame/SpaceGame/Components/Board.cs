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
using STDatabase;
using SpaceGame.GameScreens;

namespace SpaceGame.Components
{
    public class Board
    {
        // database access
        public IDatabase dbs = new Database();
        public IDatabase dbf = new Fakedatabse();

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
        public int sessionId;

        private List<Tile> tiles;

        public Board()
        {
            tiles = new List<Tile>();
        }

        public void setSession(int session)
        {
            Console.WriteLine("Board session = " + session);
            sessionId = session;
        }
        /*
        public List<Tile> getTile()
        {
            return tiles;
        }
        */
       

        public void LoadContent(ContentManager content)
        {
            font1 = content.Load<SpriteFont>(@"Fonts\CourierNew");
            space = content.Load<Texture2D>(@"Textures\space");
            mercury = content.Load<Texture2D>(@"Textures\mercury");
            venus = content.Load<Texture2D>(@"Textures\venus");
            earth = content.Load<Texture2D>(@"Textures\earth");
            mars = content.Load<Texture2D>(@"Textures\mars");
            jupiter = content.Load<Texture2D>(@"Textures\jupiter");
            saturn = content.Load<Texture2D>(@"Textures\saturn");
            uranus = content.Load<Texture2D>(@"Textures\uranus");
            neptune = content.Load<Texture2D>(@"Textures\neptune");

            PlanetData();

        }

        public Texture2D textureReturn(string ele)
        {
            // this will convert the string from the database to the texture2d
            switch (ele)
            {
                case "space":
                    return space;
                case "mercury":
                    return mercury;
                case "venus":
                    return venus;
                case "earth":
                    return earth;
                case "mars":
                    return mars;
                case "jupiter":
                    return jupiter;
                case "saturn":
                    return saturn;
                case "uranus":
                    return uranus;
                case "neptune":
                    return neptune;
                default:
                    return space;
            }
        }

        public Boolean PlanetData()
        {


            // connects to the database, grabs the current session number then gets the planets from that list 

            // need to change this so that it will work on loaded session
            // need to change this to work propperly
            
            Console.WriteLine("session = " + sessionId);
            //tiles.Add(new Tile(space, new Planet("space", 0, "space")));
            List<Planetdata> sesPlanet = dbs.SessionWithPlanet(sessionId);
            Console.WriteLine("sesPlanet count =" + sesPlanet.Count);
            for (int i = 0; i < sesPlanet.Count; i++)
            {

                tiles.Add(new Tile(textureReturn(sesPlanet[i].File_loc), new Planet(sesPlanet[i].Name, sesPlanet[i].Planet_id, sesPlanet[i].File_loc)));

            }
            Console.WriteLine("tiles = " + tiles.Count);
            for (int i = 0; i < tiles.Count; i++)
            {
                Planet pl = tiles[i].getPlanet();
                pl.generateResource();

            }

            return true;
        }


        public void Draw(Game1 game, SpriteBatch spriteBatch, SpaceShip myShip)
        {
            ship = myShip;
            int k = 0;
            for (int x = 0; x < NumberofTilesWidth; x++)
            {
                spriteBatch.Draw(tiles.ElementAt(k).getTexture(), new Rectangle(50 + (x * Tile.TileWidth), 50 + (0 * Tile.TileHeight), Tile.TileWidth, Tile.TileHeight), Color.White);
                k++;
            }
            for (int y = 1; y < NumberofTilesHeight; y++)
            {
                spriteBatch.Draw(tiles.ElementAt(k).getTexture(), new Rectangle(50 + (10 * Tile.TileWidth), 50 + (y * Tile.TileHeight), Tile.TileWidth, Tile.TileHeight), Color.White);
                k++;
            }
            for (int x = 9; x >= 0; x--)
            {
                spriteBatch.Draw(tiles.ElementAt(k).getTexture(), new Rectangle(50 + (x * Tile.TileWidth), 50 + (10 * Tile.TileHeight), Tile.TileWidth, Tile.TileHeight), Color.White);
                k++;
            }
            for (int y = 9; y > 0; y--)
            {
                spriteBatch.Draw(tiles.ElementAt(k).getTexture(), new Rectangle(50 + (0 * Tile.TileWidth), 50 + (y * Tile.TileHeight), Tile.TileWidth, Tile.TileHeight), Color.White);
                k++;
            }

            string ourLocation = "We are at " + tiles.ElementAt(boardLocationToListLocation(ship.getShipLocation())).getPlanet().getName();
            Vector2 FontOrigin = font1.MeasureString(ourLocation) / 2;
            fontPosition = new Vector2(230, 125);
            spriteBatch.DrawString(font1, ourLocation, fontPosition, Color.White, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            List<Resource> resource = new List<Resource>();
            resource = tiles.ElementAt(boardLocationToListLocation(ship.getShipLocation())).getPlanet().getResourceList();
            int l = 0;
            for (int i = 0; i < resource.Count; i++)
            {
                string myResource = "Available Resources: " + resource[i].amount + " , " + resource[i].name + " for " + resource[i].getPrice() + " each!";
                fontPosition = new Vector2(230, 150 + l);
                spriteBatch.DrawString(font1, myResource, fontPosition, Color.Red, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                l = l + 25;
            }
            string Position = CorrentScorePosition(ship.getMoney());
            fontPosition = new Vector2(200, 740);
            spriteBatch.DrawString(font1, Position, fontPosition, Color.Yellow, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

            /*
            foreach (List<Resource> m in resource)
            {
                string myResource = "Available Resources: "+ m.Value+" " + m.Key.getName() + " for " + m.Key.getPrice()+" each";
                fontPosition = new Vector2(230, 150 + l);
                spriteBatch.DrawString(font1, myResource, fontPosition, Color.Red, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                l = l + 25;
            }
            */
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

        public List<Resource> getResourceList()
        {
            return tiles.ElementAt(boardLocationToListLocation(ship.getShipLocation())).getPlanet().getResourceList();
        }
        public void SaveBoard()
        {
            Console.WriteLine("Save Board activated");
            DatabasePopulate dbp = new DatabasePopulate();
            
            dbp.saveSession(tiles, ship);
        }

        public string CorrentScorePosition(int correntMoney)
        {
            int pos;
            int top;
            string command;
            List<Userdata> scores = dbs.getHightScore();
            if (scores.Count >= 1)
            {
                top = scores[0].HighScore;
                pos = top - correntMoney;
                if (pos >= 0)
                {
                    command = String.Format("You are {0} ahead of all the other Captians!", pos);
                }
                else
                {
                    command = String.Format("You need {0} to be the top Captian!", pos);
                }
            }
            else
            {
                command = "My Captian! You have no competition you are ahead of all others!";
            }


            return command;
        }
    }
}

