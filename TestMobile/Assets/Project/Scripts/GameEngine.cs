using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public List<EnemyEngine> enemies = new List<EnemyEngine>();
    public Team team = new Team();
    public Turn active = Turn.PLAYER;
    public HandEngine handEngine;
    public DeckEngine deckEngine;
    public DrawCardButton debugbutton;

    public enum Turn{
        PLAYER,
        ENEMY
    }

    private void Start() {
        team.SetHeroes(GameObject.FindGameObjectsWithTag("Player"));
        handEngine = GameObject.FindGameObjectWithTag("Hand").GetComponent<HandEngine>();
        foreach(HeroEngine heroGO in team.teamGO)
            heroGO.InitializeDeck();
        SwitchActiveCharacter(team.selectedHero);
        FindEnemieEngines();
        

        // Hardcoded enemies to test
        Enemy[] tmps = new Enemy[3];
        for(int i = 0; i < tmps.GetLength(0); i++)
            tmps[i] = EnemyInitializer.InitializeEnemyWithName("plantdog");
        InitializeEnemies(tmps);
    }

    private void InitializeEnemies(Enemy[] _enemies){
        for(int i = 0; i < _enemies.GetLength(0); i++)
            enemies[i].SetNewEnemy(_enemies[i]);
    }

    private void FindEnemieEngines(){
        GameObject[] enemiesEngines = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemiesEngines)
            enemies.Add(enemy.GetComponent<EnemyEngine>());
    }

    public void SwitchActiveCharacter(HeroEngine hero){
        if(!hero.hero.diceased){
            team.selectedHero = hero;
            handEngine.SwitchHand(team.selectedHero.hero.hand.hand); }
    }

    public void ForceSwitcHero(){
        foreach(HeroEngine en in team.teamGO){
            if(!en.hero.diceased)
                SwitchActiveCharacter(en);
        }
    }

    public bool HasPlayerLost(){
        foreach (HeroEngine en in team.teamGO)
        {
            if (!en.hero.diceased)
                return false;
        }
        Debug.Log("PLAYER LOST");
        return true;
    }

    public void EndTurn(){
        if(active == Turn.PLAYER){
            active = Turn.ENEMY;
            EnemyTurn();
        }
        else
        {
            active = Turn.PLAYER;
            team.RefreshMana();
            DrawCard();
        }
    }

    private void EnemyTurn(){
        foreach(EnemyEngine en in enemies)
            en.enemy.enemyAI.RunEnemyAI();
        EndTurn();
    }

    public void UseCard(CardEngine _cardEngine, CharacterEngine target = default(CharacterEngine))
    {  
        Card _card = _cardEngine.card;
        if(!HasPlayerLost() && enemies.Count > 0 && active == Turn.PLAYER && (!_card.area || !target.ReturnAssociatedCharacter().diceased) && team.currentMana >= _card.manaCost){
            switch (_card.type)
            {  
                default:
                    if (!_card.area)
                        UseSingle(_card, target);
                    else
                        UseAOE(_card, _card.type == Card.Action_Type.Defense);
                    break;
                case Card.Action_Type.Special:
                    UseSpecial(_card, target);
                    break;
            }
            team.currentMana -= _card.manaCost;
            handEngine.UpdateUsedCard(_cardEngine);
            //SwitchActiveCharacter(team.selectedHero); //Refresh hand
        }  
    }

    public void DrawCard(){
        team.DrawCardForEachHero();
        SwitchActiveCharacter(team.selectedHero);
    }

    /*
        Use an AREA OF EFFECT card on all targets
        isDefensive - if card is a defensive card then works only on friendly chars
    */
    private void UseAOE(Card _card, bool isDefensive){
        if(!isDefensive)
            foreach (EnemyEngine enemy in enemies)
                team.selectedHero.hero.hand.UseCard(_card, enemy.enemy);
        else
            foreach(HeroEngine hero in team.teamGO)
                team.selectedHero.hero.hand.UseCard(_card, hero.hero);
        UpdateEnemyStatus();
    }

    /*
        Use a card on single target, and check if target died (if enemy remove it from list of enemies)
    */
    private void UseSingle(Card _card, CharacterEngine target){
        team.selectedHero.hero.hand.UseCard(_card, target.ReturnAssociatedCharacter());
        target.UpdateStatus();
        UpdateEnemyStatus();
    }

    private void UpdateEnemyStatus(){
        for(int i = 0; i < enemies.Count; i++)
            if(enemies[i].enemy.diceased)
                enemies.Remove(enemies[i]); 
    }

    //TODO Special interaction
    private void UseSpecial(Card _card, CharacterEngine target){}
}