using System;
using OP_Engine.Enums;

namespace OP_Engine.Time
{
    public class TimeHandler : IDisposable
    {
        #region Variables

        public TimeRate Interval; //Global rate for increasing time, else use methods with custom rate
        public bool Processing; //Something to check if it's taking a long time to process all the events for a given amount of time

        public long TotalMilliseconds;
        public long TotalSeconds;
        public long TotalMinutes;
        public long TotalHours;
        public long TotalDays;
        public long TotalMonths;
        public long TotalYears;
        public long TotalDecades;
        public long TotalCenturies;
        public long TotalMillennia;

        public long Milliseconds;
        public long Seconds;
        public long Minutes;
        public long Hours;
        public long Days;
        public long Months;
        public long Years;
        public long Decades;
        public long Centuries;
        public long Millennia;

        public int Hours_In_Day = 24;
        public int Days_In_Month = 30;
        public int Months_In_Year = 12;

        #endregion

        #region Events

        public event EventHandler OnMillisecondsChange;
        public event EventHandler OnSecondsChange;
        public event EventHandler OnMinutesChange;
        public event EventHandler OnHoursChange;
        public event EventHandler OnDaysChange;
        public event EventHandler OnMonthsChange;
        public event EventHandler OnYearsChange;
        public event EventHandler OnDecadesChange;
        public event EventHandler OnCenturiesChange;
        public event EventHandler OnMillenniaChange;

        #endregion

        #region Constructors

        public TimeHandler()
        {

        }

        public TimeHandler(TimeHandler current_time)
        {
            Millennia = current_time.Millennia;
            Centuries = current_time.Centuries;
            Decades = current_time.Decades;
            Years = current_time.Years;
            Months = current_time.Months;
            Days = current_time.Days;
            Hours = current_time.Hours;
            Minutes = current_time.Minutes;
            Seconds = current_time.Seconds;
            Milliseconds = current_time.Milliseconds;

            TotalMilliseconds = current_time.TotalMilliseconds;
            TotalSeconds = current_time.TotalSeconds;
            TotalMinutes = current_time.TotalMinutes;
            TotalHours = current_time.TotalHours;
            TotalDays = current_time.TotalDays;
            TotalMonths = current_time.TotalMonths;
            TotalYears = current_time.TotalYears;
            TotalDecades = current_time.TotalDecades;
            TotalCenturies = current_time.TotalCenturies;
            TotalMillennia = current_time.TotalMillennia;
        }

        public TimeHandler(TimeHandler current_time, TimeSpan additional_time)
        {
            Millennia = current_time.Millennia;
            Centuries = current_time.Centuries;
            Decades = current_time.Decades;
            Years = current_time.Years;
            Months = current_time.Months;
            Days = current_time.Days;
            Hours = current_time.Hours;
            Minutes = current_time.Minutes;
            Seconds = current_time.Seconds;
            Milliseconds = current_time.Milliseconds;

            TotalMilliseconds = current_time.TotalMilliseconds;
            TotalSeconds = current_time.TotalSeconds;
            TotalMinutes = current_time.TotalMinutes;
            TotalHours = current_time.TotalHours;
            TotalDays = current_time.TotalDays;
            TotalMonths = current_time.TotalMonths;
            TotalYears = current_time.TotalYears;
            TotalDecades = current_time.TotalDecades;
            TotalCenturies = current_time.TotalCenturies;
            TotalMillennia = current_time.TotalMillennia;

            if (additional_time.Days > 0)
            {
                AddDays(additional_time.Days, TimeRate.Day);
            }

            if (additional_time.Hours > 0)
            {
                AddHours(additional_time.Hours, TimeRate.Hour);
            }

            if (additional_time.Minutes > 0)
            {
                AddMinutes(additional_time.Minutes, TimeRate.Minute);
            }

            if (additional_time.Seconds > 0)
            {
                AddSeconds(additional_time.Seconds, TimeRate.Second);
            }

            if (additional_time.Milliseconds > 0)
            {
                AddMilliseconds(additional_time.Milliseconds);
            }
        }

        public TimeHandler(int year, int month, int day, int hour)
        {
            if (year > 0)
            {
                AddYears(year, TimeRate.Year);
            }

            if (month > 0)
            {
                AddMonths(month, TimeRate.Month);
            }

            if (day > 0)
            {
                AddDays(day, TimeRate.Day);
            }

            if (hour > 0)
            {
                AddHours(hour, TimeRate.Hour);
            }
        }

        public TimeHandler(long millisecond)
        {
            if (millisecond > 0)
            {
                AddMilliseconds(millisecond);
            }
        }

        public TimeHandler(long second, long millisecond)
        {
            if (second > 0)
            {
                AddSeconds(second, TimeRate.Second);
            }

            if (millisecond > 0)
            {
                AddMilliseconds(millisecond);
            }
        }

        public TimeHandler(long minute, long second, long millisecond)
        {
            if (minute > 0)
            {
                AddMinutes(minute, TimeRate.Minute);
            }

            if (second > 0)
            {
                AddSeconds(second, TimeRate.Second);
            }

            if (millisecond > 0)
            {
                AddMilliseconds(millisecond);
            }
        }

        public TimeHandler(long hour, long minute, long second, long millisecond)
        {
            if (hour > 0)
            {
                AddHours(hour, TimeRate.Hour);
            }

            if (minute > 0)
            {
                AddMinutes(minute, TimeRate.Minute);
            }

            if (second > 0)
            {
                AddSeconds(second, TimeRate.Second);
            }

            if (millisecond > 0)
            {
                AddMilliseconds(millisecond);
            }
        }

        public TimeHandler(long day, long hour, long minute, long second, long millisecond)
        {
            if (day > 0)
            {
                AddDays(day, TimeRate.Day);
            }

            if (hour > 0)
            {
                AddHours(hour, TimeRate.Hour);
            }

            if (minute > 0)
            {
                AddMinutes(minute, TimeRate.Minute);
            }

            if (second > 0)
            {
                AddSeconds(second, TimeRate.Second);
            }

            if (millisecond > 0)
            {
                AddMilliseconds(millisecond);
            }
        }

        public TimeHandler(long month, long day, long hour, long minute, long second, long millisecond)
        {
            if (month > 0)
            {
                AddMonths(month, TimeRate.Month);
            }

            if (day > 0)
            {
                AddDays(day, TimeRate.Day);
            }

            if (hour > 0)
            {
                AddHours(hour, TimeRate.Hour);
            }

            if (minute > 0)
            {
                AddMinutes(minute, TimeRate.Minute);
            }

            if (second > 0)
            {
                AddSeconds(second, TimeRate.Second);
            }

            if (millisecond > 0)
            {
                AddMilliseconds(millisecond);
            }
        }

        public TimeHandler(long year, long month, long day, long hour, long minute, long second, long millisecond)
        {
            if (year > 0)
            {
                AddYears(year, TimeRate.Year);
            }

            if (month > 0)
            {
                AddMonths(month, TimeRate.Month);
            }

            if (day > 0)
            {
                AddDays(day, TimeRate.Day);
            }

            if (hour > 0)
            {
                AddHours(hour, TimeRate.Hour);
            }

            if (minute > 0)
            {
                AddMinutes(minute, TimeRate.Minute);
            }

            if (second > 0)
            {
                AddSeconds(second, TimeRate.Second);
            }

            if (millisecond > 0)
            {
                AddMilliseconds(millisecond);
            }
        }

        public TimeHandler(long decade, long year, long month, long day, long hour, long minute, long second, long millisecond)
        {
            if (decade > 0)
            {
                AddDecades(decade, TimeRate.Decade);
            }

            if (year > 0)
            {
                AddYears(year, TimeRate.Year);
            }

            if (month > 0)
            {
                AddMonths(month, TimeRate.Month);
            }

            if (day > 0)
            {
                AddDays(day, TimeRate.Day);
            }

            if (hour > 0)
            {
                AddHours(hour, TimeRate.Hour);
            }

            if (minute > 0)
            {
                AddMinutes(minute, TimeRate.Minute);
            }

            if (second > 0)
            {
                AddSeconds(second, TimeRate.Second);
            }

            if (millisecond > 0)
            {
                AddMilliseconds(millisecond);
            }
        }

        public TimeHandler(long century, long decade, long year, long month, long day, long hour, long minute, long second, long millisecond)
        {
            if (century > 0)
            {
                AddCenturies(century, TimeRate.Century);
            }

            if (decade > 0)
            {
                AddDecades(decade, TimeRate.Decade);
            }

            if (year > 0)
            {
                AddYears(year, TimeRate.Year);
            }

            if (month > 0)
            {
                AddMonths(month, TimeRate.Month);
            }

            if (day > 0)
            {
                AddDays(day, TimeRate.Day);
            }

            if (hour > 0)
            {
                AddHours(hour, TimeRate.Hour);
            }

            if (minute > 0)
            {
                AddMinutes(minute, TimeRate.Minute);
            }

            if (second > 0)
            {
                AddSeconds(second, TimeRate.Second);
            }

            if (millisecond > 0)
            {
                AddMilliseconds(millisecond);
            }
        }

        public TimeHandler(long millennia, long century, long decade, long year, long month, long day, long hour, long minute, long second, long millisecond)
        {
            if (millennia > 0)
            {
                AddMillennia(millennia, TimeRate.Millennia);
            }

            if (century > 0)
            {
                AddCenturies(century, TimeRate.Century);
            }

            if (decade > 0)
            {
                AddDecades(decade, TimeRate.Decade);
            }

            if (year > 0)
            {
                AddYears(year, TimeRate.Year);
            }

            if (month > 0)
            {
                AddMonths(month, TimeRate.Month);
            }

            if (day > 0)
            {
                AddDays(day, TimeRate.Day);
            }

            if (hour > 0)
            {
                AddHours(hour, TimeRate.Hour);
            }

            if (minute > 0)
            {
                AddMinutes(minute, TimeRate.Minute);
            }

            if (second > 0)
            {
                AddSeconds(second, TimeRate.Second);
            }

            if (millisecond > 0)
            {
                AddMilliseconds(millisecond);
            }
        }

        #endregion

        #region Methods

        public virtual void AddMilliseconds(long amount)
        {
            Processing = true;

            for (long i = 1; i <= amount; i++)
            {
                TotalMilliseconds++;
                Milliseconds++;
                OnMillisecondsChange?.Invoke(this, EventArgs.Empty);

                if (Milliseconds >= 1000)
                {
                    Milliseconds = 0;

                    TotalSeconds++;
                    Seconds++;
                    OnSecondsChange?.Invoke(this, EventArgs.Empty);
                }

                if (Seconds >= 60)
                {
                    Seconds = 0;

                    TotalMinutes++;
                    Minutes++;
                    OnMinutesChange?.Invoke(this, EventArgs.Empty);
                }

                if (Minutes >= 60)
                {
                    Minutes = 0;

                    TotalHours++;
                    Hours++;
                    OnHoursChange?.Invoke(this, EventArgs.Empty);
                }

                if (Hours >= Hours_In_Day)
                {
                    Hours = 0;
                    TotalDays++;
                    Days++;
                    OnDaysChange?.Invoke(this, EventArgs.Empty);
                }

                if (Days > Days_In_Month)
                {
                    Days = 1;
                    TotalMonths++;
                    Months++;
                    OnMonthsChange?.Invoke(this, EventArgs.Empty);
                }

                if (Months > Months_In_Year)
                {
                    Months = 1;
                    TotalYears++;
                    Years++;
                    OnYearsChange?.Invoke(this, EventArgs.Empty);
                }

                if (Years >= 10)
                {
                    Years = 0;
                    TotalDecades++;
                    Decades++;
                    OnDecadesChange?.Invoke(this, EventArgs.Empty);
                }

                if (Decades >= 10)
                {
                    Decades = 0;
                    TotalCenturies++;
                    Centuries++;
                    OnCenturiesChange?.Invoke(this, EventArgs.Empty);
                }

                if (Centuries >= 10)
                {
                    Centuries = 0;
                    TotalMillennia++;
                    Millennia++;
                    OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                }
            }

            Processing = false;
        }

        public virtual void AddSeconds(long amount)
        {
            long total_milliseconds = amount * 1000;

            switch (Interval)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                case TimeRate.Minute:
                case TimeRate.Hour:
                case TimeRate.Day:
                case TimeRate.Month:
                case TimeRate.Year:
                case TimeRate.Decade:
                case TimeRate.Century:
                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalSeconds++;
                        Seconds++;
                        OnSecondsChange?.Invoke(this, EventArgs.Empty);

                        if (Seconds >= 60)
                        {
                            Seconds = 0;

                            TotalMinutes++;
                            Minutes++;
                            OnMinutesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Minutes >= 60)
                        {
                            Minutes = 0;

                            TotalHours++;
                            Hours++;
                            OnHoursChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Hours >= Hours_In_Day)
                        {
                            Hours = 0;
                            TotalDays++;
                            Days++;
                            OnDaysChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Days > Days_In_Month)
                        {
                            Days = 1;
                            TotalMonths++;
                            Months++;
                            OnMonthsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Months > Months_In_Year)
                        {
                            Months = 1;
                            TotalYears++;
                            Years++;
                            OnYearsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Years >= 10)
                        {
                            Years = 0;
                            TotalDecades++;
                            Decades++;
                            OnDecadesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Decades >= 10)
                        {
                            Decades = 0;
                            TotalCenturies++;
                            Centuries++;
                            OnCenturiesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddSeconds(long amount, TimeRate rate)
        {
            long total_milliseconds = amount * 1000;

            switch (rate)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                case TimeRate.Minute:
                case TimeRate.Hour:
                case TimeRate.Day:
                case TimeRate.Month:
                case TimeRate.Year:
                case TimeRate.Decade:
                case TimeRate.Century:
                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalSeconds++;
                        Seconds++;
                        OnSecondsChange?.Invoke(this, EventArgs.Empty);

                        if (Seconds >= 60)
                        {
                            Seconds = 0;

                            TotalMinutes++;
                            Minutes++;
                            OnMinutesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Minutes >= 60)
                        {
                            Minutes = 0;

                            TotalHours++;
                            Hours++;
                            OnHoursChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Hours >= Hours_In_Day)
                        {
                            Hours = 0;
                            TotalDays++;
                            Days++;
                            OnDaysChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Days > Days_In_Month)
                        {
                            Days = 1;
                            TotalMonths++;
                            Months++;
                            OnMonthsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Months > Months_In_Year)
                        {
                            Months = 1;
                            TotalYears++;
                            Years++;
                            OnYearsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Years >= 10)
                        {
                            Years = 0;
                            TotalDecades++;
                            Decades++;
                            OnDecadesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Decades >= 10)
                        {
                            Decades = 0;
                            TotalCenturies++;
                            Centuries++;
                            OnCenturiesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddMinutes(long amount)
        {
            long total_seconds = amount * 60;
            long total_milliseconds = total_seconds * 1000;

            switch (Interval)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                    TotalMilliseconds += total_milliseconds;
                    AddSeconds(total_seconds);
                    break;

                case TimeRate.Minute:
                case TimeRate.Hour:
                case TimeRate.Day:
                case TimeRate.Month:
                case TimeRate.Year:
                case TimeRate.Decade:
                case TimeRate.Century:
                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalMinutes++;
                        Minutes++;
                        OnMinutesChange?.Invoke(this, EventArgs.Empty);

                        if (Minutes >= 60)
                        {
                            Minutes = 0;

                            TotalHours++;
                            Hours++;
                            OnHoursChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Hours >= Hours_In_Day)
                        {
                            Hours = 0;
                            TotalDays++;
                            Days++;
                            OnDaysChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Days > Days_In_Month)
                        {
                            Days = 1;
                            TotalMonths++;
                            Months++;
                            OnMonthsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Months > Months_In_Year)
                        {
                            Months = 1;
                            TotalYears++;
                            Years++;
                            OnYearsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Years >= 10)
                        {
                            Years = 0;
                            TotalDecades++;
                            Decades++;
                            OnDecadesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Decades >= 10)
                        {
                            Decades = 0;
                            TotalCenturies++;
                            Centuries++;
                            OnCenturiesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddMinutes(long amount, TimeRate rate)
        {
            long total_seconds = amount * 60;
            long total_milliseconds = total_seconds * 1000;

            switch (rate)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                    TotalMilliseconds += total_milliseconds;
                    AddSeconds(total_seconds, rate);
                    break;

                case TimeRate.Minute:
                case TimeRate.Hour:
                case TimeRate.Day:
                case TimeRate.Month:
                case TimeRate.Year:
                case TimeRate.Decade:
                case TimeRate.Century:
                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalMinutes++;
                        Minutes++;
                        OnMinutesChange?.Invoke(this, EventArgs.Empty);

                        if (Minutes >= 60)
                        {
                            Minutes = 0;

                            TotalHours++;
                            Hours++;
                            OnHoursChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Hours >= Hours_In_Day)
                        {
                            Hours = 0;
                            TotalDays++;
                            Days++;
                            OnDaysChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Days > Days_In_Month)
                        {
                            Days = 1;
                            TotalMonths++;
                            Months++;
                            OnMonthsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Months > Months_In_Year)
                        {
                            Months = 1;
                            TotalYears++;
                            Years++;
                            OnYearsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Years >= 10)
                        {
                            Years = 0;
                            TotalDecades++;
                            Decades++;
                            OnDecadesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Decades >= 10)
                        {
                            Decades = 0;
                            TotalCenturies++;
                            Centuries++;
                            OnCenturiesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddHours(long amount)
        {
            long total_minutes = amount * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;

            switch (Interval)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                    TotalMilliseconds += total_milliseconds;
                    AddSeconds(total_seconds);
                    break;

                case TimeRate.Minute:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    AddMinutes(total_minutes);
                    break;

                case TimeRate.Hour:
                case TimeRate.Day:
                case TimeRate.Month:
                case TimeRate.Year:
                case TimeRate.Decade:
                case TimeRate.Century:
                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalHours++;
                        Hours++;
                        OnHoursChange?.Invoke(this, EventArgs.Empty);

                        if (Hours >= Hours_In_Day)
                        {
                            Hours = 0;
                            TotalDays++;
                            Days++;
                            OnDaysChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Days > Days_In_Month)
                        {
                            Days = 1;
                            TotalMonths++;
                            Months++;
                            OnMonthsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Months > Months_In_Year)
                        {
                            Months = 1;
                            TotalYears++;
                            Years++;
                            OnYearsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Years >= 10)
                        {
                            Years = 0;
                            TotalDecades++;
                            Decades++;
                            OnDecadesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Decades >= 10)
                        {
                            Decades = 0;
                            TotalCenturies++;
                            Centuries++;
                            OnCenturiesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddHours(long amount, TimeRate rate)
        {
            long total_minutes = amount * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;

            switch (rate)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                    TotalMilliseconds += total_milliseconds;
                    AddSeconds(total_seconds, rate);
                    break;

                case TimeRate.Minute:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    AddMinutes(total_minutes, rate);
                    break;

                case TimeRate.Hour:
                case TimeRate.Day:
                case TimeRate.Month:
                case TimeRate.Year:
                case TimeRate.Decade:
                case TimeRate.Century:
                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalHours++;
                        Hours++;
                        OnHoursChange?.Invoke(this, EventArgs.Empty);

                        if (Hours >= Hours_In_Day)
                        {
                            Hours = 0;
                            TotalDays++;
                            Days++;
                            OnDaysChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Days > Days_In_Month)
                        {
                            Days = 1;
                            TotalMonths++;
                            Months++;
                            OnMonthsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Months > Months_In_Year)
                        {
                            Months = 1;
                            TotalYears++;
                            Years++;
                            OnYearsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Years >= 10)
                        {
                            Years = 0;
                            TotalDecades++;
                            Decades++;
                            OnDecadesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Decades >= 10)
                        {
                            Decades = 0;
                            TotalCenturies++;
                            Centuries++;
                            OnCenturiesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddDays(long amount)
        {
            long total_hours = amount * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;

            switch (Interval)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                    TotalMilliseconds += total_milliseconds;
                    AddSeconds(total_seconds);
                    break;

                case TimeRate.Minute:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    AddMinutes(total_minutes);
                    break;

                case TimeRate.Hour:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    AddHours(total_hours);
                    break;

                case TimeRate.Day:
                case TimeRate.Month:
                case TimeRate.Year:
                case TimeRate.Decade:
                case TimeRate.Century:
                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalDays++;
                        Days++;
                        OnDaysChange?.Invoke(this, EventArgs.Empty);

                        if (Days > Days_In_Month)
                        {
                            Days = 1;
                            TotalMonths++;
                            Months++;
                            OnMonthsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Months > Months_In_Year)
                        {
                            Months = 1;
                            TotalYears++;
                            Years++;
                            OnYearsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Years >= 10)
                        {
                            Years = 0;
                            TotalDecades++;
                            Decades++;
                            OnDecadesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Decades >= 10)
                        {
                            Decades = 0;
                            TotalCenturies++;
                            Centuries++;
                            OnCenturiesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddDays(long amount, TimeRate rate)
        {
            long total_hours = amount * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;

            switch (rate)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                    TotalMilliseconds += total_milliseconds;
                    AddSeconds(total_seconds, rate);
                    break;

                case TimeRate.Minute:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    AddMinutes(total_minutes, rate);
                    break;

                case TimeRate.Hour:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    AddHours(total_hours, rate);
                    break;

                case TimeRate.Day:
                case TimeRate.Month:
                case TimeRate.Year:
                case TimeRate.Decade:
                case TimeRate.Century:
                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalDays++;
                        Days++;
                        OnDaysChange?.Invoke(this, EventArgs.Empty);

                        if (Days > Days_In_Month)
                        {
                            Days = 1;
                            TotalMonths++;
                            Months++;
                            OnMonthsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Months > Months_In_Year)
                        {
                            Months = 1;
                            TotalYears++;
                            Years++;
                            OnYearsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Years >= 10)
                        {
                            Years = 0;
                            TotalDecades++;
                            Decades++;
                            OnDecadesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Decades >= 10)
                        {
                            Decades = 0;
                            TotalCenturies++;
                            Centuries++;
                            OnCenturiesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddMonths(long amount)
        {
            long total_days = amount * Days_In_Month;
            long total_hours = total_days * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;

            switch (Interval)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                    TotalMilliseconds += total_milliseconds;
                    AddSeconds(total_seconds);
                    break;

                case TimeRate.Minute:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    AddMinutes(total_minutes);
                    break;

                case TimeRate.Hour:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    AddHours(total_hours);
                    break;

                case TimeRate.Day:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    AddDays(total_days);
                    break;

                case TimeRate.Month:
                case TimeRate.Year:
                case TimeRate.Decade:
                case TimeRate.Century:
                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalMonths++;
                        Months++;
                        OnMonthsChange?.Invoke(this, EventArgs.Empty);

                        if (Months > Months_In_Year)
                        {
                            Months = 1;
                            TotalYears++;
                            Years++;
                            OnYearsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Years >= 10)
                        {
                            Years = 0;
                            TotalDecades++;
                            Decades++;
                            OnDecadesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Decades >= 10)
                        {
                            Decades = 0;
                            TotalCenturies++;
                            Centuries++;
                            OnCenturiesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddMonths(long amount, TimeRate rate)
        {
            long total_days = amount * Days_In_Month;
            long total_hours = total_days * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;

            switch (rate)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                    TotalMilliseconds += total_milliseconds;
                    AddSeconds(total_seconds, rate);
                    break;

                case TimeRate.Minute:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    AddMinutes(total_minutes, rate);
                    break;

                case TimeRate.Hour:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    AddHours(total_hours, rate);
                    break;

                case TimeRate.Day:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    AddDays(total_days, rate);
                    break;

                case TimeRate.Month:
                case TimeRate.Year:
                case TimeRate.Decade:
                case TimeRate.Century:
                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalMonths++;
                        Months++;
                        OnMonthsChange?.Invoke(this, EventArgs.Empty);

                        if (Months > Months_In_Year)
                        {
                            Months = 1;
                            TotalYears++;
                            Years++;
                            OnYearsChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Years >= 10)
                        {
                            Years = 0;
                            TotalDecades++;
                            Decades++;
                            OnDecadesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Decades >= 10)
                        {
                            Decades = 0;
                            TotalCenturies++;
                            Centuries++;
                            OnCenturiesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddYears(long amount)
        {
            long total_months = amount * Months_In_Year;
            long total_days = total_months * Days_In_Month;
            long total_hours = total_days * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;

            switch (Interval)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                    TotalMilliseconds += total_milliseconds;
                    AddSeconds(total_seconds);
                    break;

                case TimeRate.Minute:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    AddMinutes(total_minutes);
                    break;

                case TimeRate.Hour:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    AddHours(total_hours);
                    break;

                case TimeRate.Day:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    AddDays(total_days);
                    break;

                case TimeRate.Month:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    AddMonths(total_months);
                    break;

                case TimeRate.Year:
                case TimeRate.Decade:
                case TimeRate.Century:
                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalYears++;
                        Years++;
                        OnYearsChange?.Invoke(this, EventArgs.Empty);

                        if (Years >= 10)
                        {
                            Years = 0;
                            TotalDecades++;
                            Decades++;
                            OnDecadesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Decades >= 10)
                        {
                            Decades = 0;
                            TotalCenturies++;
                            Centuries++;
                            OnCenturiesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddYears(long amount, TimeRate rate)
        {
            long total_months = amount * Months_In_Year;
            long total_days = total_months * Days_In_Month;
            long total_hours = total_days * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;

            switch (rate)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                    TotalMilliseconds += total_milliseconds;
                    AddSeconds(total_seconds, rate);
                    break;

                case TimeRate.Minute:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    AddMinutes(total_minutes, rate);
                    break;

                case TimeRate.Hour:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    AddHours(total_hours, rate);
                    break;

                case TimeRate.Day:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    AddDays(total_days, rate);
                    break;

                case TimeRate.Month:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    AddMonths(total_months, rate);
                    break;

                case TimeRate.Year:
                case TimeRate.Decade:
                case TimeRate.Century:
                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalYears++;
                        Years++;
                        OnYearsChange?.Invoke(this, EventArgs.Empty);

                        if (Years >= 10)
                        {
                            Years = 0;
                            TotalDecades++;
                            Decades++;
                            OnDecadesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Decades >= 10)
                        {
                            Decades = 0;
                            TotalCenturies++;
                            Centuries++;
                            OnCenturiesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddDecades(long amount)
        {
            long total_years = amount * 10;
            long total_months = total_years * Months_In_Year;
            long total_days = total_months * Days_In_Month;
            long total_hours = total_days * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;

            switch (Interval)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                    TotalMilliseconds += total_milliseconds;
                    AddSeconds(total_seconds);
                    break;

                case TimeRate.Minute:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    AddMinutes(total_minutes);
                    break;

                case TimeRate.Hour:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    AddHours(total_hours);
                    break;

                case TimeRate.Day:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    AddDays(total_days);
                    break;

                case TimeRate.Month:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    AddMonths(total_months);
                    break;

                case TimeRate.Year:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    AddYears(total_years);
                    break;

                case TimeRate.Decade:
                case TimeRate.Century:
                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    TotalYears += total_years;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalDecades++;
                        Decades++;
                        OnDecadesChange?.Invoke(this, EventArgs.Empty);

                        if (Decades >= 10)
                        {
                            Decades = 0;
                            TotalCenturies++;
                            Centuries++;
                            OnCenturiesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddDecades(long amount, TimeRate rate)
        {
            long total_years = amount * 10;
            long total_months = total_years * Months_In_Year;
            long total_days = total_months * Days_In_Month;
            long total_hours = total_days * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;

            switch (rate)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                    TotalMilliseconds += total_milliseconds;
                    AddSeconds(total_seconds, rate);
                    break;

                case TimeRate.Minute:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    AddMinutes(total_minutes, rate);
                    break;

                case TimeRate.Hour:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    AddHours(total_hours, rate);
                    break;

                case TimeRate.Day:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    AddDays(total_days, rate);
                    break;

                case TimeRate.Month:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    AddMonths(total_months, rate);
                    break;

                case TimeRate.Year:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    AddYears(total_years, rate);
                    break;

                case TimeRate.Decade:
                case TimeRate.Century:
                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    TotalYears += total_years;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalDecades++;
                        Decades++;
                        OnDecadesChange?.Invoke(this, EventArgs.Empty);

                        if (Decades >= 10)
                        {
                            Decades = 0;
                            TotalCenturies++;
                            Centuries++;
                            OnCenturiesChange?.Invoke(this, EventArgs.Empty);
                        }

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddCenturies(long amount)
        {
            long total_decades = amount * 10;
            long total_years = total_decades * 10;
            long total_months = total_years * Months_In_Year;
            long total_days = total_months * Days_In_Month;
            long total_hours = total_days * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;

            switch (Interval)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                    TotalMilliseconds += total_milliseconds;
                    AddSeconds(total_seconds);
                    break;

                case TimeRate.Minute:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    AddMinutes(total_minutes);
                    break;

                case TimeRate.Hour:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    AddHours(total_hours);
                    break;

                case TimeRate.Day:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    AddDays(total_days);
                    break;

                case TimeRate.Month:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    AddMonths(total_months);
                    break;

                case TimeRate.Year:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    AddYears(total_years);
                    break;

                case TimeRate.Decade:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    TotalYears += total_years;
                    AddDecades(total_decades);
                    break;

                case TimeRate.Century:
                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    TotalYears += total_years;
                    TotalDecades += total_decades;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalCenturies++;
                        Centuries++;
                        OnCenturiesChange?.Invoke(this, EventArgs.Empty);

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddCenturies(long amount, TimeRate rate)
        {
            long total_decades = amount * 10;
            long total_years = total_decades * 10;
            long total_months = total_years * Months_In_Year;
            long total_days = total_months * Days_In_Month;
            long total_hours = total_days * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;

            switch (rate)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                    TotalMilliseconds += total_milliseconds;
                    AddSeconds(total_seconds, rate);
                    break;

                case TimeRate.Minute:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    AddMinutes(total_minutes, rate);
                    break;

                case TimeRate.Hour:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    AddHours(total_hours, rate);
                    break;

                case TimeRate.Day:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    AddDays(total_days, rate);
                    break;

                case TimeRate.Month:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    AddMonths(total_months, rate);
                    break;

                case TimeRate.Year:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    AddYears(total_years, rate);
                    break;

                case TimeRate.Decade:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    TotalYears += total_years;
                    AddDecades(total_decades, rate);
                    break;

                case TimeRate.Century:
                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    TotalYears += total_years;
                    TotalDecades += total_decades;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalCenturies++;
                        Centuries++;
                        OnCenturiesChange?.Invoke(this, EventArgs.Empty);

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddMillennia(long amount)
        {
            long total_centuries = amount * 10;
            long total_decades = total_centuries * 10;
            long total_years = total_decades * 10;
            long total_months = total_years * Months_In_Year;
            long total_days = total_months * Days_In_Month;
            long total_hours = total_days * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;

            switch (Interval)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                    TotalMilliseconds += total_milliseconds;
                    AddSeconds(total_seconds);
                    break;

                case TimeRate.Minute:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    AddMinutes(total_minutes);
                    break;

                case TimeRate.Hour:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    AddHours(total_hours);
                    break;

                case TimeRate.Day:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    AddDays(total_days);
                    break;

                case TimeRate.Month:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    AddMonths(total_months);
                    break;

                case TimeRate.Year:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    AddYears(total_years);
                    break;

                case TimeRate.Decade:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    TotalYears += total_years;
                    AddDecades(total_decades);
                    break;

                case TimeRate.Century:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    TotalYears += total_years;
                    TotalDecades += total_decades;
                    AddCenturies(total_centuries);
                    break;

                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    TotalYears += total_years;
                    TotalDecades += total_decades;
                    TotalCenturies += total_centuries;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalMillennia++;
                        Millennia++;
                        OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddMillennia(long amount, TimeRate rate)
        {
            long total_centuries = amount * 10;
            long total_decades = total_centuries * 10;
            long total_years = total_decades * 10;
            long total_months = total_years * Months_In_Year;
            long total_days = total_months * Days_In_Month;
            long total_hours = total_days * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;

            switch (rate)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(total_milliseconds);
                    break;

                case TimeRate.Second:
                    TotalMilliseconds += total_milliseconds;
                    AddSeconds(total_seconds, rate);
                    break;

                case TimeRate.Minute:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    AddMinutes(total_minutes, rate);
                    break;

                case TimeRate.Hour:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    AddHours(total_hours, rate);
                    break;

                case TimeRate.Day:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    AddDays(total_days, rate);
                    break;

                case TimeRate.Month:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    AddMonths(total_months, rate);
                    break;

                case TimeRate.Year:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    AddYears(total_years, rate);
                    break;

                case TimeRate.Decade:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    TotalYears += total_years;
                    AddDecades(total_decades, rate);
                    break;

                case TimeRate.Century:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    TotalYears += total_years;
                    TotalDecades += total_decades;
                    AddCenturies(total_centuries, rate);
                    break;

                case TimeRate.Millennia:
                case TimeRate.Nothing:
                    TotalMilliseconds += total_milliseconds;
                    TotalSeconds += total_seconds;
                    TotalMinutes += total_minutes;
                    TotalHours += total_hours;
                    TotalDays += total_days;
                    TotalMonths += total_months;
                    TotalYears += total_years;
                    TotalDecades += total_decades;
                    TotalCenturies += total_centuries;

                    Processing = true;

                    for (long i = 1; i <= amount; i++)
                    {
                        TotalMillennia++;
                        Millennia++;
                        OnMillenniaChange?.Invoke(this, EventArgs.Empty);
                    }

                    Processing = false;
                    break;
            }
        }

        public virtual void AddTime(TimeRate rate, long amount)
        {
            switch (rate)
            {
                case TimeRate.Millisecond:
                    AddMilliseconds(amount);
                    break;

                case TimeRate.Second:
                    AddSeconds(amount);
                    break;

                case TimeRate.Minute:
                    AddMinutes(amount);
                    break;

                case TimeRate.Hour:
                    AddHours(amount);
                    break;

                case TimeRate.Day:
                    AddDays(amount);
                    break;

                case TimeRate.Month:
                    AddMonths(amount);
                    break;

                case TimeRate.Year:
                    AddYears(amount);
                    break;

                case TimeRate.Decade:
                    AddDecades(amount);
                    break;

                case TimeRate.Century:
                    AddCenturies(amount);
                    break;

                case TimeRate.Millennia:
                    AddMillennia(amount);
                    break;
            }
        }

        public virtual void AddTime(TimeRate rate, TimeSpan amount)
        {
            switch (rate)
            {
                case TimeRate.Millisecond:
                case TimeRate.Nothing:
                    AddMilliseconds((long)amount.TotalMilliseconds);
                    break;

                case TimeRate.Second:
                    AddSeconds((long)amount.TotalSeconds);
                    break;

                case TimeRate.Minute:
                    AddMinutes((long)amount.TotalMinutes);
                    break;

                case TimeRate.Hour:
                    AddHours((long)amount.TotalHours);
                    break;

                case TimeRate.Day:
                    AddDays((long)amount.TotalDays);
                    break;

                case TimeRate.Month:
                    long total_months = (long)amount.TotalDays / Days_In_Month;
                    AddMonths(total_months);
                    break;

                case TimeRate.Year:
                    long total_years = (long)amount.TotalDays / Days_In_Month / Months_In_Year;
                    AddYears(total_years);
                    break;

                case TimeRate.Decade:
                    long total_decades = (long)amount.TotalDays / Days_In_Month / Months_In_Year / 10;
                    AddDecades(total_decades);
                    break;

                case TimeRate.Century:
                    long total_centuries = (long)amount.TotalDays / Days_In_Month / Months_In_Year / 100;
                    AddCenturies(total_centuries);
                    break;

                case TimeRate.Millennia:
                    long total_millennia = (long)amount.TotalDays / Days_In_Month / Months_In_Year / 1000;
                    AddMillennia(total_millennia);
                    break;
            }
        }

        public virtual void AddTime(TimeSpan amount)
        {
            switch (Interval)
            {
                case TimeRate.Millisecond:
                case TimeRate.Nothing:
                    AddMilliseconds((long)amount.TotalMilliseconds);
                    break;

                case TimeRate.Second:
                    AddSeconds((long)amount.TotalSeconds);
                    break;

                case TimeRate.Minute:
                    AddMinutes((long)amount.TotalMinutes);
                    break;

                case TimeRate.Hour:
                    AddHours((long)amount.TotalHours);
                    break;

                case TimeRate.Day:
                    AddDays((long)amount.TotalDays);
                    break;

                case TimeRate.Month:
                    long total_months = (long)amount.TotalDays / Days_In_Month;
                    AddMonths(total_months);
                    break;

                case TimeRate.Year:
                    long total_years = (long)amount.TotalDays / Days_In_Month / Months_In_Year;
                    AddYears(total_years);
                    break;

                case TimeRate.Decade:
                    long total_decades = (long)amount.TotalDays / Days_In_Month / Months_In_Year / 10;
                    AddDecades(total_decades);
                    break;

                case TimeRate.Century:
                    long total_centuries = (long)amount.TotalDays / Days_In_Month / Months_In_Year / 100;
                    AddCenturies(total_centuries);
                    break;

                case TimeRate.Millennia:
                    long total_millennia = (long)amount.TotalDays / Days_In_Month / Months_In_Year / 1000;
                    AddMillennia(total_millennia);
                    break;
            }
        }

        public virtual void CopyTime(TimeHandler time)
        {
            Milliseconds = time.Milliseconds;
            Seconds = time.Seconds;
            Minutes = time.Minutes;
            Hours = time.Hours;
            Days = time.Days;
            Months = time.Months;
            Years = time.Years;
            Decades = time.Decades;
            Centuries = time.Centuries;
            Millennia = time.Millennia;
        }

        public virtual string ToString(bool military_time, bool include_milliseconds, bool include_date)
        {
            string result;
            int NewHours = (int)Hours;
            string hours;
            string minutes;
            string seconds;
            string am_pm = " AM";

            if (!military_time)
            {
                if (NewHours > 12)
                {
                    NewHours -= 12;
                    am_pm = " PM";
                }
                else if (NewHours == 0)
                {
                    NewHours = 12;
                }
                else if (NewHours == 12)
                {
                    am_pm = " PM";
                }
            }

            if (NewHours < 10)
            {
                hours = "0" + NewHours.ToString();
            }
            else
            {
                hours = NewHours.ToString();
            }

            if (Minutes < 10)
            {
                minutes = "0" + Minutes.ToString();
            }
            else
            {
                minutes = Minutes.ToString();
            }

            if (Seconds < 10)
            {
                seconds = "0" + Seconds.ToString();
            }
            else
            {
                seconds = Seconds.ToString();
            }

            string time = hours + ":" + minutes + ":" + seconds;

            if (include_milliseconds)
            {
                string milliseconds;
                if (Milliseconds < 10)
                {
                    milliseconds = "00" + Milliseconds.ToString();
                }
                else if (Milliseconds < 100)
                {
                    milliseconds = "0" + Milliseconds.ToString();
                }
                else
                {
                    milliseconds = Milliseconds.ToString();
                }

                time += "." + milliseconds;
            }

            if (include_date)
            {
                string days;
                if (Days < 10)
                {
                    days = "0" + Days.ToString();
                }
                else
                {
                    days = Days.ToString();
                }

                string months;
                if (Months < 10)
                {
                    months = "0" + Months.ToString();
                }
                else
                {
                    months = Months.ToString();
                }

                string years = Millennia.ToString() + Centuries.ToString() + Decades.ToString() + Years.ToString();

                result = months + "/" + days + "/" + years + " " + time;
            }
            else
            {
                result = time;
            }

            if (!military_time)
            {
                result += am_pm;
            }

            return result;
        }

        public virtual string ToString(bool military_time, bool include_milliseconds)
        {
            int NewHours = (int)Hours;
            string hours;
            string minutes;
            string seconds;
            string am_pm = " AM";

            if (!military_time)
            {
                if (NewHours > 12)
                {
                    NewHours -= 12;
                    am_pm = " PM";
                }
                else if (NewHours == 0)
                {
                    NewHours = 12;
                }
                else if (NewHours == 12)
                {
                    am_pm = " PM";
                }
            }

            if (NewHours < 10)
            {
                hours = "0" + NewHours.ToString();
            }
            else
            {
                hours = NewHours.ToString();
            }

            if (Minutes < 10)
            {
                minutes = "0" + Minutes.ToString();
            }
            else
            {
                minutes = Minutes.ToString();
            }

            if (Seconds < 10)
            {
                seconds = "0" + Seconds.ToString();
            }
            else
            {
                seconds = Seconds.ToString();
            }

            string time = hours + ":" + minutes + ":" + seconds;

            if (include_milliseconds)
            {
                string milliseconds;
                if (Milliseconds < 10)
                {
                    milliseconds = "00" + Milliseconds.ToString();
                }
                else if (Milliseconds < 100)
                {
                    milliseconds = "0" + Milliseconds.ToString();
                }
                else
                {
                    milliseconds = Milliseconds.ToString();
                }

                time += "." + milliseconds;
            }

            if (military_time)
            {
                return time;
            }
            else
            {
                return time + am_pm;
            }
        }

        public virtual string ToString(bool military_time)
        {
            int NewHours = (int)Hours;
            string hours;
            string minutes;
            string seconds;
            string am_pm = " AM";

            if (!military_time)
            {
                if (NewHours > 12)
                {
                    NewHours -= 12;
                    am_pm = " PM";
                }
                else if (NewHours == 0)
                {
                    NewHours = 12;
                }
                else if (NewHours == 12)
                {
                    am_pm = " PM";
                }
            }

            if (NewHours < 10)
            {
                hours = "0" + NewHours.ToString();
            }
            else
            {
                hours = NewHours.ToString();
            }

            if (Minutes < 10)
            {
                minutes = "0" + Minutes.ToString();
            }
            else
            {
                minutes = Minutes.ToString();
            }

            if (Seconds < 10)
            {
                seconds = "0" + Seconds.ToString();
            }
            else
            {
                seconds = Seconds.ToString();
            }

            string time = hours + ":" + minutes + ":" + seconds;

            if (military_time)
            {
                return time;
            }
            else
            {
                return time + am_pm;
            }
        }

        public virtual void Dispose()
        {

        }

        #endregion
    }
}
