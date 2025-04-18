using System;
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
            game.Exiting += Game_Exiting;

            CurrentWeather = WeatherType.Clear;
        }

        #endregion

        #region Methods

        public static void Update(Point resolution)
        {
            int count = Weathers.Count;
            for (int i = 0; i < count; i++)
            {
                Weather weather = Weathers[i];
                weather.Update(resolution);
            }
        }

        public static void Load(List<Texture2D> Particles)
        {
            Weathers.Add(new Weather(null, WeatherType.Clear, 0));
            Weathers.Add(new Weather(Particles, WeatherType.Rain, 0));
            Weathers.Add(new Weather(Particles, WeatherType.Storm, 0));
            Weathers.Add(new Weather(Particles, WeatherType.Snow, 0));
            Weathers.Add(new Weather(Particles, WeatherType.Fog, 0));
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            int count = Weathers.Count;
            for (int i = 0; i < count; i++)
            {
                Weather weather = Weathers[i];
                weather.Draw(spriteBatch);
            }
        }

        public static void ChangeWeather(WeatherType type)
        {
            WeatherTransition CurrentTransition = TransitionType;

            switch (CurrentWeather)
            {
                case WeatherType.Clear:
                    switch (type)
                    {
                        case WeatherType.Rain:
                            TransitionType = WeatherTransition.ClearToRain;
                            break;

                        case WeatherType.Storm:
                            TransitionType = WeatherTransition.ClearToStorm;
                            Lightning = true;
                            break;

                        case WeatherType.Snow:
                            TransitionType = WeatherTransition.ClearToSnow;
                            break;

                        case WeatherType.Fog:
                            TransitionType = WeatherTransition.ClearToFog;
                            break;
                    }
                    break;

                case WeatherType.Rain:
                    switch (type)
                    {
                        case WeatherType.Clear:
                            TransitionType = WeatherTransition.RainToClear;
                            break;

                        case WeatherType.Storm:
                            TransitionType = WeatherTransition.RainToStorm;
                            Lightning = true;
                            break;

                        case WeatherType.Snow:
                            TransitionType = WeatherTransition.RainToSnow;
                            break;

                        case WeatherType.Fog:
                            TransitionType = WeatherTransition.RainToFog;
                            break;
                    }
                    break;

                case WeatherType.Storm:
                    switch (type)
                    {
                        case WeatherType.Clear:
                            TransitionType = WeatherTransition.StormToClear;
                            Lightning = false;
                            break;

                        case WeatherType.Rain:
                            TransitionType = WeatherTransition.StormToRain;
                            Lightning = false;
                            break;

                        case WeatherType.Snow:
                            TransitionType = WeatherTransition.StormToSnow;
                            Lightning = false;
                            break;

                        case WeatherType.Fog:
                            TransitionType = WeatherTransition.StormToFog;
                            Lightning = false;
                            break;
                    }
                    break;

                case WeatherType.Snow:
                    switch (type)
                    {
                        case WeatherType.Clear:
                            TransitionType = WeatherTransition.SnowToClear;
                            break;

                        case WeatherType.Rain:
                            TransitionType = WeatherTransition.SnowToRain;
                            break;

                        case WeatherType.Storm:
                            TransitionType = WeatherTransition.SnowToStorm;
                            Lightning = true;
                            break;

                        case WeatherType.Fog:
                            TransitionType = WeatherTransition.SnowToFog;
                            break;
                    }
                    break;

                case WeatherType.Fog:
                    switch (type)
                    {
                        case WeatherType.Clear:
                            TransitionType = WeatherTransition.FogToClear;
                            break;

                        case WeatherType.Storm:
                            TransitionType = WeatherTransition.FogToRain;
                            break;

                        case WeatherType.Snow:
                            TransitionType = WeatherTransition.FogToStorm;
                            Lightning = true;
                            break;

                        case WeatherType.Fog:
                            TransitionType = WeatherTransition.FogToSnow;
                            break;
                    }
                    break;
            }

            if (TransitionType != CurrentTransition)
            {
                foreach (Weather weather in Weathers)
                {
                    if (weather.Type == type)
                    {
                        Transitioning = true;
                        weather.TransitionTime = 0;
                        weather.Visible = true;
                        break;
                    }
                }
            }
        }

        public static Weather GetWeather(WeatherType type)
        {
            foreach (Weather weather in Weathers)
            {
                if (weather.Type == type)
                {
                    return weather;
                }
            }

            return null;
        }

        public static Weather GetWeather_TransitioningTo()
        {
            WeatherType type = WeatherType.Clear;

            switch (TransitionType)
            {
                case WeatherTransition.ClearToRain:
                    type = WeatherType.Rain;
                    break;

                case WeatherTransition.ClearToStorm:
                    type = WeatherType.Storm;
                    break;

                case WeatherTransition.ClearToSnow:
                    type = WeatherType.Snow;
                    break;

                case WeatherTransition.ClearToFog:
                    type = WeatherType.Fog;
                    break;

                case WeatherTransition.RainToStorm:
                    type = WeatherType.Storm;
                    break;

                case WeatherTransition.RainToSnow:
                    type = WeatherType.Snow;
                    break;

                case WeatherTransition.RainToFog:
                    type = WeatherType.Fog;
                    break;

                case WeatherTransition.StormToRain:
                    type = WeatherType.Rain;
                    break;

                case WeatherTransition.StormToSnow:
                    type = WeatherType.Snow;
                    break;

                case WeatherTransition.StormToFog:
                    type = WeatherType.Fog;
                    break;

                case WeatherTransition.SnowToRain:
                    type = WeatherType.Rain;
                    break;

                case WeatherTransition.SnowToStorm:
                    type = WeatherType.Storm;
                    break;

                case WeatherTransition.SnowToFog:
                    type = WeatherType.Fog;
                    break;

                case WeatherTransition.FogToRain:
                    type = WeatherType.Rain;
                    break;

                case WeatherTransition.FogToStorm:
                    type = WeatherType.Storm;
                    break;

                case WeatherTransition.FogToSnow:
                    type = WeatherType.Snow;
                    break;
            }

            foreach (Weather weather in Weathers)
            {
                if (weather.Type == type)
                {
                    return weather;
                }
            }

            return null;
        }

        private void Game_Exiting(object sender, EventArgs e)
        {
            foreach (Weather weather in Weathers)
            {
                weather.Dispose();
            }
        }

        #endregion
    }
}
