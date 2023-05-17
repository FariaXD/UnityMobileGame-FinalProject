using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team {
    /*
        Class holding every hero
    */
    public List<HeroEngine> teamGO = new List<HeroEngine>(); //List of HeroEngines
    public static float startingMana = 7f; //Starting mana
    public float currentMana = 7f; //Current mana
    public HeroEngine selectedHero; //Currently selected hero

    //Generate each hero
    public void SetHeroes(GameObject[] heroesGO){
        teamGO.Clear(); //Refresh list
        foreach (GameObject heroGO in heroesGO)
        {
            teamGO.Add(heroGO.GetComponent<HeroEngine>()); //Add hero engine
        }
        selectedHero = teamGO[0];
        foreach(HeroEngine en in teamGO)
                en.SetHero(); //Generate or refresh hero
    }

    //Sets mana back to max
    public void RefreshMana(){
        currentMana = startingMana;
    }

    //Return random alive hero
    public Character GetRandomHero(){
        int index = 0;
        bool valid = false;
        while(!valid){
            index = Random.Range(0, teamGO.Count);
            if(!teamGO[index].hero.diceased)
                valid = true;
        }
        return teamGO[index].hero;
    }

    //Check if all heroes are dead 
    public bool GameEnded(){
        foreach(HeroEngine en in teamGO)
            if(!en.hero.diceased)
                return false;
        return true;
    }

    //Draw card for each hero
    public void DrawCardForEachHero(){
        foreach(HeroEngine en in teamGO){
            en.hero.hand.DrawCard();
        }
    }

    //Reduce status effect duration
    public void RefreshStatusEffects(){
        foreach(HeroEngine en in teamGO)
            en.hero.ReduceStatusEffectDurations();
    }

    public void StatusEffectEndTurn(){
        foreach(HeroEngine en in teamGO)
            if(!en.hero.diceased)
                en.hero.CheckActionForStatus(Character.Character_Action.END_TURN);
    }
}