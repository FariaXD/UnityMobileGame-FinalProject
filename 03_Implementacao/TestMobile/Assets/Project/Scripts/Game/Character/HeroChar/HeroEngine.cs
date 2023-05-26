using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class HeroEngine : CharacterEngine {

    /*
        *Runtime class
        Class that loads the players character dynamically
    */
    public Hero hero; //Associated hero
    private bool heroSet = false; //If hero isnt loaded yet
   
    
    //Load hero if set or not
    public void SetHero(Hero _hero){
        this.hero = _hero;
        anim.runtimeAnimatorController = hero.anim;
        UpdateStatus();
        heroSet = true;
        SetDead(false);
    }
    private void Update() {
        if(heroSet)
            UpdateStatusHero();
    }
    //Get Deck from JSON, and Draw 3 card
    public void InitializeDeck(){
        hero.deck.deck = DeckInitializer.InitializeDeck(hero.name);//DeckInitializer.InitializeDeck(heroName);
        hero.hand.hand.Clear();
        hero.deck.ShuffleDeck();
        for(int i = 0; i < Hand.NUM_STARTING_CARDS;i++)
            hero.hand.DrawCard();
    }
    //Click the object
    private void OnMouseDown() {
        engine.SwitchActiveCharacter(this);
    }

    
    //Update the hero visually
    public void UpdateStatusHero()
    {
        UpdateStatus();
        if(hero.diceased && engine.team.selectedHero.heroName == heroName)
            engine.ForceSwitcHero();
    }
    //Return associated character
    public override Character ReturnAssociatedCharacter(){
        return hero;
    }
}