using FsCheck;
using FsCheckCarAlarm.FSharp;
using FsCheckCarAlarm.Test.Specification.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsCheckCarAlarm.Test
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
            return Gen.Elements(new Command<CarAlarm, CarAlarmState>[] 
            {
                new Lock(),
                new Unlock(),
                new Open(),
                new Close()
            });
        }
    }
}
