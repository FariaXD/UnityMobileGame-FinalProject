using UnityEngine;
using System.Collections.Generic;

public class ClassDeckMenuEngine : MonoBehaviour {
    /*
    Runtime class responsible for controlling all the card slots
    */
    public Deck currentDeck; //Current deck to be displayed
    public List<CardMenuEngine> cardInventorySlots = new List<CardMenuEngine>(); //list of card slots
    private DisplayDeckMenuEngine displayDeckMenuEngine; //display detailed card engine

    public void Initialize() {
        displayDeckMenuEngine = GameObject.FindGameObjectWithTag("MenuDisplayDeck").GetComponent<DisplayDeckMenuEngine>();
        GameObject[] cardSlots = GameObject.FindGameObjectsWithTag("MenuCardSlot");
        for(int i = 0; i < cardSlots.Length; i++)
            cardInventorySlots.Add(cardSlots[i].GetComponent<CardMenuEngine>()); //load card slots
    }
    //Set current deck and updated inventory
    public CardCountPair SetCurrentDeck(Deck currentDeck){
        this.currentDeck = currentDeck;
        EmptyCardFrames();
        return UpdateInventory();
    }
    //Set selected card
    public void SetSelectedCard(CardCountPair selectedCardAndCount){
        displayDeckMenuEngine.displayCardMenuEngine.SetCard(displayDeckMenuEngine.selectedHero, selectedCardAndCount);
    }
    //Clear slots
    private void EmptyCardFrames(){
        cardInventorySlots.ForEach(cis => cis.ClearSlot());
    }
    //Update inventory creating a new dictionary with unique cards and there count in deck
    private CardCountPair UpdateInventory(){
        Dictionary<Card, int> uniqueCards = new Dictionary<Card, int>();
        List<string> cardNames = new List<string>();
        int indexI = 0;
        foreach (Card cardi in currentDeck.deck)
        {
            int indexJ = 0;
            int count = 1;
            foreach (Card cardj in currentDeck.deck)
            {
                if(indexI != indexJ && cardi.cardName == cardj.cardName){
                    count++;
                }
                indexJ++;
            }
            if(!cardNames.Contains(cardi.cardName)){
                uniqueCards.Add(cardi, count);
                cardNames.Add(cardi.cardName);
            }
            indexI++;
        }
        int j = 0;
        foreach(KeyValuePair<Card, int> cc in uniqueCards)
        {
            cardInventorySlots[j].SetCard(new CardCountPair(cc.Key, cc.Value));
            j++;
        }
        CardCountPair defCard = new CardCountPair(null, 0);
        foreach (KeyValuePair<Card, int> tmp in uniqueCards){
            defCard = new CardCountPair(tmp.Key, tmp.Value);
            return defCard;
        }
        return defCard; //returns a default card to be displayed
    }
}

//Data class that holds a card and their count in deck
public class CardCountPair
{
    public Card Card { get; private set; }
    public int Count { get; private set; }

    public CardCountPair(Card card, int value)
    {
        Card = card;
        Count = value;
    }
}