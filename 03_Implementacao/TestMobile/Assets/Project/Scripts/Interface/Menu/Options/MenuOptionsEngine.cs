using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MenuOptionsEngine : MonoBehaviour
{
    private int stages_completed = 0;
    private int enemies_slain = 0;
    private int artifacts_count = 0;
    public TextMeshProUGUI sc, es, ac;

    public enum STATS{
        STAGES_COMPLETED,
        ENEMIES_SLAIN,
        ARTIFACTS_COUNT
    }

    public void IncrementStat(STATS stat){
        switch(stat){
            case STATS.STAGES_COMPLETED:
            stages_completed++;
            break;
            case STATS.ENEMIES_SLAIN:
            enemies_slain++;
            break;
            case STATS.ARTIFACTS_COUNT:
            artifacts_count++;
            break;
        }
    }

    public int GetStat(STATS stat){
        switch (stat)
        {
            case STATS.STAGES_COMPLETED:
                return stages_completed;
            case STATS.ENEMIES_SLAIN:
                return enemies_slain;
            case STATS.ARTIFACTS_COUNT:
                return artifacts_count;
        }
        return 0;
    }

    private void Update(){  
        sc.text = stages_completed.ToString();
        es.text = enemies_slain.ToString();
        ac.text = artifacts_count.ToString();
    }
}
