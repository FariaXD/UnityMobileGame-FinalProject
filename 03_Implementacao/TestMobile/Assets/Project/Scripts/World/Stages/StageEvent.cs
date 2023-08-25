using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEvent : Stage
{
    //Stage event responsible for loading the event
    public Event sEvent;
    public StageEvent(int _pathNumber, Event _sEvent) : base(_pathNumber, StageType.EVENT){
        this.sEvent = _sEvent;
    }
}
