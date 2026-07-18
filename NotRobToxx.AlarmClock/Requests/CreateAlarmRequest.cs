using NotRobToxx.AlarmClock.Models;

namespace NotRobToxx.AlarmClock.Requests {
    
    public struct CreateAlarmRequest {
        public string Name { get; set; }
        public Schedule Schedule { get; set; }
        public Snooze Snooze { get; set; }
    }
}
