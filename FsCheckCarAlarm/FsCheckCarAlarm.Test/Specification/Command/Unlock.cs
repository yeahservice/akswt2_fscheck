using FsCheck;
using FsCheckCarAlarm.FSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsCheckCarAlarm.Test.Specification.Command
{
    public class Unlock : Command<CarAlarm, CarAlarmState>
    {
        public override CarAlarm RunActual(CarAlarm carAlarm)
        {
            carAlarm.Unlock();
            return carAlarm;
        }

        public override CarAlarmState RunModel(CarAlarmState carAlarmState)
        {
            switch (carAlarmState)
            {
                case CarAlarmState.OpenAndLocked:
                    return CarAlarmState.OpenAndUnlocked;
                case CarAlarmState.ClosedAndLocked:
                    return CarAlarmState.ClosedAndUnlocked;
                default:
                    return carAlarmState;
            }
        }

        public override bool Pre(CarAlarmState carAlarmState)
        {
            return (carAlarmState == CarAlarmState.OpenAndLocked) ||
                (carAlarmState == CarAlarmState.ClosedAndLocked);
        }

        public override Property Post(CarAlarm carAlarm, CarAlarmState carAlarmState)
        {
            return (carAlarm.State == carAlarmState).ToProperty();
        }

        public override string ToString()
        {
            return "unlock";
        }
    }
}
