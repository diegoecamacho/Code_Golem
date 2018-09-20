using UnityEngine;
using UnityEngine.AI;
using WeaponSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterStats characterStats;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private Transform spawnPoint; //Skill Spawn point

    //TODO: Implement weapon selection system
    [SerializeField] private BulletInstance weaponPrefab;

    private bool UIenabled = false;

    public NavMeshAgent Agent;

    public float movementSpeed = 1.0f;

    /// <summary>
    /// Gets the current Characters stats card.
    /// </summary>
    /// <returns>CharacterStats</returns>
    public CharacterStats GetStatsCard()
    {
        return characterStats;
    }

    private void Start()
    {
        Debug.Log(gameObject.GetInstanceID());
        PoolManager.Instance.CreatePool(weaponPrefab, 3);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            UIenabled = !UIenabled;
        }
        if (!UIenabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        Debug.Log("Hit");
                        Agent.SetDestination(hit.point);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PoolManager.Instance.SpawnBullet(weaponPrefab, spawnPoint);
            }
        }
    }
}