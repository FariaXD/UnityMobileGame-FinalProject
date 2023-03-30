using UnityEngine;

public class CardBehaviour : MonoBehaviour {

    public PlayerEngine player;
    public int indexInHand;
    private void OnMouseDown() {
        UseCard();
    }

    public void UseCard(){
        player.UseCard(indexInHand);
    }
    
}