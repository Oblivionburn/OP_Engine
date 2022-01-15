using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;

namespace OP_Engine.Tiles
{
    public class World : Something
    {
        #region Variables

        public List<Map> Maps = new List<Map>();

        public List<string> Names = new List<string>();
        private List<string> Names1 = new List<string>();
        private List<string> Names2 = new List<string>();

        #endregion

        #region Constructor

        public World()
        {
            LoadNames();
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            foreach (Map map in Maps)
            {
                map.Update();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            if (Visible)
            {
                foreach (Map map in Maps)
                {
                    map.Draw(spriteBatch, resolution);
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible)
            {
                foreach (Map map in Maps)
                {
                    map.Draw(spriteBatch, resolution, color);
                }
            }
        }

        public virtual Map GetMap(long id)
        {
            foreach (Map map in Maps)
            {
                if (map.ID == id)
                {
                    return map;
                }
            }

            return null;
        }

        public virtual Map GetMap(string name)
        {
            foreach (Map map in Maps)
            {
                if (map.Name == name)
                {
                    return map;
                }
            }

            return null;
        }

        private void LoadNames()
        {
            LoadNames1();
            LoadNames2();

            foreach (string first in Names1)
            {
                foreach (string second in Names2)
                {
                    if (first != second &&
                        first[first.Length - 1] != second[0])
                    {
                        Names.Add(first + second);
                    }
                }
            }
        }

        private void LoadNames1()
        {
            Names1.Add("Ac");
            Names1.Add("Al");
            Names1.Add("Amber");
            Names1.Add("Angel");
            Names1.Add("Apple");
            Names1.Add("Arrow");
            Names1.Add("Autumn");
            Names1.Add("Bare");
            Names1.Add("Basin");
            Names1.Add("Bay");
            Names1.Add("Beach");
            Names1.Add("Bear");
            Names1.Add("Bell");
            Names1.Add("Ber");
            Names1.Add("Black");
            Names1.Add("Bleak");
            Names1.Add("Blind");
            Names1.Add("Bone");
            Names1.Add("Boulder");
            Names1.Add("Bre");
            Names1.Add("Bridge");
            Names1.Add("Brine");
            Names1.Add("Brittle");
            Names1.Add("Bronze");
            Names1.Add("Castle");
            Names1.Add("Cave");
            Names1.Add("Chill");
            Names1.Add("Clay");
            Names1.Add("Clear");
            Names1.Add("Cliff");
            Names1.Add("Cloud");
            Names1.Add("Cold");
            Names1.Add("Crag");
            Names1.Add("Crow");
            Names1.Add("Crystal");
            Names1.Add("Curse");
            Names1.Add("Dark");
            Names1.Add("Dawn");
            Names1.Add("Dead");
            Names1.Add("Deep");
            Names1.Add("Deer");
            Names1.Add("Demon");
            Names1.Add("Dew");
            Names1.Add("Dim");
            Names1.Add("Dire");
            Names1.Add("Dirt");
            Names1.Add("Dog");
            Names1.Add("Dragon");
            Names1.Add("Dry");
            Names1.Add("Dub");
            Names1.Add("Dusk");
            Names1.Add("Dust");
            Names1.Add("Eagle");
            Names1.Add("Earth");
            Names1.Add("East");
            Names1.Add("Ebon");
            Names1.Add("Edge");
            Names1.Add("Elder");
            Names1.Add("Ember");
            Names1.Add("Ever");
            Names1.Add("Fair");
            Names1.Add("Fall");
            Names1.Add("False");
            Names1.Add("Far");
            Names1.Add("Fay");
            Names1.Add("Fear");
            Names1.Add("Flame");
            Names1.Add("Flat");
            Names1.Add("Frey");
            Names1.Add("Frost");
            Names1.Add("Ghost");
            Names1.Add("Glimmer");
            Names1.Add("Gloom");
            Names1.Add("Gold");
            Names1.Add("Grass");
            Names1.Add("Gray");
            Names1.Add("Green");
            Names1.Add("Grim");
            Names1.Add("Grime");
            Names1.Add("Hazel");
            Names1.Add("Heart");
            Names1.Add("High");
            Names1.Add("Hollow");
            Names1.Add("Honey");
            Names1.Add("Hound");
            Names1.Add("Ice");
            Names1.Add("Iron");
            Names1.Add("Kil");
            Names1.Add("Knight");
            Names1.Add("Lagoon");
            Names1.Add("Lake");
            Names1.Add("Lan");
            Names1.Add("Lang");
            Names1.Add("Last");
            Names1.Add("Light");
            Names1.Add("Lime");
            Names1.Add("Little");
            Names1.Add("Lost");
            Names1.Add("Mad");
            Names1.Add("Mage");
            Names1.Add("Maple");
            Names1.Add("Mid");
            Names1.Add("Might");
            Names1.Add("Mill");
            Names1.Add("Mist");
            Names1.Add("Moon");
            Names1.Add("Moss");
            Names1.Add("Mud");
            Names1.Add("Mute");
            Names1.Add("Myth");
            Names1.Add("Never");
            Names1.Add("New");
            Names1.Add("Night");
            Names1.Add("Nor");
            Names1.Add("North");
            Names1.Add("Oaken");
            Names1.Add("Ocean");
            Names1.Add("Old");
            Names1.Add("Ox");
            Names1.Add("Pearl");
            Names1.Add("Pen");
            Names1.Add("Pine");
            Names1.Add("Pit");
            Names1.Add("Pond");
            Names1.Add("Pure");
            Names1.Add("Quick");
            Names1.Add("Rage");
            Names1.Add("Raven");
            Names1.Add("Red");
            Names1.Add("Rime");
            Names1.Add("River");
            Names1.Add("Rock");
            Names1.Add("Rogue");
            Names1.Add("Rose");
            Names1.Add("Rust");
            Names1.Add("Salt");
            Names1.Add("Sand");
            Names1.Add("Scorch");
            Names1.Add("Shade");
            Names1.Add("Shadow");
            Names1.Add("Shimmer");
            Names1.Add("Shroud");
            Names1.Add("Silent");
            Names1.Add("Silk");
            Names1.Add("Silver");
            Names1.Add("Sleek");
            Names1.Add("Sleet");
            Names1.Add("Sly");
            Names1.Add("Small");
            Names1.Add("Smooth");
            Names1.Add("Snake");
            Names1.Add("Snow");
            Names1.Add("South");
            Names1.Add("Spirit");
            Names1.Add("Spring");
            Names1.Add("Stag");
            Names1.Add("Star");
            Names1.Add("Steam");
            Names1.Add("Steel");
            Names1.Add("Steep");
            Names1.Add("Still");
            Names1.Add("Stone");
            Names1.Add("Storm");
            Names1.Add("Summer");
            Names1.Add("Sun");
            Names1.Add("Swamp");
            Names1.Add("Swan");
            Names1.Add("Swift");
            Names1.Add("Thorn");
            Names1.Add("Timber");
            Names1.Add("Trade");
            Names1.Add("West");
            Names1.Add("Whale");
            Names1.Add("Whit");
            Names1.Add("White");
            Names1.Add("Wild");
            Names1.Add("Wilde");
            Names1.Add("Win");
            Names1.Add("Wind");
            Names1.Add("Winter");
            Names1.Add("Wolf");
            Names1.Add("Wort");
        }

        private void LoadNames2()
        {
            Names2.Add("acre");
            Names2.Add("ay");
            Names2.Add("band");
            Names2.Add("barrow");
            Names2.Add("bay");
            Names2.Add("beck");
            Names2.Add("bell");
            Names2.Add("berg");
            Names2.Add("berry");
            Names2.Add("born");
            Names2.Add("borough");
            Names2.Add("bourne");
            Names2.Add("breach");
            Names2.Add("break");
            Names2.Add("brook");
            Names2.Add("burgh");
            Names2.Add("burn");
            Names2.Add("bury");
            Names2.Add("cairn");
            Names2.Add("call");
            Names2.Add("caster");
            Names2.Add("chester");
            Names2.Add("chill");
            Names2.Add("cliff");
            Names2.Add("coast");
            Names2.Add("cot");
            Names2.Add("crest");
            Names2.Add("cross");
            Names2.Add("dale");
            Names2.Add("denn");
            Names2.Add("drift");
            Names2.Add("ey");
            Names2.Add("fair");
            Names2.Add("fall");
            Names2.Add("falls");
            Names2.Add("fell");
            Names2.Add("field");
            Names2.Add("ford");
            Names2.Add("forest");
            Names2.Add("fort");
            Names2.Add("front");
            Names2.Add("frost");
            Names2.Add("garde");
            Names2.Add("gart");
            Names2.Add("garth");
            Names2.Add("gate");
            Names2.Add("glen");
            Names2.Add("grasp");
            Names2.Add("grave");
            Names2.Add("grove");
            Names2.Add("guard");
            Names2.Add("gulch");
            Names2.Add("gulf");
            Names2.Add("hall");
            Names2.Add("hallow");
            Names2.Add("ham");
            Names2.Add("hand");
            Names2.Add("harbor");
            Names2.Add("haven");
            Names2.Add("helm");
            Names2.Add("hill");
            Names2.Add("hold");
            Names2.Add("holde");
            Names2.Add("hollow");
            Names2.Add("hope");
            Names2.Add("horn");
            Names2.Add("host");
            Names2.Add("ing");
            Names2.Add("keep");
            Names2.Add("land");
            Names2.Add("light");
            Names2.Add("lin");
            Names2.Add("ling");
            Names2.Add("low");
            Names2.Add("lyn");
            Names2.Add("maw");
            Names2.Add("meadow");
            Names2.Add("mere");
            Names2.Add("mire");
            Names2.Add("mond");
            Names2.Add("moor");
            Names2.Add("more");
            Names2.Add("mount");
            Names2.Add("mouth");
            Names2.Add("pass");
            Names2.Add("peak");
            Names2.Add("point");
            Names2.Add("pond");
            Names2.Add("port");
            Names2.Add("post");
            Names2.Add("reach");
            Names2.Add("rest");
            Names2.Add("rock");
            Names2.Add("run");
            Names2.Add("scar");
            Names2.Add("shade");
            Names2.Add("shaw");
            Names2.Add("shear");
            Names2.Add("shell");
            Names2.Add("shield");
            Names2.Add("shore");
            Names2.Add("shire");
            Names2.Add("side");
            Names2.Add("spell");
            Names2.Add("spire");
            Names2.Add("stall");
            Names2.Add("stan");
            Names2.Add("sten");
            Names2.Add("ster");
            Names2.Add("wich");
            Names2.Add("minster");
            Names2.Add("star");
            Names2.Add("storm");
            Names2.Add("strand");
            Names2.Add("summit");
            Names2.Add("tide");
            Names2.Add("ton");
            Names2.Add("town");
            Names2.Add("tun");
            Names2.Add("vale");
            Names2.Add("valley");
            Names2.Add("vault");
            Names2.Add("vein");
            Names2.Add("view");
            Names2.Add("ville");
            Names2.Add("wall");
            Names2.Add("wallow");
            Names2.Add("ward");
            Names2.Add("watch");
            Names2.Add("water");
            Names2.Add("well");
            Names2.Add("wharf");
            Names2.Add("wick");
            Names2.Add("wind");
            Names2.Add("wood");
            Names2.Add("yard");
            Names2.Add("y");
        }

        public override void Dispose()
        {
            foreach (Map map in Maps)
            {
                map.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}
