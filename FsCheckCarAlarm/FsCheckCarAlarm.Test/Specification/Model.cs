using System;
using System.Collections.Generic;

namespace FsCheckCarAlarm.Test.Specification
{
    public class Model
    {
        public CarAlarmState State { get; private set; }

        private Dictionary<Tuple<CarAlarmState, Action>, CarAlarmState> transitions;

        public Model()
        {
            this.State = CarAlarmState.OpenAndUnlocked;
            this.transitions = new Dictionary<Tuple<CarAlarmState, Action>, CarAlarmState>()
            {
                { Tuple.Create(CarAlarmState.OpenAndUnlocked, Action.Close), CarAlarmState.ClosedAndUnlocked },
                { Tuple.Create(CarAlarmState.OpenAndUnlocked, Action.Lock), CarAlarmState.OpenAndLocked },
                { Tuple.Create(CarAlarmState.OpenAndLocked, Action.Unlock), CarAlarmState.OpenAndUnlocked },
                { Tuple.Create(CarAlarmState.OpenAndLocked, Action.Close), CarAlarmState.ClosedAndLocked },
                { Tuple.Create(CarAlarmState.ClosedAndUnlocked, Action.Open), CarAlarmState.OpenAndUnlocked },
                { Tuple.Create(CarAlarmState.ClosedAndUnlocked, Action.Lock), CarAlarmState.ClosedAndLocked },
                { Tuple.Create(CarAlarmState.ClosedAndLocked, Action.Unlock), CarAlarmState.ClosedAndUnlocked },
                { Tuple.Create(CarAlarmState.ClosedAndLocked, Action.Open), CarAlarmState.OpenAndLocked }
            };

        }



        public void makeTransition(Action action)
        {
            CarAlarmState newState;
            if (transitions.TryGetValue(Tuple.Create(this.State, action), out newState))
            {
                this.State = newState;
            }
        }
    }
}
