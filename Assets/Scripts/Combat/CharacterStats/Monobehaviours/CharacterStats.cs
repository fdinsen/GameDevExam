using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterStats_SO characterDefinition;

    #region Constructors
    public CharacterStats()
    {

    }
    #endregion

    #region Initializations
    void Start()
    {
        if (!characterDefinition.setManually)
        {
            characterDefinition.maxHealth = 100;
            characterDefinition.currentHealth = 50;

            characterDefinition.maxMana = 25;
            characterDefinition.currentMana = 10;

            characterDefinition.maxWealth = 500;
            characterDefinition.currentWealth = 0;

            characterDefinition.baseDamage = 2;
            characterDefinition.currentDamage = characterDefinition.baseDamage;

            characterDefinition.baseResistance = 0;
            characterDefinition.currentResistance = 0;

            characterDefinition.maxEncumbrance = 50f;
            characterDefinition.currentEncumbrance = 0f;

            characterDefinition.charExperience = 0;
            characterDefinition.charLevel = 1;
        }
        HealthHandler.InvokeSetMaxHealth(characterDefinition.maxHealth);
        HealthHandler.InvokeHealthChanged(characterDefinition.currentHealth);
    }
    #endregion

    #region Stat Increasers
    public void ApplyHealth(int healthAmount)
    {
        characterDefinition.ApplyHealth(healthAmount);
        HealthHandler.InvokeHealthChanged(characterDefinition.currentHealth);
    }

    public void ApplyMana(int manaAmount)
    {
        characterDefinition.ApplyMana(manaAmount);
    }

    public void GiveWealth(int wealthAmount)
    {
        characterDefinition.GiveWealth(wealthAmount);
    }

    public void IncreaseStats(Attributes attr, int amount)
    {
        if (attr == Attributes.maxHealth)
        {
            characterDefinition.maxHealth += amount;
            HealthHandler.InvokeSetMaxHealth(characterDefinition.maxHealth);
            HealthHandler.InvokeHealthChanged(characterDefinition.currentHealth);
        }
        else if (attr == Attributes.baseDamage)
        {
            characterDefinition.baseDamage += amount;
        }
        else if (attr == Attributes.baseResistance)
        {
            characterDefinition.baseResistance += amount;
        }
    }
    #endregion

    #region Stat Reducers
    public void TakeDamage(int amount)
    {
        characterDefinition.TakeDamage(amount);
        HealthHandler.InvokeHealthChanged(characterDefinition.currentHealth);
    }

    public void TakeMana(int amount)
    {
        characterDefinition.TakeMana(amount);
    }

    public void DecreaseStats(Attributes attr, int amount)
    {
        if (attr == Attributes.maxHealth)
        {
            characterDefinition.maxHealth -= amount;
            HealthHandler.InvokeSetMaxHealth(characterDefinition.maxHealth);
        }
        else if (attr == Attributes.baseDamage)
        {
            characterDefinition.baseDamage -= amount;
        }
        else if (attr == Attributes.baseResistance)
        {
            characterDefinition.baseResistance -= amount;
        }
    }
    #endregion

    #region Reporters
    public int GetHealth()
    {
        return characterDefinition.currentHealth;
    }

    public int GetDamage()
    {
        return characterDefinition.currentDamage;
    }

    public float GetResistance()
    {
        return characterDefinition.currentResistance;
    }

    public float GetStat(Attributes attr)
    {
        switch (attr)
        {
            case Attributes.maxHealth:
                return characterDefinition.maxHealth;
            case Attributes.baseDamage:
                return characterDefinition.baseDamage;
            case Attributes.baseResistance:
                return characterDefinition.baseResistance;
        }
        return 0f;
    }
    #endregion
}