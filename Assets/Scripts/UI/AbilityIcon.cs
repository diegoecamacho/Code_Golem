using UnityEngine;
using UnityEngine.UI;

namespace CodeGolem.UI
{
    public class AbilityIcon : MonoBehaviour
    {
        [Header("Icon Instance Fields")]
        public Image buttomImage;

        public Image darkMask;
        public Image activeMask;
        public Text coolDownText;

        [HideInInspector]
        public float coolDownDuration;

        private float timeElapsed = 0;

        [Header("Instance Specific")]
        private bool m_onCoolDown = false;

        private void Start()
        {
        }

        /// <summary>
        /// Is skill currently on Cooldown.
        /// </summary>
        /// <returns>bool skillActive</returns>
        public bool IsActive()
        {
            return m_onCoolDown;
        }

        /// <summary>
        /// Enables Skill Icon Active Effect
        /// </summary>
        public void EnableIconEffect()
        {
            activeMask.enabled = true;
        }

        /// <summary>
        /// Starts the Cooldown timer animation.
        /// </summary>
        public void ActivateIconCoolDown()
        {
            m_onCoolDown = true;
            activeMask.enabled = false;
            darkMask.enabled = true;
            coolDownText.enabled = true;
        }

        /// <summary>
        /// Resets the Cooldown Icon.
        /// </summary>
        private void ResetSkillIcon()
        {
            darkMask.enabled = false;
            coolDownText.enabled = false;
            m_onCoolDown = false;
            timeElapsed = 0;
        }

        /// <summary>
        /// Plays the Cooldown Animation
        /// </summary>
        private void PlayCoolDownAnim()
        {
            float coolDownTimeLeft = coolDownDuration - timeElapsed;
            float roundCd = Mathf.Floor(coolDownTimeLeft);
            coolDownText.text = roundCd.ToString();
            darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
        }

        // Update is called once per frame
        private void Update()
        {
            if (m_onCoolDown)
            {
                if (timeElapsed >= coolDownDuration)
                {
                    ResetSkillIcon();
                }
                else
                {
                    timeElapsed += Time.deltaTime;
                    PlayCoolDownAnim();
                }
            }
        }
    }
}