using System.Collections;
using UnityEngine;


public class HeroEngine : MonoBehaviour {

    public string heroName = "Color";
    public Hero hero;
    public float startingHealth = 20f;
    public float startingShield = 5f;
    private Animator anim;
    private GameEngine engine;

    private void Start() {
        anim = GetComponent<Animator>();
        engine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>(); 
        hero = new Hero(heroName, anim, startingHealth, startingShield);
    }

    public IEnumerator InitializeDeck(){
        hero.deck.deck = DeckInitializer.InitializeDeck(heroName);
        for(int i = 0; i < Hand.NUM_STARTING_CARDS;i++)
            hero.hand.DrawCard();
        yield return null;
    }

    private void OnMouseDown() {
        engine.SwitchActiveCharacter(this);
        Debug.Log("SWITCHING " + heroName);
    }
}