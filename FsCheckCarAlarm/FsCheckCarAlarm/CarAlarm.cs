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
        private string pin = "1234";
        private int pinChangeAttempts = 0;
        private int unlockAttempts = 0;
        private int closedDoors = 0;

        public CarAlarmState State
        {
            get { return state; }
        }

        public void SetPinCode(string oldPin, string newPin)
        {
            if (state == CarAlarmState.ClosedAndUnlocked || state == CarAlarmState.OpenAndUnlocked)
            {
                if (oldPin.Equals(pin))
                {
                    pin = newPin;
                    pinChangeAttempts = 0;
                    // Console.WriteLine("newPinSet");
                }
                else
                {
                    pinChangeAttempts++;
                    if (pinChangeAttempts == 3)
                    {
                        pinChangeAttempts = 0;
                        ChangeState(state, CarAlarmState.FlashAndSound);
                    }
                }
            }
        }

        public void OpenDoor()
        {
            if (closedDoors > 0)
                closedDoors--;

            if (closedDoors < 4)
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
                        ChangeState(CarAlarmState.Armed, CarAlarmState.FlashAndSound);
                        break;
                }
            }
        }

        public void CloseDoor()
        {
            if (closedDoors < 4)
            {
                closedDoors++;

                if (closedDoors == 4)
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

        public void Unlock(string pin)
        {
            if (pin.Equals(this.pin))
            {
                unlockAttempts = 0;
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
                    case CarAlarmState.FlashAndSound:
                        ChangeState(CarAlarmState.FlashAndSound, CarAlarmState.OpenAndUnlocked);
                        break;
                    case CarAlarmState.Flash:
                        ChangeState(CarAlarmState.Flash, CarAlarmState.OpenAndUnlocked);
                        break;
                }
            }
            else
            {
                if (state == CarAlarmState.Armed)
                {
                    unlockAttempts++;
                    if (unlockAttempts >= 3)
                    {
                        unlockAttempts = 0;
                        ChangeState(state, CarAlarmState.FlashAndSound);
                    }
                }
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
                case CarAlarmState.FlashAndSound:
                    Console.WriteLine("Activate Alarms");
                    break;
            }

            switch(from)
            {
                case CarAlarmState.Armed:
                    Console.WriteLine("Unarmed");
                    break;
                case CarAlarmState.FlashAndSound:
                case CarAlarmState.Flash:
                    Console.WriteLine("Deactivate Alarms");
                    break;
            }*/
        }

        public void Tick()
        {
            if (state == CarAlarmState.ClosedAndLocked || state == CarAlarmState.FlashAndSound || state == CarAlarmState.Flash)
                ticks = ticks + 10;

            if (state == CarAlarmState.ClosedAndLocked && ticks >= 20)
            {
                ChangeState(CarAlarmState.ClosedAndLocked, CarAlarmState.Armed);
                ticks = 0;
            }

            if (state == CarAlarmState.FlashAndSound && ticks >= 30)
            {
                ChangeState(CarAlarmState.FlashAndSound, CarAlarmState.Flash);
                ticks = 0;
            }

            if (state == CarAlarmState.Flash && ticks >= 300)
            {
                ChangeState(CarAlarmState.Flash, CarAlarmState.SilentAndOpen);
                ticks = 0;
            }
        }
    }
}
