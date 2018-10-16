using CodeGolem.Managers;
using CodeGolem.Player;
using UnityEngine;

namespace CodeGolem.Level

{
    public class LevelManager : Singleton<LevelManager>
    {
        public static PlayerController Player;

        public Transform SpawnLocation;
        public GameObject PlayerPrefabGameObject;

        private void Start()
        {
            if (Player != null) return;
            Player = FindObjectOfType<PlayerController>();
            if (Player == null)
            {
                Player = Instantiate(PlayerPrefabGameObject, SpawnLocation).GetComponent<PlayerController>();
            }
        }
    }
}