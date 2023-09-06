using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand
{
    public const int MAX_HAND_SIZE = 6; //Max size of cards in hand
    public const int NUM_STARTING_CARDS = 3; //Number of starting cards
    public List<Card> hand = new List<Card>(MAX_HAND_SIZE); //Hand List

    public Deck deck; //Deck reference
    public Hand(Deck _deck){
        this.deck = _deck;
    }
    //Draws card from deck and adds it to and
    public void DrawCard(){
        if(hand.Count < MAX_HAND_SIZE)
            hand.Add(deck.DrawCard());
    }

    //Uses card on target and shuffles it to deck
    public void UseCard(Card c, Character target, PartyStats ps){
        c.UseCardOnTarget(target, ps); //Use on target
        hand.Remove(c); //Removes from hand
        deck.ShuffleCard(c); //Adds to deck
    }
}
