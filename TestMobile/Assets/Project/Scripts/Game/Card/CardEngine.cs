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
        sRenderer = this.GetComponent<SpriteRenderer>();
        hitmarker = GetComponentInChildren<CardHitmarker>();
    }
    private void Update() {
        if(moving){
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);
        }
        else if(!moving && Vector2.Distance(this.transform.localPosition, resetPosition) > 0){
            this.gameObject.transform.localPosition = Vector2.MoveTowards(this.transform.localPosition, resetPosition, speed * Time.deltaTime);
            
        }
        else if(used)
            used = false;
        if(Vector2.Distance(this.transform.localPosition, resetPosition) == 0)
            hitmarker.SetSpriteRendererAndCollider(moving);

    }
    public void UpdateCard(Card c){
        card = c;
        this.GetComponent<SpriteRenderer>().sprite = card.cardSprite;
        
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
