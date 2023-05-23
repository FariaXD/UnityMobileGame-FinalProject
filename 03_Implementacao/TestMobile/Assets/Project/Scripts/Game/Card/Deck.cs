using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    public const int MAX_DECK_SIZE = 15; //Max size
    public List<Card> deck = new List<Card>(MAX_DECK_SIZE); //Deck list
    
    //Return card and remove it from deck
    public Card DrawCard(){
        Card c = deck[0];
        deck.Remove(c);
        return c;
    }
    //Adds card to end of deck
    public void ShuffleCard(Card c){
        deck.Add(c);
    }
    //Randomizes deck
    public void ShuffleDeck(){
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }
}
