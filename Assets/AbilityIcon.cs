using UnityEngine;
using UnityEngine.UI;
using CodeGolem_WeaponSystem;

namespace CodeGolem_UI
{

    public class AbilityIcon : MonoBehaviour
    {
        public const string abilityButtom = "SkillSlot1";
        public Image darkMask;
        public Image activeMask;
        public Text coolDownText;

        public float coolDownDuration;
        private float activeTime = 0;
        private float coolDownTimeLeft;

        public Image buttomImage;

        [Header("Instance Specific")]
        bool m_inCoolDown = false;

        private void Start()
        {
           // PlayerController.skillsUIUpdate += Initialize;
            buttomImage = GetComponent<Image>();
        }

        public bool IsActive()
        {
            return m_inCoolDown;
        }


        public void ActivateSkill()
        {
            activeMask.enabled = true;
            m_inCoolDown = true;
        }

        public void ActivateCooldown ()
        {
            activeMask.enabled = false;
            activeTime = coolDownDuration + coolDownDuration + Time.time;
            coolDownTimeLeft = coolDownDuration;
            darkMask.enabled = true;
            coolDownText.enabled = true;
        }

        private void ResetSkill()
        {
            darkMask.enabled = false;
            coolDownText.enabled = false;
            m_inCoolDown = false;
        }

        private void CoolDown()
        {
            
            coolDownTimeLeft -= Time.deltaTime;
            float roundCd = Mathf.Floor(coolDownTimeLeft);
            coolDownText.text = roundCd.ToString();
            darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
        }

        // Update is called once per frame
        private void Update()
        {
            bool cooldownComplete = (Time.time > activeTime);
            if (cooldownComplete)
            {
                ResetSkill();
            }
            else
            {
                CoolDown();
            }
        }
    }
}