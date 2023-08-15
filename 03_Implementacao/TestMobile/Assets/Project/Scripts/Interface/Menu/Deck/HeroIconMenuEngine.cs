using UnityEngine;

public class HeroIconMenuEngine : MonoBehaviour {
    private SpriteRenderer heroImageRenderer;
    private DisplayDeckMenuEngine displayDeckMenuEngine;
    private Hero hero;

    private void Start() {
        heroImageRenderer = GetComponent<SpriteRenderer>();
        displayDeckMenuEngine = GameObject.FindGameObjectWithTag("MenuDisplayDeck").GetComponent<DisplayDeckMenuEngine>();
    }

    public void SetHero(Hero hero){
        this.hero = hero;
        SetHeroImage(hero.heroToken);
    }

    private void SetHeroImage(Sprite heroToken) {
        //heroImageRenderer.sprite = heroToken;
    }

    private void OnMouseDown() {
        displayDeckMenuEngine.SelectHero(hero);
    }
}