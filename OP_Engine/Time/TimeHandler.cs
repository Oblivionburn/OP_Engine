using System;

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

        public event EventHandler MillisecondsChanged;
        public event EventHandler SecondsChanged;
        public event EventHandler MinutesChanged;
        public event EventHandler HoursChanged;
        public event EventHandler DaysChanged;
        public event EventHandler MonthsChanged;
        public event EventHandler YearsChanged;
        public event EventHandler DecadesChanged;
        public event EventHandler CenturiesChanged;
        public event EventHandler MillenniaChanged;

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
                MillisecondsChanged?.Invoke(this, EventArgs.Empty);

                if (Milliseconds >= 1000)
                {
                    Milliseconds = 0;

                    TotalSeconds++;
                    Seconds++;
                    SecondsChanged?.Invoke(this, EventArgs.Empty);

                    if (Seconds >= 60)
                    {
                        Seconds = 0;

                        TotalMinutes++;
                        Minutes++;
                        MinutesChanged?.Invoke(this, EventArgs.Empty);

                        if (Minutes >= 60)
                        {
                            Minutes = 0;

                            TotalHours++;
                            Hours++;
                            HoursChanged?.Invoke(this, EventArgs.Empty);

                            if (Hours >= Hours_In_Day)
                            {
                                Hours = 0;
                                TotalDays++;
                                Days++;
                                DaysChanged?.Invoke(this, EventArgs.Empty);

                                if (Days > Days_In_Month)
                                {
                                    Days = 1;
                                    TotalMonths++;
                                    Months++;
                                    MonthsChanged?.Invoke(this, EventArgs.Empty);

                                    if (Months > Months_In_Year)
                                    {
                                        Months = 1;
                                        TotalYears++;
                                        Years++;
                                        YearsChanged?.Invoke(this, EventArgs.Empty);

                                        if (Years >= 10)
                                        {
                                            Years = 0;
                                            TotalDecades++;
                                            Decades++;
                                            DecadesChanged?.Invoke(this, EventArgs.Empty);

                                            if (Decades >= 10)
                                            {
                                                Decades = 0;
                                                TotalCenturies++;
                                                Centuries++;
                                                CenturiesChanged?.Invoke(this, EventArgs.Empty);

                                                if (Centuries >= 10)
                                                {
                                                    Centuries = 0;
                                                    TotalMillennia++;
                                                    Millennia++;
                                                    MillenniaChanged?.Invoke(this, EventArgs.Empty);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Processing = false;
        }

        public virtual void AddSeconds(long amount)
        {
            if (Interval == TimeRate.Millisecond)
            {
                long total_milliseconds = amount * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (Interval == TimeRate.Nothing ||
                     Interval == TimeRate.Second ||
                     Interval == TimeRate.Minute ||
                     Interval == TimeRate.Hour ||
                     Interval == TimeRate.Day ||
                     Interval == TimeRate.Month ||
                     Interval == TimeRate.Year ||
                     Interval == TimeRate.Decade ||
                     Interval == TimeRate.Century ||
                     Interval == TimeRate.Millennia)
            {
                TotalMilliseconds += amount * 1000;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalSeconds++;
                    Seconds++;
                    SecondsChanged?.Invoke(this, EventArgs.Empty);

                    if (Seconds >= 60)
                    {
                        Seconds = 0;

                        TotalMinutes++;
                        Minutes++;
                        MinutesChanged?.Invoke(this, EventArgs.Empty);

                        if (Minutes >= 60)
                        {
                            Minutes = 0;

                            TotalHours++;
                            Hours++;
                            HoursChanged?.Invoke(this, EventArgs.Empty);

                            if (Hours >= Hours_In_Day)
                            {
                                Hours = 0;
                                TotalDays++;
                                Days++;
                                DaysChanged?.Invoke(this, EventArgs.Empty);

                                if (Days > Days_In_Month)
                                {
                                    Days = 1;
                                    TotalMonths++;
                                    Months++;
                                    MonthsChanged?.Invoke(this, EventArgs.Empty);

                                    if (Months > Months_In_Year)
                                    {
                                        Months = 1;
                                        TotalYears++;
                                        Years++;
                                        YearsChanged?.Invoke(this, EventArgs.Empty);

                                        if (Years >= 10)
                                        {
                                            Years = 0;
                                            TotalDecades++;
                                            Decades++;
                                            DecadesChanged?.Invoke(this, EventArgs.Empty);

                                            if (Decades >= 10)
                                            {
                                                Decades = 0;
                                                TotalCenturies++;
                                                Centuries++;
                                                CenturiesChanged?.Invoke(this, EventArgs.Empty);

                                                if (Centuries >= 10)
                                                {
                                                    Centuries = 0;
                                                    TotalMillennia++;
                                                    Millennia++;
                                                    MillenniaChanged?.Invoke(this, EventArgs.Empty);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                Processing = false;
            }
        }

        public virtual void AddSeconds(long amount, TimeRate rate)
        {
            if (rate == TimeRate.Millisecond)
            {
                long total_milliseconds = amount * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (rate == TimeRate.Nothing ||
                     rate == TimeRate.Second ||
                     rate == TimeRate.Minute ||
                     rate == TimeRate.Hour ||
                     rate == TimeRate.Day ||
                     rate == TimeRate.Month ||
                     rate == TimeRate.Year ||
                     rate == TimeRate.Decade ||
                     rate == TimeRate.Century ||
                     rate == TimeRate.Millennia)
            {
                TotalMilliseconds += amount * 1000;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalSeconds++;
                    Seconds++;
                    SecondsChanged?.Invoke(this, EventArgs.Empty);

                    if (Seconds >= 60)
                    {
                        Seconds = 0;

                        TotalMinutes++;
                        Minutes++;
                        MinutesChanged?.Invoke(this, EventArgs.Empty);

                        if (Minutes >= 60)
                        {
                            Minutes = 0;

                            TotalHours++;
                            Hours++;
                            HoursChanged?.Invoke(this, EventArgs.Empty);

                            if (Hours >= Hours_In_Day)
                            {
                                Hours = 0;
                                TotalDays++;
                                Days++;
                                DaysChanged?.Invoke(this, EventArgs.Empty);

                                if (Days > Days_In_Month)
                                {
                                    Days = 1;
                                    TotalMonths++;
                                    Months++;
                                    MonthsChanged?.Invoke(this, EventArgs.Empty);

                                    if (Months > Months_In_Year)
                                    {
                                        Months = 1;
                                        TotalYears++;
                                        Years++;
                                        YearsChanged?.Invoke(this, EventArgs.Empty);

                                        if (Years >= 10)
                                        {
                                            Years = 0;
                                            TotalDecades++;
                                            Decades++;
                                            DecadesChanged?.Invoke(this, EventArgs.Empty);

                                            if (Decades >= 10)
                                            {
                                                Decades = 0;
                                                TotalCenturies++;
                                                Centuries++;
                                                CenturiesChanged?.Invoke(this, EventArgs.Empty);

                                                if (Centuries >= 10)
                                                {
                                                    Centuries = 0;
                                                    TotalMillennia++;
                                                    Millennia++;
                                                    MillenniaChanged?.Invoke(this, EventArgs.Empty);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                Processing = false;
            }
        }

        public virtual void AddMinutes(long amount)
        {
            if (Interval == TimeRate.Millisecond)
            {
                long total_seconds = amount * 60;
                long total_milliseconds = total_seconds * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (Interval == TimeRate.Second)
            {
                long total_seconds = amount * 60;
                TotalMilliseconds += total_seconds * 1000;
                AddSeconds(total_seconds);
            }
            else if (Interval == TimeRate.Nothing ||
                     Interval == TimeRate.Minute ||
                     Interval == TimeRate.Hour ||
                     Interval == TimeRate.Day ||
                     Interval == TimeRate.Month ||
                     Interval == TimeRate.Year ||
                     Interval == TimeRate.Decade ||
                     Interval == TimeRate.Century ||
                     Interval == TimeRate.Millennia)
            {
                long total_seconds = amount * 60;
                long total_milliseconds = total_seconds * 1000;

                TotalSeconds += total_seconds;
                TotalMilliseconds += total_milliseconds;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalMinutes++;
                    Minutes++;
                    MinutesChanged?.Invoke(this, EventArgs.Empty);

                    if (Minutes >= 60)
                    {
                        Minutes = 0;

                        TotalHours++;
                        Hours++;
                        HoursChanged?.Invoke(this, EventArgs.Empty);

                        if (Hours >= Hours_In_Day)
                        {
                            Hours = 0;
                            TotalDays++;
                            Days++;
                            DaysChanged?.Invoke(this, EventArgs.Empty);

                            if (Days > Days_In_Month)
                            {
                                Days = 1;
                                TotalMonths++;
                                Months++;
                                MonthsChanged?.Invoke(this, EventArgs.Empty);

                                if (Months > Months_In_Year)
                                {
                                    Months = 1;
                                    TotalYears++;
                                    Years++;
                                    YearsChanged?.Invoke(this, EventArgs.Empty);

                                    if (Years >= 10)
                                    {
                                        Years = 0;
                                        TotalDecades++;
                                        Decades++;
                                        DecadesChanged?.Invoke(this, EventArgs.Empty);

                                        if (Decades >= 10)
                                        {
                                            Decades = 0;
                                            TotalCenturies++;
                                            Centuries++;
                                            CenturiesChanged?.Invoke(this, EventArgs.Empty);

                                            if (Centuries >= 10)
                                            {
                                                Centuries = 0;
                                                TotalMillennia++;
                                                Millennia++;
                                                MillenniaChanged?.Invoke(this, EventArgs.Empty);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                Processing = false;
            }
        }

        public virtual void AddMinutes(long amount, TimeRate rate)
        {
            if (rate == TimeRate.Millisecond)
            {
                long total_seconds = amount * 60;
                long total_milliseconds = total_seconds * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (rate == TimeRate.Second)
            {
                long total_seconds = amount * 60;
                TotalMilliseconds += total_seconds * 1000;
                AddSeconds(total_seconds, rate);
            }
            else if (rate == TimeRate.Nothing ||
                     rate == TimeRate.Minute ||
                     rate == TimeRate.Hour ||
                     rate == TimeRate.Day ||
                     rate == TimeRate.Month ||
                     rate == TimeRate.Year ||
                     rate == TimeRate.Decade ||
                     rate == TimeRate.Century ||
                     rate == TimeRate.Millennia)
            {
                long total_seconds = amount * 60;
                long total_milliseconds = total_seconds * 1000;

                TotalSeconds += total_seconds;
                TotalMilliseconds += total_milliseconds;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalMinutes++;
                    Minutes++;
                    MinutesChanged?.Invoke(this, EventArgs.Empty);

                    if (Minutes >= 60)
                    {
                        Minutes = 0;

                        TotalHours++;
                        Hours++;
                        HoursChanged?.Invoke(this, EventArgs.Empty);

                        if (Hours >= Hours_In_Day)
                        {
                            Hours = 0;
                            TotalDays++;
                            Days++;
                            DaysChanged?.Invoke(this, EventArgs.Empty);

                            if (Days > Days_In_Month)
                            {
                                Days = 1;
                                TotalMonths++;
                                Months++;
                                MonthsChanged?.Invoke(this, EventArgs.Empty);

                                if (Months > Months_In_Year)
                                {
                                    Months = 1;
                                    TotalYears++;
                                    Years++;
                                    YearsChanged?.Invoke(this, EventArgs.Empty);

                                    if (Years >= 10)
                                    {
                                        Years = 0;
                                        TotalDecades++;
                                        Decades++;
                                        DecadesChanged?.Invoke(this, EventArgs.Empty);

                                        if (Decades >= 10)
                                        {
                                            Decades = 0;
                                            TotalCenturies++;
                                            Centuries++;
                                            CenturiesChanged?.Invoke(this, EventArgs.Empty);

                                            if (Centuries >= 10)
                                            {
                                                Centuries = 0;
                                                TotalMillennia++;
                                                Millennia++;
                                                MillenniaChanged?.Invoke(this, EventArgs.Empty);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                Processing = false;
            }
        }

        public virtual void AddHours(long amount)
        {
            if (Interval == TimeRate.Millisecond)
            {
                long total_minutes = amount * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (Interval == TimeRate.Second)
            {
                long total_minutes = amount * 60;
                long total_seconds = total_minutes * 60;
                TotalMilliseconds += total_seconds * 1000;
                AddSeconds(total_seconds);
            }
            else if (Interval == TimeRate.Minute)
            {
                long total_minutes = amount * 60;
                TotalSeconds += total_minutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMinutes(total_minutes);
            }
            else if (Interval == TimeRate.Nothing ||
                     Interval == TimeRate.Hour ||
                     Interval == TimeRate.Day ||
                     Interval == TimeRate.Month ||
                     Interval == TimeRate.Year ||
                     Interval == TimeRate.Decade ||
                     Interval == TimeRate.Century ||
                     Interval == TimeRate.Millennia)
            {
                long total_minutes = amount * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;

                TotalMinutes += total_minutes;
                TotalSeconds += total_seconds;
                TotalMilliseconds += total_milliseconds;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalHours++;
                    Hours++;
                    HoursChanged?.Invoke(this, EventArgs.Empty);

                    if (Hours >= Hours_In_Day)
                    {
                        Hours = 0;
                        TotalDays++;
                        Days++;
                        DaysChanged?.Invoke(this, EventArgs.Empty);

                        if (Days > Days_In_Month)
                        {
                            Days = 1;
                            TotalMonths++;
                            Months++;
                            MonthsChanged?.Invoke(this, EventArgs.Empty);

                            if (Months > Months_In_Year)
                            {
                                Months = 1;
                                TotalYears++;
                                Years++;
                                YearsChanged?.Invoke(this, EventArgs.Empty);

                                if (Years >= 10)
                                {
                                    Years = 0;
                                    TotalDecades++;
                                    Decades++;
                                    DecadesChanged?.Invoke(this, EventArgs.Empty);

                                    if (Decades >= 10)
                                    {
                                        Decades = 0;
                                        TotalCenturies++;
                                        Centuries++;
                                        CenturiesChanged?.Invoke(this, EventArgs.Empty);

                                        if (Centuries >= 10)
                                        {
                                            Centuries = 0;
                                            TotalMillennia++;
                                            Millennia++;
                                            MillenniaChanged?.Invoke(this, EventArgs.Empty);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                Processing = false;
            }
        }

        public virtual void AddHours(long amount, TimeRate rate)
        {
            if (rate == TimeRate.Millisecond)
            {
                long total_minutes = amount * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (rate == TimeRate.Second)
            {
                long total_minutes = amount * 60;
                long total_seconds = total_minutes * 60;
                TotalMilliseconds += total_seconds * 1000;
                AddSeconds(total_seconds, rate);
            }
            else if (rate == TimeRate.Minute)
            {
                long total_minutes = amount * 60;
                TotalSeconds += total_minutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMinutes(total_minutes, rate);
            }
            else if (rate == TimeRate.Nothing ||
                     rate == TimeRate.Hour ||
                     rate == TimeRate.Day ||
                     rate == TimeRate.Month ||
                     rate == TimeRate.Year ||
                     rate == TimeRate.Decade ||
                     rate == TimeRate.Century ||
                     rate == TimeRate.Millennia)
            {
                long total_minutes = amount * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;

                TotalMinutes += total_minutes;
                TotalSeconds += total_seconds;
                TotalMilliseconds += total_milliseconds;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalHours++;
                    Hours++;
                    HoursChanged?.Invoke(this, EventArgs.Empty);

                    if (Hours >= Hours_In_Day)
                    {
                        Hours = 0;
                        TotalDays++;
                        Days++;
                        DaysChanged?.Invoke(this, EventArgs.Empty);

                        if (Days > Days_In_Month)
                        {
                            Days = 1;
                            TotalMonths++;
                            Months++;
                            MonthsChanged?.Invoke(this, EventArgs.Empty);

                            if (Months > Months_In_Year)
                            {
                                Months = 1;
                                TotalYears++;
                                Years++;
                                YearsChanged?.Invoke(this, EventArgs.Empty);

                                if (Years >= 10)
                                {
                                    Years = 0;
                                    TotalDecades++;
                                    Decades++;
                                    DecadesChanged?.Invoke(this, EventArgs.Empty);

                                    if (Decades >= 10)
                                    {
                                        Decades = 0;
                                        TotalCenturies++;
                                        Centuries++;
                                        CenturiesChanged?.Invoke(this, EventArgs.Empty);

                                        if (Centuries >= 10)
                                        {
                                            Centuries = 0;
                                            TotalMillennia++;
                                            Millennia++;
                                            MillenniaChanged?.Invoke(this, EventArgs.Empty);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                Processing = false;
            }
        }

        public virtual void AddDays(long amount)
        {
            if (Interval == TimeRate.Millisecond)
            {
                long total_hours = amount * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (Interval == TimeRate.Second)
            {
                long total_hours = amount * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                TotalMilliseconds += total_seconds * 1000;
                AddSeconds(total_seconds);
            }
            else if (Interval == TimeRate.Minute)
            {
                long total_hours = amount * Hours_In_Day;
                long total_minutes = total_hours * 60;
                TotalSeconds += total_minutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMinutes(total_minutes);
            }
            else if (Interval == TimeRate.Hour)
            {
                long total_hours = amount * Hours_In_Day;
                TotalMinutes += total_hours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddHours(total_hours);
            }
            else if (Interval == TimeRate.Nothing ||
                     Interval == TimeRate.Day ||
                     Interval == TimeRate.Month ||
                     Interval == TimeRate.Year ||
                     Interval == TimeRate.Decade ||
                     Interval == TimeRate.Century ||
                     Interval == TimeRate.Millennia)
            {
                long total_hours = amount * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;

                TotalHours += total_hours;
                TotalMinutes += total_minutes;
                TotalSeconds += total_seconds;
                TotalMilliseconds += total_milliseconds;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalDays++;
                    Days++;
                    DaysChanged?.Invoke(this, EventArgs.Empty);

                    if (Days > Days_In_Month)
                    {
                        Days = 1;
                        TotalMonths++;
                        Months++;
                        MonthsChanged?.Invoke(this, EventArgs.Empty);

                        if (Months > Months_In_Year)
                        {
                            Months = 1;
                            TotalYears++;
                            Years++;
                            YearsChanged?.Invoke(this, EventArgs.Empty);

                            if (Years >= 10)
                            {
                                Years = 0;
                                TotalDecades++;
                                Decades++;
                                DecadesChanged?.Invoke(this, EventArgs.Empty);

                                if (Decades >= 10)
                                {
                                    Decades = 0;
                                    TotalCenturies++;
                                    Centuries++;
                                    CenturiesChanged?.Invoke(this, EventArgs.Empty);

                                    if (Centuries >= 10)
                                    {
                                        Centuries = 0;
                                        TotalMillennia++;
                                        Millennia++;
                                        MillenniaChanged?.Invoke(this, EventArgs.Empty);
                                    }
                                }
                            }
                        }
                    }
                }

                Processing = false;
            }
        }

        public virtual void AddDays(long amount, TimeRate rate)
        {
            if (rate == TimeRate.Millisecond)
            {
                long total_hours = amount * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (rate == TimeRate.Second)
            {
                long total_hours = amount * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                TotalMilliseconds += total_seconds * 1000;
                AddSeconds(total_seconds, rate);
            }
            else if (rate == TimeRate.Minute)
            {
                long total_hours = amount * Hours_In_Day;
                long total_minutes = total_hours * 60;
                TotalSeconds += total_minutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMinutes(total_minutes, rate);
            }
            else if (rate == TimeRate.Hour)
            {
                long total_hours = amount * Hours_In_Day;
                TotalMinutes += total_hours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddHours(total_hours, rate);
            }
            else if (rate == TimeRate.Nothing ||
                     rate == TimeRate.Day ||
                     rate == TimeRate.Month ||
                     rate == TimeRate.Year ||
                     rate == TimeRate.Decade ||
                     rate == TimeRate.Century ||
                     rate == TimeRate.Millennia)
            {
                long total_hours = amount * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;

                TotalHours += total_hours;
                TotalMinutes += total_minutes;
                TotalSeconds += total_seconds;
                TotalMilliseconds += total_milliseconds;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalDays++;
                    Days++;
                    DaysChanged?.Invoke(this, EventArgs.Empty);

                    if (Days > Days_In_Month)
                    {
                        Days = 1;
                        TotalMonths++;
                        Months++;
                        MonthsChanged?.Invoke(this, EventArgs.Empty);

                        if (Months > Months_In_Year)
                        {
                            Months = 1;
                            TotalYears++;
                            Years++;
                            YearsChanged?.Invoke(this, EventArgs.Empty);

                            if (Years >= 10)
                            {
                                Years = 0;
                                TotalDecades++;
                                Decades++;
                                DecadesChanged?.Invoke(this, EventArgs.Empty);

                                if (Decades >= 10)
                                {
                                    Decades = 0;
                                    TotalCenturies++;
                                    Centuries++;
                                    CenturiesChanged?.Invoke(this, EventArgs.Empty);

                                    if (Centuries >= 10)
                                    {
                                        Centuries = 0;
                                        TotalMillennia++;
                                        Millennia++;
                                        MillenniaChanged?.Invoke(this, EventArgs.Empty);
                                    }
                                }
                            }
                        }
                    }
                }

                Processing = false;
            }
        }

        public virtual void AddMonths(long amount)
        {
            if (Interval == TimeRate.Millisecond)
            {
                long total_days = amount * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (Interval == TimeRate.Second)
            {
                long total_days = amount * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                TotalMilliseconds += total_seconds * 1000;
                AddSeconds(total_seconds);
            }
            else if (Interval == TimeRate.Minute)
            {
                long total_days = amount * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                TotalSeconds += total_minutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMinutes(total_minutes);
            }
            else if (Interval == TimeRate.Hour)
            {
                long total_days = amount * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                TotalMinutes += total_hours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddHours(total_hours);
            }
            else if (Interval == TimeRate.Day)
            {
                long total_days = amount * Days_In_Month;
                TotalHours += total_days * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddDays(total_days);
            }
            else if (Interval == TimeRate.Nothing ||
                     Interval == TimeRate.Month ||
                     Interval == TimeRate.Year ||
                     Interval == TimeRate.Decade ||
                     Interval == TimeRate.Century ||
                     Interval == TimeRate.Millennia)
            {
                long total_days = amount * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;

                TotalDays += total_days;
                TotalHours += total_hours;
                TotalMinutes += total_minutes;
                TotalSeconds += total_seconds;
                TotalMilliseconds += total_milliseconds;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalMonths++;
                    Months++;
                    MonthsChanged?.Invoke(this, EventArgs.Empty);

                    if (Months > Months_In_Year)
                    {
                        Months = 1;
                        TotalYears++;
                        Years++;
                        YearsChanged?.Invoke(this, EventArgs.Empty);

                        if (Years >= 10)
                        {
                            Years = 0;
                            TotalDecades++;
                            Decades++;
                            DecadesChanged?.Invoke(this, EventArgs.Empty);

                            if (Decades >= 10)
                            {
                                Decades = 0;
                                TotalCenturies++;
                                Centuries++;
                                CenturiesChanged?.Invoke(this, EventArgs.Empty);

                                if (Centuries >= 10)
                                {
                                    Centuries = 0;
                                    TotalMillennia++;
                                    Millennia++;
                                    MillenniaChanged?.Invoke(this, EventArgs.Empty);
                                }
                            }
                        }
                    }
                }

                Processing = false;
            }
        }

        public virtual void AddMonths(long amount, TimeRate rate)
        {
            if (rate == TimeRate.Millisecond)
            {
                long total_days = amount * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (rate == TimeRate.Second)
            {
                long total_days = amount * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                TotalMilliseconds += total_seconds * 1000;
                AddSeconds(total_seconds, rate);
            }
            else if (rate == TimeRate.Minute)
            {
                long total_days = amount * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                TotalSeconds += total_minutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMinutes(total_minutes, rate);
            }
            else if (rate == TimeRate.Hour)
            {
                long total_days = amount * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                TotalMinutes += total_hours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddHours(total_hours, rate);
            }
            else if (rate == TimeRate.Day)
            {
                long total_days = amount * Days_In_Month;
                TotalHours += total_days * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddDays(total_days, rate);
            }
            else if (rate == TimeRate.Nothing ||
                     rate == TimeRate.Month ||
                     rate == TimeRate.Year ||
                     rate == TimeRate.Decade ||
                     rate == TimeRate.Century ||
                     rate == TimeRate.Millennia)
            {
                long total_days = amount * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;

                TotalDays += total_days;
                TotalHours += total_hours;
                TotalMinutes += total_minutes;
                TotalSeconds += total_seconds;
                TotalMilliseconds += total_milliseconds;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalMonths++;
                    Months++;
                    MonthsChanged?.Invoke(this, EventArgs.Empty);

                    if (Months > Months_In_Year)
                    {
                        Months = 1;
                        TotalYears++;
                        Years++;
                        YearsChanged?.Invoke(this, EventArgs.Empty);

                        if (Years >= 10)
                        {
                            Years = 0;
                            TotalDecades++;
                            Decades++;
                            DecadesChanged?.Invoke(this, EventArgs.Empty);

                            if (Decades >= 10)
                            {
                                Decades = 0;
                                TotalCenturies++;
                                Centuries++;
                                CenturiesChanged?.Invoke(this, EventArgs.Empty);

                                if (Centuries >= 10)
                                {
                                    Centuries = 0;
                                    TotalMillennia++;
                                    Millennia++;
                                    MillenniaChanged?.Invoke(this, EventArgs.Empty);
                                }
                            }
                        }
                    }
                }

                Processing = false;
            }
        }

        public virtual void AddYears(long amount)
        {
            if (Interval == TimeRate.Millisecond)
            {
                long total_months = amount * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (Interval == TimeRate.Second)
            {
                long total_months = amount * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                TotalMilliseconds += total_seconds * 1000;
                AddSeconds(total_seconds);
            }
            else if (Interval == TimeRate.Minute)
            {
                long total_months = amount * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                TotalSeconds += total_minutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMinutes(total_minutes);
            }
            else if (Interval == TimeRate.Hour)
            {
                long total_months = amount * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                TotalMinutes += total_hours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddHours(total_hours);
            }
            else if (Interval == TimeRate.Day)
            {
                long total_months = amount * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                TotalHours += total_days * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddDays(total_days);
            }
            else if (Interval == TimeRate.Month)
            {
                long total_months = amount * Months_In_Year;
                TotalDays += total_months * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMonths(total_months);
            }
            else if (Interval == TimeRate.Nothing ||
                     Interval == TimeRate.Year ||
                     Interval == TimeRate.Decade ||
                     Interval == TimeRate.Century ||
                     Interval == TimeRate.Millennia)
            {
                long total_months = amount * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;

                TotalMonths += total_months;
                TotalDays += total_days;
                TotalHours += total_hours;
                TotalMinutes += total_minutes;
                TotalSeconds += total_seconds;
                TotalMilliseconds += total_milliseconds;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalYears++;
                    Years++;
                    YearsChanged?.Invoke(this, EventArgs.Empty);

                    if (Years >= 10)
                    {
                        Years = 0;
                        TotalDecades++;
                        Decades++;
                        DecadesChanged?.Invoke(this, EventArgs.Empty);

                        if (Decades >= 10)
                        {
                            Decades = 0;
                            TotalCenturies++;
                            Centuries++;
                            CenturiesChanged?.Invoke(this, EventArgs.Empty);

                            if (Centuries >= 10)
                            {
                                Centuries = 0;
                                TotalMillennia++;
                                Millennia++;
                                MillenniaChanged?.Invoke(this, EventArgs.Empty);
                            }
                        }
                    }
                }

                Processing = false;
            }
        }

        public virtual void AddYears(long amount, TimeRate rate)
        {
            if (rate == TimeRate.Millisecond)
            {
                long total_months = amount * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (rate == TimeRate.Second)
            {
                long total_months = amount * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                TotalMilliseconds += total_seconds * 1000;
                AddSeconds(total_seconds, rate);
            }
            else if (rate == TimeRate.Minute)
            {
                long total_months = amount * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                TotalSeconds += total_minutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMinutes(total_minutes, rate);
            }
            else if (rate == TimeRate.Hour)
            {
                long total_months = amount * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                TotalMinutes += total_hours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddHours(total_hours, rate);
            }
            else if (rate == TimeRate.Day)
            {
                long total_months = amount * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                TotalHours += total_days * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddDays(total_days, rate);
            }
            else if (rate == TimeRate.Month)
            {
                long total_months = amount * Months_In_Year;
                TotalDays += total_months * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMonths(total_months, rate);
            }
            else if (rate == TimeRate.Nothing ||
                     rate == TimeRate.Year ||
                     rate == TimeRate.Decade ||
                     rate == TimeRate.Century ||
                     rate == TimeRate.Millennia)
            {
                long total_months = amount * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;

                TotalMonths += total_months;
                TotalDays += total_days;
                TotalHours += total_hours;
                TotalMinutes += total_minutes;
                TotalSeconds += total_seconds;
                TotalMilliseconds += total_milliseconds;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalYears++;
                    Years++;
                    YearsChanged?.Invoke(this, EventArgs.Empty);

                    if (Years >= 10)
                    {
                        Years = 0;
                        TotalDecades++;
                        Decades++;
                        DecadesChanged?.Invoke(this, EventArgs.Empty);

                        if (Decades >= 10)
                        {
                            Decades = 0;
                            TotalCenturies++;
                            Centuries++;
                            CenturiesChanged?.Invoke(this, EventArgs.Empty);

                            if (Centuries >= 10)
                            {
                                Centuries = 0;
                                TotalMillennia++;
                                Millennia++;
                                MillenniaChanged?.Invoke(this, EventArgs.Empty);
                            }
                        }
                    }
                }

                Processing = false;
            }
        }

        public virtual void AddDecades(long amount)
        {
            if (Interval == TimeRate.Millisecond)
            {
                long total_years = amount * 10;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (Interval == TimeRate.Second)
            {
                long total_years = amount * 10;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                TotalMilliseconds += total_seconds * 1000;
                AddSeconds(total_seconds);
            }
            else if (Interval == TimeRate.Minute)
            {
                long total_years = amount * 10;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                TotalSeconds += total_minutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMinutes(total_minutes);
            }
            else if (Interval == TimeRate.Hour)
            {
                long total_years = amount * 10;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                TotalMinutes += total_hours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddHours(total_hours);
            }
            else if (Interval == TimeRate.Day)
            {
                long total_years = amount * 10;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                TotalHours += total_days * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddDays(total_days);
            }
            else if (Interval == TimeRate.Month)
            {
                long total_years = amount * 10;
                long total_months = total_years * Months_In_Year;
                TotalDays += total_months * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMonths(total_months);
            }
            else if (Interval == TimeRate.Year)
            {
                long total_years = amount * 10;
                TotalMonths += total_years * Months_In_Year;
                TotalDays += TotalMonths * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddYears(total_years);
            }
            else if (Interval == TimeRate.Nothing ||
                     Interval == TimeRate.Decade ||
                     Interval == TimeRate.Century ||
                     Interval == TimeRate.Millennia)
            {
                long total_years = amount * 10;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;

                TotalYears += total_years;
                TotalMonths += total_months;
                TotalDays += total_days;
                TotalHours += total_hours;
                TotalMinutes += total_minutes;
                TotalSeconds += total_seconds;
                TotalMilliseconds += total_milliseconds;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalDecades++;
                    Decades++;
                    DecadesChanged?.Invoke(this, EventArgs.Empty);

                    if (Decades >= 10)
                    {
                        Decades = 0;
                        TotalCenturies++;
                        Centuries++;
                        CenturiesChanged?.Invoke(this, EventArgs.Empty);

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            MillenniaChanged?.Invoke(this, EventArgs.Empty);
                        }
                    }
                }

                Processing = false;
            }
        }

        public virtual void AddDecades(long amount, TimeRate rate)
        {
            if (rate == TimeRate.Millisecond)
            {
                long total_years = amount * 10;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (rate == TimeRate.Second)
            {
                long total_years = amount * 10;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                TotalMilliseconds += total_seconds * 1000;
                AddSeconds(total_seconds, rate);
            }
            else if (rate == TimeRate.Minute)
            {
                long total_years = amount * 10;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                TotalSeconds += total_minutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMinutes(total_minutes, rate);
            }
            else if (rate == TimeRate.Hour)
            {
                long total_years = amount * 10;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                TotalMinutes += total_hours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddHours(total_hours, rate);
            }
            else if (rate == TimeRate.Day)
            {
                long total_years = amount * 10;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                TotalHours += total_days * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddDays(total_days, rate);
            }
            else if (rate == TimeRate.Month)
            {
                long total_years = amount * 10;
                long total_months = total_years * Months_In_Year;
                TotalDays += total_months * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMonths(total_months, rate);
            }
            else if (rate == TimeRate.Year)
            {
                long total_years = amount * 10;
                TotalMonths += total_years * Months_In_Year;
                TotalDays += TotalMonths * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddYears(total_years, rate);
            }
            else if (rate == TimeRate.Nothing ||
                     rate == TimeRate.Decade ||
                     rate == TimeRate.Century ||
                     rate == TimeRate.Millennia)
            {
                long total_years = amount * 10;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;

                TotalYears += total_years;
                TotalMonths += total_months;
                TotalDays += total_days;
                TotalHours += total_hours;
                TotalMinutes += total_minutes;
                TotalSeconds += total_seconds;
                TotalMilliseconds += total_milliseconds;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalDecades++;
                    Decades++;
                    DecadesChanged?.Invoke(this, EventArgs.Empty);

                    if (Decades >= 10)
                    {
                        Decades = 0;
                        TotalCenturies++;
                        Centuries++;
                        CenturiesChanged?.Invoke(this, EventArgs.Empty);

                        if (Centuries >= 10)
                        {
                            Centuries = 0;
                            TotalMillennia++;
                            Millennia++;
                            MillenniaChanged?.Invoke(this, EventArgs.Empty);
                        }
                    }
                }

                Processing = false;
            }
        }

        public virtual void AddCenturies(long amount)
        {
            if (Interval == TimeRate.Millisecond)
            {
                long total_years = amount * 100;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (Interval == TimeRate.Second)
            {
                long total_years = amount * 100;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                TotalMilliseconds += total_seconds * 1000;
                AddSeconds(total_seconds);
            }
            else if (Interval == TimeRate.Minute)
            {
                long total_years = amount * 100;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                TotalSeconds += total_minutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMinutes(total_minutes);
            }
            else if (Interval == TimeRate.Hour)
            {
                long total_years = amount * 100;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                TotalMinutes += total_hours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddHours(total_hours);
            }
            else if (Interval == TimeRate.Day)
            {
                long total_years = amount * 100;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                TotalHours += total_days * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddDays(total_days);
            }
            else if (Interval == TimeRate.Month)
            {
                long total_years = amount * 100;
                long total_months = total_years * Months_In_Year;
                TotalDays += total_months * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMonths(total_months);
            }
            else if (Interval == TimeRate.Year)
            {
                long total_years = amount * 100;
                TotalMonths += total_years * Months_In_Year;
                TotalDays += TotalMonths * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddYears(total_years);
            }
            else if (Interval == TimeRate.Decade)
            {
                long total_years = amount * 10;
                TotalYears += total_years * 10;
                TotalMonths += TotalYears * Months_In_Year;
                TotalDays += TotalMonths * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddDecades(total_years);
            }
            else if (Interval == TimeRate.Nothing ||
                     Interval == TimeRate.Century ||
                     Interval == TimeRate.Millennia)
            {
                long total_decades = amount * 10;
                long total_years = total_decades * 10;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;

                TotalDecades += total_decades;
                TotalYears += total_years;
                TotalMonths += total_months;
                TotalDays += total_days;
                TotalHours += total_hours;
                TotalMinutes += total_minutes;
                TotalSeconds += total_seconds;
                TotalMilliseconds += total_milliseconds;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalCenturies++;
                    Centuries++;
                    CenturiesChanged?.Invoke(this, EventArgs.Empty);

                    if (Centuries >= 10)
                    {
                        Centuries = 0;
                        TotalMillennia++;
                        Millennia++;
                        MillenniaChanged?.Invoke(this, EventArgs.Empty);
                    }
                }

                Processing = false;
            }
        }

        public virtual void AddCenturies(long amount, TimeRate rate)
        {
            if (rate == TimeRate.Millisecond)
            {
                long total_years = amount * 100;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (rate == TimeRate.Second)
            {
                long total_years = amount * 100;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                TotalMilliseconds += total_seconds * 1000;
                AddSeconds(total_seconds, rate);
            }
            else if (rate == TimeRate.Minute)
            {
                long total_years = amount * 100;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                TotalSeconds += total_minutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMinutes(total_minutes, rate);
            }
            else if (rate == TimeRate.Hour)
            {
                long total_years = amount * 100;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                TotalMinutes += total_hours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddHours(total_hours, rate);
            }
            else if (rate == TimeRate.Day)
            {
                long total_years = amount * 100;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                TotalHours += total_days * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddDays(total_days, rate);
            }
            else if (rate == TimeRate.Month)
            {
                long total_years = amount * 100;
                long total_months = total_years * Months_In_Year;
                TotalDays += total_months * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMonths(total_months, rate);
            }
            else if (rate == TimeRate.Year)
            {
                long total_years = amount * 100;
                TotalMonths += total_years * Months_In_Year;
                TotalDays += TotalMonths * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddYears(total_years, rate);
            }
            else if (rate == TimeRate.Decade)
            {
                long total_years = amount * 10;
                TotalYears += total_years * 10;
                TotalMonths += TotalYears * Months_In_Year;
                TotalDays += TotalMonths * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddDecades(total_years, rate);
            }
            else if (rate == TimeRate.Nothing ||
                     rate == TimeRate.Century ||
                     rate == TimeRate.Millennia)
            {
                long total_decades = amount * 10;
                long total_years = total_decades * 10;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;

                TotalDecades += total_decades;
                TotalYears += total_years;
                TotalMonths += total_months;
                TotalDays += total_days;
                TotalHours += total_hours;
                TotalMinutes += total_minutes;
                TotalSeconds += total_seconds;
                TotalMilliseconds += total_milliseconds;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalCenturies++;
                    Centuries++;
                    CenturiesChanged?.Invoke(this, EventArgs.Empty);

                    if (Centuries >= 10)
                    {
                        Centuries = 0;
                        TotalMillennia++;
                        Millennia++;
                        MillenniaChanged?.Invoke(this, EventArgs.Empty);
                    }
                }

                Processing = false;
            }
        }

        public virtual void AddMillennia(long amount)
        {
            if (Interval == TimeRate.Millisecond)
            {
                long total_years = amount * 1000;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (Interval == TimeRate.Second)
            {
                long total_years = amount * 1000;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                TotalMilliseconds += total_seconds * 1000;
                AddSeconds(total_seconds);
            }
            else if (Interval == TimeRate.Minute)
            {
                long total_years = amount * 1000;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                TotalSeconds += total_minutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMinutes(total_minutes);
            }
            else if (Interval == TimeRate.Hour)
            {
                long total_years = amount * 1000;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                TotalMinutes += total_hours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddHours(total_hours);
            }
            else if (Interval == TimeRate.Day)
            {
                long total_years = amount * 1000;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                TotalHours += total_days * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddDays(total_days);
            }
            else if (Interval == TimeRate.Month)
            {
                long total_years = amount * 1000;
                long total_months = total_years * Months_In_Year;
                TotalDays += total_months * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMonths(total_months);
            }
            else if (Interval == TimeRate.Year)
            {
                long total_years = amount * 1000;
                TotalMonths += total_years * Months_In_Year;
                TotalDays += TotalMonths * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddYears(total_years);
            }
            else if (Interval == TimeRate.Decade)
            {
                long total_years = amount * 100;
                TotalYears += total_years * 10;
                TotalMonths += TotalYears * Months_In_Year;
                TotalDays += TotalMonths * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddDecades(total_years);
            }
            else if (Interval == TimeRate.Century)
            {
                long total_years = amount * 10;
                TotalDecades += total_years * 10;
                TotalYears += TotalDecades * 10;
                TotalMonths += TotalYears * Months_In_Year;
                TotalDays += TotalMonths * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddCenturies(total_years);
            }
            else if (Interval == TimeRate.Nothing ||
                     Interval == TimeRate.Millennia)
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

                TotalCenturies += total_centuries;
                TotalDecades += total_decades;
                TotalYears += total_years;
                TotalMonths += total_months;
                TotalDays += total_days;
                TotalHours += total_hours;
                TotalMinutes += total_minutes;
                TotalSeconds += total_seconds;
                TotalMilliseconds += total_milliseconds;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalMillennia++;
                    Millennia++;
                    MillenniaChanged?.Invoke(this, EventArgs.Empty);
                }

                Processing = false;
            }
        }

        public virtual void AddMillennia(long amount, TimeRate rate)
        {
            if (rate == TimeRate.Millisecond)
            {
                long total_years = amount * 1000;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                long total_milliseconds = total_seconds * 1000;
                AddMilliseconds(total_milliseconds);
            }
            else if (rate == TimeRate.Second)
            {
                long total_years = amount * 1000;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                long total_seconds = total_minutes * 60;
                TotalMilliseconds += total_seconds * 1000;
                AddSeconds(total_seconds, rate);
            }
            else if (rate == TimeRate.Minute)
            {
                long total_years = amount * 1000;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                long total_minutes = total_hours * 60;
                TotalSeconds += total_minutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMinutes(total_minutes, rate);
            }
            else if (rate == TimeRate.Hour)
            {
                long total_years = amount * 1000;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                long total_hours = total_days * Hours_In_Day;
                TotalMinutes += total_hours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddHours(total_hours, rate);
            }
            else if (rate == TimeRate.Day)
            {
                long total_years = amount * 1000;
                long total_months = total_years * Months_In_Year;
                long total_days = total_months * Days_In_Month;
                TotalHours += total_days * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddDays(total_days, rate);
            }
            else if (rate == TimeRate.Month)
            {
                long total_years = amount * 1000;
                long total_months = total_years * Months_In_Year;
                TotalDays += total_months * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddMonths(total_months, rate);
            }
            else if (rate == TimeRate.Year)
            {
                long total_years = amount * 1000;
                TotalMonths += total_years * Months_In_Year;
                TotalDays += TotalMonths * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddYears(total_years, rate);
            }
            else if (rate == TimeRate.Decade)
            {
                long total_years = amount * 100;
                TotalYears += total_years * 10;
                TotalMonths += TotalYears * Months_In_Year;
                TotalDays += TotalMonths * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddDecades(total_years, rate);
            }
            else if (rate == TimeRate.Century)
            {
                long total_years = amount * 10;
                TotalDecades += total_years * 10;
                TotalYears += TotalDecades * 10;
                TotalMonths += TotalYears * Months_In_Year;
                TotalDays += TotalMonths * Days_In_Month;
                TotalHours += TotalDays * Hours_In_Day;
                TotalMinutes += TotalHours * 60;
                TotalSeconds += TotalMinutes * 60;
                TotalMilliseconds += TotalSeconds * 1000;
                AddCenturies(total_years, rate);
            }
            else if (rate == TimeRate.Nothing ||
                     rate == TimeRate.Millennia)
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

                TotalCenturies += total_centuries;
                TotalDecades += total_decades;
                TotalYears += total_years;
                TotalMonths += total_months;
                TotalDays += total_days;
                TotalHours += total_hours;
                TotalMinutes += total_minutes;
                TotalSeconds += total_seconds;
                TotalMilliseconds += total_milliseconds;

                Processing = true;

                for (long i = 1; i <= amount; i++)
                {
                    TotalMillennia++;
                    Millennia++;
                    MillenniaChanged?.Invoke(this, EventArgs.Empty);
                }

                Processing = false;
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

        public virtual void AddTimeSpan(TimeSpan amount)
        {
            if (Interval == TimeRate.Nothing ||
                Interval == TimeRate.Millisecond)
            {
                AddMilliseconds((long)amount.TotalMilliseconds);
            }
            else if (Interval == TimeRate.Second)
            {
                AddSeconds((long)amount.TotalSeconds);
            }
            else if (Interval == TimeRate.Minute)
            {
                AddMinutes((long)amount.TotalMinutes);
            }
            else if (Interval == TimeRate.Hour)
            {
                AddHours((long)amount.TotalHours);
            }
            else if (Interval == TimeRate.Day)
            {
                AddDays((long)amount.TotalDays);
            }
            else if (Interval == TimeRate.Month)
            {
                long total_months = (long)amount.TotalDays / Days_In_Month;
                AddMonths(total_months);
            }
            else if (Interval == TimeRate.Year)
            {
                long total_months = (long)amount.TotalDays / Days_In_Month;
                long total_years = total_months / Months_In_Year;
                AddYears(total_years);
            }
            else if (Interval == TimeRate.Decade)
            {
                long total_months = (long)amount.TotalDays / Days_In_Month;
                long total_years = total_months / Months_In_Year;
                long total_decades = total_years / 10;
                AddDecades(total_decades);
            }
            else if (Interval == TimeRate.Century)
            {
                long total_months = (long)amount.TotalDays / Days_In_Month;
                long total_years = total_months / Months_In_Year;
                long total_centuries = total_years / 100;
                AddCenturies(total_centuries);
            }
            else if (Interval == TimeRate.Millennia)
            {
                long total_months = (long)amount.TotalDays / Days_In_Month;
                long total_years = total_months / Months_In_Year;
                long total_millennia = total_years / 1000;
                AddMillennia(total_millennia);
            }
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
