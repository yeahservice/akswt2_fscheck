using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsCheckCarAlarm
{
    public class CarAlarmSpec : ICommandGenerator<CarAlarm, CarAlarmState>
    {
        public CarAlarm InitialActual
        {
            get
            {
                return new CarAlarm();
            }
        }

        public CarAlarmState InitialModel
        {
            get
            {
                return CarAlarmState.OpenAndUnlocked;
            }
        }

        public Gen<Command<CarAlarm, CarAlarmState>> Next(CarAlarmState value)
        {
            return Gen.Elements(new Command<CarAlarm, CarAlarmState>[] { new Lock(), new Unlock(), new Open(), new Close() });
        }

        private class Lock : Command<CarAlarm, CarAlarmState>
        {
            public override CarAlarm RunActual(CarAlarm carAlarm)
            {
                carAlarm.lockCar();
                return carAlarm;
            }

            public override CarAlarmState RunModel(CarAlarmState carAlarmState)
            {
                switch(carAlarmState)
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

        private class Unlock : Command<CarAlarm, CarAlarmState>
        {
            public override CarAlarm RunActual(CarAlarm carAlarm)
            {
                carAlarm.unlockCar();
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
                return (carAlarm.getState() == carAlarmState).ToProperty();
            }

            public override string ToString()
            {
                return "unlock";
            }
        }

        private class Open : Command<CarAlarm, CarAlarmState>
        {
            public override CarAlarm RunActual(CarAlarm carAlarm)
            {
                carAlarm.openCar();
                return carAlarm;
            }

            public override CarAlarmState RunModel(CarAlarmState carAlarmState)
            {
                switch (carAlarmState)
                {
                    case CarAlarmState.ClosedAndUnlocked:
                        return CarAlarmState.OpenAndUnlocked;
                    case CarAlarmState.ClosedAndLocked:
                        return CarAlarmState.OpenAndLocked;
                    default:
                        return carAlarmState;
                }
            }

            public override bool Pre(CarAlarmState carAlarmState)
            {
                return (carAlarmState == CarAlarmState.ClosedAndUnlocked) ||
                    (carAlarmState == CarAlarmState.ClosedAndLocked);
            }

            public override Property Post(CarAlarm carAlarm, CarAlarmState carAlarmState)
            {
                return (carAlarm.getState() == carAlarmState).ToProperty();
            }

            public override string ToString()
            {
                return "open";
            }
        }

        private class Close : Command<CarAlarm, CarAlarmState>
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
}
