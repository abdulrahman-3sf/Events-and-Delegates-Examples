using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // Example 1
    public class TemperatureChangedEventArgs : EventArgs
    {
        public double OldTemperature { get; }
        public double NewTemperature { get; }
        public double Difference { get; }

        public TemperatureChangedEventArgs(double OldTemperature, double NewTemperature)
        {
            this.OldTemperature = OldTemperature;
            this.NewTemperature = NewTemperature;
            Difference = NewTemperature - OldTemperature;
        }
    }
    public class Thermostat
    {
        public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged;

        private double OldTemperature;
        private double CurrentTemperature;

        public void SetTemperature(double NewTemperature)
        {
            if (NewTemperature != CurrentTemperature)
            {
                OldTemperature = CurrentTemperature;
                CurrentTemperature = NewTemperature;
                OnTemperatureChanged(OldTemperature, CurrentTemperature);
            }
        }

        public void OnTemperatureChanged(double OldTemperature, double CurrentTemperature)
        {
            OnTemperatureChanged(new TemperatureChangedEventArgs(OldTemperature, CurrentTemperature));
        }

        public virtual void OnTemperatureChanged(TemperatureChangedEventArgs e)
        {
            TemperatureChanged?.Invoke(this, e);
        }
    }
    public class Display
    {
        public void Subscribe(Thermostat thermostat)
        {
            thermostat.TemperatureChanged += HandleTemperatureChange;
        }

        public void HandleTemperatureChange(object sender, TemperatureChangedEventArgs e)
        {
            Console.WriteLine("\n\nTemperature Changed:");
            Console.WriteLine("Temperature Changed from: " + e.OldTemperature);
            Console.WriteLine("Temperature Changed to: " + e.NewTemperature);
            Console.WriteLine("Temperature Differance: " + e.Difference);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Thermostat thermostat = new Thermostat();
            Display display = new Display();

            display.Subscribe(thermostat);

            thermostat.SetTemperature(50);
            thermostat.SetTemperature(20);
        }
    }
}
