using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stage
{
    public int pathNumber;
    public enum StageType {
        COMBAT,
        EVENT,
        MERCHANT,
        NONE
    }

    public StageIcon iconLoader;
    public StageType type;
    public Sprite image;
    public bool completed = false;
    public StageCombat.CombatType difficulty;

    public Stage(int _pathNumber, StageType _type, StageCombat.CombatType _difficulty = default(StageCombat.CombatType)){
        this.type = _type;
        this.pathNumber = _pathNumber;
        if(_type != StageType.NONE){
            this.difficulty = _difficulty;
            iconLoader = GameObject.FindGameObjectWithTag("StageIconLoader").GetComponent<StageIcon>();
            LoadImage();
        }  
    }

    public void LoadImage(){
        image = iconLoader.LoadImageFromType(this);
    }
}
