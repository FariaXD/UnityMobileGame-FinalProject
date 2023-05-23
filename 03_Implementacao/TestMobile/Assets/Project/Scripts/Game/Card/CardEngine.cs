using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEngine : MonoBehaviour {

    /*
        *Runtime class
        Player hand cards loaded dynamically
        May be invisible if card not loaded
    */
    public Card card {get; set;} //Current card can be null
    [HideInInspector]
    public bool moving {get; set;} //If player is moving card
    private float startPosX, startPosY; //World positions
    private Vector3 resetPosition; //Starting positions
    private const float speed = 50f; //Speed of reseting position
    [HideInInspector]
    public bool used {get; set;} //If card has been used
    private SpriteRenderer sRenderer {get; set;} //Sprite renderer object
    private CardHitmarker hitmarker; //Hitmarker object
    private void Start() {
        resetPosition = this.transform.localPosition;
        sRenderer = this.GetComponent<SpriteRenderer>();
        hitmarker = GetComponentInChildren<CardHitmarker>();
    }
    //Move Card 
    private void Update() {
        if(moving){
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, resetPosition.z);
        }
        else if(!moving && Vector2.Distance(this.transform.localPosition, resetPosition) > 0){
            this.gameObject.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, resetPosition, speed * Time.deltaTime);
            
        }
        else if(used)
            used = false;
        if(Vector2.Distance(this.transform.localPosition, resetPosition) == 0)
            hitmarker.SetSpriteRendererAndCollider(moving);

    }
    //Change card dynamically
    public void UpdateCard(Card c){
        card = c;
        this.GetComponent<SpriteRenderer>().sprite = card.cardSprite;
        if(card.id == -1)
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        else
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        
    }
    //If card is pressed
    private void OnMouseDown()
    {        
        if(Input.GetMouseButtonDown(0) && card.id != -1){
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            moving = true;

            sRenderer.color = new Color(1f, 1f, 1f, .5f);
            hitmarker.SetSpriteRendererAndCollider(moving);
        }
    }
    //Is player releases card
    private void OnMouseUp() 
    {
        moving = false;
        sRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}
