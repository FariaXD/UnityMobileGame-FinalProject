using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
public class HeroInitializer {

    //Initializes an Hero from the corresponding JSON file
    public static List<Hero> InitializeHeroes()
    {
        Dictionary<string, string> team = new Dictionary<string, string>();
        TextAsset mytxtData = (TextAsset)Resources.Load("data/heroes/team"); //Load from Resources folder
        string txt = mytxtData.text;
        team = JsonConvert.DeserializeObject<Dictionary<string, string>>(txt); //Convert to JSON

        List<string> heroNames = new List<string>
        {
            team["hero1"],
            team["hero2"],
            team["hero3"]
        };
        List<Hero> heroList = new List<Hero>();
        foreach(string heroName in heroNames){
            TextAsset myheroData = (TextAsset)Resources.Load("data/heroes/classes/"+heroName+"/"+heroName); //Load from Resources folder
            string heroJson = myheroData.text;
            Dictionary<string, string>  heroInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(heroJson); //Convert to JSON
            RuntimeAnimatorController anim = (RuntimeAnimatorController)Resources.Load(heroInfo["anim"]);
            heroList.Add(new Hero(heroName, float.Parse(heroInfo["maxHealth"]), float.Parse(heroInfo["startingShield"]), heroInfo["cardTemplate"], anim));
        }
        return heroList;
    }
}