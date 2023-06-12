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
                if (associatedCard.card.type != Card.Action_Type.Defense){
                    if(associatedCard.card.area)
                        enemy.engine.TargetingAllEnemies(true);
                    else
                        enemy.targetedIcon.enabled = true;
                }
                onTarget = true;
            }
            else if (other.gameObject.TryGetComponent<HeroEngine>(out HeroEngine hero)){
                if (associatedCard.card.type == Card.Action_Type.Defense){
                    if (associatedCard.card.area) 
                        hero.engine.team.TargetingAllAllies(true);
                    else
                        if (associatedCard.card.type == Card.Action_Type.Defense)
                            hero.targetedIcon.enabled = true;
                }
                onTarget = true;
            }
        }
    }

    //If hitmarker leaves target
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<EnemyEngine>(out EnemyEngine enemy)){
            if (associatedCard.card.area)
                enemy.engine.TargetingAllEnemies(false);
            else
                enemy.targetedIcon.enabled = false;
            onTarget = false;
        }
        else if (other.gameObject.TryGetComponent<HeroEngine>(out HeroEngine hero)){
            if (associatedCard.card.area)
                hero.engine.team.TargetingAllAllies(false);
            else
                hero.targetedIcon.enabled = false;
            onTarget = false;
        }
    }

    //If hitmarker stays on target and is released uses card
    private void OnCollisionStay2D(Collision2D other) {
       if(!associatedCard.moving && onTarget && !associatedCard.used){
            if (other.gameObject.TryGetComponent<EnemyEngine>(out EnemyEngine enemy)){
                GameObject.FindGameObjectWithTag("CombatEngine").GetComponent<CombatEngine>().UseCard(associatedCard, enemy);
                enemy.engine.TargetingAllEnemies(false, true);
            }
            else if (other.gameObject.TryGetComponent<HeroEngine>(out HeroEngine hero)){
                GameObject.FindGameObjectWithTag("CombatEngine").GetComponent<CombatEngine>().UseCard(associatedCard, hero);
                hero.engine.team.TargetingAllAllies(false, true);
            }
            associatedCard.used = true;
            SetSpriteRendererAndCollider(false);
        }
        if(associatedCard.moving)
            onTarget = true;
    }
    
}