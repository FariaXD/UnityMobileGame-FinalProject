using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    public float numCardsInHand;
    public List<Card> cardsInHand = new List<Card>(MAX_HAND_SIZE);
    public const int DECK_SIZE = 20;
    public const int MAX_HAND_SIZE = 10;
    public float cardsUsed = 0f;
    public float cardsInDeck;

    public Player(Animator _anim, float _health, float _mana) : base(_anim, _health, _mana){}

    


}
