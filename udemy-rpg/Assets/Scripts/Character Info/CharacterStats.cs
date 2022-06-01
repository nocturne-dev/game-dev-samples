using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] string charName;
    [SerializeField] int playerLvl = 1;
    [SerializeField] int currentEXP = 0;
    [SerializeField] int[] expToNextLvl;
    [SerializeField] int maxLvl = 100;
    [SerializeField] int baseEXP = 1000;

    [SerializeField] int currentHP = 0;
    [SerializeField] int maxHP = 100;
    [SerializeField] int currentMP = 0;
    [SerializeField] int maxMP = 100;
    [SerializeField] int strength = 15;
    [SerializeField] int defense = 12;
    [SerializeField] int wpnPwr;
    [SerializeField] int armrPwr;
    [SerializeField] string equippedWpn;
    [SerializeField] string equippedArmr;
    [SerializeField] Sprite charImage;

    // Start is called before the first frame update
    void Start()
    {
        SetupEXPRoadMap();
        currentHP = maxHP;
        currentMP = maxMP;
    }

    // Update is called once per frame
    void Update()
    {
        // DEBUGGING
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddEXP(5000000);
        }
    }

    void SetupEXPRoadMap()
    {
        expToNextLvl = new int[maxLvl];

        for (int i = 0; i < expToNextLvl.Length; i++)
        {
            int n = i + 1;
            float partialExpCalc = 0.0f;

            // Level 1 to 15
            if (n < 16)
            {
                partialExpCalc = Mathf.Floor((n + 1) / 3);
                partialExpCalc += 24;
                partialExpCalc /= 50;
                partialExpCalc *= Mathf.Pow(n, 3);
            }

            // Level 16 to 36
            else if (n >= 16 && n <= 36)
            {
                partialExpCalc = n + 14;
                partialExpCalc /= 50;
                partialExpCalc *= Mathf.Pow(n, 3);
            }

            // Level 37 to 100
            else if (n > 36)
            {
                partialExpCalc = Mathf.Floor(n / 2);
                partialExpCalc += 32;
                partialExpCalc /= 50;
                partialExpCalc *= Mathf.Pow(n, 3);
            }

            expToNextLvl[i] = Mathf.RoundToInt(partialExpCalc);
        }
    }

    private void CalculateEXP()
    {
        if (currentEXP >= expToNextLvl[playerLvl])
        {
            currentEXP -= expToNextLvl[playerLvl];
            IncreaseStats();

            // Recursively call function to check if
            // currentEXP is still more than or equal
            // to expToNextLvl
            if(playerLvl < maxLvl)
            {
                CalculateEXP();
            }

            // If player has reached the max level,
            // stop incrementing exp
            else
            {
                currentEXP = expToNextLvl[playerLvl - 1];
            }
        }
    }

    private void IncreaseStats()
    {
        // Increase level
        playerLvl++;

        // Increase other stats
        maxHP       += Mathf.FloorToInt(Mathf.Sqrt(maxHP / maxLvl) + playerLvl + 10);
        maxMP       += Mathf.FloorToInt(Mathf.Sqrt(maxMP / maxLvl) + (playerLvl * 0.1f));
        strength    += Mathf.FloorToInt((Mathf.Sqrt(strength * playerLvl / maxLvl) + 5) * 0.25f);
        defense     += Mathf.FloorToInt((Mathf.Sqrt(defense * playerLvl / maxLvl) + 5) * 0.25f);

        // Refill HP and MP
        currentHP   = maxHP;
        currentMP   = maxMP;
    }

    public void AddEXP(int expToAdd)
    {
        if (playerLvl < maxLvl)
        {
            currentEXP += expToAdd;
            CalculateEXP();
        }
    }

    public string GetCharName() { return charName; }
    public int GetPlayerLvl() { return playerLvl; }
    public void SetPlayerLvl(int lvl) { playerLvl = lvl; }
    public int GetCurrentEXP() { return currentEXP; }
    public void SetCurrentEXP(int exp) { currentEXP = exp; }
    public int[] GetEXPToNextLvl() { return expToNextLvl; }
    public int GetMaxLvl() { return maxLvl; }
    public int GetCurrentHP() { return currentHP; }
    public void SetCurrentHP(int amountToChange) 
    { 
        currentHP = amountToChange;
        if(currentHP >= maxHP) { currentHP = maxHP; }
    }
    public int GetMaxHP() { return maxHP; }
    public void SetMaxHP(int hp) { maxHP = hp; }
    public int GetCurrentMP() { return currentMP; }
    public void SetCurrentMP(int amountToChange)
    {
        currentMP = amountToChange;
        if(currentMP >= maxMP) { currentMP = maxMP; }
    }
    public int GetMaxMP() { return maxMP; }
    public void SetMaxMP(int mp) { maxMP = mp; }
    public int GetStrength() { return strength; }
    public void SetStrength(int str) { strength = str; }
    public int GetDefense() { return defense; }
    public void SetDefense(int def) { defense = def; }
    public int GetWpnPwr() { return wpnPwr; }
    public void SetWpnPwr(int wpnValue) { wpnPwr = wpnValue; }
    public int GetArmrPwr() { return armrPwr; }
    public void SetArmrPwr(int armrValue) { armrPwr = armrValue; }
    public string GetEquippedWpn() {  return equippedWpn == null ? "" : equippedWpn; }
    public void SetEquippedWpn(string equipment) { equippedWpn = equipment; }
    public string GetEquippedArmr() { return equippedArmr == null ? "" : equippedArmr; }
    public void SetEquippedArmr(string equipment) { equippedArmr = equipment; }
    public Sprite GetCharImage() { return charImage; }
}