using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;

namespace OP_Engine.Tiles
{
    public class World : IDisposable
    {
        #region Variables

        public long ID;
        public string? Name;
        public string? Type;
        public bool Visible;

        public List<Map> Maps = [];

        public List<string> Names = [];
        private List<string> Names1 = [];
        private List<string> Names2 = [];

        #endregion

        #region Constructor

        public World()
        {
            
        }

        #endregion

        #region Methods

        public virtual void Init()
        {
            LoadNames();
        }

        public virtual void Update()
        {
            int count = Maps.Count;
            for (int i = 0; i < count; i++)
            {
                Maps[i].Update();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            if (Visible)
            {
                int count = Maps.Count;
                for (int i = 0; i < count; i++)
                {
                    Maps[i].Draw_Layers(spriteBatch, resolution);
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible)
            {
                int count = Maps.Count;
                for (int i = 0; i < count; i++)
                {
                    Maps[i].Draw_Layers(spriteBatch, resolution, color);
                }
            }
        }

        public virtual Map? GetMap(long id)
        {
            int count = Maps.Count;
            for (int i = 0; i < count; i++)
            {
                Map existing = Maps[i];
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual Map? GetMap(string name)
        {
            int count = Maps.Count;
            for (int i = 0; i < count; i++)
            {
                Map existing = Maps[i];
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual void LoadNames()
        {
            LoadNames1();
            LoadNames2();

            int nameCount1 = Names1.Count;
            for (int i = 0; i < nameCount1; i++)
            {
                string first = Names1[i];

                int nameCount2 = Names2.Count;
                for (int j = 0; j < nameCount2; j++)
                {
                    string second = Names2[j];

                    if (first != second &&
                        first[first.Length - 1] != second[0])
                    {
                        Names.Add(first + second);
                    }
                }
            }
        }

        public virtual void LoadNames1()
        {
            Names1 =
            [
                "Ac",
                "Al",
                "Amber",
                "Angel",
                "Apple",
                "Arrow",
                "Autumn",
                "Bare",
                "Basin",
                "Bay",
                "Beach",
                "Bear",
                "Bell",
                "Ber",
                "Black",
                "Bleak",
                "Blind",
                "Bone",
                "Boulder",
                "Bre",
                "Bridge",
                "Brine",
                "Brittle",
                "Bronze",
                "Castle",
                "Cave",
                "Chill",
                "Clay",
                "Clear",
                "Cliff",
                "Cloud",
                "Cold",
                "Crag",
                "Crow",
                "Crystal",
                "Curse",
                "Dark",
                "Dawn",
                "Dead",
                "Deep",
                "Deer",
                "Demon",
                "Dew",
                "Dim",
                "Dire",
                "Dirt",
                "Dog",
                "Dragon",
                "Dry",
                "Dub",
                "Dusk",
                "Dust",
                "Eagle",
                "Earth",
                "East",
                "Ebon",
                "Edge",
                "Elder",
                "Ember",
                "Ever",
                "Fair",
                "Fall",
                "False",
                "Far",
                "Fay",
                "Fear",
                "Flame",
                "Flat",
                "Frey",
                "Frost",
                "Ghost",
                "Glimmer",
                "Gloom",
                "Gold",
                "Grass",
                "Gray",
                "Green",
                "Grim",
                "Grime",
                "Hazel",
                "Heart",
                "High",
                "Hollow",
                "Honey",
                "Hound",
                "Ice",
                "Iron",
                "Kil",
                "Knight",
                "Lagoon",
                "Lake",
                "Lan",
                "Lang",
                "Last",
                "Light",
                "Lime",
                "Little",
                "Lost",
                "Mad",
                "Mage",
                "Maple",
                "Mid",
                "Might",
                "Mill",
                "Mist",
                "Moon",
                "Moss",
                "Mud",
                "Mute",
                "Myth",
                "Never",
                "New",
                "Night",
                "Nor",
                "North",
                "Oaken",
                "Ocean",
                "Old",
                "Ox",
                "Pearl",
                "Pen",
                "Pine",
                "Pit",
                "Pond",
                "Pure",
                "Quick",
                "Rage",
                "Raven",
                "Red",
                "Rime",
                "River",
                "Rock",
                "Rogue",
                "Rose",
                "Rust",
                "Salt",
                "Sand",
                "Scorch",
                "Shade",
                "Shadow",
                "Shimmer",
                "Shroud",
                "Silent",
                "Silk",
                "Silver",
                "Sleek",
                "Sleet",
                "Sly",
                "Small",
                "Smooth",
                "Snake",
                "Snow",
                "South",
                "Spirit",
                "Spring",
                "Stag",
                "Star",
                "Steam",
                "Steel",
                "Steep",
                "Still",
                "Stone",
                "Storm",
                "Summer",
                "Sun",
                "Swamp",
                "Swan",
                "Swift",
                "Thorn",
                "Timber",
                "Trade",
                "West",
                "Whale",
                "Whit",
                "White",
                "Wild",
                "Wilde",
                "Win",
                "Wind",
                "Winter",
                "Wolf",
                "Wort",
            ];
        }

        public virtual void LoadNames2()
        {
            Names2 =
            [
                "acre",
                "ay",
                "band",
                "barrow",
                "bay",
                "beck",
                "bell",
                "berg",
                "berry",
                "born",
                "borough",
                "bourne",
                "breach",
                "break",
                "brook",
                "burgh",
                "burn",
                "bury",
                "cairn",
                "call",
                "caster",
                "chester",
                "chill",
                "cliff",
                "coast",
                "cot",
                "crest",
                "cross",
                "dale",
                "denn",
                "drift",
                "ey",
                "fair",
                "fall",
                "falls",
                "fell",
                "field",
                "ford",
                "forest",
                "fort",
                "front",
                "frost",
                "garde",
                "gart",
                "garth",
                "gate",
                "glen",
                "grasp",
                "grave",
                "grove",
                "guard",
                "gulch",
                "gulf",
                "hall",
                "hallow",
                "ham",
                "hand",
                "harbor",
                "haven",
                "helm",
                "hill",
                "hold",
                "holde",
                "hollow",
                "hope",
                "horn",
                "host",
                "ing",
                "keep",
                "land",
                "light",
                "lin",
                "ling",
                "low",
                "lyn",
                "maw",
                "meadow",
                "mere",
                "mire",
                "mond",
                "moor",
                "more",
                "mount",
                "mouth",
                "pass",
                "peak",
                "point",
                "pond",
                "port",
                "post",
                "reach",
                "rest",
                "rock",
                "run",
                "scar",
                "shade",
                "shaw",
                "shear",
                "shell",
                "shield",
                "shore",
                "shire",
                "side",
                "spell",
                "spire",
                "stall",
                "stan",
                "sten",
                "ster",
                "wich",
                "minster",
                "star",
                "storm",
                "strand",
                "summit",
                "tide",
                "ton",
                "town",
                "tun",
                "vale",
                "valley",
                "vault",
                "vein",
                "view",
                "ville",
                "wall",
                "wallow",
                "ward",
                "watch",
                "water",
                "well",
                "wharf",
                "wick",
                "wind",
                "wood",
                "yard",
                "y",
            ];
        }

        public virtual void Dispose()
        {
            foreach (Map map in Maps)
            {
                map.Dispose();
            }
        }

        #endregion
    }
}
