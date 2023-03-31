using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DeckInitializer {
    public Dictionary<string, Dictionary<string, string>> values;
    public DeckInitializer(){
        string json = File.ReadAllText(Application.dataPath +"/Project/Scripts/Data/red.json");
        values = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
    }
}