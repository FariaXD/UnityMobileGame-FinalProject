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
        card = cardAndCount.Card;
        manaCostField.text = card.manaCost.ToString();
        cardDamageField.text = card.GetAmmountDynamically().ToString();
        cardDescField.text = card.GetCardDescriptionDynamically();
        heroNameField.text = hero.name;
        descriptionField.text = card.GetCardDescriptionDynamically();
        countInDeckField.text = cardAndCount.Count.ToString() + " in Deck";
        typeField.text = card.type.ToString();
        cardType.sprite = Card.LoadCardTypeDynamically(card);
        cardCharacter.sprite = hero.cardTemplate;
        cardImage.sprite = card.cardImage;
        heroIcon.sprite = hero.heroToken;
    }
}