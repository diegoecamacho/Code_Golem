using UnityEngine;
using System.Collections.Generic;

namespace InputHandler
{
    public class InputHandler : MonoBehaviour
    {
        private InputHandler _instance;

        List<string> inputHandles = new List<string>() { "CameraHorizontal", "CameraVertical", "PlayerMove" , "SkillSlot1" , "SkillSlot2", "SkillSlot3" };

        // Use this for initialization
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        // Update is called once per frame
        private void Update()
        {
           
        }
    }
}