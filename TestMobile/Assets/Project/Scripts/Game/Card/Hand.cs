using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand
{
    public const int MAX_HAND_SIZE = 6;
    public const int NUM_STARTING_CARDS = 3;
    public List<Card> hand = new List<Card>(MAX_HAND_SIZE);

    public Deck deck;
    public Hand(Deck _deck){
        this.deck = _deck;
    }
    public void DrawCard(){
        if(hand.Count < MAX_HAND_SIZE)
            hand.Add(deck.DrawCard());
    }
    public void UseCard(Card c, Character target){
        c.UseCardOnTarget(target);
        hand.Remove(c);
        deck.ShuffleCard(c);
    }
}
