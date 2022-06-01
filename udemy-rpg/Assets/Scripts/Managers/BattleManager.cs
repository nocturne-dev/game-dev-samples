using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [Header("Battle Details")]
    [SerializeField] List<BattleTargets> battleTargets  = new List<BattleTargets>();
    [SerializeField] List<BattleItem> battleItems       = new List<BattleItem>();
    [SerializeField] List<BattleMagic> battleMagic      = new List<BattleMagic>();
    [SerializeField] BattleNotification battleNotice    = null;
    [SerializeField] GameObject 
        battleScene                     = null,
        battleMenu                      = null,
        targetMenu                      = null,
        magicMenu                       = null,
        itemMenu                        = null;
    [SerializeField] List<string> rewardItems = new List<string>();
    [SerializeField] int
        rewardEXP = 0,
        chanceToFlee = 25;
    private bool 
        battleActive                    = false, 
        battleTurnWaiting               = false,
        battleFleeing                   = false,
        cannotFlee                      = false;

    [Header("Unit Info")]
    [SerializeField] List<BattleMoves> movesList    = new List<BattleMoves>();
    [SerializeField] List<BattleUnits> activeUnits  = new List<BattleUnits>();
    [SerializeField] DamageDisplay damageDisplay    = null;
    [SerializeField] int 
        battlePlayerCount   = 0, 
        battleEnemyCount    = 0, 
        battleUnitTurn      = 0;

    [Header("Player Info")]
    [SerializeField] BattleUnits[] playerUnits      = null;
    [SerializeField] Transform[] playerPositions    = null;
    [SerializeField] List<Text> 
        playerNames = new List<Text>(), 
        playerHP    = new List<Text>(), 
        playerMP    = new List<Text>();

    [Header("Enemy Info")]
    [SerializeField] BattleUnits[] enemyUnits       = null;
    [SerializeField] Transform[] enemyPositions     = null;
    [SerializeField] GameObject enemyAttackEffect   = null;
    private bool shouldCompleteQuest                = false;
    private string questToComplete                  = "";

    [Header("Battle End")]
    [SerializeField] string gameOverScene   = "";

    private void Awake()
    {
        int numOfBattleManagers = FindObjectsOfType<BattleManager>().Length;
        if(numOfBattleManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DeactivateBattle();
        BattleCleanUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (battleActive)
        {
            if (battleTurnWaiting)
            {
                // TESTING
                if (Input.GetKeyDown(KeyCode.N))
                {
                    NextTurn();
                }

                if(activeUnits[battleUnitTurn].GetIsPlayer())
                {
                    battleMenu.SetActive(true);
                }

                else
                {
                    // Enemy turn
                    battleMenu.SetActive(false);
                    StartCoroutine("EnemyTurn");
                }
            }
        }
    }

    private void DeactivateBattle()
    {
        battleActive = false;
        targetMenu.SetActive(false);
        magicMenu.SetActive(false);
        itemMenu.SetActive(false);
        battleMenu.SetActive(false);
    }

    private void BattleCleanUp()
    { 
        foreach(BattleUnits unit in activeUnits)
        {
            Destroy(unit.gameObject);
        }
        activeUnits.Clear();

        battleUnitTurn = 0;
        battleFleeing = false; 
        battleScene.SetActive(false);
        //FindObjectOfType<GameManager>().SetBattleActive(false);

    }

    public void BattleStart(List<string> enemies, bool isBossBattle)
    {
        if (battleActive)
        {
            return;
        }

        cannotFlee = isBossBattle;
        
        FindObjectOfType<GameManager>().SetBattleActive(true);
        FindObjectOfType<AudioManager>().PlayBGM(0);

        battleScene.SetActive(true);
        battleScene.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);

        InstantiatePlayers();
        InstantiateEnemies(enemies);

        // Set initial battle parameters
        battleActive = true;
        battleTurnWaiting = true;
        battleUnitTurn = UnityEngine.Random.Range(0, activeUnits.Count);

        UpdateUIStats();
    }

    private void InstantiatePlayers()
    {
        for(int i = 0; i < playerPositions.Length; i++)
        {
            if (FindObjectOfType<GameManager>().GetStatsList().Length > i &&
                FindObjectOfType<GameManager>().GetStatsList()[i] != null &&
                FindObjectOfType<GameManager>().GetStatsList()[i].gameObject.activeInHierarchy)
            {
                CharacterStats stats = FindObjectOfType<GameManager>().GetStatsList()[i];
                foreach (BattleUnits unit in playerUnits)
                {
                    if (unit.GetUnitName().ToUpper().Equals(stats.GetCharName().ToUpper()))
                    {
                        BattleUnits newPlayer = Instantiate(unit, playerPositions[i].position, playerPositions[i].rotation);
                        newPlayer.transform.parent = playerPositions[i];
                        newPlayer.SetCurrHP(stats.GetCurrentHP());
                        newPlayer.SetMaxHP(stats.GetMaxHP());
                        newPlayer.SetCurrMP(stats.GetCurrentMP());
                        newPlayer.SetMaxMP(stats.GetMaxMP());
                        newPlayer.SetStr(stats.GetStrength());
                        newPlayer.SetDef(stats.GetDefense());
                        newPlayer.SetWpnPwr(stats.GetWpnPwr());
                        newPlayer.SetArmrPwr(stats.GetArmrPwr());
                        activeUnits.Add(newPlayer);
                        battlePlayerCount++;
                    }
                }
            }
        }
    }

    private void InstantiateEnemies(List<string> enemies)
    {
        //int enemyPos = UnityEngine.Random.Range(0, enemyPositions.Length) + 1;
        for(int i = 0; i < enemies.Count; i++)
        {
            for(int j = 0; j < enemyUnits.Length; j++)
            {
                if (enemies[i].ToUpper().Equals(enemyUnits[j].GetUnitName().ToUpper()))
                {
                    //int enemySpawn = UnityEngine.Random.Range(0, enemyUnits.Length);
                    BattleUnits newEnemy = Instantiate(enemyUnits[j], enemyPositions[i].position, enemyPositions[i].rotation);
                    newEnemy.transform.parent = enemyPositions[i];
                    activeUnits.Add(newEnemy);
                    battleEnemyCount++;
                    break;
                }
            }
        }
    }

    private void NextTurn()
    {
        battleUnitTurn = (battleUnitTurn + 1) % activeUnits.Count;
        battleTurnWaiting = true;
        UpdateBattle();
        UpdateUIStats();
    }

    private void UpdateBattle()
    {
        foreach(BattleUnits unit in activeUnits)
        {
            if(unit.gameObject.activeInHierarchy &&
                unit.GetCurrHP() <= 0 &&
                !unit.GetIsDead())
            {
                unit.SetCurrHP(0);

                if (unit.GetIsPlayer())
                {
                    unit.SetIsDead(true);
                    battlePlayerCount--;
                }
                else
                {
                    unit.SetIsDead(true);
                    unit.StartFadingWhenDead();
                    battleEnemyCount--;
                }
            }
        }

        // Player victory
        if(battleEnemyCount <= 0)
        {
            StartCoroutine("BattleVictory");
        }

        // Player defeat
        else if (battlePlayerCount <= 0)
        {
            StartCoroutine("GameOver");
        }

        else
        {
            while(activeUnits[battleUnitTurn].GetCurrHP() <= 0)
            {
                battleUnitTurn = (battleUnitTurn + 1) % activeUnits.Count;
            }
        }
    }


    public void Flee()
    {
        if (cannotFlee)
        {
            battleNotice.SetNotificationText("Can't flee from this battle!");
            battleNotice.Activate();
        }

        else
        {
            int fleeSuccess = UnityEngine.Random.Range(0, 100);
            if(fleeSuccess < chanceToFlee)
            {
                // End the battle
                //StartCoroutine("BattleFlee");

                //FindObjectOfType<AudioManager>().PlayBGM(3);
                //DeactivateBattle();
                battleFleeing = true;
                StartCoroutine("BattleVictory");
            }

            else
            {
                battleNotice.SetNotificationText("Failed To Escape!");
                battleNotice.Activate();
                NextTurn();
            }
        }

    }

    IEnumerator BattleVictory()
    {
        DeactivateBattle();
        yield return new WaitForSecondsRealtime(0.5f);

        FindObjectOfType<UIManager>().ActivateTransitionScreen(true);
        FindObjectOfType<UIFadeTransition>().StartFadingToBlack();
        yield return new WaitForSecondsRealtime(1.5f);

        for(int i = 0; i < activeUnits.Count; i++)
        {
            if (activeUnits[i].GetIsPlayer())
            {
                BattleUnits player = activeUnits[i];
                GameManager gm = FindObjectOfType<GameManager>();

                for(int j = 0; j < gm.GetStatsList().Length; j++)
                {
                    if (player.GetUnitName().ToUpper().Equals(gm.GetStatsList()[j].GetCharName().ToUpper()))
                    {
                        gm.GetStatsList()[j].SetCurrentHP(player.GetCurrHP());
                        gm.GetStatsList()[j].SetCurrentMP(player.GetCurrMP());
                    }
                }
            }
        }
        if (battleFleeing)
        {
            FindObjectOfType<GameManager>().SetBattleActive(false);
        }
        else
        {
            // Open rewards screen
            FindObjectOfType<UIManager>().ActivateRewardsScreen(true);
            FindObjectOfType<BattleResults>().OpenRewardsScreen(rewardEXP, rewardItems, shouldCompleteQuest, questToComplete);
        }

        BattleCleanUp();
        FindObjectOfType<AudioManager>().PlayBGM(FindObjectOfType<CameraController>().GetAudioToPlay());
        FindObjectOfType<UIFadeTransition>().StartFadingFromBlack();
        yield return new WaitForSecondsRealtime(2f);

        FindObjectOfType<UIManager>().ActivateTransitionScreen(false);
    }

    IEnumerator GameOver()
    {
        DeactivateBattle();
        yield return new WaitForSecondsRealtime(0.5f);

        FindObjectOfType<UIManager>().ActivateTransitionScreen(true);
        FindObjectOfType<UIFadeTransition>().StartFadingToBlack();
        yield return new WaitForSecondsRealtime(1.5f);

        BattleCleanUp();
        SceneManager.LoadScene(gameOverScene);
    }

    public void OpenPlayerTargetMenu(bool isItem, string moveName)
    {
        targetMenu.SetActive(true);

        List<int> players = new List<int>();
        for(int i = 0; i < activeUnits.Count; i++)
        {
            if (activeUnits[i].GetIsPlayer())
            {
                players.Add(i);
            }
        }

        for (int j = 0; j < battleTargets.Count; j++)
        {
            if (j < players.Count)
            {
                battleTargets[j].gameObject.SetActive(true);
                battleTargets[j].SetMoveName(moveName);
                battleTargets[j].SetIsItem(isItem);
                battleTargets[j].SetActiveUnitTarget(players[j]);
                battleTargets[j].SetTargetName(activeUnits[players[j]].GetUnitName());
            }
            else
            {
                battleTargets[j].gameObject.SetActive(false);
            }
        }
    }

    public void OpenEnemyTargetMenu(string moveName)
    {
        targetMenu.SetActive(true);

        List<int> enemies = new List<int>();
        for(int i = 0; i < activeUnits.Count; i++)
        {
            if(!activeUnits[i].GetIsPlayer() && activeUnits[i].GetCurrHP() > 0)
            {
                enemies.Add(i);
            }
        }

        for(int j = 0; j < battleTargets.Count; j++)
        {
            if(j < enemies.Count)
            {
                battleTargets[j].gameObject.SetActive(true);
                battleTargets[j].SetMoveName(moveName);
                battleTargets[j].SetIsItem(false);
                battleTargets[j].SetActiveUnitTarget(enemies[j]);
                battleTargets[j].SetTargetName(activeUnits[enemies[j]].GetUnitName());
            }
            else
            {
                battleTargets[j].gameObject.SetActive(false);
            }
        }
    }

    public void OpenMagicMenu()
    {
        magicMenu.SetActive(true);
        
        for(int i = 0; i < battleMagic.Count; i++)
        {
            if(activeUnits[battleUnitTurn].GetMovesAvailable().Length > i)
            {
                battleMagic[i].gameObject.SetActive(true);
                battleMagic[i].SetSpellName(activeUnits[battleUnitTurn].GetMovesAvailable()[i]);
                
                foreach(BattleMoves move in movesList)
                {
                    if (battleMagic[i].GetSpellName().ToUpper().Equals(move.GetMoveName().ToUpper()))
                    {
                        battleMagic[i].SetSpellCost(move.GetMoveCost());
                    }
                }
            }
            else
            {
                battleMagic[i].gameObject.SetActive(false);
            }
        }
    }

    public void CloseMagicMenu()
    {
        magicMenu.SetActive(false);
    }

    public void OpenItemMenu()
    {
        itemMenu.SetActive(true);

        string[] itemsHeld = FindObjectOfType<GameManager>().GetItemsHeld();
        int[] itemsAmount = FindObjectOfType<GameManager>().GetNumOfItems();

        for(int i = 0; i < battleItems.Count; i++)
        {
            if (i < itemsHeld.Length)
            {
                Item item = FindObjectOfType<GameManager>().GetItem(itemsHeld[i]);

                if (item == null || !item.GetIsItem())
                {
                    battleItems[i].gameObject.SetActive(false);
                    continue;
                }
                else
                {
                    battleItems[i].gameObject.SetActive(true);
                    battleItems[i].SetIsItem(item.GetIsItem());
                    battleItems[i].SetAffectsHP(item.GetAffectsHP());
                    battleItems[i].SetAffectsMP(item.GetAffectsMP());
                    battleItems[i].SetItemValue(item.GetAmountToChange());
                    battleItems[i].SetItemImage(item.GetItemSprite());
                    battleItems[i].SetItemName(itemsHeld[i]);
                    battleItems[i].SetItemAmount(itemsAmount[i]);
                }
            }

            else
            {
                battleItems[i].gameObject.SetActive(false);
            }
        }
    }

    public void CloseItemMenu()
    {
        itemMenu.SetActive(false);
    }

    public void PlayerAttack(bool isItem, string moveName, int selectedTarget)
    {

        if (isItem) 
        { 
            foreach(BattleItem item in battleItems)
            {
                if (item.GetItemName().ToUpper().Equals(moveName.ToUpper()))
                {
                    UseItem(selectedTarget, item.GetItemValue(), item.GetAffectsHP(), item.GetAffectsMP());
                    break;
                }
            }
        }

        else
        {
            int selectedMove = 0;
            foreach (BattleMoves move in movesList)
            {
                if (move.GetMoveName().ToUpper().Equals(moveName.ToUpper()))
                {
                    Instantiate(move.GetMoveEffects(), activeUnits[selectedTarget].transform.position, activeUnits[selectedTarget].transform.rotation);
                    selectedMove = move.GetMovePwr();
                    break;
                }
            }

            Instantiate(enemyAttackEffect, activeUnits[battleUnitTurn].transform.position, activeUnits[battleUnitTurn].transform.rotation);
            DealDamage(selectedTarget, selectedMove);
        }

        battleMenu.SetActive(false);
        targetMenu.SetActive(false);
        NextTurn();
    }

    IEnumerator EnemyTurn()
    {
        battleTurnWaiting = false;
        yield return new WaitForSecondsRealtime(2f);
        EnemyAttack();
        yield return new WaitForSecondsRealtime(1f);
        NextTurn();
    }

    private void EnemyAttack()
    {
        List<int> players = new List<int>();
        for(int i = 0; i < activeUnits.Count; i++)
        {
            if(activeUnits[i].GetIsPlayer() && activeUnits[i].GetCurrHP() > 0)
            {
                players.Add(i);
            }
        }

        int selectedTarget = players[UnityEngine.Random.Range(0, players.Count)];
        int selectedAttack = UnityEngine.Random.Range(0, activeUnits[battleUnitTurn].GetMovesAvailable().Length);
        int selectedMove = 0;
        foreach(BattleMoves move in movesList)
        {
            if (move.GetMoveName().ToUpper().Equals(activeUnits[battleUnitTurn].GetMovesAvailable()[selectedAttack].ToUpper()))
            {
                Instantiate(move.GetMoveEffects(), activeUnits[selectedTarget].transform.position, activeUnits[selectedTarget].transform.rotation);
                selectedMove = move.GetMovePwr();
                break;
            }
        }

        Instantiate(enemyAttackEffect, activeUnits[battleUnitTurn].transform.position, activeUnits[battleUnitTurn].transform.rotation);
        DealDamage(selectedTarget, selectedMove);
    }

    private void DealDamage(int target, int movePwr)
    {
        float atkPwr = activeUnits[battleUnitTurn].GetStr() + activeUnits[battleUnitTurn].GetWpnPwr();
        float defPwr = activeUnits[target].GetDef() + activeUnits[target].GetArmrPwr();
        float dmgCalc = (atkPwr / defPwr) * movePwr * UnityEngine.Random.Range(0.9f, 1.1f);
        int dmgOut = Mathf.FloorToInt(dmgCalc);

        Debug.Log(activeUnits[battleUnitTurn].GetUnitName() + " is dealing " + dmgOut + " damage to " + activeUnits[target].GetUnitName());

        activeUnits[target].SetCurrHP(activeUnits[target].GetCurrHP() - dmgOut);
        Instantiate(damageDisplay, activeUnits[target].transform.position, activeUnits[target].transform.rotation).SetDamage(dmgOut);

        UpdateUIStats();
    }

    private void UseItem(int target, int amount, bool hp, bool mp)
    {
        if (hp)
        {
            int recoveredAmount = activeUnits[target].GetCurrHP() + amount;
            int setHP = recoveredAmount >= activeUnits[target].GetMaxHP() ? activeUnits[target].GetMaxHP() : recoveredAmount;
            activeUnits[target].SetCurrHP(setHP);
            Debug.Log(activeUnits[target].GetUnitName() + " has recovered " + amount + " HP");
        }

        else if (mp)
        {
            int recoveredAmount = activeUnits[target].GetCurrHP() + amount;
            int setMP = recoveredAmount >= activeUnits[target].GetMaxMP() ? activeUnits[target].GetMaxHP() : recoveredAmount;
            activeUnits[target].SetCurrMP(setMP);
            Debug.Log(activeUnits[target].GetUnitName() + " has recovered " + amount + " MP");
        }

        Instantiate(damageDisplay, activeUnits[target].transform.position, activeUnits[target].transform.rotation).SetDamage(amount);
        UpdateUIStats();
    }

    private void UpdateUIStats()
    {
        for(int i = 0; i < playerNames.Count; i++)
        {
            if(i < activeUnits.Count)
            {
                if (activeUnits[i].GetIsPlayer())
                {
                    BattleUnits playerData = activeUnits[i];

                    playerNames[i].gameObject.SetActive(true);
                    playerNames[i].text = playerData.GetUnitName();
                    playerHP[i].text = Mathf.Clamp(playerData.GetCurrHP(), 0, int.MaxValue) + "/" + playerData.GetMaxHP();
                    playerMP[i].text = Mathf.Clamp(playerData.GetCurrMP(), 0, int.MaxValue) + "/" + playerData.GetMaxMP();
                }

                else
                {
                    playerNames[i].gameObject.SetActive(false);
                }
            }

            else
            {
                playerNames[i].gameObject.SetActive(false);
            }
        }
    }

    public BattleUnits GetCurrentActiveUnit()
    {
        return activeUnits[battleUnitTurn];
    }

    public BattleNotification GetBattleNotification()
    {
        return battleNotice;
    }

    public void SetRewardItems(List<string> rewards)
    {
        rewardItems = rewards;
    }

    public void SetRewardEXP(int exp)
    {
        rewardEXP = exp;
    }

    public void SetShouldCompleteQuest(bool b)
    {
        shouldCompleteQuest = b;
    }

    public void SetQuestToComplete(string s)
    {
        questToComplete = s;
    }
}
