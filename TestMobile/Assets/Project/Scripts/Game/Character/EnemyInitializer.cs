using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class EnemyInitializer{

    //TODO GET ATTACK LIST, attacks not implemented yet
    public static Enemy InitializeEnemyWithName(string _name){
        Dictionary<string, string> val = new Dictionary<string, string>();
        List<Card> deck = new List<Card>();
        TextAsset mytxtData = (TextAsset)Resources.Load("data/enemies/" + _name); //Load from Resources folder
        string txt = mytxtData.text;
        val = JsonConvert.DeserializeObject<Dictionary<string, string>>(txt); //Convert to JSON
        string name = val["name"];
        float hp = float.Parse(val["hp"]);
        float shield = float.Parse(val["shield"]);
        RuntimeAnimatorController anim = (RuntimeAnimatorController)Resources.Load(val["anim"]);
        return new Enemy(name, hp,shield,anim);
    }

}