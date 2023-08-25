using UnityEngine;
using System.Collections.Generic;

public class DisplayDeckMenuEngine : MonoBehaviour {
    /*
    Runtime class responsible for selecting a hero 
    amd updating the current selected deck and
    */
    public Hero selectedHero;
    public DisplayCardMenuEngine displayCardMenuEngine;
    public List<HeroIconMenuEngine> unselectedHeroesEngines;
    public MenuDeckEngine menuDeckEngine;

    private void Start() {
        menuDeckEngine = GameObject.FindGameObjectWithTag("MenuDeckEngine").GetComponent<MenuDeckEngine>();
        displayCardMenuEngine = GameObject.FindGameObjectWithTag("MenuDisplayCard").GetComponent<DisplayCardMenuEngine>();
    }
    //Selects the hero received, updating the interface 
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