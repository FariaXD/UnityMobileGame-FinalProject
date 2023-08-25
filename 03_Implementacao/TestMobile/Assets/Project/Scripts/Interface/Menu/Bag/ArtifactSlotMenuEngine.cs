using UnityEngine;

public class ArtifactSlotMenuEngine : MonoBehaviour {
    /*
        RunTimeClass responsible for loading a slot in the artifact menu
    */
    public SpriteRenderer slotImg, frame; //sprite renderers
    private BoxCollider2D col; //collider for selecting the artifact
    public ArtifactCountPair artifact; //Artifact and the ammount of them in the inventory
    public InventoryMenuEngine eng;
    private void Start() {
        eng = GameObject.FindGameObjectWithTag("MenuBagInventoryEngine").GetComponent<InventoryMenuEngine>();
        col = GetComponent<BoxCollider2D>();
        frame = GetComponent<SpriteRenderer>();
    }

    //Set new artifact 
    public void SetArtifact(ArtifactCountPair artifact){
        SetVisibility(true);
        this.artifact = artifact;
        slotImg.sprite = artifact?.Artifact.GetSpriteViaString();
    }
    //Clear slot
    public void ClearSlot(){
        SetVisibility(false);
    }
    //Set Visibility of slot
    private void SetVisibility(bool state)
    {
        slotImg.enabled = state;
        frame.enabled = state;
        col.enabled = state;
    }
    //Select artifact to be displayed
    private void OnMouseDown() {
        eng.SelectArtifact(artifact);
    }
}