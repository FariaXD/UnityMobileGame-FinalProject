using UnityEngine;

public class PlayerEngine : MonoBehaviour {


    public Player player;
    public float startingHealth = 20f;
    public float startingMana = 4f;
    private Animator anim;
    public CardBehaviour[] usableCardsInHand = new CardBehaviour[Player.MAX_HAND_SIZE];
    private GameEngine engine;

    private void Start() {
        anim = GetComponent<Animator>();
        engine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>();
        player = new Player(anim, startingHealth, startingMana);
        for(int i = 0; i < Player.MAX_HAND_SIZE; i++){
            CardDamage card = new CardDamage("Slice", 1f, 1f, 7f);
            player.cardsInHand.Add((Card) card);
        }
    }

    public void UseCard(int _index){
        Card cardToUse = player.cardsInHand[_index];
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
        }
    }
}