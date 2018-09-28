using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void UpdateUI();

    public static UpdateUI onStatsUpdate;
}