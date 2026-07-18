using System;

namespace NotRobToxx.AlarmClock.Models {
    public struct Alarm {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public Schedule Schedule { get; set; }
        public Snooze Snooze { get; set; }
    }
}