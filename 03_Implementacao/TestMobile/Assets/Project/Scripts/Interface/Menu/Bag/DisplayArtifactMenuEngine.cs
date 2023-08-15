using UnityEngine;
using TMPro;
public class DisplayArtifactMenuEngine : MonoBehaviour {
    public ArtifactCountPair artifact;
    public TextMeshProUGUI nameArt, desc, count;
    public SpriteRenderer artImg;

    public void SetArtifact(ArtifactCountPair artifact) {
        if(artifact != null){
            nameArt.text = artifact.Artifact.name;
            desc.text = artifact.Artifact.description;
            count.text = artifact.Count.ToString();
            artImg.sprite = artifact.Artifact.GetSpriteViaString();
            nameArt.color = Artifact.GetColorViaRarity(artifact.Rarity);
        }
        else{
            nameArt.text = "No Artifacts";
            desc.text = "The party does not possess any artifacts.";
            count.text = "";
            artImg.sprite = Resources.Load<Sprite>("sprites/GameUI/X");
            nameArt.color = new Color(177/255f,47/255f,47/255f);
        }
    }
}