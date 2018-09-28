using UnityEngine;

/*****************************************************
// Author: The Messy Coder
// Date: January 2018
// Video Tutorial: https://youtu.be/6d7pmRE0T6c
// Please support the channel, facebook and on twitter
// YouTube:  www.YouTube.com/TheMessyCoder
// Facebook: www.Facebook.com/TheMessyCoder
// Twitter:  www.Twitter.com/TheMessyCoder
*****************************************************/

namespace Messyspace
{
    public class PlayerHandler : MonoBehaviour
    {
        public PlayerStats Player;

        [SerializeField]
        private Canvas m_Canvas;

        private bool m_SeeCanvas;

        // Update is called once per frame
        private void Update()
        {
            // if you press the E key
            if (Input.GetKeyDown("tab"))
            {
                if (m_Canvas)
                {
                    m_SeeCanvas = !m_SeeCanvas;
                    m_Canvas.gameObject.SetActive(m_SeeCanvas); // display or not the canvas (following the state of the bool)
                }
            }
        }
    }
}