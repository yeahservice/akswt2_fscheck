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
                return this.carAlarm.State;
            }
        }

        public SUT()
        {
            this.carAlarm = new CarAlarm();
        }

        public void ExecuteAction(Action action, string modelPin, string newPin)
        {
            switch (action)
            {
                case Action.Tick20:
                    for (int i = 0; i < 20; ++i)
                    {
                        CallSUTMethod("Tick");
                    }
                    break;
                case Action.Tick30:
                    for (int i = 0; i < 30; ++i)
                    {
                        CallSUTMethod("Tick");
                    }
                    break;
                case Action.Tick300:
                    for (int i = 0; i < 300; ++i)
                    {
                        CallSUTMethod("Tick");
                    }
                    break;
                case Action.Unlock:
                    CallSUTMethod(action.ToString(), new[] { modelPin });
                    break;
                case Action.SetPinCode:
                    CallSUTMethod(action.ToString(), new[] { modelPin, newPin });
                    break;
                default:
                    CallSUTMethod(action.ToString());
                    break;
            }
        }

        private void CallSUTMethod(string name, object[] parameters)
        {
            MethodInfo methodInfo = carAlarm.GetType().GetMethod(name);
            methodInfo.Invoke(this.carAlarm, parameters);
        }

        private void CallSUTMethod(string name)
        {
            CallSUTMethod(name, null);
        }
    }
}