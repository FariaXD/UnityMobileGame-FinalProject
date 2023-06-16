﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CombatEngine : MonoBehaviour {
    public List<EnemyEngine> enemies = new List<EnemyEngine>(); //List of Enemy Engines
    public Team team = new Team(); //Player's Team
    public Turn active = Turn.None; //Active side
    public HandEngine handEngine; //Hand Engine (displays current character hand)
    public float difficultyModifier = 1f; //Difficulty modifier has the turns increase so does the diff
    public int turnCount = 0; //Turn counter
    public int enemyCount = 0; //How many enemies stage has
    public GameEngine engine;
    private float stageCompleteHeal = 0.1f;

    public enum Turn
    {
        PLAYER,
        ENEMY,
        None
    }

    private void Start() {
        engine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
        team.SetHeroes(GameObject.FindGameObjectsWithTag("Player")); //SetHeroes
        handEngine = GameObject.FindGameObjectWithTag("Hand").GetComponent<HandEngine>(); //SetHand
    }
    private void Update()
    {
        if (active != Turn.None)
            CheckGameEnded();
    }
    public void NewStageCombat(StageCombat sc){
        RestartValues();
        SwitchActiveCharacter(team.selectedHero); //switch active character and update UI
        InitializeEnemies(sc.enemies);
        enemyCount = sc.enemies.GetLength(0);
        active = Turn.PLAYER;
    }
    //Initialize list of enemies (Attacks and Visuals)
    private void InitializeEnemies(Enemy[] _enemies)
    {
        for (int i = 0; i < _enemies.GetLength(0); i++)
            enemies[i].SetNewEnemy(_enemies[i]);
    }
    private void RestartValues()
    {
        team.RefreshMana();
        team.teamGO.ForEach(heroGO => heroGO.InitializeDeck()); //Initialize heroes deck
        SwitchActiveCharacter(team.selectedHero); //switch active character and update UI
        difficultyModifier = 1f; //Difficulty modifier has the turns increase so does the diff
        turnCount = 0; //Turn counter
        active = Turn.PLAYER;
    }
    //Switch active character and display hand
    public void SwitchActiveCharacter(HeroEngine _hero)
    {
        if (!_hero.hero.diceased)
        {
            team.selectedHero = _hero;
            handEngine.SwitchHand(team.selectedHero.hero.hand.hand);
        }
    }
    public void AddArtifact(Artifact artifact){
        team.inventory.AddArtifact(artifact);
    }

    //Force switch hero 
    public void ForceSwitcHero()
    {
        foreach (HeroEngine en in team.teamGO)
        {
            if (!en.hero.diceased)
                SwitchActiveCharacter(en);
        }
    }

    //End Turn method
    public void EndTurn()
    {
        if (!team.GameEnded() && !CheckIfStageCompleted())
        {
            if (active == Turn.PLAYER)
            {
                engine.artifactEngine.RunArtifacts(Artifact.ArtifactActivation.END_TURN);
                team.RefreshStatusEffects(); //Decrease status turn for player
                team.StatusEffectEndTurn();
                active = Turn.ENEMY;
                EnemyTurn(); //Run enemy actions
            }
            else
            {
                foreach (EnemyEngine en in enemies)
                {
                    en.StatusEffectEndTurn();
                    en.ReduceStatusEffectDurations(); //Decrease status turn for enemies
                }
                engine.artifactEngine.RunArtifacts(Artifact.ArtifactActivation.START_TURN);
                active = Turn.PLAYER;
                team.RefreshMana(); //Refresh mana
                DrawCard(); //Draw card for each hero
                turnCount++; //increase turn
                if (turnCount % 5 == 0) //check if diff increase
                    difficultyModifier += 0.25f;
            }
        }
    }
    //Checks if player won or lost
    public void CheckGameEnded()
    {
        if (CheckIfStageCompleted())
        {
            engine.StageCompletedOrWorldEnded(true);
            active = Turn.None;
            enemies.ForEach(enemy => enemy.UnLoadEnemy());
            team.HealHeroesPercentage(stageCompleteHeal);
            engine.artifactEngine.RunArtifacts(Artifact.ArtifactActivation.END_STAGE);
        }
        else if (team.GameEnded())
        {
            engine.StageCompletedOrWorldEnded(false);
            team.RestartHeroes();
            active = Turn.None;
            enemies.ForEach(enemy => enemy.UnLoadEnemy());
        }
    }
    //Runs enemy attack AI engine
    private void EnemyTurn()
    {
        enemies.ForEach(en => en.RunEnemyMachine());
        EndTurn();
    }
    /*Player USE Card
    Checks if multi target 
    Updates used card
    */
    public void UseCard(CardEngine _cardEngine, CharacterEngine _target = default(CharacterEngine))
    {
        Card _card = _cardEngine.card;
        if (UseCardConditions(_card, _target))
        {
            engine.artifactEngine.RunArtifacts(Artifact.ArtifactActivation.PLAY_CARD);
            switch (_card.type)
            {
                default:
                    if (!_card.area)
                        UseSingle(_card, _target);
                    else
                        UseAOE(_card, _card.type == Card.Action_Type.Defense);
                    break;
                case Card.Action_Type.Special:
                    UseSpecial(_card, _target);
                    break;
            }
            team.currentMana -= _card.manaCost;
            handEngine.UpdateUsedCard(_cardEngine); //updates used card
            TargetingAllEnemies(false, true);
            team.TargetingAllAllies(false, true);
        }
    }
    /*
        Targets all enemies highlighting them
    */
    public void TargetingAllEnemies(bool _state, bool _force = default(bool))
    {
        foreach (EnemyEngine enemy in enemies)
        {
            if (enemy.enemy != null && !enemy.enemy.diceased)
            {
                enemy.targetedIcon.enabled = _state;
            }
            else if (_force)
            {
                enemy.targetedIcon.enabled = _state;
            }
        }
    }
    /*
        Can use card if
        Hasnt lost
        if stage is not completed
        its player turn
        is not affected by status effect SHOCK
        can only use single at alive char
        has mana to cast
        if target isnt diceased
    */
    public bool UseCardConditions(Card _card, CharacterEngine _target)
    {
        return (!team.GameEnded() &&
         !CheckIfStageCompleted() &&
          active == Turn.PLAYER &&
            (!_card.area || !_target.ReturnAssociatedCharacter().diceased) &&
             team.currentMana >= _card.manaCost &&
             CheckIfUseCardIsPlayedCorrectly(_card, _target) &&
             team.selectedHero.hero.CheckActionForStatus(Character.Character_Action.USE_ATTACK));
    }
    /*
    Damage cards cant be used on allies
    Defense cards cant be used on enemies
    */
    public bool CheckIfUseCardIsPlayedCorrectly(Card _card, CharacterEngine _target)
    {
        if (((_card.type == Card.Action_Type.Damage || _card.type == Card.Action_Type.Status) && _target.ReturnAssociatedCharacter() is Hero) ||
         _card.type == Card.Action_Type.Defense && _target.ReturnAssociatedCharacter() is Enemy)
            return false;
        return true;
    }

    //Draws a card for each hero
    public void DrawCard()
    {
        team.DrawCardForEachHero();
        SwitchActiveCharacter(team.selectedHero);
    }

    /*
        Use an AREA OF EFFECT card on all targets
        isDefensive - if card is a defensive card then works only on friendly chars
    */
    private void UseAOE(Card _card, bool isDefensive)
    {
        if (isDefensive)
            foreach (HeroEngine hero in team.teamGO)
            {
                if (!hero.hero.diceased)
                    team.selectedHero.hero.hand.UseCard(_card, hero.hero);
            }
        else
            foreach (EnemyEngine enemy in enemies)
                if (enemy.enemy != null && !enemy.enemy.diceased)
                    team.selectedHero.hero.hand.UseCard(_card, enemy.enemy);
    }

    /*
        Use a card on single target, and check if target died (if enemy remove it from list of enemies)
    */
    private void UseSingle(Card _card, CharacterEngine target)
    {
        team.selectedHero.hero.hand.UseCard(_card, target.ReturnAssociatedCharacter());
        target.UpdateStatus();
    }

    public bool CheckIfStageCompleted()
    {
        int count = 0;
        foreach (EnemyEngine enemyEn in enemies)
        {
            if (enemyEn.enemy != null && enemyEn.enemy.diceased)
                count++;
        }
        if (count == enemyCount && count != 0)
            return true;
        return false;
    }
    //TODO Special interaction
    private void UseSpecial(Card _card, CharacterEngine target) { }
}