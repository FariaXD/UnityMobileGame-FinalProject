using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
public class ArtifactInitializer
{   
    public static List<Artifact> GetArtifactsByRarity(string worldName, string rarity)
    {
        Dictionary<string, Dictionary<string, Artifact>> artifactData = new Dictionary<string, Dictionary<string, Artifact>>();
        TextAsset mytxtData = Resources.Load<TextAsset>("data/worlds/" + worldName + "/artifacts");
        string txt = mytxtData.text;
        artifactData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Artifact>>>(txt);
        List<Artifact> artifacts = new List<Artifact>();

        if (artifactData.ContainsKey(rarity))
        {
            Dictionary<string, Artifact> rarityData = artifactData[rarity];
            artifacts.AddRange(rarityData.Values);
        }
        else
        {
            Debug.LogWarning("Rarity not found: " + rarity);
        }

        return artifacts;
    }
}