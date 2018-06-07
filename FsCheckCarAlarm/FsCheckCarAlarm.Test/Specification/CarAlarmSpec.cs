using FsCheck;
using FsCheckCarAlarm.Test.Specification;


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
            return Gen.Elements(new Command<SUT, Model>[] 
            {
                new DynamicCommand(Action.Lock),
                new DynamicCommand(Action.Unlock),
                new DynamicCommand(Action.Open),
                new DynamicCommand(Action.Close)
            });
        }
    }
}
