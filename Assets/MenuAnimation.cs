using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animations
{
    public class MenuAnimation : MonoBehaviour
    {
        RectTransform rect;

        bool animate = false;
        private void OnEnable()
        {
            Debug.Log("Play");
            
        }


        // Use this for initialization
        void Start()
        {
            rect = GetComponent<RectTransform>();
            gameObject.SetActive(false);


        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
