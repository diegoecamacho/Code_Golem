using UnityEngine;
using UnityEngine.UI;
using CodeGolem_WeaponSystem;

namespace CodeGolem_UI
{

    public class AbilityIcon : MonoBehaviour
    {
        public const string abilityButtom = "SkillSlot1";
        public Image darkMask;
        public Text coolDownText;

        private AbilityBase ability;
        private GameObject weaponHolder;
        private Image buttomImage;
        private float coolDownDuration;
        private float nextReadyTime;
        private float coolDownTimeLeft;

        private void Start()
        {
            PlayerController.skillsUIUpdate += Initialize;
        }

        public void Initialize(AbilityBase selectedAbility, GameObject weaponHolder)
        {
            ability = selectedAbility;
            buttomImage = GetComponent<Image>();
            buttomImage.sprite = ability.m_Sprite;
            coolDownDuration = ability.m_baseCoolDown;
            ability.Initialize(weaponHolder);
            AbilityEnable();
        }

        private void AbilityEnable()
        {
            darkMask.enabled = false;
            coolDownText.enabled = false;
        }

        private void CoolDown()
        {
            coolDownTimeLeft -= Time.deltaTime;
            float roundCd = Mathf.Floor(coolDownTimeLeft);
            coolDownText.text = roundCd.ToString();
            darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
        }

        private void TriggerCooldown()
        {
            nextReadyTime = coolDownDuration + Time.time;
            coolDownTimeLeft = coolDownDuration;
            darkMask.enabled = true;
            coolDownText.enabled = true;

            //#Play Sound?

            ability.ActivateSkill();
        }

        // Update is called once per frame
        private void Update()
        {
            bool cooldownComplete = (Time.time > nextReadyTime);
            if (cooldownComplete)
            {
                AbilityEnable();
                if (Input.GetButtonDown(abilityButtom))
                {
                    TriggerCooldown();
                }
            }
            else
            {
                CoolDown();
            }
        }
    }
}