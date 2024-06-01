using System;
using System.Linq;
using Microsoft.Xna.Framework;

using OP_Engine.Rendering;
using OP_Engine.Sounds;
using OP_Engine.Utility;
using OP_Engine.Weathers;

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
            int weather = random.Next(1, 11);

            if (WeatherManager.CurrentWeather == WeatherType.Clear)
            {
                if (weather <= 2 &&
                    WeatherOptions.Contains(WeatherType.Rain))
                {
                    WeatherManager.ChangeWeather(WeatherType.Rain);
                }
                else if (weather <= 4 &&
                         WeatherOptions.Contains(WeatherType.Snow))
                {
                    WeatherManager.ChangeWeather(WeatherType.Snow);
                }
                else if (weather <= 6 &&
                         WeatherOptions.Contains(WeatherType.Fog))
                {
                    WeatherManager.ChangeWeather(WeatherType.Fog);
                }
            }
            else if (WeatherManager.CurrentWeather == WeatherType.Rain)
            {
                if (weather <= 2 &&
                    WeatherOptions.Contains(WeatherType.Storm))
                {
                    WeatherManager.ChangeWeather(WeatherType.Storm);
                }
                else if (weather <= 8 &&
                         WeatherOptions.Contains(WeatherType.Clear))
                {
                    WeatherManager.ChangeWeather(WeatherType.Clear);
                }
            }
            else if (WeatherManager.CurrentWeather == WeatherType.Storm)
            {
                if (weather <= 4 &&
                    WeatherOptions.Contains(WeatherType.Rain))
                {
                    WeatherManager.ChangeWeather(WeatherType.Rain);
                }
                else if (weather <= 8 &&
                         WeatherOptions.Contains(WeatherType.Clear))
                {
                    WeatherManager.ChangeWeather(WeatherType.Clear);
                }
            }
            else if (WeatherManager.CurrentWeather == WeatherType.Snow)
            {
                if (weather <= 5 &&
                    WeatherOptions.Contains(WeatherType.Clear))
                {
                    WeatherManager.ChangeWeather(WeatherType.Clear);
                }
            }
            else if (WeatherManager.CurrentWeather == WeatherType.Fog)
            {
                if (weather <= 5 &&
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
                ClearToRain();
            }
            else if (WeatherManager.TransitionType == WeatherTransition.ClearToStorm)
            {
                ClearToStorm();
            }
            else if (WeatherManager.TransitionType == WeatherTransition.ClearToSnow)
            {
                ClearToSnow();
            }
            else if (WeatherManager.TransitionType == WeatherTransition.RainToClear)
            {
                RainToClear();
            }
            else if (WeatherManager.TransitionType == WeatherTransition.RainToStorm)
            {
                RainToStorm();
            }
            else if (WeatherManager.TransitionType == WeatherTransition.StormToClear)
            {
                StormToClear();
            }
            else if (WeatherManager.TransitionType == WeatherTransition.StormToRain)
            {
                StormToRain();
            }
            else if (WeatherManager.TransitionType == WeatherTransition.SnowToClear)
            {
                SnowToClear();
            }
        }

        private static void ClearToRain()
        {
            foreach (Weather weather in WeatherManager.Weathers)
            {
                if (weather.Type == WeatherType.Rain)
                {
                    if (weather.TransitionTime < 50)
                    {
                        weather.TransitionTime++;
                    }

                    if (SoundManager.AmbientEnabled &&
                        !SoundManager.AmbientPlaying)
                    {
                        AssetManager.PlayAmbient("Rain", true);
                    }

                    if (SoundManager.AmbientFade > 0)
                    {
                        SoundManager.AmbientFade -= 0.02f;
                    }

                    if (SoundManager.AmbientFade <= 0)
                    {
                        SoundManager.AmbientFade = 0;
                        WeatherManager.Transitioning = false;
                        WeatherManager.TransitionType = WeatherTransition.None;
                        WeatherManager.CurrentWeather = WeatherType.Rain;
                    }

                    break;
                }
            }
        }

        private static void RainToClear()
        {
            foreach (Weather weather in WeatherManager.Weathers)
            {
                if (weather.Type == WeatherType.Rain)
                {
                    if (weather.TransitionTime > 0)
                    {
                        weather.TransitionTime--;
                    }

                    if (SoundManager.AmbientEnabled &&
                        !SoundManager.AmbientPlaying)
                    {
                        AssetManager.PlayAmbient("Rain", true);
                    }

                    if (SoundManager.AmbientFade < 1)
                    {
                        SoundManager.AmbientFade += 0.02f;
                    }

                    if (SoundManager.AmbientFade >= 1)
                    {
                        SoundManager.AmbientFade = 1;
                        SoundManager.StopAmbient();

                        weather.ParticleManager.Particles.Clear();
                        weather.Visible = false;

                        WeatherManager.Transitioning = false;
                        WeatherManager.TransitionType = WeatherTransition.None;
                        WeatherManager.CurrentWeather = WeatherType.Clear;
                    }

                    break;
                }
            }
        }

        private static void RainToStorm()
        {
            foreach (Weather weather in WeatherManager.Weathers)
            {
                if (weather.Type == WeatherType.Storm)
                {
                    if (weather.TransitionTime < 50)
                    {
                        weather.TransitionTime++;
                    }
                    break;
                }
            }

            foreach (Weather weather in WeatherManager.Weathers)
            {
                if (weather.Type == WeatherType.Rain)
                {
                    if (weather.TransitionTime >= 50 &&
                        !SoundManager.AmbientPlaying)
                    {
                        AssetManager.PlayAmbient("Storm", true);
                    }
                    if (weather.TransitionTime > 0)
                    {
                        weather.TransitionTime--;
                    }

                    if (weather.TransitionTime == 0)
                    {
                        weather.ParticleManager.Particles.Clear();
                        weather.Visible = false;

                        WeatherManager.Transitioning = false;
                        WeatherManager.TransitionType = WeatherTransition.None;
                        WeatherManager.CurrentWeather = WeatherType.Storm;
                    }

                    break;
                }
            }
        }

        private static void StormToClear()
        {
            foreach (Weather weather in WeatherManager.Weathers)
            {
                if (weather.Type == WeatherType.Storm)
                {
                    if (weather.TransitionTime > 0)
                    {
                        weather.TransitionTime--;
                    }

                    if (SoundManager.AmbientEnabled &&
                        !SoundManager.AmbientPlaying)
                    {
                        AssetManager.PlayAmbient("Storm", true);
                    }

                    if (SoundManager.AmbientFade < 1)
                    {
                        SoundManager.AmbientFade += 0.02f;
                    }

                    if (SoundManager.AmbientFade >= 1)
                    {
                        SoundManager.AmbientFade = 1;
                        SoundManager.StopAmbient();

                        weather.ParticleManager.Particles.Clear();
                        weather.Visible = false;

                        WeatherManager.Transitioning = false;
                        WeatherManager.TransitionType = WeatherTransition.None;
                        WeatherManager.CurrentWeather = WeatherType.Clear;
                    }

                    break;
                }
            }

            WeatherManager.Lightning = false;
        }

        private static void StormToRain()
        {
            foreach (Weather weather in WeatherManager.Weathers)
            {
                if (weather.Type == WeatherType.Rain)
                {
                    if (weather.TransitionTime < 50)
                    {
                        weather.TransitionTime++;
                    }
                    break;
                }
            }

            foreach (Weather weather in WeatherManager.Weathers)
            {
                if (weather.Type == WeatherType.Storm)
                {
                    if (weather.TransitionTime >= 50 &&
                        !SoundManager.AmbientPlaying)
                    {
                        AssetManager.PlayAmbient("Rain", true);
                    }
                    if (weather.TransitionTime > 0)
                    {
                        weather.TransitionTime--;
                    }

                    if (weather.TransitionTime == 0)
                    {
                        weather.ParticleManager.Particles.Clear();
                        weather.Visible = false;

                        WeatherManager.Transitioning = false;
                        WeatherManager.TransitionType = WeatherTransition.None;
                        WeatherManager.CurrentWeather = WeatherType.Rain;
                    }

                    break;
                }
            }
        }

        private static void ClearToStorm()
        {
            foreach (Weather weather in WeatherManager.Weathers)
            {
                if (weather.Type == WeatherType.Storm)
                {
                    if (weather.TransitionTime < 50)
                    {
                        weather.TransitionTime++;
                    }
                    
                    if (SoundManager.AmbientEnabled &&
                        !SoundManager.AmbientPlaying)
                    {
                        AssetManager.PlayAmbient("Storm", true);
                    }

                    if (SoundManager.AmbientFade > 0)
                    {
                        SoundManager.AmbientFade -= 0.02f;
                    }

                    if (SoundManager.AmbientFade <= 0)
                    {
                        SoundManager.AmbientFade = 0;
                        WeatherManager.Transitioning = false;
                        WeatherManager.TransitionType = WeatherTransition.None;
                        WeatherManager.CurrentWeather = WeatherType.Storm;
                    }

                    break;
                }
            }
        }

        private static void ClearToSnow()
        {
            foreach (Weather weather in WeatherManager.Weathers)
            {
                if (weather.Type == WeatherType.Snow)
                {
                    if (weather.TransitionTime < 50)
                    {
                        weather.TransitionTime++;
                    }

                    if (SoundManager.AmbientEnabled &&
                        !SoundManager.AmbientPlaying)
                    {
                        AssetManager.PlayAmbient("Wind", true);
                    }

                    if (SoundManager.AmbientFade > 0)
                    {
                        SoundManager.AmbientFade -= 0.02f;
                    }

                    if (SoundManager.AmbientFade <= 0)
                    {
                        SoundManager.AmbientFade = 0;
                        WeatherManager.Transitioning = false;
                        WeatherManager.TransitionType = WeatherTransition.None;
                        WeatherManager.CurrentWeather = WeatherType.Snow;
                    }

                    break;
                }
            }
        }

        private static void SnowToClear()
        {
            foreach (Weather weather in WeatherManager.Weathers)
            {
                if (weather.Type == WeatherType.Snow)
                {
                    if (weather.TransitionTime > 0)
                    {
                        weather.TransitionTime--;
                    }

                    if (SoundManager.AmbientEnabled &&
                        !SoundManager.AmbientPlaying)
                    {
                        AssetManager.PlayAmbient("Wind", true);
                    }

                    if (SoundManager.AmbientFade < 1)
                    {
                        SoundManager.AmbientFade += 0.02f;
                    }

                    if (SoundManager.AmbientFade >= 1)
                    {
                        SoundManager.AmbientFade = 1;
                        SoundManager.StopAmbient();

                        weather.ParticleManager.Particles.Clear();
                        weather.Visible = false;

                        WeatherManager.Transitioning = false;
                        WeatherManager.TransitionType = WeatherTransition.None;
                        WeatherManager.CurrentWeather = WeatherType.Clear;
                    }

                    break;
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
