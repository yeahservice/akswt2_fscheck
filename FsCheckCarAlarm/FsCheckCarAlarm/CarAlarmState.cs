using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsCheckCarAlarm
{
    public enum CarAlarmState
    {
        OpenAndUnlocked,
        ClosedAndUnlocked,
        OpenAndLocked,
        ClosedAndLocked,
        Armed,
        SilentAndOpen,
        Alarm
    }
}
