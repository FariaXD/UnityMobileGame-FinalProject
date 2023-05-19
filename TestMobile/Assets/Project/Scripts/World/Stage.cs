using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stage
{
    public enum StageType {
        COMBAT,
        EVENT,
        MERCHANT
    }

    public StageType type;
    public Sprite image;
    public Stage(StageType _type){
        this.type = _type;
        LoadImage();
    }

    public void LoadImage(){
        
    }
}
