using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BookingApp
{
    public abstract class BaseVerificationCode<T>
    {
        public abstract string Login { get; set; }
        public abstract T Code { get; set; }

        public virtual void DeleteByTime()
        {
            Timer timer = new Timer(1 * 60 * 1000);
            timer.Elapsed += (sender, e) => RemoveFromDatabase();
            timer.AutoReset = false;
            timer.Start();
        }

        public abstract void GenerateCode();
        public abstract bool VerifyCode(string enteredCode);
        public abstract void AddToDatabase();
        public abstract void RemoveFromDatabase();
    }
}
