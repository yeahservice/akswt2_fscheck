using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsCheckCarAlarm.Test.Specification
{
    public enum Action
    {
        CloseDoor,
        OpenDoor,
        Lock,
        UnlockWithPinCorrect,
        UnlockWithPinWrong,
        Tick20,
        Tick30,
        Tick300,
        SetPinCorrect,
        SetPinWrong,
        LockTrunk,
        UnlockTrunk
    }
}
