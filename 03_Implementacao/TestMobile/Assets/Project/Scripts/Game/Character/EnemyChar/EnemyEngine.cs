using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class EnemyEngine : CharacterEngine {
    /*
        *Runtime Class
        In the game there can be three enemies at the same time
        This class loads the enemies dynamically based on the name received from the game engine
    */

    public string enemyName = "Enemy"; //Enemy name
    public Enemy enemy; //Associated enemy
    public Image healthBGImage; //Health bg Image obj
    public TextMeshProUGUI attackInfo; //Attack Text obj
    public SpriteRenderer attackIcon;

    void Awake(){
        SetEnemyUI(false);
        shieldIcon.enabled = false;
        shieldText.enabled = false;
    }

    void Update() {
        UpdateStatusEnemy(); //Updates status in each frame
    }

    //Sets new enemy
    public void SetNewEnemy(Enemy _enemy){
        this.enemy = _enemy;
        anim.runtimeAnimatorController = _enemy.anim;
        SetEnemyUI(true);
        enemy.enemyActionMachine.SetEngine(engine, this);
        UpdateStatus();
    }

    //Unloads current enemy
    public void UnLoadEnemy(){
        this.enemy = null;
        anim.runtimeAnimatorController = null;
        SetEnemyUI(false);
    }

    public void RunEnemyMachine(){
        if(enemy != null && !enemy.diceased)
            enemy.enemyActionMachine.RunEnemyMachine();
    }

    public void AttackAnimation(){
        anim.SetTrigger("Attack");
    }

    public void ReduceStatusEffectDurations(){
        if(enemy != null)
            enemy.ReduceStatusEffectDurations();
    }

    public void SetEnemyUI(bool status){
        healthImage.enabled = status;
        healthText.enabled = status;
        attackInfo.enabled = status;
        healthBGImage.enabled = status;
        attackIcon.enabled = status;
        foreach(SpriteRenderer statusImage in statusImages)
            statusImage.enabled = status;
        foreach (TextMeshProUGUI statusText in statusTexts)
            statusText.enabled = status;
        this.GetComponent<BoxCollider2D>().enabled = status;
    }

    public void SetNewPreparedAttack(Card preparedAttack){
        switch(preparedAttack.type){
            case Card.Action_Type.Damage:
                CardDamage cd = (CardDamage) preparedAttack;
                SwitchAttackUI(cd.currentDamage, enemy.icons.attackIcon);
                break;
            case Card.Action_Type.Defense:
                CardDefense cdef = (CardDefense)preparedAttack;
                SwitchAttackUI(cdef.currentAmmount, (cdef.defType == CardDefense.Defense_Type.Healing) ? enemy.icons.healIcon : enemy.icons.shieldIcon);
                break;
            case Card.Action_Type.Status:
                CardStatus cs = (CardStatus) preparedAttack;
                switch(cs.effect){
                    case StatusEffect.Effect.Burn:
                        SwitchAttackUI(cs.currentDamage, enemy.icons.burnIcon);
                        break;
                    case StatusEffect.Effect.Poison:
                        SwitchAttackUI(cs.currentDamage, enemy.icons.poisonIcon);
                        break;
                    case StatusEffect.Effect.Shock:
                        SwitchAttackUI(cs.currentDamage, enemy.icons.shockedIcon);
                        break;
                }
                break;
            case Card.Action_Type.Special:
                break;
        }
    }

    public void SwitchAttackUI(float ammount, Sprite icon){
        attackInfo.text = ammount.ToString();
        attackIcon.sprite = icon;
    }

    // TODO pretty it
    public void UpdateStatusEnemy(){
        if(enemy != null){
            UpdateStatus();
            if (enemy.currentHealth <= 0)
            {
                this.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    public void StatusEffectEndTurn(){
        if (enemy != null && !enemy.diceased)
            enemy.CheckActionForStatus(Character.Character_Action.END_TURN);
    }

    public override Character ReturnAssociatedCharacter()
    {
        return enemy;
    }
}