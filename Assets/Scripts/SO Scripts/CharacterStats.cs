using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Character/Stats", order = 1)]
public class CharacterStats : ScriptableObject
{
    private int level = 1;

    private IDictionary<string, int> playerStars = new Dictionary<string, int>(){
        {"Strength" , 10 },
        {"Constitution" , 10 },
        {"Intelligence" , 10 },
        {"Defense" , 10 }
    };

    public int Strength
    {
        get { return playerStars["Strength"]; }
        set
        {
            playerStars["Strength"] = value;
            if (EventManager.onStatsUpdate != null)
            {
                EventManager.onStatsUpdate();
            }
        }
    }

    public int Constitution
    {
        get { return playerStars["Constitution"]; }

        set
        {
            playerStars["Constitution"] = value;
            if (EventManager.onStatsUpdate != null)
            {
                EventManager.onStatsUpdate();
            }
        }
    }

    public int Intelligence
    {
        get { return playerStars["Intelligence"]; }

        set
        {
            playerStars["Intelligence"] = value;
            if (EventManager.onStatsUpdate != null)
            {
                EventManager.onStatsUpdate();
            }
        }
    }

    public int Defense
    {
        get { return playerStars["Defense"]; ; }
        set
        {
            playerStars["Defense"] = value;
            if (EventManager.onStatsUpdate != null)
            {
                EventManager.onStatsUpdate();
            }
        }
    }

    public int this[string i]
    {
        get
        {
            return playerStars[i];
        }
        set
        {
            playerStars[i] = value;
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