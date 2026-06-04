using System;

namespace NotRobToxx.AlarmClock.Models {
    
    public enum ScheduleType { Once, Daily, Weekly }

    [Flags]
    public enum WeekDays: byte {
        None = 0,
        Sunday = 1 << 0,
        Monday = 1 << 1,
        Tuesday = 1 << 2,
        Wednesday = 1 << 3,
        Thursday = 1 << 4,
        Friday = 1 << 5,
        Saturday = 1 << 6,
        All = Sunday | Monday | Tuesday | Wednesday | Thursday | Friday | Saturday
    }
    
    public struct Schedule {
        
        public ScheduleType Type { get; set; }
        public DateTime Occurrence { get; set; }
        public WeekDays Days { get; set; }
        
        public bool TryGetNextOccurrence(DateTime now, out DateTime occurrence) {

            return this.Type switch {

                ScheduleType.Once => this.TryGetOnceOccurrence(
                    now,
                    out occurrence
                ),
                ScheduleType.Daily => this.TryGetDailyOccurrence(
                    now,
                    out occurrence
                ),
                ScheduleType.Weekly => this.TryGetWeeklyOccurrence(
                    now, 
                    out occurrence
                ),
                _ => throw new InvalidOperationException(
                    $"Unsupported schedule type: {this.Type}"
                )
            };
        }
        
        private bool TryGetOnceOccurrence(
            DateTime now,
            out DateTime occurrence
        ) {

            if (this.Occurrence < now) {

                occurrence = DateTime.MinValue;
                return false;
            }

            occurrence = this.Occurrence;

            return true;
        }

        private bool TryGetDailyOccurrence(
            DateTime now,
            out DateTime occurrence
        ) {

            if (this.Occurrence.TimeOfDay < now.TimeOfDay) {

                occurrence = now.Date
                             .AddDays(1)
                             .Add(this.Occurrence.TimeOfDay);

                return true;
            }

            occurrence = now.Date
                         .Add(this.Occurrence.TimeOfDay);

            return true;
        }

        private bool TryGetWeeklyOccurrence(
            DateTime now,
            out DateTime occurrence
        ) {

            for (byte i = 0; i < 7; i++) {

                // NOTE: This variable name is a terrible pun and will not be renamed.
                var candiDate = now.Date.AddDays(i);

                var day =
                    (WeekDays)(1 << (int)candiDate.DayOfWeek);

                if ((this.Days & day) == 0) {
                    continue;
                }

                var time =
                    candiDate.Add(this.Occurrence.TimeOfDay);

                if (time < now) {
                    continue;
                }

                occurrence = time;

                return true;
            }

            occurrence = DateTime.MinValue;

            return false;
        }
    }
}