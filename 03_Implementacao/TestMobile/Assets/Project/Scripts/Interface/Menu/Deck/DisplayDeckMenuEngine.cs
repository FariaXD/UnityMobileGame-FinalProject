using UnityEngine;
using System.Collections.Generic;

public class DisplayDeckMenuEngine : MonoBehaviour {
    public Hero selectedHero;
    public DisplayCardMenuEngine displayCardMenuEngine;
    public List<HeroIconMenuEngine> unselectedHeroesEngines;
    public MenuDeckEngine menuDeckEngine;

    private void Start() {
        menuDeckEngine = GameObject.FindGameObjectWithTag("MenuDeckEngine").GetComponent<MenuDeckEngine>();
        displayCardMenuEngine = GameObject.FindGameObjectWithTag("MenuDisplayCard").GetComponent<DisplayCardMenuEngine>();

    }

    public void SelectHero(Hero hero){
        selectedHero = hero;
        CardCountPair defCard = menuDeckEngine.classDeckMenuEngine.SetCurrentDeck(selectedHero.deck);
        displayCardMenuEngine.SetCard(hero, defCard);
        int i = 0;
        foreach (Hero h in menuDeckEngine.heroes)
        {
            if(h.name != selectedHero.name){
                unselectedHeroesEngines[i].SetHero(h);
                i++;
            }
        }
    }
}