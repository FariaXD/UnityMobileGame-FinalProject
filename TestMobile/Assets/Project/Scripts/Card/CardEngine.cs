using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEngine : MonoBehaviour {

    public Card card {get; set;}
    public bool moving {get; set;}
    private float startPosX, startPosY;
    private Vector3 resetPosition;
    private const float speed = 50f;
    private bool used {get; set;}
    private void Start() {
        resetPosition = this.transform.localPosition;
    }
    private void Update() {
        if(moving){
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);
        }
        if(!moving && Vector2.Distance(this.transform.localPosition, resetPosition) > 0)
            this.gameObject.transform.localPosition = Vector2.MoveTowards(this.transform.localPosition, resetPosition, speed * Time.deltaTime);
        else if(used)
            used = false;

    }
    public void UpdateCard(Card c){
        card = c;
        this.GetComponent<SpriteRenderer>().sprite = card.cardSprite;
        
    }
    private void OnMouseDown()
    {
        Debug.Log("HELLO?");
        
        if(Input.GetMouseButtonDown(0)){
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            moving = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(!used && !moving){
            if (other.gameObject.TryGetComponent<EnemyEngine>(out EnemyEngine enemy))
                GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>().UseCard(this, enemy);
            else if (other.gameObject.TryGetComponent<HeroEngine>(out HeroEngine hero))
                GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>().UseCard(this, hero);
            used = true;
        }   
    }

    private void OnMouseUp() 
    {
        moving = false;
    }
}
