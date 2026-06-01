using System;

namespace NotRobToxx.AlarmClock.Models {

    public record Alarm(
        Guid Id,
        string Name,
        bool Enabled,
        ISchedule Schedule, 
        Notification Notification
    );
    
    public interface ISchedule {
        
        bool TryGetNextOccurrence(DateTime now, out DateTime occurrence);
    }

    public record OneTimeSchedule(DateTime Occurrence): ISchedule {

        public bool TryGetNextOccurrence(DateTime now, out DateTime occurrence) {
            
            if (this.Occurrence < now) {
                
                occurrence = DateTime.MinValue;
                return false;
            }
            
            occurrence = this.Occurrence;
            return true;
        }
    }

    public record DailySchedule(TimeSpan Occurrence): ISchedule {
        
        public bool TryGetNextOccurrence(DateTime now, out DateTime occurrence) {

            if (now.TimeOfDay < this.Occurrence) {
                
                
            }
        }
    }
    
    public 
    
    public record Notification(
        byte RepeatCount,
        TimeSpan RepeatInterval
    );
}