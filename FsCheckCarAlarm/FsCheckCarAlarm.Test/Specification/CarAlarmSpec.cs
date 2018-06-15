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
            System.Console.WriteLine($"Next ({model.Uuid})");
            IEnumerable<Action> actions = model.GetPossibleActions();
            List<Command<SUT, Model>> commands = new List<Command<SUT, Model>>();

            foreach (Action action in actions)
            {
                if (action == Action.Unlock)
                    commands.Add(new DynamicCommand(action, model.Pin));
                else if (action == Action.SetPinCode)
                    commands.Add(new DynamicCommand(action, model.Pin, "2132"));
                else
                    commands.Add(new DynamicCommand(action));
            }
            //commands.AddRange(actions.Select(a => new DynamicCommand(a)));
            return Gen.Elements(commands.ToArray());
        }
    }
}
