using System;
using System.Collections.Generic;

namespace FsCheckCarAlarm.Test.Specification
{
    public class Model
    {
        private CarAlarmState state;

        public CarAlarmState State
        {
            get
            {
                Console.WriteLine($"model state get ({this.state})");
                return state;
            }
        }

        //public CarAlarmState State { get; private set; }

        private Dictionary<Tuple<CarAlarmState, Action>, CarAlarmState> transitions;

        public Model()
        {
            this.state = CarAlarmState.OpenAndUnlocked;
            Console.WriteLine($"new model ({this.state})");
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
                { Tuple.Create(CarAlarmState.Armed, Action.Open), CarAlarmState.Alarm },
                { Tuple.Create(CarAlarmState.Alarm, Action.Unlock), CarAlarmState.OpenAndUnlocked },
                { Tuple.Create(CarAlarmState.Alarm, Action.Tick300), CarAlarmState.SilentAndOpen },
                { Tuple.Create(CarAlarmState.SilentAndOpen, Action.Close), CarAlarmState.Armed },
                { Tuple.Create(CarAlarmState.SilentAndOpen, Action.Unlock), CarAlarmState.OpenAndUnlocked }
            };
        }

        public IEnumerable<Action> GetPossibleActions()
        {
            foreach (Tuple<CarAlarmState, Action> keys in transitions.Keys)
            {
                if (keys.Item1 == this.state)
                    yield return keys.Item2;
            }
        }

        public void MakeTransition(Action action)
        {
            CarAlarmState newState;
            if (transitions.TryGetValue(Tuple.Create(this.state, action), out newState))
            {
                this.state = newState;
            }
        }
    }
}
