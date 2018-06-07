﻿using FsCheck;
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
            model.makeTransition(this.action);
            return model;
        }

        public override SUT RunActual(SUT sut)
        {
            sut.executeAction(this.action);
            return sut;
        }

        public override bool Pre(Model model)
        {
            return true;
        }

        public override Property Post(SUT sut, Model model)
        {
            return (sut.State == model.State).ToProperty();
        }

        public override string ToString()
        {
            return this.action.ToString();
        }
    }
}