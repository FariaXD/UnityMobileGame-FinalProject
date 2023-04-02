using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team {
    public List<HeroEngine> teamGO = new List<HeroEngine>();
    public float startingMana = 7f;
    public float currentMana = 7f;
    public HeroEngine selectedHero;

    public void SetHeroes(GameObject[] heroesGO){
        foreach (GameObject heroGO in heroesGO)
        {
            teamGO.Add(heroGO.GetComponent<HeroEngine>());
        }
        selectedHero = teamGO[0];
    }
}