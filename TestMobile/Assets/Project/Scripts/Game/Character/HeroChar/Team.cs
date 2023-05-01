using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team {
    public List<HeroEngine> teamGO = new List<HeroEngine>();
    public static float startingMana = 7f;
    public float currentMana = 7f;
    public HeroEngine selectedHero;

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

    public Character GetRandomHero(){
        int index = 0;
        bool valid = false;
        while(!valid){
            index = Random.Range(0, teamGO.Count);
            if(teamGO[index].hero.currentHealth > 0)
                valid = true;
        }
        return teamGO[index].hero;
    }

    public bool GameEnded(){
        foreach(HeroEngine en in teamGO)
            if(en.hero.currentHealth > 0)
                return false;
        return true;
    }

    public void DrawCardForEachHero(){
        foreach(HeroEngine en in teamGO){
            en.hero.hand.DrawCard();
        }
    }
}