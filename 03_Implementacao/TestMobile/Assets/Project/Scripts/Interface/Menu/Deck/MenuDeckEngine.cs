using UnityEngine;
using System.Collections.Generic;

public class MenuDeckEngine : MonoBehaviour {
    private GameEngine gameEngine;
    public ClassDeckMenuEngine classDeckMenuEngine;
    public DisplayDeckMenuEngine displayDeckMenuEngine;
    public List<Hero> heroes = new List<Hero>();

    public void Initialize()
    {
        gameEngine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
        displayDeckMenuEngine = GameObject.FindGameObjectWithTag("MenuDisplayDeck").GetComponent<DisplayDeckMenuEngine>();
        SetHeroes(gameEngine.combatEngine.team.GetHeroObjects());
        classDeckMenuEngine.Initialize();
        displayDeckMenuEngine.SelectHero(heroes[0]);
    }

    public void SetHeroes(List<Hero> heroes)
    {
        this.heroes = heroes;
    }
}