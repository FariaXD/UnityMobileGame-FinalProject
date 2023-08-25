using UnityEngine;

public class CardMenuEngine : MonoBehaviour {
    /*
        Runtime class responsible for controlling a card slot in the deck menu
    */
    public Card card; //associated card
    public int countInDeck; //count of card in deck
    public SpriteRenderer cardImageRenderer, frame; //sprites renreders
    public ClassDeckMenuEngine classDeckMenuEngine; //deck menu engine
    private BoxCollider2D col; //collider
    private void Start() {
        classDeckMenuEngine = GameObject.FindGameObjectWithTag("MenuDisplayedDeck").GetComponent<ClassDeckMenuEngine>();
        col = GetComponent<BoxCollider2D>();
    }
    //Associates a card to the slot
    public void SetCard(CardCountPair cardAndCount) {
        SetVisibility(true);
        this.card = cardAndCount.Card;
        this.countInDeck = cardAndCount.Count;
        cardImageRenderer.sprite = this.card.cardImage;
    }
    //Clears the slot
    public void ClearSlot(){
        SetVisibility(false);
    }
    //Sets the visibility of the slot
    private void SetVisibility(bool state){
        cardImageRenderer.enabled = state;
        frame.enabled = state;
        col.enabled = state;
    }
    //Selects the associated card to be displayed
    private void OnMouseDown() {
        if(card != null){
            classDeckMenuEngine.SetSelectedCard(new CardCountPair(card, countInDeck));
        }
    } 
}