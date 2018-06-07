using System.Reflection;

namespace FsCheckCarAlarm.Test.Specification
{
    public class SUT
    {
        private CarAlarm carAlarm;
        public CarAlarmState State
        {
            get
            {
                return this.carAlarm.GetState();
            }
        }

        public SUT()
        {
            this.carAlarm = new CarAlarm();
        }

        public void executeAction(Action action)
        {
            MethodInfo methodInfo = carAlarm.GetType().GetMethod(action.ToString());
            methodInfo.Invoke(this.carAlarm, null);
        }
    }
}