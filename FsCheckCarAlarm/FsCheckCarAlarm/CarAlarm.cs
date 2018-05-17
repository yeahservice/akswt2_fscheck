using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsCheckCarAlarm
{
    public enum CarAlarmState
    {
        OpenAndUnlocked,
        ClosedAndUnlocked,
        OpenAndLocked,
        ClosedAndLocked
    }

    public class CarAlarm
    {
        private CarAlarmState currentState = CarAlarmState.OpenAndUnlocked;

        public void openCar()
        {
            switch (currentState)
            {
                case CarAlarmState.ClosedAndUnlocked:
                    currentState = CarAlarmState.OpenAndUnlocked;
                    break;
                case CarAlarmState.ClosedAndLocked:
                    currentState = CarAlarmState.OpenAndLocked;
                    break;
            }
        }

        public void closeCar()
        {
            switch (currentState)
            {
                case CarAlarmState.OpenAndUnlocked:
                    currentState = CarAlarmState.ClosedAndUnlocked;
                    break;
                case CarAlarmState.OpenAndLocked:
                    currentState = CarAlarmState.ClosedAndLocked;
                    break;
            }
        }

        public void lockCar()
        {
            switch (currentState)
            {
                case CarAlarmState.OpenAndUnlocked:
                    currentState = CarAlarmState.OpenAndLocked;
                    break;
                case CarAlarmState.ClosedAndUnlocked:
                    currentState = CarAlarmState.ClosedAndLocked;
                    break;
            }
        }

        public void unlockCar()
        {
            switch (currentState)
            {
                case CarAlarmState.OpenAndLocked:
                    currentState = CarAlarmState.OpenAndUnlocked;
                    break;
                case CarAlarmState.ClosedAndLocked:
                    currentState = CarAlarmState.ClosedAndUnlocked;
                    break;
            }
        }

        public CarAlarmState getState()
        {
            return this.currentState;
        }

        public void tick()
        {

        }
    }
}
