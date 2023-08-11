using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team {
    /*
        Class holding every hero
    */
    public Inventory inventory;
    public List<HeroEngine> teamGO = new List<HeroEngine>(); //List of HeroEngines
    public static float startingMana = 7f; //Starting mana
    public float currentMana = 7f; //Current mana
    public HeroEngine selectedHero; //Currently selected hero
    public float damageModifier = 1f;

    //Generate each hero
    public void SetHeroes(GameObject[] heroesGO){
        inventory = new Inventory();
        teamGO.Clear(); //Refresh list
        foreach (GameObject heroGO in heroesGO)
        {
            teamGO.Add(heroGO.GetComponent<HeroEngine>()); //Add hero engine
        }
        selectedHero = teamGO[0];
        RestartHeroes();
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

    public void TargetingAllAllies(bool state, bool force = default(bool))
    {
        foreach (HeroEngine hero in teamGO)
        {
            if (hero.hero != null && !hero.hero.diceased)
            {
                hero.targetedIcon.enabled = state;
            }
            else if (force)
            {
                hero.targetedIcon.enabled = state;
            }
        }
    }

    public void RestartHeroes(){
        List<Hero> heroList = HeroInitializer.InitializeHeroes();
        for(int i = 0; i < heroList.Count; i++){
            teamGO[i].SetHero(heroList[i]);
        }
    }

    //Draw card for each hero
    public void DrawCardForEachHero(){
        foreach(HeroEngine en in teamGO){
            en.hero.hand.DrawCard();
        }
    }

    public void HealHeroesPercentage(float healPercentage)
    {
        foreach (HeroEngine en in teamGO)
        {
            if(en.hero.diceased)
                en.hero.currentHealth = 0f;
            float newHealth = Mathf.Round(en.hero.currentHealth + (en.hero.maxHealth * healPercentage));
            en.hero.currentHealth = (newHealth > en.hero.maxHealth) ? en.hero.maxHealth : newHealth;
            en.SetDead(false);
            en.hero.diceased = false;
        }
    }
    public void ShieldHeroesAmmount(float ammount){
        foreach (HeroEngine en in teamGO)
        {
            if (!en.hero.diceased){
                en.hero.shield += ammount;
            }
        }
    }

    public void ResetShieldCharacters(){
        teamGO.ForEach(hero => hero.hero.shield = hero.hero.maxShield);
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

    public List<Hero> GetHeroObjects(){
        List<Hero> result = new List<Hero>();
        teamGO.ForEach(x => result.Add(x.hero));
        return result;
    }
}