using UnityEngine;

public class HeroEngine : MonoBehaviour {


    public Hero hero;
    public float startingHealth = 20f;
    public float startingShield = 5f;
    private Animator anim;
    private GameEngine engine;

    private void Start() {
        anim = GetComponent<Animator>();
        engine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>(); 
        hero = new Hero(anim, startingHealth, startingShield);
    }

    public void UseCard(int _index){
        /* Card cardToUse = hero.cardsInHand[_index];
        switch (cardToUse.type)
        {
            case Card.Card_Type.Damage:
                CardDamage usingCard = (CardDamage) cardToUse;
                usingCard.UseCardOnTarget((Character) engine.enemies[0].enemy);
                engine.enemies[0].UpdateStatus();
                break;
            case Card.Card_Type.Status:
                break;
            case Card.Card_Type.Defense:
                break;
            case Card.Card_Type.Special:
                break;
        } */
    }
}