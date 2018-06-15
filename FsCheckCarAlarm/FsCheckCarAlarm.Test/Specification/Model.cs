using System;
using System.Collections.Generic;

namespace FsCheckCarAlarm.Test.Specification
{
    public class Model
    {
        private string uuid;

        public string Uuid
        {
            get { return uuid; }
        }

        private CarAlarmState state;

        public CarAlarmState State
        {
            get
            {
                //Console.WriteLine($"model state get ({this.state})");
                return state;
            }
        }

        private string pin;

        public string Pin
        {
            get { return pin; }
        }

        //public CarAlarmState State { get; private set; }

        private Dictionary<Tuple<CarAlarmState, Action>, CarAlarmState> transitions;

        public Model()
        {
            this.uuid = Guid.NewGuid().ToString();
            this.state = CarAlarmState.OpenAndUnlocked;
            this.pin = "1234";

            Console.WriteLine($"new model ({this.state}, {this.uuid})");
            this.transitions = new Dictionary<Tuple<CarAlarmState, Action>, CarAlarmState>()
            {
                { Tuple.Create(CarAlarmState.OpenAndUnlocked, Action.Close), CarAlarmState.ClosedAndUnlocked },
                { Tuple.Create(CarAlarmState.OpenAndUnlocked, Action.Lock), CarAlarmState.OpenAndLocked },
                { Tuple.Create(CarAlarmState.OpenAndLocked, Action.Unlock), CarAlarmState.OpenAndUnlocked },
                { Tuple.Create(CarAlarmState.OpenAndLocked, Action.Close), CarAlarmState.ClosedAndLocked },
                { Tuple.Create(CarAlarmState.ClosedAndUnlocked, Action.Open), CarAlarmState.OpenAndUnlocked },
                { Tuple.Create(CarAlarmState.ClosedAndUnlocked, Action.Lock), CarAlarmState.ClosedAndLocked },
                { Tuple.Create(CarAlarmState.ClosedAndLocked, Action.Unlock), CarAlarmState.ClosedAndUnlocked },
                { Tuple.Create(CarAlarmState.ClosedAndLocked, Action.Open), CarAlarmState.OpenAndLocked },
                { Tuple.Create(CarAlarmState.ClosedAndLocked, Action.Tick20), CarAlarmState.Armed },
                { Tuple.Create(CarAlarmState.Armed, Action.Unlock), CarAlarmState.ClosedAndUnlocked },
                { Tuple.Create(CarAlarmState.Armed, Action.Open), CarAlarmState.FlashAndSound },
                { Tuple.Create(CarAlarmState.FlashAndSound, Action.Unlock), CarAlarmState.OpenAndUnlocked },
                { Tuple.Create(CarAlarmState.FlashAndSound, Action.Tick30), CarAlarmState.Flash },
                { Tuple.Create(CarAlarmState.Flash, Action.Unlock), CarAlarmState.OpenAndUnlocked },
                { Tuple.Create(CarAlarmState.Flash, Action.Tick300), CarAlarmState.SilentAndOpen },
                { Tuple.Create(CarAlarmState.SilentAndOpen, Action.Close), CarAlarmState.Armed },
                { Tuple.Create(CarAlarmState.SilentAndOpen, Action.Unlock), CarAlarmState.OpenAndUnlocked },
                // setPinCode requirements
                { Tuple.Create(CarAlarmState.ClosedAndUnlocked, Action.SetPinCode), CarAlarmState.ClosedAndUnlocked },
                { Tuple.Create(CarAlarmState.OpenAndUnlocked, Action.SetPinCode), CarAlarmState.OpenAndUnlocked },
            };
        }

        public IEnumerable<Action> GetPossibleActions()
        {
            //Console.WriteLine($"GetPossibleActions ({this.uuid})");
            foreach (Tuple<CarAlarmState, Action> keys in transitions.Keys)
            {
                if (keys.Item1 == this.state)
                    yield return keys.Item2;
            }
        }

        public void MakeTransition(Action action, string newPin)
        {
            Console.WriteLine($"MakeTransition ({action}, {this.uuid})");
            CarAlarmState newState;
            if (transitions.TryGetValue(Tuple.Create(this.state, action), out newState))
            {
                if (action == Action.SetPinCode)
                    this.pin = newPin;

                this.state = newState;
            }
        }
    }
}
