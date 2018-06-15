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
        private string modelPin;

        public DynamicCommand(Action action) : this(action, null, null) { }

        public DynamicCommand(Action action, string modelPin) : this(action, modelPin, null) { }

        public DynamicCommand(Action action, string modelPin, string newPin)
        {
            //Console.WriteLine($"action={action.ToString()}");
            this.action = action;
            this.newPin = newPin;
            this.modelPin = modelPin;
        }

        public override Model RunModel(Model model)
        {
            //Console.WriteLine($"RunModel model=({model.State}, {model.Uuid})");
            model.MakeTransition(this.action, newPin);
            return model;
        }

        public override SUT RunActual(SUT sut)
        {
            //Console.WriteLine($"RunActual");
            sut.ExecuteAction(this.action, modelPin, newPin);
            return sut;
        }

        public override Property Post(SUT sut, Model model)
        {
            Console.WriteLine($"Post sut=({sut.State}) model=({model.State}, {model.Uuid})");
            return (sut.State == model.State).ToProperty();
        }

        public override string ToString()
        {
            return this.action.ToString();
        }
    }
}
