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
    private DeckInitializer initialize;

    private void Start() {
        anim = GetComponent<Animator>();
        engine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>(); 
        hero = new Hero(heroName, anim, startingHealth, startingShield);
        initialize = new DeckInitializer(engine);
    }
    //Get Deck from JSON, and Draw 3 card
    public void InitializeDeck(){
        hero.deck.deck = initialize.InitializeDeck(heroName);//DeckInitializer.InitializeDeck(heroName);
        
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
        image.fillAmount = ((100 * hero.currentHealth) / startingHealth) / 100;
        return (hero.currentHealth <= 0);
    }

    public Character ReturnAssociatedCharacter(){
        return hero;
    }
}