using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsCheckCarAlarm.Test.Specification
{
    public class Transition
    {
        private CarAlarmState from;
        private Action action;
        private CarAlarmState? to;
        private CarAlarmState? conditionalTo;

        public CarAlarmState? ConditionalTo
        {
            get
            {
                return conditionalTo;
            }
        }

        public CarAlarmState? To
        {
            get
            {
                return to;
            }
        }

        public Action Action
        {
            get
            {
                return action;
            }
        }

        public CarAlarmState From
        {
            get
            {
                return from;
            }
        }

        public Transition(CarAlarmState from, Action action, CarAlarmState to) : this(from, action, to, null) { }

        public Transition (CarAlarmState from, Action action, CarAlarmState? to, CarAlarmState? conditionalTo)
        {
            this.from = from;
            this.action = action;
            this.to = to;
            this.conditionalTo = conditionalTo;
        }
    }
}
