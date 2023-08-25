using UnityEngine;
using TMPro;

public class DisplayCardMenuEngine : MonoBehaviour {
    /*
    Runtime class responsible for updating the detailed card interface
    */
    public Card card;
    public TextMeshProUGUI manaCostField, cardDamageField, cardDescField, heroNameField, descriptionField, countInDeckField, typeField;
    public SpriteRenderer cardType, cardCharacter, cardImage, heroIcon;

    public void SetCard(Hero hero, CardCountPair cardAndCount) {
        this.card = cardAndCount.Card;
        manaCostField.text = card.manaCost.ToString();
        cardDamageField.text = Card.GetAmmountDynamically(card).ToString();
        cardDescField.text = Card.GetCardDescriptionDynamically(card);
        heroNameField.text = hero.name;
        descriptionField.text = Card.GetCardDescriptionDynamically(card);
        countInDeckField.text = cardAndCount.Count.ToString() + " in Deck";
        typeField.text = card.type.ToString();
        cardType.sprite = card.cardTypeIcon;
        cardCharacter.sprite = hero.cardTemplate;
        cardImage.sprite = card.cardImage;
        heroIcon.sprite = hero.heroToken;
    }
}