module CarAlarm =

    type CarAlarm() = 
        let mutable n = 0
        member this.Open() = n
        member this.Close() = n
        member this.Lock() = n
        member this.Unlock() = n
        member this.GetState = n