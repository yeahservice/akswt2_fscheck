using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsCheckCarAlarm.Specification.Command
{
    public class Close : Command<CarAlarm, CarAlarmState>
    {
        public override CarAlarm RunActual(CarAlarm sut)
        {
            sut.closeCar();
            return sut;
        }

        public override CarAlarmState RunModel(CarAlarmState model)
        {
            switch (model)
            {
                case CarAlarmState.OpenAndUnlocked:
                    return CarAlarmState.ClosedAndUnlocked;
                case CarAlarmState.OpenAndLocked:
                    return CarAlarmState.ClosedAndLocked;
                default:
                    return model;
            }
        }

        public override bool Pre(CarAlarmState carAlarmState)
        {
            return (carAlarmState == CarAlarmState.OpenAndLocked) ||
                (carAlarmState == CarAlarmState.OpenAndUnlocked);
        }

        public override Property Post(CarAlarm sut, CarAlarmState model)
        {
            return (sut.getState() == model).ToProperty();
        }

        public override string ToString()
        {
            return "close";
        }
    }
}

