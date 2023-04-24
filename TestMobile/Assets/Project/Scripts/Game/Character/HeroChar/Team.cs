using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team {
    public List<HeroEngine> teamGO = new List<HeroEngine>();
    public static float startingMana = 7f;
    public float currentMana = 7f;
    public HeroEngine selectedHero;
    public enum CLASS{
        RED,
        BLUE,
        GREEN
    }

    public void SetHeroes(GameObject[] heroesGO){
        foreach (GameObject heroGO in heroesGO)
        {
            teamGO.Add(heroGO.GetComponent<HeroEngine>());
        }
        selectedHero = teamGO[0];
    }

    public void RefreshMana(){
        currentMana = startingMana;
    }
}