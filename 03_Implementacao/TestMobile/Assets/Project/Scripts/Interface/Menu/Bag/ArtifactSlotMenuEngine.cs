using UnityEngine;

public class ArtifactSlotMenuEngine : MonoBehaviour {

    public SpriteRenderer slotImg, frame;
    private BoxCollider2D col;
    public ArtifactCountPair artifact;
    public InventoryMenuEngine eng;
    private void Start() {
        eng = GameObject.FindGameObjectWithTag("MenuBagInventoryEngine").GetComponent<InventoryMenuEngine>();
        col = GetComponent<BoxCollider2D>();
        frame = GetComponent<SpriteRenderer>();
    }

    public void SetArtifact(ArtifactCountPair artifact){
        SetVisibility(true);
        this.artifact = artifact;
        slotImg.sprite = artifact?.Artifact.GetSpriteViaString();
    }

    public void ClearSlot(){
        SetVisibility(false);
    }

    private void SetVisibility(bool state)
    {
        slotImg.enabled = state;
        frame.enabled = state;
        col.enabled = state;
    }

    private void OnMouseDown() {
        eng.SelectArtifact(artifact);
    }
}