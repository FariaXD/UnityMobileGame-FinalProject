using UnityEngine;

public class HeroInventoryEngine : MonoBehaviour {
    private SpriteRenderer heroImageRenderer;
    private InventoryEngine inventoryEngine;
    private Hero hero;

    private void Start() {
        heroImageRenderer = GetComponent<SpriteRenderer>();
        inventoryEngine = GameObject.FindGameObjectWithTag("InventoryEngine").GetComponent<InventoryEngine>();
    }

    public void SetHero(Hero hero){
        this.hero = hero;
        SetHeroImage(hero.heroToken);
    }

    private void SetHeroImage(Sprite heroToken) {
        //heroImageRenderer.sprite = heroToken;
    }

    private void OnMouseDown() {
        inventoryEngine.SelectHero(hero);
    }
}