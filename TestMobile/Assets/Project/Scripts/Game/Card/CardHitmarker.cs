﻿using UnityEngine;

public class CardHitmarker : MonoBehaviour {
    private CardEngine associatedCard;
    public bool onTarget = false;

    private void Start() {
        associatedCard = GetComponentInParent<CardEngine>();
    }

    public void SetSpriteRendererAndCollider(bool state){
        this.GetComponent<SpriteRenderer>().enabled = state;
        this.GetComponent<CircleCollider2D>().enabled = state;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(associatedCard.moving){
            if (other.gameObject.TryGetComponent<EnemyEngine>(out EnemyEngine enemy)){
                Debug.Log("ENTERED TARGET: " + enemy.name);
                onTarget = true;
            }
            else if (other.gameObject.TryGetComponent<HeroEngine>(out HeroEngine hero))
                onTarget = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<EnemyEngine>(out EnemyEngine enemy)){
            Debug.Log("EXITED TARGET: " + enemy.name);
            onTarget = false;
        }
        else if (other.gameObject.TryGetComponent<HeroEngine>(out HeroEngine hero))
            onTarget = false;
    }

    private void OnCollisionStay2D(Collision2D other) {
       if(!associatedCard.moving && onTarget && !associatedCard.used){
            if (other.gameObject.TryGetComponent<EnemyEngine>(out EnemyEngine enemy)){
                GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>().UseCard(associatedCard, enemy);
                Debug.Log("USE CARD");
            }
            else if (other.gameObject.TryGetComponent<HeroEngine>(out HeroEngine hero))
                GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>().UseCard(associatedCard, enemy);
            associatedCard.used = true;
            SetSpriteRendererAndCollider(false);
        }
        if(associatedCard.moving)
            onTarget = true;
    }
    
}