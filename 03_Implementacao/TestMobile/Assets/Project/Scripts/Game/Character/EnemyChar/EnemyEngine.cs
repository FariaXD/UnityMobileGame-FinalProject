using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class EnemyEngine : CharacterEngine {
    /*
        *Runtime Class
        In the game there can be three enemies at the same time
        This class loads one enemy dynamically received from the combatEngine
        Its responsible for executing every function of the enemy including
        attacks, statusupdates, loading/unloading
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

    //Sets new enemy, loading the objects and updating graphics
    public void SetNewEnemy(Enemy _enemy){
        this.enemy = _enemy;
        anim.runtimeAnimatorController = _enemy.anim;
        SetEnemyUI(true);
        enemy.enemyActionMachine.SetEngine(engine, this);
        UpdateStatus();
    }

    //Unloads current enemy, hiding the graphics
    public void UnLoadEnemy(){
        this.enemy = null;
        anim.runtimeAnimatorController = null;
        SetEnemyUI(false);
    }

    //Runs the enemy action machine
    public void RunEnemyMachine(){
        if(enemy != null && !enemy.diceased)
            enemy.enemyActionMachine.RunEnemyMachine();
    }
    //Set attack animation
    public void AttackAnimation(){
        anim.SetTrigger("Attack");
    }
    //Reduces all status effects duration
    public void ReduceStatusEffectDurations(){
        if(enemy != null)
            enemy.ReduceStatusEffectDurations();
    }

    //Sets the enemy graphics
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

    //Prepares next attack graphics, updating the icon and damage/defense ammount
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

    //Loads the new atack graphics
    public void SwitchAttackUI(float ammount, Sprite icon){
        attackInfo.text = ammount.ToString();
        attackIcon.sprite = icon;
    }

    //Update enemy graphics or disable colliders if diceased
    public void UpdateStatusEnemy(){
        if(enemy != null){
            UpdateStatus();
            if (enemy.currentHealth <= 0)
                this.GetComponent<BoxCollider2D>().enabled = false;
            
        }
    }
    //Check for end of turn status effects and execute them
    public void StatusEffectEndTurn(){
        if (enemy != null && !enemy.diceased)
            enemy.CheckActionForStatus(Character.Character_Action.END_TURN);
    }
    //Returns the associated character
    public override Character ReturnAssociatedCharacter()
    {
        return enemy;
    }
}