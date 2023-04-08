using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEngine : MonoBehaviour
{
    public CardEngine[] cardsEngine = new CardEngine[7];
    private GameEngine engine;
    private void Start() {
        engine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>();
    }

    public void SwitchHand(List<Card> cards){
        for(int i = 0; i < cards.Count; i++)
            cardsEngine[i].UpdateCard(cards[i]);
        for(int i = cards.Count; i < Hand.MAX_HAND_SIZE; i++)
            cardsEngine[i].UpdateCard(GenerateEmpty());
    }
    public void UpdateUsedCard(CardEngine _cardEngine){
        _cardEngine.UpdateCard(GenerateEmpty());
    }
    private Card GenerateEmpty(){
        return new CardDamage(-1, null,-1,-1,null) as Card;
    }
}
