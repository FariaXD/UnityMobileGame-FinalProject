using UnityEngine;
using UnityEngine.UI;

public class EnemyEngine : MonoBehaviour, CharacterEngine {
    public string enemyName = "Enemy";
    public Enemy enemy;
    public float startingHealth = 20f;
    public float startingShield = 5f;
    private Animator anim;
    private GameEngine engine;
    public Image image;

    private void Start()
    {
        anim = GetComponent<Animator>();
        engine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>();
        enemy = new Enemy(enemyName, anim, startingHealth, startingShield);
    }
    public bool UpdateStatus(){
        image.fillAmount = ((100*enemy.currentHealth)/startingHealth) / 100;
        return (enemy.currentHealth <= 0);
    }

    public Character ReturnAssociatedCharacter()
    {
        return enemy;
    }
}