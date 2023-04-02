using UnityEngine;

public class CardEngine : MonoBehaviour {

    private Card card;
    public void UpdateCard(Card c){
        card = c;
        this.GetComponent<SpriteRenderer>().sprite = card.cardSprite;
    }
    private void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>().UseCard(card);
    }
}
