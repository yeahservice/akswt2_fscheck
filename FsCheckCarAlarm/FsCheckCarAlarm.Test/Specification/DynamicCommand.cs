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

        public DynamicCommand(Action action)
        {
            this.action = action;
        }

        public override Model RunModel(Model model)
        {
            model.MakeTransition(this.action);
            return model;
        }

        public override SUT RunActual(SUT sut)
        {
            sut.ExecuteAction(this.action);
            return sut;
        }

        public override bool Pre(Model model)
        {
            Console.WriteLine($"model={model.State}");
            return true;
        }

        public override Property Post(SUT sut, Model model)
        {
            Console.WriteLine($"sut={sut.State} model={model.State}");
            return (sut.State == model.State).ToProperty();
        }

        public override string ToString()
        {
            return this.action.ToString();
        }
    }
}
