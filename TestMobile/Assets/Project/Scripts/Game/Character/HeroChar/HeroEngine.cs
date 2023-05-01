using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class HeroEngine : MonoBehaviour, CharacterEngine {

    public string heroName = "Color";
    public Hero hero;
    public float startingHealth = 20f;
    public float startingShield = 5f;
    private Animator anim;
    private GameEngine engine;
    public Image image;

    private void Start() {
        anim = GetComponent<Animator>();
        engine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>(); 
        hero = new Hero(heroName, startingHealth, startingShield);
    }

    private void Update() {
        UpdateStatus();
    }
    //Get Deck from JSON, and Draw 3 card
    public void InitializeDeck(){
        hero.deck.deck = DeckInitializer.InitializeDeck(heroName);//DeckInitializer.InitializeDeck(heroName);
        
        hero.deck.ShuffleDeck();
        for(int i = 0; i < Hand.NUM_STARTING_CARDS;i++)
            hero.hand.DrawCard();
    }
    //Click the object
    private void OnMouseDown() {
        engine.SwitchActiveCharacter(this);
    }
    public bool UpdateStatus()
    {
        /* image.fillAmount = ((100 * hero.currentHealth) / startingHealth) / 100;
        return (hero.currentHealth <= 0); */
        if(hero.currentHealth <= 0){ 
            anim.SetBool("Dead", true);
            hero.diceased = true;
            engine.ForceSwitcHero();
        }
        return false;
    }

    public Character ReturnAssociatedCharacter(){
        return hero;
    }
}