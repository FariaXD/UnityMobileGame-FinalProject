using UnityEngine;

public class CardInventoryEngine : MonoBehaviour {

    public Card card;
    public int countInDeck;
    public SpriteRenderer cardImageRenderer, frame;
    public DeckInventoryEngine deckInventoryEngine;
    private void Start() {
        deckInventoryEngine = GameObject.FindGameObjectWithTag("DeckInventory").GetComponent<DeckInventoryEngine>();
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
    }

    private void OnMouseDown() {
        deckInventoryEngine.SetSelectedCard(new CardCountPair(card, countInDeck));
    }
    
}