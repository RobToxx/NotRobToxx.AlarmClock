using System;
using NotRobToxx.AlarmClock.Models;
using NotRobToxx.AlarmClock.Repositories;
using NotRobToxx.AlarmClock.Requests;

namespace NotRobToxx.AlarmClock.Services {
    
    public class AlarmService {

        private readonly AlarmRepository alarmRepository;
        
        public AlarmService(AlarmRepository alarmRepository) {
            
            this.alarmRepository = alarmRepository;
        }

        public void Create(CreateAlarmRequest request) {

            if (request.Name.Length == 0) {
                
                throw new ArgumentException(
                    "Alarm name cannot be empty.", nameof(request.Name)
                );
            }
            
            if (!request.Snooze.IsEmpty) {

                if (request.Snooze.Count > 10) {
                    
                    throw new ArgumentException(
                        "Snooze count cannot be greater than 10.", nameof(request.Snooze)
                    );
                }
                
                if (request.Snooze.Interval > 30) {
                    
                    throw new ArgumentException(
                        "Snooze interval cannot be greater than 30 minutes.", nameof(request.Snooze)
                    );
                }
            }
            
            var valid = request.Schedule.TryGetNextOccurrence(DateTime.UtcNow, out var _);

            if (!valid) {

                throw new ArgumentException(
                    "Invalid schedule.", nameof(request.Schedule)
                );
            }

            var alarm = new Alarm {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Schedule = request.Schedule,
                Snooze = request.Snooze,
                Enabled = true
            };
            
            this.alarmRepository.Add(alarm);
        }
    }
}