using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsCheckCarAlarm.Specification.Command
{
    public class Lock : Command<CarAlarm, CarAlarmState>
    {
        public override CarAlarm RunActual(CarAlarm carAlarm)
        {
            carAlarm.lockCar();
            return carAlarm;
        }

        public override CarAlarmState RunModel(CarAlarmState carAlarmState)
        {
            switch (carAlarmState)
            {
                case CarAlarmState.OpenAndUnlocked:
                    return CarAlarmState.OpenAndLocked;
                case CarAlarmState.ClosedAndUnlocked:
                    return CarAlarmState.ClosedAndLocked;
                default:
                    return carAlarmState;
            }
        }

        public override bool Pre(CarAlarmState carAlarmState)
        {
            return (carAlarmState == CarAlarmState.OpenAndUnlocked) ||
                (carAlarmState == CarAlarmState.ClosedAndUnlocked);
        }

        public override Property Post(CarAlarm carAlarm, CarAlarmState carAlarmState)
        {
            return (carAlarm.getState() == carAlarmState).ToProperty();
        }

        public override string ToString()
        {
            return "lock";
        }
    }
}
