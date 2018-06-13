using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsCheckCarAlarm
{
    public class CarAlarm
    {
        private CarAlarmState state = CarAlarmState.OpenAndUnlocked;
        private int ticks = 0;

        public CarAlarmState State
        {
            get { return state; }
        }

        public void Open()
        {
            switch (state)
            {
                case CarAlarmState.ClosedAndUnlocked:
                    ChangeState(CarAlarmState.ClosedAndUnlocked, CarAlarmState.OpenAndUnlocked); 
                    break;
                case CarAlarmState.ClosedAndLocked:
                    ChangeState(CarAlarmState.ClosedAndLocked, CarAlarmState.OpenAndLocked);
                    break;
                case CarAlarmState.Armed:
                    ChangeState(CarAlarmState.Armed, CarAlarmState.Alarm);
                    break;
            }
        }

        public void Close()
        {
            switch (state)
            {
                case CarAlarmState.OpenAndUnlocked:
                    ChangeState(CarAlarmState.OpenAndUnlocked, CarAlarmState.ClosedAndUnlocked);
                    break;
                case CarAlarmState.OpenAndLocked:
                    ChangeState(CarAlarmState.OpenAndLocked, CarAlarmState.ClosedAndLocked);
                    break;
                case CarAlarmState.SilentAndOpen:
                    ChangeState(CarAlarmState.SilentAndOpen, CarAlarmState.Armed); 
                    break;
            }
        }

        public void Lock()
        {
            switch (state)
            {
                case CarAlarmState.OpenAndUnlocked:
                    ChangeState(CarAlarmState.OpenAndUnlocked, CarAlarmState.OpenAndLocked);
                    break;
                case CarAlarmState.ClosedAndUnlocked:
                    ChangeState(CarAlarmState.ClosedAndUnlocked, CarAlarmState.ClosedAndLocked);
                    break;
            }
        }

        public void Unlock()
        {
            switch (state)
            {
                case CarAlarmState.OpenAndLocked:
                    ChangeState(CarAlarmState.OpenAndLocked, CarAlarmState.OpenAndUnlocked);
                    break;
                case CarAlarmState.ClosedAndLocked:
                    ChangeState(CarAlarmState.ClosedAndLocked, CarAlarmState.ClosedAndUnlocked);
                    break;
                case CarAlarmState.Armed:
                    ChangeState(CarAlarmState.Armed, CarAlarmState.ClosedAndUnlocked); 
                    break;
                case CarAlarmState.SilentAndOpen:
                    ChangeState(CarAlarmState.SilentAndOpen, CarAlarmState.OpenAndUnlocked);
                    break;
                case CarAlarmState.Alarm:
                    ChangeState(CarAlarmState.Alarm, CarAlarmState.OpenAndUnlocked);
                    break;
            }
        }

        private void ChangeState(CarAlarmState from, CarAlarmState to)
        {
            state = to;

            /*switch(to)
            {
                case CarAlarmState.Armed:
                    Console.WriteLine("Armed");
                    break;
                case CarAlarmState.Alarm:
                    Console.WriteLine("Activate Alarms");
                    break;
            }

            switch(from)
            {
                case CarAlarmState.Armed:
                    Console.WriteLine("Unarmed");
                    break;
                case CarAlarmState.Alarm:
                    Console.WriteLine("Deactivate Alarms");
                    break;
            }*/
        }

        public void Tick()
        {
            if (state == CarAlarmState.ClosedAndLocked || state == CarAlarmState.Alarm)
                ticks = ticks + 10;

            if (state == CarAlarmState.ClosedAndLocked && ticks >= 20)
            {
                ChangeState(CarAlarmState.ClosedAndLocked, CarAlarmState.Armed);
                ticks = 0;
            }

            if (state == CarAlarmState.Alarm && ticks >= 300)
            {
                ChangeState(CarAlarmState.Alarm, CarAlarmState.SilentAndOpen);
                ticks = 0;
            }
        }
    }
}
