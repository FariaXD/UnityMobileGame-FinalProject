using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class HeroEngine : MonoBehaviour, CharacterEngine {

    /*
        *Runtime class
        Class that loads the players character dynamically
    */
    public string heroName = "Color"; //Hero names
    public Hero hero; //Associated hero
    public float startingHealth = 20f; //The hero starting hp
    public float startingShield = 5f; //The hero starting shield
    private Animator anim; //Animation Controller obj
    private GameEngine engine; //GameEngine reference
    public Image healthImage; //Associated health Image obj
    public TextMeshProUGUI healthText; //Health text obj
    public Image shieldIcon;
    public TextMeshProUGUI shieldText;
    private bool heroSet = false; //If hero isnt loaded yet
    public List<SpriteRenderer> statusImages = new List<SpriteRenderer>(); //List of status imagerenderer obj
    public List<TextMeshProUGUI> statusTexts = new List<TextMeshProUGUI>(); //list of status text obj

    private void Start() {
        anim = GetComponent<Animator>();
        engine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>();
        shieldIcon.enabled = false;
        shieldText.enabled = false;
    }
    //Load hero if set or not
    public void SetHero(){
        hero = new Hero(heroName, startingHealth, startingShield);
        UpdateStatus();
        heroSet = true;
        SetDead(false);
    }
    private void Update() {
        if(heroSet)
            UpdateStatus();
    }
    //Get Deck from JSON, and Draw 3 card
    public void InitializeDeck(){
        hero.deck.deck = DeckInitializer.InitializeDeck(heroName);//DeckInitializer.InitializeDeck(heroName);
        hero.hand.hand.Clear();
        hero.deck.ShuffleDeck();
        for(int i = 0; i < Hand.NUM_STARTING_CARDS;i++)
            hero.hand.DrawCard();
    }
    //Click the object
    private void OnMouseDown() {
        engine.SwitchActiveCharacter(this);
    }

    public void SetDead(bool dead){
        anim.SetBool("Dead", dead);
    }
    //Update the hero visually
    public void UpdateStatus()
    {
        healthImage.fillAmount = ((100 * hero.currentHealth) / startingHealth) / 100;
        healthText.text = hero.currentHealth + "/" + hero.maxHealth;
        if(hero.currentHealth <= 0){
            SetDead(true);
            hero.diceased = true;
            if(engine.team.selectedHero.heroName == heroName)
                engine.ForceSwitcHero();
        }
        for(int i = 0; i < statusImages.Count; i++){
            if(hero.debuffs.Count - 1 >= i && hero.debuffs[i] != null){
                statusImages[i].sprite = hero.icons.GetStatusIcon(hero.debuffs[i]);
                statusTexts[i].text = hero.debuffs[i].duration.ToString();
            }
            else{
                statusImages[i].sprite = null;
                statusTexts[i].text = null;
            }
        }
        if(hero.shield > 0){
            shieldIcon.enabled = true;
            shieldText.enabled = true;
        }
        else{
            shieldIcon.enabled = false;
            shieldText.enabled = false;
        }
        shieldText.text = Mathf.Round(hero.shield).ToString();

    }
    //Return associated character
    public Character ReturnAssociatedCharacter(){
        return hero;
    }
}