using UnityEngine;
using UnityEngine.UI;

public class EnemyEngine : MonoBehaviour {
    public Enemy enemy;
    public float startingHealth = 20f; //tochange
    public float startingMana = 4f; //tochange
    private Animator anim;
    private GameEngine engine;
    public Image image;

    private void Start()
    {
        anim = GetComponent<Animator>();
        engine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>();
        enemy = new Enemy(anim, startingHealth, startingMana);
    }
    public void UpdateStatus(){
        image.fillAmount = ((100*enemy.health)/startingHealth) / 100;
    }
}