using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public abstract class CharacterEngine : MonoBehaviour {
    /*
        ?Interface
        Containing common methods to be implemented
    */
    public string heroName = "Color"; //Hero names
    public Animator anim; //Animation Controller obj
    public CombatEngine engine; //GameEngine reference
    public Image healthImage; //Associated health Image obj
    public TextMeshProUGUI healthText; //Health text obj
    public Image shieldIcon;
    public TextMeshProUGUI shieldText;
    public List<SpriteRenderer> statusImages = new List<SpriteRenderer>(); //List of status imagerenderer obj
    public List<TextMeshProUGUI> statusTexts = new List<TextMeshProUGUI>(); //list of status text obj
    public SpriteRenderer targetedIcon;
    public float startingHealth = 20f; //The hero starting hp
    private void Start()
    {
        anim = GetComponent<Animator>();
        engine = GameObject.FindGameObjectWithTag("CombatEngine").GetComponent<CombatEngine>();
        shieldIcon.enabled = false;
        shieldText.enabled = false;
    }
    public void UpdateStatus(){
        Character character = ReturnAssociatedCharacter();
        healthImage.fillAmount = ((100 * character.currentHealth) / character.maxHealth) / 100;
        healthText.text = character.currentHealth + "/" + character.maxHealth;
        if (character.currentHealth <= 0)
        {
            SetDead(true);
            character.diceased = true;
        }
        for (int i = 0; i < statusImages.Count; i++)
        {
            if (character.debuffs.Count - 1 >= i && character.debuffs[i] != null)
            {
                statusImages[i].sprite = character.icons.GetStatusIcon(character.debuffs[i]);
                statusTexts[i].text = character.debuffs[i].duration.ToString();
            }
            else
            {
                statusImages[i].sprite = null;
                statusTexts[i].text = null;
            }
        }
        if (character.shield > 0)
        {
            shieldIcon.enabled = true;
            shieldText.enabled = true;
        }
        else
        {
            shieldIcon.enabled = false;
            shieldText.enabled = false;
        }
        shieldText.text = Mathf.Round(character.shield).ToString();
    }

    public void SetDead(bool dead)
    {
        anim.SetBool("Dead", dead);
    }

    public abstract Character ReturnAssociatedCharacter();
}