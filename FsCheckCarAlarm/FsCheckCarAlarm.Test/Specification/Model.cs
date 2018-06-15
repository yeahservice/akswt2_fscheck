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
        private int unlockAttempts = 0;
        private int pinChangeAttempts = 0;

        public CarAlarmState State
        {
            get { return state; }
        }

        public string Pin
        {
            get { return pin; }
        }

        public Model()
        {
            this.state = CarAlarmState.OpenAndUnlocked;
            this.pin = "1234";

            this.transitions = new HashSet<Transition>()
            {
                { new Transition(CarAlarmState.OpenAndUnlocked, Action.Close, CarAlarmState.ClosedAndUnlocked) },
                { new Transition(CarAlarmState.OpenAndUnlocked, Action.Lock, CarAlarmState.OpenAndLocked) },

                { new Transition(CarAlarmState.OpenAndLocked, Action.UnlockWithPinCorrect, CarAlarmState.OpenAndUnlocked) },
                { new Transition(CarAlarmState.OpenAndLocked, Action.UnlockWithPinWrong, CarAlarmState.OpenAndLocked) },
                { new Transition(CarAlarmState.OpenAndLocked, Action.Close, CarAlarmState.ClosedAndLocked) },

                { new Transition(CarAlarmState.ClosedAndUnlocked, Action.Open, CarAlarmState.OpenAndUnlocked) },
                { new Transition(CarAlarmState.ClosedAndUnlocked, Action.Lock, CarAlarmState.ClosedAndLocked) },

                { new Transition(CarAlarmState.ClosedAndLocked, Action.UnlockWithPinCorrect, CarAlarmState.ClosedAndUnlocked) },
                { new Transition(CarAlarmState.ClosedAndLocked, Action.UnlockWithPinWrong, CarAlarmState.ClosedAndLocked) },
                { new Transition(CarAlarmState.ClosedAndLocked, Action.Open, CarAlarmState.OpenAndLocked) },
                { new Transition(CarAlarmState.ClosedAndLocked, Action.Tick20, CarAlarmState.Armed) },

                { new Transition(CarAlarmState.Armed, Action.UnlockWithPinCorrect, CarAlarmState.ClosedAndUnlocked) },
                { new Transition(CarAlarmState.Armed, Action.UnlockWithPinWrong, CarAlarmState.Armed, CarAlarmState.FlashAndSound) },
                { new Transition(CarAlarmState.Armed, Action.Open, CarAlarmState.FlashAndSound) },

                { new Transition(CarAlarmState.FlashAndSound, Action.UnlockWithPinCorrect, CarAlarmState.OpenAndUnlocked) },
                { new Transition(CarAlarmState.FlashAndSound, Action.UnlockWithPinWrong, CarAlarmState.FlashAndSound) },
                { new Transition(CarAlarmState.FlashAndSound, Action.Tick30, CarAlarmState.Flash) },

                { new Transition(CarAlarmState.Flash, Action.UnlockWithPinCorrect, CarAlarmState.OpenAndUnlocked) },
                { new Transition(CarAlarmState.Flash, Action.UnlockWithPinWrong, CarAlarmState.Flash) },
                { new Transition(CarAlarmState.Flash, Action.Tick300, CarAlarmState.SilentAndOpen) },

                { new Transition(CarAlarmState.SilentAndOpen, Action.Close, CarAlarmState.Armed) },
                { new Transition(CarAlarmState.SilentAndOpen, Action.UnlockWithPinCorrect, CarAlarmState.OpenAndUnlocked) },
                { new Transition(CarAlarmState.SilentAndOpen, Action.UnlockWithPinWrong, CarAlarmState.SilentAndOpen) },

                // setPinCode
                { new Transition(CarAlarmState.ClosedAndUnlocked, Action.SetPinCorrect, CarAlarmState.ClosedAndUnlocked) },
                { new Transition(CarAlarmState.OpenAndUnlocked, Action.SetPinWrong, CarAlarmState.OpenAndUnlocked, CarAlarmState.FlashAndSound) }
            };
        }

        public IEnumerable<Action> GetPossibleActions()
        {
            foreach (Transition transition in transitions)
            {
                if (transition.From == state)
                    yield return transition.Action;
            }
        }

        public void MakeTransition(Action action, string newPin)
        {
            foreach (Transition transition in transitions)
            {
                if (transition.From == state && transition.Action == action)
                {
                    this.state = transition.To;

                    if (action == Action.UnlockWithPinWrong && transition.From == CarAlarmState.Armed)
                    {
                        unlockAttempts++;
                        if (unlockAttempts >= 3)
                        {
                            unlockAttempts = 0;
                            Debug.Assert(transition.ConditionalTo.HasValue, $"transition.ConditionalTo should have a value from={transition.From}, action={action}, to={transition.ConditionalTo}, conditionalTo={transition.ConditionalTo}, attempts={unlockAttempts}");
                            state = transition.ConditionalTo.Value;
                        }
                    }
                    else if (action == Action.UnlockWithPinCorrect)
                        unlockAttempts = 0;
                    else if (action == Action.SetPinCorrect)
                    {
                        pinChangeAttempts = 0;
                        this.pin = newPin;
                    }
                    else if (action == Action.SetPinWrong)
                    {
                        pinChangeAttempts++;
                        if (pinChangeAttempts >= 3)
                        {
                            pinChangeAttempts = 0;
                            Debug.Assert(transition.ConditionalTo.HasValue, $"transition.ConditionalTo should have a value from={transition.From}, action={action}, to={transition.ConditionalTo}, conditionalTo={transition.ConditionalTo}, attempts={pinChangeAttempts}");
                            state = transition.ConditionalTo.Value;
                        }
                    }
                }
            }
        }
    }
}
