using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FsCheckCarAlarm.Test.Specification
{
    public class Model
    {
        private HashSet<Transition> transitions;
        private CarAlarmState state;
        private string pin;
        private int unlockAttempts;
        private int pinChangeAttempts;
        private int closedDoors;
        private bool unlockedTrunk;

        private Random random;

        public CarAlarmState State
        {
            get { return state; }
        }

        public string Pin
        {
            get { return pin; }
        }

        public bool UnlockedTrunk
        {
            get { return unlockedTrunk; }
        }

        public Model()
        {
            state = CarAlarmState.OpenAndUnlocked;
            pin = "1234";
            unlockAttempts = 0;
            pinChangeAttempts = 0;
            closedDoors = 0;
            unlockedTrunk = true;
            random = new Random();

            transitions = new HashSet<Transition>()
            {
                { new Transition(CarAlarmState.OpenAndUnlocked, Action.CloseDoor, CarAlarmState.OpenAndUnlocked, CarAlarmState.ClosedAndUnlocked) },
                { new Transition(CarAlarmState.OpenAndUnlocked, Action.Lock, CarAlarmState.OpenAndLocked) },
                { new Transition(CarAlarmState.OpenAndUnlocked, Action.OpenDoor, CarAlarmState.OpenAndUnlocked) },

                { new Transition(CarAlarmState.OpenAndLocked, Action.UnlockWithPinCorrect, CarAlarmState.OpenAndUnlocked) },
                { new Transition(CarAlarmState.OpenAndLocked, Action.UnlockWithPinWrong, CarAlarmState.OpenAndLocked) },
                { new Transition(CarAlarmState.OpenAndLocked, Action.CloseDoor, CarAlarmState.OpenAndLocked, CarAlarmState.ClosedAndLocked) },
                { new Transition(CarAlarmState.OpenAndLocked, Action.OpenDoor, CarAlarmState.OpenAndLocked) },

                { new Transition(CarAlarmState.ClosedAndUnlocked, Action.OpenDoor, CarAlarmState.OpenAndUnlocked) },
                { new Transition(CarAlarmState.ClosedAndUnlocked, Action.Lock, CarAlarmState.ClosedAndLocked) },

                { new Transition(CarAlarmState.ClosedAndLocked, Action.UnlockWithPinCorrect, CarAlarmState.ClosedAndUnlocked) },
                { new Transition(CarAlarmState.ClosedAndLocked, Action.UnlockWithPinWrong, CarAlarmState.ClosedAndLocked) },
                { new Transition(CarAlarmState.ClosedAndLocked, Action.OpenDoor, CarAlarmState.OpenAndLocked) },
                { new Transition(CarAlarmState.ClosedAndLocked, Action.Tick20, CarAlarmState.Armed) },

                { new Transition(CarAlarmState.Armed, Action.UnlockWithPinCorrect, CarAlarmState.ClosedAndUnlocked) },
                { new Transition(CarAlarmState.Armed, Action.UnlockWithPinWrong, CarAlarmState.Armed, CarAlarmState.FlashAndSound) },
                { new Transition(CarAlarmState.Armed, Action.OpenDoor, CarAlarmState.FlashAndSound) },

                { new Transition(CarAlarmState.FlashAndSound, Action.UnlockWithPinCorrect, CarAlarmState.OpenAndUnlocked) },
                { new Transition(CarAlarmState.FlashAndSound, Action.UnlockWithPinWrong, CarAlarmState.FlashAndSound) },
                { new Transition(CarAlarmState.FlashAndSound, Action.Tick30, CarAlarmState.Flash) },
                { new Transition(CarAlarmState.FlashAndSound, Action.OpenDoor, CarAlarmState.FlashAndSound) },

                { new Transition(CarAlarmState.Flash, Action.UnlockWithPinCorrect, CarAlarmState.OpenAndUnlocked) },
                { new Transition(CarAlarmState.Flash, Action.UnlockWithPinWrong, CarAlarmState.Flash) },
                { new Transition(CarAlarmState.Flash, Action.Tick300, CarAlarmState.SilentAndOpen) },
                { new Transition(CarAlarmState.Flash, Action.OpenDoor, CarAlarmState.Flash) },

                { new Transition(CarAlarmState.SilentAndOpen, Action.CloseDoor, CarAlarmState.SilentAndOpen, CarAlarmState.Armed) },
                { new Transition(CarAlarmState.SilentAndOpen, Action.UnlockWithPinCorrect, CarAlarmState.OpenAndUnlocked) },
                { new Transition(CarAlarmState.SilentAndOpen, Action.UnlockWithPinWrong, CarAlarmState.SilentAndOpen) },
                { new Transition(CarAlarmState.SilentAndOpen, Action.OpenDoor, CarAlarmState.SilentAndOpen) },

                // setPinCode
                { new Transition(CarAlarmState.ClosedAndUnlocked, Action.SetPinCorrect, CarAlarmState.ClosedAndUnlocked) },
                { new Transition(CarAlarmState.OpenAndUnlocked, Action.SetPinWrong, CarAlarmState.OpenAndUnlocked, CarAlarmState.FlashAndSound) },

                // unlock/lock trunk
                { new Transition(CarAlarmState.OpenAndUnlocked, Action.LockTrunk, CarAlarmState.OpenAndUnlocked) },
                { new Transition(CarAlarmState.OpenAndUnlocked, Action.UnlockTrunk, CarAlarmState.OpenAndUnlocked) },

                { new Transition(CarAlarmState.OpenAndLocked, Action.LockTrunk, CarAlarmState.OpenAndLocked) },
                { new Transition(CarAlarmState.OpenAndLocked, Action.UnlockTrunk, CarAlarmState.OpenAndLocked) },

                { new Transition(CarAlarmState.ClosedAndUnlocked, Action.LockTrunk, CarAlarmState.ClosedAndUnlocked) },
                { new Transition(CarAlarmState.ClosedAndUnlocked, Action.UnlockTrunk, CarAlarmState.ClosedAndUnlocked) },

                { new Transition(CarAlarmState.ClosedAndLocked, Action.LockTrunk, CarAlarmState.ClosedAndLocked) },
                { new Transition(CarAlarmState.ClosedAndLocked, Action.UnlockTrunk, CarAlarmState.ClosedAndLocked) },

                { new Transition(CarAlarmState.Armed, Action.LockTrunk, CarAlarmState.Armed) },
                { new Transition(CarAlarmState.Armed, Action.UnlockTrunk, CarAlarmState.Armed) },

                { new Transition(CarAlarmState.FlashAndSound, Action.LockTrunk, CarAlarmState.FlashAndSound) },
                { new Transition(CarAlarmState.FlashAndSound, Action.UnlockTrunk, CarAlarmState.FlashAndSound) },

                { new Transition(CarAlarmState.Flash, Action.LockTrunk, CarAlarmState.Flash) },
                { new Transition(CarAlarmState.Flash, Action.UnlockTrunk, CarAlarmState.Flash) },

                { new Transition(CarAlarmState.SilentAndOpen, Action.LockTrunk, CarAlarmState.SilentAndOpen) },
                { new Transition(CarAlarmState.SilentAndOpen, Action.UnlockTrunk, CarAlarmState.SilentAndOpen) },
            };
        }

        public IEnumerable<Action> GetPossibleActions()
        {
            foreach (Transition transition in transitions)
            {
                if (transition.From == state)
                {
                    switch (transition.Action)
                    {
                        case Action.OpenDoor:
                            if (closedDoors > 0) yield return transition.Action;
                            break;
                        case Action.CloseDoor:
                            if (closedDoors < 4) yield return transition.Action;
                            break;
                        case Action.UnlockTrunk:
                            if (!unlockedTrunk) yield return transition.Action;
                            break;
                        case Action.LockTrunk:
                            if (unlockedTrunk) yield return transition.Action;
                            break;
                        default:
                            yield return transition.Action;
                            break;
                    }
                }
            }
        }

        public string GeneratePin()
        {
            int randomNumber = random.Next(0, 10000);
            return randomNumber.ToString("D4");
        }

        public void MakeTransition(Action action, string newPin)
        {
            foreach (Transition transition in transitions)
            {
                if (transition.From == state && transition.Action == action)
                {
                    state = transition.To;

                    if (action == Action.UnlockWithPinWrong && transition.From == CarAlarmState.Armed)
                    {
                        unlockAttempts++;
                        if (unlockAttempts == 3)
                        {
                            unlockAttempts = 0;
                            Debug.Assert(transition.ConditionalTo.HasValue, $"transition.ConditionalTo should have a value from={transition.From}, action={action}, to={transition.ConditionalTo}, conditionalTo={transition.ConditionalTo}, attempts={unlockAttempts}");
                            state = transition.ConditionalTo.Value;
                        }
                    }
                    else if (action == Action.UnlockWithPinCorrect)
                    {
                        unlockAttempts = 0;
                    }
                    else if (action == Action.SetPinCorrect)
                    {
                        pinChangeAttempts = 0;
                        pin = newPin;
                    }
                    else if (action == Action.SetPinWrong)
                    {
                        pinChangeAttempts++;
                        if (pinChangeAttempts == 3)
                        {
                            pinChangeAttempts = 0;
                            Debug.Assert(transition.ConditionalTo.HasValue, $"transition.ConditionalTo should have a value from={transition.From}, action={action}, to={transition.ConditionalTo}, conditionalTo={transition.ConditionalTo}, attempts={pinChangeAttempts}");
                            state = transition.ConditionalTo.Value;
                        }
                    }
                    else if (action == Action.OpenDoor)
                    {
                        closedDoors--;
                    }
                    else if (action == Action.CloseDoor)
                    {
                        closedDoors++;
                        if (closedDoors == 4)
                        {
                            Debug.Assert(transition.ConditionalTo.HasValue, $"transition.ConditionalTo should have a value from={transition.From}, action={action}, to={transition.ConditionalTo}, conditionalTo={transition.ConditionalTo}, closedDoors={closedDoors}");
                            state = transition.ConditionalTo.Value;
                        }
                    }
                    else if (action == Action.UnlockTrunk)
                    {
                        unlockedTrunk = true;
                    }
                    else if (action == Action.LockTrunk)
                    {
                        unlockedTrunk = false;
                    }

                    break;
                }
            }
        }
    }
}
