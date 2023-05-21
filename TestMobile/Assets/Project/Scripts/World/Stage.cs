using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stage
{
    public int pathNumber;
    public enum StageType {
        COMBAT,
        EVENT,
        MERCHANT
    }

    public StageIcon iconLoader;
    public StageType type;
    public Sprite image;
    public bool completed = false;
    public StageCombat.CombatType difficulty;

    public Stage(int _pathNumber, StageType _type, StageCombat.CombatType _difficulty = default(StageCombat.CombatType)){
        this.pathNumber = _pathNumber;
        this.difficulty = _difficulty;
        iconLoader = GameObject.FindGameObjectWithTag("StageIconLoader").GetComponent<StageIcon>();
        this.type = _type;
        LoadImage();
    }

    public void LoadImage(){
        image = iconLoader.LoadImageFromType(this);
    }

    public void IsCompleted(){
        completed = true;
        image = iconLoader.LoadImageFromType(this);
    }
}
