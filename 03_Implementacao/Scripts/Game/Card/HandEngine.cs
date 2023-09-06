using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEngine : MonoBehaviour
{
    /*
        *Runtime Class
        This class represents the hand of the player and is used to update the display
        of each card dynamically
    */
    public CardEngine[] cardsEngine = new CardEngine[Hand.MAX_HAND_SIZE]; //CardEngine Array

    //Switch the active character hand (Switch class)
    public void SwitchHand(List<Card> cards){
        for(int i = 0; i < cards.Count; i++)
            cardsEngine[i].UpdateCard(cards[i]);
        for(int i = cards.Count; i < Hand.MAX_HAND_SIZE; i++)
            cardsEngine[i].UpdateCard(GenerateEmpty());
    }
    //Update Card if its used
    public void UpdateUsedCard(CardEngine _cardEngine){
        _cardEngine.UpdateCard(GenerateEmpty());
    }
    //Generates an empty card
    private Card GenerateEmpty(){
        return new CardDamage(-1, null,-1, false, -1,null) as Card;
    }
}
