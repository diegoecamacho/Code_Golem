using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterStats characterStats;
    [SerializeField] GameObject pauseMenu;

    [SerializeField] Transform spawnPoint; //Skill Spawn point


    bool UIenabled = false;


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

    // Update is called once per frame
    void Update()
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
                //GameObject atk = Instantiate()


            }
        }
    }
}
