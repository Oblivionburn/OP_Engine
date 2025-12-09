using System;
using System.Linq;
using Microsoft.Xna.Framework;

using OP_Engine.Rendering;
using OP_Engine.Sounds;
using OP_Engine.Utility;
using OP_Engine.Weathers;
using OP_Engine.Enums;

namespace OP_Engine.Time
{
    public class TimeManager : GameComponent
    {
        #region Variables

        private static TimeHandler _now;

        public static double GameTimeTick;
        public static double MainGameTime;
        public static double Interval = 1;
        public static bool Paused;
        public static WeatherType[] WeatherOptions = { WeatherType.Clear };

        #endregion

        #region Properties

        public static TimeHandler Now
        {
            get { return _now; }
        }

        #endregion

        #region Constructor

        public TimeManager(Game game) : base(game)
        {
            game.Exiting += Game_Exiting;
        }

        #endregion

        #region Methods

        public static void Init()
        {
            _now = new TimeHandler();
        }

        public static void Init(int year, int month, int day, int hour)
        {
            _now = new TimeHandler(year, month, day, hour);
        }

        public static void Update(GameTime gameTime, TimeRate timeRate)
        {
            MainGameTime = gameTime.TotalGameTime.TotalMilliseconds;

            if (RenderingManager.Lighting.FadingIn)
            {
                if (RenderingManager.Lighting.LerpAmount < 1)
                {
                    RenderingManager.Lighting.LerpAmount += 0.025f;
                    RenderingManager.Lighting.FadeIn();
                }
                else
                {
                    RenderingManager.Lighting.FadingIn = false;
                    RenderingManager.Lighting.LerpAmount = 0;
                }
            }
            else if (RenderingManager.Lighting.FadingOut)
            {
                if (RenderingManager.Lighting.LerpAmount < 1)
                {
                    RenderingManager.Lighting.LerpAmount += 0.025f;
                    RenderingManager.Lighting.FadeOut();
                }
                else
                {
                    RenderingManager.Lighting.FadingOut = false;
                    RenderingManager.Lighting.LerpAmount = 0;
                }
            }

            if (GameTimeTick <= MainGameTime)
            {
                GameTimeTick = MainGameTime + Interval;

                Now.AddTime(timeRate, 1);
            }
        }

        public static void Update(double totalMilliseconds, TimeRate timeRate)
        {
            MainGameTime = totalMilliseconds;

            if (RenderingManager.Lighting.FadingIn)
            {
                if (RenderingManager.Lighting.LerpAmount < 1)
                {
                    RenderingManager.Lighting.LerpAmount += 0.025f;
                    RenderingManager.Lighting.FadeIn();
                }
                else
                {
                    RenderingManager.Lighting.FadingIn = false;
                    RenderingManager.Lighting.LerpAmount = 0;
                }
            }
            else if (RenderingManager.Lighting.FadingOut)
            {
                if (RenderingManager.Lighting.LerpAmount < 1)
                {
                    RenderingManager.Lighting.LerpAmount += 0.025f;
                    RenderingManager.Lighting.FadeOut();
                }
                else
                {
                    RenderingManager.Lighting.FadingOut = false;
                    RenderingManager.Lighting.LerpAmount = 0;
                }
            }

            if (GameTimeTick <= MainGameTime)
            {
                GameTimeTick = MainGameTime + Interval;

                Now.AddTime(timeRate, 1);
            }
        }

        public static void MinutesChange(object sender, EventArgs e)
        {
            if (WeatherManager.Transitioning)
            {
                Transition();
            }

            if (!RenderingManager.Lighting.FadingIn &&
                !RenderingManager.Lighting.FadingOut)
            {
                if (RenderingManager.Lighting.LerpAmount < 1)
                {
                    RenderingManager.Lighting.LerpAmount += 0.0167f;
                }
                RenderingManager.Lighting.Update();
            }
        }

        public static void HourChange(object sender, EventArgs e)
        {
            RenderingManager.Lighting.LerpAmount = 0;
            WeatherUpdate();
        }

        public static void WeatherUpdate()
        {
            CryptoRandom random = new CryptoRandom();
            int weather = random.Next(1, 21);

            if (WeatherManager.CurrentWeather == WeatherType.Clear)
            {
                if (weather == 5 &&
                    WeatherOptions.Contains(WeatherType.Storm))
                {
                    WeatherManager.ChangeWeather(WeatherType.Storm);
                }
                else if (weather == 10 &&
                         WeatherOptions.Contains(WeatherType.Fog))
                {
                    WeatherManager.ChangeWeather(WeatherType.Fog);
                }
                else if (weather == 15 &&
                         WeatherOptions.Contains(WeatherType.Snow))
                {
                    WeatherManager.ChangeWeather(WeatherType.Snow);
                }
                else if (weather == 20 &&
                         WeatherOptions.Contains(WeatherType.Rain))
                {
                    WeatherManager.ChangeWeather(WeatherType.Rain);
                }
            }
            else if (WeatherManager.CurrentWeather == WeatherType.Rain)
            {
                if (weather == 2 &&
                    WeatherOptions.Contains(WeatherType.Snow))
                {
                    WeatherManager.ChangeWeather(WeatherType.Snow);
                }
                else if (weather == 4 &&
                         WeatherOptions.Contains(WeatherType.Fog))
                {
                    WeatherManager.ChangeWeather(WeatherType.Fog);
                }
                else if (weather == 8 &&
                         WeatherOptions.Contains(WeatherType.Storm))
                {
                    WeatherManager.ChangeWeather(WeatherType.Storm);
                }
                else if (weather <= 12 &&
                         WeatherOptions.Contains(WeatherType.Clear))
                {
                    WeatherManager.ChangeWeather(WeatherType.Clear);
                }
            }
            else if (WeatherManager.CurrentWeather == WeatherType.Storm)
            {
                if (weather == 2 &&
                    WeatherOptions.Contains(WeatherType.Snow))
                {
                    WeatherManager.ChangeWeather(WeatherType.Snow);
                }
                else if (weather == 4 &&
                         WeatherOptions.Contains(WeatherType.Fog))
                {
                    WeatherManager.ChangeWeather(WeatherType.Fog);
                }
                else if (weather == 8 &&
                         WeatherOptions.Contains(WeatherType.Rain))
                {
                    WeatherManager.ChangeWeather(WeatherType.Rain);
                }
                else if (weather <= 12 &&
                         WeatherOptions.Contains(WeatherType.Clear))
                {
                    WeatherManager.ChangeWeather(WeatherType.Clear);
                }
            }
            else if (WeatherManager.CurrentWeather == WeatherType.Snow)
            {
                if (weather == 2 &&
                    WeatherOptions.Contains(WeatherType.Storm))
                {
                    WeatherManager.ChangeWeather(WeatherType.Storm);
                }
                else if (weather == 4 &&
                         WeatherOptions.Contains(WeatherType.Fog))
                {
                    WeatherManager.ChangeWeather(WeatherType.Fog);
                }
                else if (weather == 8 &&
                         WeatherOptions.Contains(WeatherType.Rain))
                {
                    WeatherManager.ChangeWeather(WeatherType.Rain);
                }
                else if (weather <= 12 &&
                         WeatherOptions.Contains(WeatherType.Clear))
                {
                    WeatherManager.ChangeWeather(WeatherType.Clear);
                }
            }
            else if (WeatherManager.CurrentWeather == WeatherType.Fog)
            {
                if (weather == 2 &&
                    WeatherOptions.Contains(WeatherType.Storm))
                {
                    WeatherManager.ChangeWeather(WeatherType.Storm);
                }
                else if (weather == 4 &&
                         WeatherOptions.Contains(WeatherType.Snow))
                {
                    WeatherManager.ChangeWeather(WeatherType.Snow);
                }
                else if (weather == 8 &&
                         WeatherOptions.Contains(WeatherType.Rain))
                {
                    WeatherManager.ChangeWeather(WeatherType.Rain);
                }
                else if (weather <= 12 &&
                         WeatherOptions.Contains(WeatherType.Clear))
                {
                    WeatherManager.ChangeWeather(WeatherType.Clear);
                }
            }
        }

        private static void Transition()
        {
            if (WeatherManager.TransitionType == WeatherTransition.ClearToRain)
            {
                ClearTo(WeatherType.Rain);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.ClearToStorm)
            {
                ClearTo(WeatherType.Storm);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.ClearToSnow)
            {
                ClearTo(WeatherType.Snow);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.ClearToFog)
            {
                ClearTo(WeatherType.Fog);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.RainToClear)
            {
                ToClear(WeatherType.Rain);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.RainToStorm)
            {
                Transition(WeatherType.Rain, WeatherType.Storm);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.RainToSnow)
            {
                Transition(WeatherType.Rain, WeatherType.Snow);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.RainToFog)
            {
                Transition(WeatherType.Rain, WeatherType.Fog);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.StormToClear)
            {
                ToClear(WeatherType.Storm);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.StormToRain)
            {
                Transition(WeatherType.Storm, WeatherType.Rain);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.StormToSnow)
            {
                Transition(WeatherType.Storm, WeatherType.Snow);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.StormToFog)
            {
                Transition(WeatherType.Storm, WeatherType.Fog);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.SnowToClear)
            {
                ToClear(WeatherType.Snow);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.SnowToRain)
            {
                Transition(WeatherType.Snow, WeatherType.Rain);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.SnowToStorm)
            {
                Transition(WeatherType.Snow, WeatherType.Storm);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.SnowToFog)
            {
                Transition(WeatherType.Snow, WeatherType.Fog);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.FogToClear)
            {
                ToClear(WeatherType.Fog);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.FogToRain)
            {
                Transition(WeatherType.Fog, WeatherType.Rain);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.FogToStorm)
            {
                Transition(WeatherType.Fog, WeatherType.Storm);
            }
            else if (WeatherManager.TransitionType == WeatherTransition.FogToSnow)
            {
                Transition(WeatherType.Fog, WeatherType.Snow);
            }
        }

        private static void Transition(WeatherType oldType, WeatherType newType)
        {
            string oldTypeString = oldType.ToString();
            string newTypeString = newType.ToString();

            if (SoundManager.AmbientEnabled &&
                !SoundManager.IsPlaying_Ambient(newTypeString))
            {
                AssetManager.PlayAmbient(newTypeString, true);
            }

            Weather newWeather = WeatherManager.GetWeather(newType);
            if (newWeather != null)
            {
                if (newWeather.TransitionTime < 50)
                {
                    newWeather.TransitionTime++;

                    if (SoundManager.AmbientFade.ContainsKey(newTypeString))
                    {
                        if (SoundManager.AmbientFade[newTypeString] > 0)
                        {
                            SoundManager.AmbientFade[newTypeString] -= 0.02f;
                        }
                    }
                }
                else if (SoundManager.AmbientFade.ContainsKey(newTypeString))
                {
                    SoundManager.AmbientFade[newTypeString] = 0;
                }
            }

            Weather oldWeather = WeatherManager.GetWeather(oldType);
            if (oldWeather != null)
            {
                if (oldWeather.TransitionTime > 0)
                {
                    oldWeather.TransitionTime--;
                }

                if (SoundManager.AmbientFade.ContainsKey(oldTypeString))
                {
                    if (SoundManager.AmbientFade[oldTypeString] < 1)
                    {
                        SoundManager.AmbientFade[oldTypeString] += 0.02f;
                    }
                }

                if (oldWeather.TransitionTime <= 0)
                {
                    SoundManager.StopAmbient(oldTypeString);
                    oldWeather.Visible = false;

                    WeatherManager.Transitioning = false;
                    WeatherManager.TransitionType = WeatherTransition.None;
                    WeatherManager.CurrentWeather = newType;
                }
            }
        }

        private static void ClearTo(WeatherType newType)
        {
            string newTypeString = newType.ToString();

            if (SoundManager.AmbientEnabled &&
                !SoundManager.IsPlaying_Ambient(newTypeString))
            {
                AssetManager.PlayAmbient(newTypeString, true);
            }

            Weather newWeather = WeatherManager.GetWeather(newType);
            if (newWeather != null)
            {
                if (newWeather.TransitionTime < 50)
                {
                    newWeather.TransitionTime++;

                    if (SoundManager.AmbientFade[newTypeString] > 0)
                    {
                        SoundManager.AmbientFade[newTypeString] -= 0.02f;
                    }
                }
                else
                {
                    SoundManager.AmbientFade[newTypeString] = 0;
                }

                if (newWeather.TransitionTime >= 50)
                {
                    WeatherManager.Transitioning = false;
                    WeatherManager.TransitionType = WeatherTransition.None;
                    WeatherManager.CurrentWeather = newType;
                }
            }
        }

        private static void ToClear(WeatherType oldType)
        {
            string oldTypeString = oldType.ToString();

            Weather oldWeather = WeatherManager.GetWeather(oldType);
            if (oldWeather != null)
            {
                if (oldWeather.TransitionTime > 0)
                {
                    oldWeather.TransitionTime--;

                    if (SoundManager.AmbientFade.ContainsKey(oldTypeString))
                    {
                        if (SoundManager.AmbientFade[oldTypeString] < 1)
                        {
                            SoundManager.AmbientFade[oldTypeString] += 0.02f;
                        }
                    }
                }

                if (oldWeather.TransitionTime <= 0)
                {
                    SoundManager.StopAmbient(oldTypeString);
                    oldWeather.Visible = false;

                    WeatherManager.Transitioning = false;
                    WeatherManager.TransitionType = WeatherTransition.None;
                    WeatherManager.CurrentWeather = WeatherType.Clear;
                }
            }
        }

        public static void Reset(TimeRate interval, int year, int month, int day, int hour)
        {
            Now.Interval = interval;

            Now.Years = year;
            Now.Months = month;
            Now.Days = day;
            Now.Hours = hour;

            if (interval == TimeRate.Millisecond)
            {
                Interval = 1;
            }
            else if (interval == TimeRate.Second)
            {
                Interval = 10;
            }
            else if (interval == TimeRate.Minute)
            {
                Interval = 100;
            }
            else if (interval == TimeRate.Hour)
            {
                Interval = 1000;
            }

            RenderingManager.Lighting.Reset();

            Paused = false;

            Now.OnMinutesChange -= MinutesChange;
            Now.OnMinutesChange += MinutesChange;

            Now.OnHoursChange -= HourChange;
            Now.OnHoursChange += HourChange;
        }

        private void Game_Exiting(object sender, EventArgs e)
        {
            if (_now != null)
            {
                _now.Dispose();
            }
        }

        #endregion
    }
}
