using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OP_Engine.Weathers
{
    public class WeatherManager : GameComponent
    {
        #region Variables

        public static List<Weather> Weathers = new List<Weather>();
        public static WeatherType CurrentWeather;
        public static WeatherTransition TransitionType;
        public static bool Transitioning;
        public static bool Flash;
        public static bool Lightning;

        #endregion

        #region Constructor

        public WeatherManager(Game game) : base(game)
        {
            CurrentWeather = WeatherType.Clear;
        }

        #endregion

        #region Methods

        public static void Update(Point resolution)
        {
            foreach (Weather weather in Weathers)
            {
                weather.Update(resolution);
            }
        }

        public static void Load(List<Texture2D> Particles)
        {
            Weathers.Add(new Weather(Particles, WeatherType.Rain, 0));
            Weathers.Add(new Weather(Particles, WeatherType.Storm, 0));
            Weathers.Add(new Weather(Particles, WeatherType.Snow, 0));
            Weathers.Add(new Weather(Particles, WeatherType.Fog, 0));
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (Weather weather in Weathers)
            {
                weather.Draw(spriteBatch);
            }
        }

        public static void ChangeWeather(WeatherType type)
        {
            Lightning = false;
            Transitioning = true;

            if (CurrentWeather == WeatherType.Clear)
            {
                if (type == WeatherType.Rain)
                {
                    TransitionType = WeatherTransition.ClearToRain;
                }
                else if (type == WeatherType.Storm)
                {
                    TransitionType = WeatherTransition.ClearToStorm;
                    Lightning = true;
                }
                else if (type == WeatherType.Snow)
                {
                    TransitionType = WeatherTransition.ClearToSnow;
                }
                else if (type == WeatherType.Fog)
                {
                    TransitionType = WeatherTransition.ClearToFog;
                }
            }
            else if (CurrentWeather == WeatherType.Rain)
            {
                if (type == WeatherType.Clear)
                {
                    TransitionType = WeatherTransition.RainToClear;
                }
                else if (type == WeatherType.Storm)
                {
                    TransitionType = WeatherTransition.RainToStorm;
                    Lightning = true;
                }
                else if (type == WeatherType.Snow)
                {
                    TransitionType = WeatherTransition.RainToSnow;
                }
                else if (type == WeatherType.Fog)
                {
                    TransitionType = WeatherTransition.RainToFog;
                }
            }
            else if (CurrentWeather == WeatherType.Storm)
            {
                if (type == WeatherType.Clear)
                {
                    TransitionType = WeatherTransition.StormToClear;
                }
                else if (type == WeatherType.Rain)
                {
                    TransitionType = WeatherTransition.StormToRain;
                }
                else if (type == WeatherType.Snow)
                {
                    TransitionType = WeatherTransition.StormToSnow;
                }
                else if (type == WeatherType.Fog)
                {
                    TransitionType = WeatherTransition.StormToFog;
                }
            }
            else if (CurrentWeather == WeatherType.Snow)
            {
                if (type == WeatherType.Clear)
                {
                    TransitionType = WeatherTransition.SnowToClear;
                }
                else if (type == WeatherType.Rain)
                {
                    TransitionType = WeatherTransition.SnowToRain;
                }
                else if (type == WeatherType.Storm)
                {
                    TransitionType = WeatherTransition.SnowToStorm;
                    Lightning = true;
                }
                else if (type == WeatherType.Fog)
                {
                    TransitionType = WeatherTransition.SnowToFog;
                }
            }
            else if (CurrentWeather == WeatherType.Fog)
            {
                if (type == WeatherType.Clear)
                {
                    TransitionType = WeatherTransition.FogToClear;
                }
                else if (type == WeatherType.Rain)
                {
                    TransitionType = WeatherTransition.FogToRain;
                }
                else if (type == WeatherType.Storm)
                {
                    TransitionType = WeatherTransition.FogToStorm;
                    Lightning = true;
                }
                else if (type == WeatherType.Snow)
                {
                    TransitionType = WeatherTransition.FogToSnow;
                }
            }

            foreach (Weather weather in Weathers)
            {
                if (weather.Type == type)
                {
                    weather.TransitionTime = 0;
                    weather.Visible = true;
                    break;
                }
            }
        }

        #endregion
    }
}
