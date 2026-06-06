using System;
using System.Collections;
using NotRobToxx.AlarmClock.Models;

namespace NotRobToxx.AlarmClock.Repositories {
    
    public class AlarmRepository {
        
        private readonly Hashtable alarms = new();
        
        public AlarmRepository() {
            
        }
        
        private readonly object syncRoot = new();

        public Alarm[] GetAll() {

            lock (this.syncRoot) {
                
                var all = new Alarm[this.alarms.Count];
                
                this.alarms.Values.CopyTo(all, 0);
                
                return all;
            }
        }

        public bool Add(Alarm alarm) {

            lock (this.syncRoot) {

                if (this.alarms.Contains(alarm.Id)) return false;

                this.alarms.Add(alarm.Id, alarm);
                return true;
            }
        }

        public bool Remove(Guid id) {

            lock (this.syncRoot) {

                if (!this.alarms.Contains(id)) return false;

                this.alarms.Remove(id);
                return true;

            }
        }

        public bool Update(Alarm alarm) {

            lock (this.syncRoot) {

                if (!this.alarms.Contains(alarm.Id)) return false;

                this.alarms[alarm.Id] = alarm;
                return true;

            }
        }
    }
}