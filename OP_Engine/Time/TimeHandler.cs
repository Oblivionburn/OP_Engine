using System;

namespace OP_Engine.Time
{
    public class TimeHandler : IDisposable
    {
        #region Variables

        public long TotalMilliseconds;
        public long TotalSeconds;
        public long TotalMinutes;
        public long TotalHours;
        public long TotalDays;
        public long TotalMonths;
        public long TotalYears;

        public long Milliseconds;
        public long Seconds;
        public long Minutes;
        public long Hours;
        public long Days;
        public long Months;
        public long Years;

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

        #endregion

        #region Constructors

        public TimeHandler()
        {

        }

        public TimeHandler(TimeHandler current_time)
        {
            Years = current_time.Years;
            Months = current_time.Months;
            Days = current_time.Days;
            Hours = current_time.Hours;
            Minutes = current_time.Minutes;
            Seconds = current_time.Seconds;
            Milliseconds = current_time.Milliseconds;
        }

        public TimeHandler(TimeHandler current_time, TimeSpan additional_time)
        {
            Years = current_time.Years;
            Months = current_time.Months;
            Days = current_time.Days;
            Hours = current_time.Hours;
            Minutes = current_time.Minutes;
            Seconds = current_time.Seconds;
            Milliseconds = current_time.Milliseconds;

            AddMilliseconds((long)additional_time.TotalMilliseconds);
        }

        public TimeHandler(long millisecond)
        {
            Milliseconds = millisecond;
        }

        public TimeHandler(long second, long millisecond)
        {
            Seconds = second;
            Milliseconds = millisecond;
        }

        public TimeHandler(long minute, long second, long millisecond)
        {
            Minutes = minute;
            Seconds = second;
            Milliseconds = millisecond;
        }

        public TimeHandler(long hour, long minute, long second, long millisecond)
        {
            Hours = hour;
            Minutes = minute;
            Seconds = second;
            Milliseconds = millisecond;
        }

        public TimeHandler(long day, long hour, long minute, long second, long millisecond)
        {
            Days = day;
            Hours = hour;
            Minutes = minute;
            Seconds = second;
            Milliseconds = millisecond;
        }

        public TimeHandler(long month, long day, long hour, long minute, long second, long millisecond)
        {
            Months = month;
            Days = day;
            Hours = hour;
            Minutes = minute;
            Seconds = second;
            Milliseconds = millisecond;
        }

        public TimeHandler(long year, long month, long day, long hour, long minute, long second, long millisecond)
        {
            Years = year;
            Months = month;
            Days = day;
            Hours = hour;
            Minutes = minute;
            Seconds = second;
            Milliseconds = millisecond;
        }

        #endregion

        #region Methods

        public virtual void AddMilliseconds(long amount)
        {
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
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public virtual void AddSeconds(long amount)
        {
            long total_milliseconds = amount * 1000;
            AddMilliseconds(total_milliseconds);
        }

        public virtual void AddMinutes(long amount)
        {
            long total_seconds = amount * 60;
            long total_milliseconds = total_seconds * 1000;
            AddMilliseconds(total_milliseconds);
        }

        public virtual void AddHours(long amount)
        {
            long total_minutes = amount * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;
            AddMilliseconds(total_milliseconds);
        }

        public virtual void AddDays(long amount)
        {
            long total_hours = amount * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;
            AddMilliseconds(total_milliseconds);
        }

        public virtual void AddMonths(long amount)
        {
            long total_days = amount * Days_In_Month;
            long total_hours = total_days * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;
            AddMilliseconds(total_milliseconds);
        }

        public virtual void AddYears(long amount)
        {
            long total_months = amount * Months_In_Year;
            long total_days = total_months * Days_In_Month;
            long total_hours = total_days * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;
            AddMilliseconds(total_milliseconds);
        }

        public virtual void CopyTime(TimeHandler time)
        {
            Years = time.Years;
            Months = time.Months;
            Days = time.Days;
            Hours = time.Hours;
            Minutes = time.Minutes;
            Seconds = time.Seconds;
            Milliseconds = time.Milliseconds;
        }

        public virtual void AddTimeSpan(TimeSpan amount)
        {
            AddMilliseconds((long)amount.TotalMilliseconds);
        }

        public virtual DateTime? ToDateTime()
        {
            try
            {
                return new DateTime((int)Years, (int)Months, (int)Days, (int)Hours, (int)Minutes, (int)Seconds, (int)Milliseconds);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual void Dispose()
        {

        }

        #endregion
    }
}
