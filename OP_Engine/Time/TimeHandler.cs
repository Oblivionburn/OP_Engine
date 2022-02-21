using System;

namespace OP_Engine.Time
{
    public class TimeHandler : IDisposable
    {
        #region Variables

        private long milliseconds;
        private long seconds;
        private long minutes;
        private long hours;
        private long days;
        private long months;
        private long years;

        public int Hours_In_Day = 24;
        public int Days_In_Month = 30;
        public int Months_In_Year = 12;

        #endregion

        #region Properties

        public long Milliseconds
        {
            get { return milliseconds; }
        }

        public long Seconds
        {
            get { return seconds; }
        }

        public long Minutes
        {
            get { return minutes; }
        }

        public long Hours
        {
            get { return hours; }
        }

        public long Days
        {
            get { return days; }
        }

        public long Months
        {
            get { return months; }
        }

        public long Years
        {
            get { return years; }
        }

        public long TotalTime
        {
            get 
            {
                return Get_TotalTime();
            }
        }

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
            years = current_time.Years;
            months = current_time.Months;
            days = current_time.Days;
            hours = current_time.Hours;
            minutes = current_time.Minutes;
            seconds = current_time.Seconds;
            milliseconds = current_time.Milliseconds;
        }

        public TimeHandler(TimeHandler current_time, TimeSpan additional_time)
        {
            years = current_time.Years;
            months = current_time.Months;
            days = current_time.Days;
            hours = current_time.Hours;
            minutes = current_time.Minutes;
            seconds = current_time.Seconds;
            milliseconds = current_time.Milliseconds;

            AddMilliseconds((long)additional_time.TotalMilliseconds);
        }

        public TimeHandler(long millisecond)
        {
            milliseconds = millisecond;
        }

        public TimeHandler(long second, long millisecond)
        {
            seconds = second;
            milliseconds = millisecond;
        }

        public TimeHandler(long minute, long second, long millisecond)
        {
            minutes = minute;
            seconds = second;
            milliseconds = millisecond;
        }

        public TimeHandler(long hour, long minute, long second, long millisecond)
        {
            hours = hour;
            minutes = minute;
            seconds = second;
            milliseconds = millisecond;
        }

        public TimeHandler(long day, long hour, long minute, long second, long millisecond)
        {
            days = day;
            hours = hour;
            minutes = minute;
            seconds = second;
            milliseconds = millisecond;
        }

        public TimeHandler(long month, long day, long hour, long minute, long second, long millisecond)
        {
            months = month;
            days = day;
            hours = hour;
            minutes = minute;
            seconds = second;
            milliseconds = millisecond;
        }

        public TimeHandler(long year, long month, long day, long hour, long minute, long second, long millisecond)
        {
            years = year;
            months = month;
            days = day;
            hours = hour;
            minutes = minute;
            seconds = second;
            milliseconds = millisecond;
        }

        #endregion

        #region Methods

        private long Get_TotalTime()
        {
            long total_months = (Years * Months_In_Year) + Months;
            long total_days = (total_months * Days_In_Month) + Days;
            long total_hours = (total_days * Hours_In_Day) + Hours;
            long total_minutes = (total_hours * 60) + Minutes;
            long total_seconds = (total_minutes * 60) + Seconds;
            long total_milliseconds = (total_seconds * 1000) + Milliseconds;

            return total_milliseconds;
        }

        public void AddMilliseconds(long amount)
        {
            for (long i = 1; i <= amount; i++)
            {
                milliseconds++;
                MillisecondsChanged?.Invoke(this, EventArgs.Empty);

                if (milliseconds >= 1000)
                {
                    milliseconds = 0;
                    seconds++;
                    SecondsChanged?.Invoke(this, EventArgs.Empty);

                    if (seconds >= 60)
                    {
                        seconds = 0;
                        minutes++;
                        MinutesChanged?.Invoke(this, EventArgs.Empty);

                        if (minutes >= 60)
                        {
                            minutes = 0;
                            hours++;
                            HoursChanged?.Invoke(this, EventArgs.Empty);

                            if (hours >= Hours_In_Day)
                            {
                                hours = 0;
                                days++;
                                DaysChanged?.Invoke(this, EventArgs.Empty);

                                if (days > Days_In_Month)
                                {
                                    days = 1;
                                    months++;
                                    MonthsChanged?.Invoke(this, EventArgs.Empty);

                                    if (months > Months_In_Year)
                                    {
                                        months = 1;
                                        years++;
                                        YearsChanged?.Invoke(this, EventArgs.Empty);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void AddSeconds(long amount)
        {
            long total_milliseconds = amount * 1000;
            AddMilliseconds(total_milliseconds);
        }

        public void AddMinutes(long amount)
        {
            long total_seconds = amount * 60;
            long total_milliseconds = total_seconds * 1000;
            AddMilliseconds(total_milliseconds);
        }

        public void AddHours(long amount)
        {
            long total_minutes = amount * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;
            AddMilliseconds(total_milliseconds);
        }

        public void AddDays(long amount)
        {
            long total_hours = amount * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;
            AddMilliseconds(total_milliseconds);
        }

        public void AddMonths(long amount)
        {
            long total_days = amount * Days_In_Month;
            long total_hours = total_days * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;
            AddMilliseconds(total_milliseconds);
        }

        public void AddYears(long amount)
        {
            long total_months = amount * Months_In_Year;
            long total_days = total_months * Days_In_Month;
            long total_hours = total_days * Hours_In_Day;
            long total_minutes = total_hours * 60;
            long total_seconds = total_minutes * 60;
            long total_milliseconds = total_seconds * 1000;
            AddMilliseconds(total_milliseconds);
        }

        public void CopyTime(TimeHandler time)
        {
            years = time.Years;
            months = time.Months;
            days = time.Days;
            hours = time.Hours;
            minutes = time.Minutes;
            seconds = time.Seconds;
            milliseconds = time.Milliseconds;
        }

        public void AddTimeSpan(TimeSpan amount)
        {
            AddMilliseconds((long)amount.TotalMilliseconds);
        }

        public DateTime? ToDateTime()
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

        public void Dispose()
        {

        }

        #endregion
    }
}
