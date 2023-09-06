using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class EnemyInitializer{

    /*
        Loads enemy from JSON file
        Loads enemy attacks from JSON file
    */
    public static Enemy InitializeEnemyWithName(string _name){

        Dictionary<string, string> val = new Dictionary<string, string>();
        TextAsset mytxtData = (TextAsset)Resources.Load("data/enemies/" + _name +"/info"); //Load from Resources folder
        string txt = mytxtData.text;
        val = JsonConvert.DeserializeObject<Dictionary<string, string>>(txt); //Convert to JSON

        string name = val["name"];
        float hp = float.Parse(val["hp"]);
        float shield = float.Parse(val["shield"]);
        RuntimeAnimatorController anim = (RuntimeAnimatorController)Resources.Load(val["anim"]);

        Enemy newEnemy = new Enemy(name, hp, shield, anim);

        Dictionary<string, Dictionary<string, string>> attacks = new Dictionary<string, Dictionary<string, string>>();
        TextAsset jsonAttack = (TextAsset)Resources.Load("data/enemies/" + _name + "/attacks"); //Load from Resources folder
        string jsonAttacktxt = jsonAttack.text;
        attacks = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonAttacktxt); //Convert to JSON

        float dmg, amm, duration;
        StatusEffect.Effect status;
        CardDefense.Defense_Type defType;
        foreach (var att in attacks)
        {
            bool area = (att.Value["area"] == "true");
            switch(att.Value["type"]){
                case "Damage":
                    dmg = float.Parse(att.Value["damage"]); //damage
                    amm = float.Parse(att.Value["ammount"]); //ammount of hits
                    newEnemy.enemyActionMachine.AddAttack(new CardDamage(area,dmg*amm));
                    break;
                case "Status":
                    status = StatusEffect.GetEffectByName(att.Value["status"]); //Status effect
                    dmg = float.Parse(att.Value["damage"]); //damage
                    duration = float.Parse(att.Value["duration"]); //duration of status
                    newEnemy.enemyActionMachine.AddAttack(new CardStatus(area, status, duration, dmg));
                    break;
                case "Defense":
                    amm = float.Parse(att.Value["ammount"]); //ammount of healing
                    defType = CardDefense.GetDefenseByName(att.Value["defense"]); //Healing or Shielding
                    newEnemy.enemyActionMachine.AddAttack(new CardDefense(area, defType, amm));
                    break;
                case "Special":
                    //TODO boss exclusive, need to plan more
                    break;
            }
        }
        return newEnemy;
    }
}