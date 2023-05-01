using UnityEngine;
using UnityEngine.UI;

public class EnemyEngine : MonoBehaviour, CharacterEngine {
    public string enemyName = "Enemy";
    public Enemy enemy;
    public float startingHealth = 20f;
    public float startingShield = 5f;
    public Animator anim;
    public GameEngine engine;

    private void Update() {
        if(enemy != null && enemy.currentHealth <= 0){
            anim.SetBool("Dead", true);
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    public void SetNewEnemy(Enemy _enemy){
        this.enemy = _enemy;
        anim.runtimeAnimatorController = _enemy.anim;
        this.GetComponent<BoxCollider2D>().enabled = true;
        enemy.enemyAI.SetEngine(engine);
        //Destroy(GetComponent<PolygonCollider2D>());
        //gameObject.AddComponent<PolygonCollider2D>();
    }
    public bool UpdateStatus(){
        /* image.fillAmount = ((100*enemy.currentHealth)/startingHealth) / 100;
        return (enemy.currentHealth <= 0); */
        return false;
    }

    public Character ReturnAssociatedCharacter()
    {
        return enemy;
    }
}