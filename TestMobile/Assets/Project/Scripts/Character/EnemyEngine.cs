using UnityEngine;
using UnityEngine.UI;

public class EnemyEngine : MonoBehaviour {
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
        enemy = new Enemy(anim, startingHealth, startingShield);
    }
    public void UpdateStatus(){
        image.fillAmount = ((100*enemy.currentHealth)/startingHealth) / 100;
    }
}