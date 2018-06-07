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

        public void Open()
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

        public void Close()
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

        public void Lock()
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

        public void Unlock()
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

        public CarAlarmState GetState()
        {
            return this.currentState;
        }

        public void Tick()
        {

        }
    }
}
