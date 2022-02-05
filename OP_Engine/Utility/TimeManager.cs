﻿using System;
using Microsoft.Xna.Framework;

namespace OP_Engine.Utility
{
    public class TimeManager : GameComponent
    {
        #region Variables

        public static bool Paused;
        private static TimeHandler _now;

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

        }

        #endregion

        #region Methods

        public static void Init()
        {
            _now = new TimeHandler();
        }

        public static void Init(int year, int month, int day, int hour, int minute, int second, int millisecond)
        {
            _now = new TimeHandler(year, month, day, hour, minute, second, millisecond);
        }

        #endregion
    }
}
