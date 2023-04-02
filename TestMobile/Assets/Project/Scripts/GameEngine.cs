using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public List<EnemyEngine> enemies = new List<EnemyEngine>();
    public Team team = new Team();
    public Turn active = Turn.PLAYER;
    public PlayerTurnState state = PlayerTurnState.WAITING;
    public HandEngine handEngine;
    public DeckEngine deckEngine;

    public enum Turn{
        PLAYER,
        ENEMY
    }

    public enum PlayerTurnState{
        WAITING,
        VIEW_HAND,
        USING_CARD,
        CHOOSE_TARGET,
        USE_CARD,
        END_TURN
    }

    private void Start() {
        team.SetHeroes(GameObject.FindGameObjectsWithTag("Player"));
        handEngine = GameObject.FindGameObjectWithTag("Hand").GetComponent<HandEngine>();
        foreach(HeroEngine heroGO in team.teamGO){
            StartCoroutine(heroGO.InitializeDeck());
            Debug.Log(heroGO.hero.deck.deck.Count);
        }
        
        SwitchActiveCharacter(team.selectedHero);
    }

    public void SwitchActiveCharacter(HeroEngine hero){
        if(state == PlayerTurnState.WAITING){
            team.selectedHero = hero;
            handEngine.SwitchHand(team.selectedHero.hero.hand.hand);
        }
    }

    public void UseCard(Card _card)
    {
         switch (_card.type)
         {
             case Card.Card_Type.Damage:
                 CardDamage usingCard = (CardDamage) _card;
                 usingCard.UseCardOnTarget((Character) enemies[0].enemy);
                 enemies[0].UpdateStatus();
                 break;
             case Card.Card_Type.Status:
                 break;
             case Card.Card_Type.Defense:
                 break;
             case Card.Card_Type.Special:
                 break;
         }
    }
}