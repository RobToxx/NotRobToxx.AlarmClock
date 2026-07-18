namespace NotRobToxx.AlarmClock.Models {
    
    public struct Snooze {
        public byte Interval { get; set; }
        public byte Count { get; set; }
        
        public bool IsEmpty => this.Interval == 0 && this.Count == 0;
    }
}