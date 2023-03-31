using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public List<EnemyEngine> enemies = new List<EnemyEngine>();
    public Team team = new Team();


    private void Start() {
        team.SetHeroes(GameObject.FindGameObjectsWithTag("Player"));
        List<Card> deck = DeckInitializer.InitializeDeck("blue");
        Debug.Log(deck.Count);
    }
}