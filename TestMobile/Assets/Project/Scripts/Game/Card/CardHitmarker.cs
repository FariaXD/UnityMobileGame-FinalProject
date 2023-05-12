using UnityEngine;

public class CardHitmarker : MonoBehaviour {
    /*
        When card is being hold a hitmarker shows
    */
    private CardEngine associatedCard; //Associated cardEngine
    public bool onTarget = false; //If a target is on hitmarker

    private void Start() {
        associatedCard = GetComponentInParent<CardEngine>();
    }

    public void SetSpriteRendererAndCollider(bool state){
        this.GetComponent<SpriteRenderer>().enabled = state;
        this.GetComponent<CircleCollider2D>().enabled = state;
    }

    //Check if hitmarker enters target
    private void OnCollisionEnter2D(Collision2D other) {
        if(associatedCard.moving){
            if (other.gameObject.TryGetComponent<EnemyEngine>(out EnemyEngine enemy)){
                onTarget = true;
            }
            else if (other.gameObject.TryGetComponent<HeroEngine>(out HeroEngine hero))
                onTarget = true;
        }
    }

    //If hitmarker leaves target
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<EnemyEngine>(out EnemyEngine enemy)){
            onTarget = false;
        }
        else if (other.gameObject.TryGetComponent<HeroEngine>(out HeroEngine hero))
            onTarget = false;
    }

    //If hitmarker stays on target and is released uses card
    private void OnCollisionStay2D(Collision2D other) {
       if(!associatedCard.moving && onTarget && !associatedCard.used){
            if (other.gameObject.TryGetComponent<EnemyEngine>(out EnemyEngine enemy)){
                GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>().UseCard(associatedCard, enemy);
            }
            else if (other.gameObject.TryGetComponent<HeroEngine>(out HeroEngine hero))
                GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>().UseCard(associatedCard, hero);
            associatedCard.used = true;
            SetSpriteRendererAndCollider(false);
        }
        if(associatedCard.moving)
            onTarget = true;
    }
    
}