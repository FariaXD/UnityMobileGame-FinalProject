using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIcon : MonoBehaviour
{
    /*
        Class that changes the current selected character icon
    */
    private CombatEngine engine;
    private SpriteRenderer icon;
    private Sprite current;
    void Start()
    {
        engine = GameObject.FindGameObjectWithTag("CombatEngine").GetComponent<CombatEngine>();
        icon = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(current == null || current != engine.team.selectedHero.hero.heroToken){
            current = engine.team.selectedHero.hero.heroToken;
            icon.sprite = current;
        }
    }
}
