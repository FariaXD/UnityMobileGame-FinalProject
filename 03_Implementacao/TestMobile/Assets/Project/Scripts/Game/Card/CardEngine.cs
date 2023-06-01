using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
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
    public SpriteRenderer cardTemplate, cardTypeIcon, cardImage; //Sprite renderer object
    private CardHitmarker hitmarker; //Hitmarker object
    public TextMeshProUGUI cardMana, cardText, cardAmmount;
    public GameEngine engine;

    private void Start() {
        engine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameEngine>();
        resetPosition = this.transform.localPosition;
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
        Debug.Log(engine.team.selectedHero.hero.cardTemplate);
        cardTemplate.sprite = engine.team.selectedHero.hero.cardTemplate;
        cardTypeIcon.sprite = Card.LoadCardTypeDynamically(card);
        cardImage.sprite = card.cardImage;
        if(card.id == -1)
            SetCardVisible(false);
        else
        {
            SetCardVisible(true);
            cardMana.text = card.manaCost.ToString();
            cardText.text = Card.GetCardDescriptionDynamically(card);
            cardAmmount.text = Card.GetAmmountDynamically(card).ToString();
        }
        
    }
    public void SetCardVisible(bool status){
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = status;
        cardMana.enabled = status;
        cardText.enabled = status;
        cardAmmount.enabled = status;
        cardTemplate.enabled = status;
        cardTypeIcon.enabled = status;
        cardImage.enabled = status;
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

            cardTemplate.color = new Color(1f, 1f, 1f, .5f);
            cardImage.color = new Color(1f, 1f, 1f, .5f);
            hitmarker.SetSpriteRendererAndCollider(moving);
        }
    }
    //Is player releases card
    private void OnMouseUp() 
    {
        moving = false;
        cardTemplate.color = new Color(1f, 1f, 1f, 1f);
        cardImage.color = new Color(1f, 1f, 1f, 1f);
    }
}
