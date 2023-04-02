using UnityEngine;

public class CardEngine : MonoBehaviour {

    private Card card;
    public void UpdateCard(Card c){
        card = c;
        Debug.Log(card.cardSprite);
        this.GetComponent<SpriteRenderer>().sprite = card.cardSprite;
    }
    private void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>().UseCard(card);
    }
}
