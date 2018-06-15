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
            model.MakeTransition(this.action, newPin);
            return model;
        }

        public override SUT RunActual(SUT sut)
        {
            sut.ExecuteAction(this.action, pin, newPin);
            return sut;
        }

        public override Property Post(SUT sut, Model model)
        {
            Console.WriteLine($"[{action}] sut=[{sut.State}] model=[{model.State}]");
            return (sut.State == model.State).ToProperty();
        }

        public override string ToString()
        {
            return this.action.ToString();
        }
    }
}
