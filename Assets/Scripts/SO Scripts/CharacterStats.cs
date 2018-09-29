
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Character/Stats", order = 1)]
public class CharacterStats : ScriptableObject
{
    private int level = 1;

    public int strength = 10;
    public int constitution = 10;
    public int intelligence = 10;
    public int defense = 10;


   

    public int Strength
    {
        get { return strength; }
        set
        {
            strength = value;
            if (EventManager.onStatsUpdate != null)
            {
                EventManager.onStatsUpdate();
            }
        }
    }

    public int Constitution
    {
        get { return constitution; }

        set
        {
            constitution = value;
            if (EventManager.onStatsUpdate != null)
            {
                EventManager.onStatsUpdate();
            }
        }
    }

    public int Intelligence
    {
        get { return intelligence; }

        set
        {
            intelligence = value;
            if (EventManager.onStatsUpdate != null)
            {
                EventManager.onStatsUpdate();
            }
        }
    }

    public int Defense
    {
        get { return defense; }
        set
        {
            defense = value;
            if (EventManager.onStatsUpdate != null)
            {
                EventManager.onStatsUpdate();
            }
        }
    }

    public int GetPlayerLevel()
    {
        return level;
    }

    public static CharacterStats operator +(CharacterStats lhs, CharacterStats rhs)
    {
        CharacterStats stats = new CharacterStats
        {
            Strength = lhs.Strength + rhs.Strength,
            Constitution = lhs.Constitution + rhs.Constitution,
            Intelligence = lhs.Intelligence + rhs.Intelligence,
            Defense = lhs.Defense + rhs.Defense
        };

        EventManager.onStatsUpdate();

        return stats;
    }
}