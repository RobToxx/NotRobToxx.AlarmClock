using System;
using System.Device.I2c;
using System.Threading;
using System.Diagnostics;
using Iot.Device.Ssd13xx;
using nanoFramework.Hardware.Esp32;
using NotRobToxx.AlarmClock.Fonts;

namespace NotRobToxx.AlarmClock {
    
    public class Program {
        
        public static void Main() {
            
            Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);
            Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);
            
            var settings = new I2cConnectionSettings(
                1, 
                Ssd1306.DefaultI2cAddress, 
                I2cBusSpeed.FastMode
            );

            using var device = new I2cDevice(settings);

            var display = new Ssd1306(
                device,
                Ssd13xx.DisplayResolution.OLED128x32
            );
            
            display.ClearScreen();

            display.Font = new BasicFont();
            
            while (true) {

                var now = DateTime.UtcNow;
                
                var date = now.ToString("d");
                var time = now.ToString("T");
                
                Debug.WriteLine($"{date} {time}");
                
                display.DrawString(0, 0, date, 1, true);
                display.DrawString(0, 16, time, 1, true);
                display.Display();
                
                Thread.Sleep(1000);
            }
        }
    }
}