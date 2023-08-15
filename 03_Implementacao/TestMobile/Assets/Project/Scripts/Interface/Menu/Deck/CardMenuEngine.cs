using UnityEngine;

public class CardMenuEngine : MonoBehaviour {

    public Card card;
    public int countInDeck;
    public SpriteRenderer cardImageRenderer, frame;
    public ClassDeckMenuEngine classDeckMenuEngine;
    private BoxCollider2D col;
    private void Start() {
        classDeckMenuEngine = GameObject.FindGameObjectWithTag("MenuDisplayedDeck").GetComponent<ClassDeckMenuEngine>();
        col = GetComponent<BoxCollider2D>();
    }
    public void SetCard(CardCountPair cardAndCount) {
        SetVisibility(true);
        this.card = cardAndCount.Card;
        this.countInDeck = cardAndCount.Count;
        cardImageRenderer.sprite = this.card.cardImage;
    }

    public void ClearSlot(){
        SetVisibility(false);
    }

    private void SetVisibility(bool state){
        cardImageRenderer.enabled = state;
        frame.enabled = state;
        col.enabled = state;
    }

    private void OnMouseDown() {
        if(card != null){
            classDeckMenuEngine.SetSelectedCard(new CardCountPair(card, countInDeck));
        }
    } 
}