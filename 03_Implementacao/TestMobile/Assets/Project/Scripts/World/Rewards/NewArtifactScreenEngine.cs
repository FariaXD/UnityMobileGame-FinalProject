using UnityEngine;
using TMPro;
public class NewArtifactScreenEngine : MonoBehaviour {
    public TextMeshProUGUI artifactTitle;
    public TextMeshProUGUI artifactDescription;

    public void SetText(Artifact artifact){
        artifactTitle.text = artifact.name;
        artifactDescription.text = artifact.description;
        artifactTitle.color = artifact.GetColorViaRarity(artifact.rarity);
        Debug.Log(artifact.rarity);
    }
    
}