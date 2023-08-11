using UnityEngine;
using System.Collections.Generic;

public class InventoryEngine : MonoBehaviour {
    private GameEngine gameEngine;
    public Hero selectedHero;
    public DisplayCardInventoryEngine displayCardInventoryEngine;
    public DeckInventoryEngine deckInventoryEngine;
    public List<HeroInventoryEngine> unselectedHeroesEngines;
    List<Hero> heroes = new List<Hero>();

    public void Initialize() {
        gameEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
        SetHeroes(gameEngine.combatEngine.team.GetHeroObjects());
        deckInventoryEngine.Initialize();
        SelectHero(heroes[0]);
    }
    public void SetHeroes(List<Hero> heroes) {
        this.heroes = heroes;
    }

    public void SelectHero(Hero hero){
        selectedHero = hero;
        CardCountPair defCard = deckInventoryEngine.SetCurrentDeck(selectedHero.deck);
        displayCardInventoryEngine.SetCard(hero, defCard);
        int i = 0;
        foreach (Hero h in heroes)
        {
            if(h.name != selectedHero.name){
                unselectedHeroesEngines[i].SetHero(h);
                i++;
            }
        }
    }
}