using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsCheckCarAlarm.Test.Specification
{
    public class DynamicCommand : Command<SUT, Model>
    {
        private Action action;
        private string newPin;
        private string pin;

        public DynamicCommand(Action action) : this(action, null, null) { }

        public DynamicCommand(Action action, string pin) : this(action, pin, null) { }

        public DynamicCommand(Action action, string pin, string newPin)
        {
            this.action = action;
            this.newPin = newPin;
            this.pin = pin;
        }

        public override Model RunModel(Model model)
        {
            model.MakeTransition(action, newPin);
            return model;
        }

        public override SUT RunActual(SUT sut)
        {
            sut.ExecuteAction(action, pin, newPin);
            return sut;
        }

        public override Property Post(SUT sut, Model model)
        {
            bool condition = (sut.State == model.State && sut.UnlockedTrunk == model.UnlockedTrunk);
            Console.WriteLine($"[{action}] sut=[{sut.State}, {sut.UnlockedTrunk}] model=[{model.State}, {model.UnlockedTrunk}] --- [{condition}]");

            return condition.ToProperty();
        }

        public override string ToString()
        {
            return action.ToString();
        }
    }
}
