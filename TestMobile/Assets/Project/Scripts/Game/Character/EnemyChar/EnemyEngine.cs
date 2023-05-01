using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyEngine : MonoBehaviour, CharacterEngine {
    public string enemyName = "Enemy";
    public Enemy enemy;
    public float startingHealth = 20f;
    public float startingShield = 5f;
    public Animator anim;
    public GameEngine engine;
    public Image healthImage;
    public TextMeshProUGUI healthText;

    private void Update() {
        UpdateStatus();
    }
    public void SetNewEnemy(Enemy _enemy){
        this.enemy = _enemy;
        anim.runtimeAnimatorController = _enemy.anim;
        this.GetComponent<BoxCollider2D>().enabled = true;
        enemy.enemyAI.SetEngine(engine);
        UpdateStatus();
        //Destroy(GetComponent<PolygonCollider2D>());
        //gameObject.AddComponent<PolygonCollider2D>();
    }
    public void UpdateStatus(){
        healthImage.fillAmount = ((100 * enemy.currentHealth) / startingHealth) / 100;
        healthText.text = enemy.currentHealth + "/" + enemy.maxHealth;
        if (enemy != null && enemy.currentHealth <= 0)
        {
            anim.SetBool("Dead", true);
            this.GetComponent<BoxCollider2D>().enabled = false;
            enemy.diceased = true;
        }
    }

    public Character ReturnAssociatedCharacter()
    {
        return enemy;
    }
}