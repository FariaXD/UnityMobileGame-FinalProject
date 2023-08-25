using UnityEngine;

public class HeroIconMenuEngine : MonoBehaviour {
    /*
    Runtime class
    Selects a hero via touch
    */
    private SpriteRenderer heroImageRenderer;
    private DisplayDeckMenuEngine displayDeckMenuEngine;
    private Hero hero;

    private void Start() {
        heroImageRenderer = GetComponent<SpriteRenderer>();
        displayDeckMenuEngine = GameObject.FindGameObjectWithTag("MenuDisplayDeck").GetComponent<DisplayDeckMenuEngine>();
    }
    //Associate hero to object
    public void SetHero(Hero hero){
        this.hero = hero;
        SetHeroImage(hero.heroToken);
    }
    //Set hero image
    private void SetHeroImage(Sprite heroToken) {
        //heroImageRenderer.sprite = heroToken;
    }
    //Select new hero
    private void OnMouseDown() {
        displayDeckMenuEngine.SelectHero(hero);
    }
}