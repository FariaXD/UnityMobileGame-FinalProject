using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public List<EnemyEngine> enemies = new List<EnemyEngine>();
    public Team team = new Team();


    private void Start() {
        team.SetHeroes(GameObject.FindGameObjectsWithTag("Player"));
        DeckInitializer test = new DeckInitializer();
        Debug.Log(test.values);
    }
}