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
                switch (action)
                {
                    case Action.UnlockWithPinCorrect:
                        commands.Add(new DynamicCommand(action, model.Pin));
                        break;
                    case Action.UnlockWithPinWrong:
                        commands.Add(new DynamicCommand(action, model.Pin + "1"));
                        break;
                    case Action.SetPinCorrect:
                        commands.Add(new DynamicCommand(action, model.Pin, model.GeneratePin()));
                        break;
                    case Action.SetPinWrong:
                        commands.Add(new DynamicCommand(action, model.Pin + "1", model.GeneratePin()));
                        break;
                    default:
                        commands.Add(new DynamicCommand(action));
                        break;
                }
            }

            return Gen.Elements(commands.ToArray());
        }
    }
}
