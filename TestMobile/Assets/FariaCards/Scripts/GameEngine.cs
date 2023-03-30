using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public PlayerEngine player;
    public List<EnemyEngine> enemies = new List<EnemyEngine>();


    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEngine>();
        GameObject[] enemiesGO = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemiesGO)
            enemies.Add(enemy.GetComponent<EnemyEngine>());
    }
}