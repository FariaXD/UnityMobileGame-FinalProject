using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEngine : MonoBehaviour {

    public Card card {get; set;}
    [HideInInspector]
    public bool moving {get; set;}
    private float startPosX, startPosY;
    private Vector3 resetPosition;
    private const float speed = 50f;
    [HideInInspector]
    public bool used {get; set;}
    private SpriteRenderer sRenderer {get; set;}
    private CardHitmarker hitmarker;
    private void Start() {
        resetPosition = this.transform.localPosition;
        Debug.Log(resetPosition);
        sRenderer = this.GetComponent<SpriteRenderer>();
        hitmarker = GetComponentInChildren<CardHitmarker>();
    }
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
    public void UpdateCard(Card c){
        card = c;
        this.GetComponent<SpriteRenderer>().sprite = card.cardSprite;
        if(card.id == -1)
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        else
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        
    }
    private void OnMouseDown()
    {        
        Debug.Log(card);
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

    private void OnMouseUp() 
    {
        moving = false;
        sRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}
