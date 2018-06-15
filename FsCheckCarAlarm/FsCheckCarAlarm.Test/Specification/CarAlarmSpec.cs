using FsCheck;
using FsCheckCarAlarm.Test.Specification;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FsCheckCarAlarm.Test
{
    public class CarAlarmSpec : ICommandGenerator<SUT, Model>
    {
        public SUT InitialActual
        {
            get
            {
                return new SUT();
            }
        }

        public Model InitialModel
        {
            get
            {
                return new Model();
            }
        }

        public Gen<Command<SUT, Model>> Next(Model model)
        {
            IEnumerable<Action> actions = model.GetPossibleActions();
            List<Command<SUT, Model>> commands = new List<Command<SUT, Model>>();

            foreach (Action action in actions)
            {
                if (action == Action.UnlockWithPinCorrect)
                    commands.Add(new DynamicCommand(action, model.Pin));
                else if (action == Action.UnlockWithPinWrong)
                    commands.Add(new DynamicCommand(action, model.Pin + "1"));
                else if (action == Action.SetPinCorrect)
                    commands.Add(new DynamicCommand(action, model.Pin, "2132"));
                else if (action == Action.SetPinWrong)
                    commands.Add(new DynamicCommand(action, model.Pin + "1", "2132"));
                else
                    commands.Add(new DynamicCommand(action));
            }

            return Gen.Elements(commands.ToArray());
        }
    }
}
