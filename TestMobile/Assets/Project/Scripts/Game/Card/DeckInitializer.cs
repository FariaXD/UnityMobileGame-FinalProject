using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DeckInitializer { 

    public static List<Card> InitializeDeck(string heroClass){
        Dictionary<string, Dictionary<string, string>> values = new Dictionary<string, Dictionary<string, string>>();
        List<Card> deck = new List<Card>();
        TextAsset mytxtData = (TextAsset)Resources.Load("data/heroes/"+heroClass); //Load from Resources folder
        string txt = mytxtData.text; 
        values = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(txt); //Convert to JSON
        foreach (var val in values)
        {
            // stats common for all cards
            int id = int.Parse(val.Key);
            string name = val.Value["name"];
            int cost = int.Parse(val.Value["cost"]);
            int duplicates = int.Parse(val.Value["duplicates"]);
            bool area = (val.Value["area"] == "true");
            string imagePath = val.Value["imagePath"];
            switch (val.Value["type"])
            {
                case "CardDamage":
                    int damageC = int.Parse(val.Value["damage"]);
                    for(int i = 0; i < duplicates; i++)
                        deck.Add(new CardDamage(id, name, cost, area, damageC, imagePath));
                    break;

                case "CardStatus":
                    int damage = int.Parse(val.Value["damage"]);
                    int duration = int.Parse(val.Value["duration"]);
                    StatusEffect.Effect status = StatusEffect.GetEffectByName(val.Value["status"]);
                    for (int i = 0; i < duplicates; i++)
                        deck.Add(new CardStatus(id, name, cost, area, status, duration, damage, imagePath));
                    break;

                case "CardDefense":
                    int ammount = int.Parse(val.Value["ammount"]);
                    CardDefense.Defense_Type defType = CardDefense.GetDefenseByName(val.Value["defenseType"]);
                    for (int i = 0; i < duplicates; i++)
                        deck.Add(new CardDefense(id, name, cost, area, defType, ammount, imagePath));
                    break;

                case "CardSpecial":
                    int damageS = int.Parse(val.Value["damage"]);
                    int durationS = int.Parse(val.Value["duration"]);
                    StatusEffect.Effect statusS = StatusEffect.GetEffectByName(val.Value["status"]);
                    int ammountS = int.Parse(val.Value["ammount"]);
                    CardDefense.Defense_Type defTypeS = CardDefense.GetDefenseByName(val.Value["defenseType"]);
                    CardDamage cdam = new CardDamage(id, name, cost, area, damageS, imagePath);
                    CardStatus csta = new CardStatus(id, name, cost, area, statusS, durationS, damageS, imagePath);
                    CardDefense cdef = new CardDefense(id, name, cost, area, defTypeS, ammountS, imagePath);
                    deck.Add(new CardSpecial(id, name, cost, area, cdam, csta,cdef, imagePath));
                    break;
                default:
                    break;
            }
        }
        return deck;
    }
}