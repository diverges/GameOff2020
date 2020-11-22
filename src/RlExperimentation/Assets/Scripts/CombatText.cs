using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CombatText : MonoBehaviour
    {
        public static void Create(Transform transform, int damageAmount, bool isHeal)
        {
            var instance = Instantiate(
                GameAssets.Instance.CombatText,
                transform.position,
                Quaternion.identity,
                transform);
            var combatText = instance.GetComponent<CombatText>();
            combatText.Setup(damageAmount, isHeal);
        }

        private Text textBox;
        private Animator animator;

        public void Setup(int damage, bool isHeal)
        {
            this.textBox.text = damage.ToString();
            this.textBox.color = (isHeal) ? Color.green : Color.red;
        }

        public void Awake()
        {
            textBox = GetComponentInChildren<Text>();
            animator = GetComponent<Animator>();
        }

        public void Start()
        {
            animator.Play("CombatText");
        }
    }
}
