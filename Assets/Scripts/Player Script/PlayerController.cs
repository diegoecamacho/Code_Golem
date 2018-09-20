using UnityEngine;
using UnityEngine.AI;
using WeaponSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private CharacterStats characterStats;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private Transform spawnPoint; //Skill Spawn point

    //TODO: Implement weapon selection system

    private bool UIenabled = false;

    public NavMeshAgent Agent;

    public  WeaponObject currWeapon;

    Transform _target;
   

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
        //PoolManager.Instance.CreatePool(weaponPrefab, 3);
        //currWeapon.Init();
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
                        //_target.position = hit.point;
                        Agent.SetDestination(hit.point);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GameObject bull = currWeapon.Instantiate(spawnPoint.position, spawnPoint.rotation);
                bull.GetComponent<WeaponBaseScript>().Fire();
            }
        }
    }
}