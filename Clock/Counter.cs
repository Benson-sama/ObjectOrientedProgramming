using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Clock
{
    public class Counter : INotifyPropertyChanged
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        public Counter()
        {
            Timer timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Now)));
        }
    }
}
