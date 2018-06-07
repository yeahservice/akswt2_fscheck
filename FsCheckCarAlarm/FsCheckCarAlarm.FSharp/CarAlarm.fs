namespace FsCheckCarAlarm.FSharp

type CarAlarmState =
    | OpenAndUnlocked = 0 
    | ClosedAndUnlocked = 1
    | OpenAndLocked = 2
    | ClosedAndLocked = 3

type CarAlarm() = 
    member val State = CarAlarmState.OpenAndUnlocked with get, set
    member this.Open() = this.State <- CarAlarmState.OpenAndUnlocked
    member this.Close() = this.State <- CarAlarmState.OpenAndUnlocked
    member this.Lock() = this.State <- CarAlarmState.OpenAndUnlocked
    member this.Unlock() = this.State <- CarAlarmState.OpenAndUnlocked
