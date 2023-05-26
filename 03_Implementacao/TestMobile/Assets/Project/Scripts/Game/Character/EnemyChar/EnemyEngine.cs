using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class EnemyEngine : MonoBehaviour, CharacterEngine {
    /*
        *Runtime Class
        In the game there can be three enemies at the same time
        This class loads the enemies dynamically based on the name received from the game engine
    */

    public string enemyName = "Enemy"; //Enemy name
    public Enemy enemy; //Associated enemy
    public Animator anim; //Associated Animation Controller
    public GameEngine engine; //GameEngine reference
    public Image healthImage; //Health Image obj
    public Image healthBGImage; //Health bg Image obj
    public Image shieldIcon;
    public TextMeshProUGUI shieldText;
    public TextMeshProUGUI healthText; //Health Text obj
    public TextMeshProUGUI attackInfo; //Attack Text obj
    public SpriteRenderer attackIcon, targetedIcon; //Atack Image obj
    public List<SpriteRenderer> statusImages = new List<SpriteRenderer>(); //List of status images obj
    public List<TextMeshProUGUI> statusTexts = new List<TextMeshProUGUI>(); //List of status text obj

    void Awake(){
        SetEnemyUI(false);
        shieldIcon.enabled = false;
        shieldText.enabled = false;
    }

    void Update() {
        UpdateStatus(); //Updates status in each frame
    }

    //Sets new enemy
    public void SetNewEnemy(Enemy _enemy){
        this.enemy = _enemy;
        anim.runtimeAnimatorController = _enemy.anim;
        SetEnemyUI(true);
        enemy.enemyAI.SetEngine(engine, this);
        UpdateStatus();
    }

    //Unloads current enemy
    public void UnLoadEnemy(){
        this.enemy = null;
        anim.runtimeAnimatorController = null;
        SetEnemyUI(false);
    }

    public void RunEnemyAI(){
        if(enemy != null && !enemy.diceased)
            enemy.enemyAI.RunEnemyAI();
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

    public void UpdateStatus(){
        if(enemy != null){
            healthImage.fillAmount = ((100 * enemy.currentHealth) / enemy.maxHealth) / 100;
            healthText.text = enemy.currentHealth + "/" + enemy.maxHealth;
            if (enemy != null && enemy.currentHealth <= 0)
            {
                anim.SetBool("Dead", true);
                this.GetComponent<BoxCollider2D>().enabled = false;
                enemy.diceased = true;
            }

            for (int i = 0; i < statusImages.Count; i++)
            {
                if (enemy.debuffs.Count - 1 >= i && enemy.debuffs[i] != null)
                {
                    statusImages[i].sprite = enemy.icons.GetStatusIcon(enemy.debuffs[i]);
                    statusTexts[i].text = enemy.debuffs[i].duration.ToString();
                }
                else
                {
                    statusImages[i].sprite = null;
                    statusTexts[i].text = null;
                }
            }
            if (enemy.shield > 0)
            {
                shieldIcon.enabled = true;
                shieldText.enabled = true;
            }
            else
            {
                shieldIcon.enabled = false;
                shieldText.enabled = false;
            }
            shieldText.text = Mathf.Round(enemy.shield).ToString();
        }
    }

    public void StatusEffectEndTurn(){
        if (enemy != null && !enemy.diceased)
            enemy.CheckActionForStatus(Character.Character_Action.END_TURN);
    }

    public Character ReturnAssociatedCharacter()
    {
        return enemy;
    }
}