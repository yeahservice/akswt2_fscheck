using System.Reflection;

namespace FsCheckCarAlarm.Test.Specification
{
    public class SUT
    {
        private CarAlarm carAlarm;

        public CarAlarmState State
        {
            get { return carAlarm.State; }
        }

        /*public bool UnlockedTrunk
        {
            get { return carAlarm.UnlockedTrunk; }
        }*/

        public SUT()
        {
            carAlarm = new CarAlarm();
        }

        public void ExecuteAction(Action action, string pin, string newPin)
        {
            switch (action)
            {
                case Action.Tick20:
                    CallTickMethod(20);
                    break;
                case Action.Tick30:
                    CallTickMethod(30);
                    break;
                case Action.Tick300:
                    CallTickMethod(300);
                    break;
                case Action.UnlockWithPinCorrect:
                case Action.UnlockWithPinWrong:
                    CallSUTMethod("Unlock", new[] { pin });
                    break;
                case Action.SetPinCorrect:
                case Action.SetPinWrong:
                    CallSUTMethod("SetPinCode", new[] { pin, newPin });
                    break;
                default:
                    CallSUTMethod(action.ToString());
                    break;
            }
        }

        private void CallTickMethod(int times)
        {
            for (int i = 0; i < times; ++i)
            {
                CallSUTMethod("Tick");
            }
        }

        private void CallSUTMethod(string name)
        {
            CallSUTMethod(name, null);
        }

        private void CallSUTMethod(string name, object[] parameters)
        {
            MethodInfo methodInfo = carAlarm.GetType().GetMethod(name);
            methodInfo.Invoke(carAlarm, parameters);
        }
    }
}