using Assets.Scripts.Core;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CaravanMember : MonoBehaviour
    {
        private const string healthFormat = "HP: {0}/{1}";
        [HideInInspector] public ActorBase actor;
        public Text actorName;
        public Text caravanClass;
        public Text health;
        public Image image;

        public GameObject tooltipPrefab;
        [HideInInspector] private GameObject tooltipInstance;
        public ActorAnimations animations;

        public void Update()
        {
            if (tooltipInstance != null)
            {
                var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0;
                tooltipInstance.transform.position = worldPosition;
            }

            if (this.actor == null)
            {
                return;
            }

            health.text = string.Format(healthFormat, actor.CurrentHealth, actor.MaxHealth);
        }

        public void SetCaravanMember(ActorBase actor)
        {
            this.actor = actor;
            var statusView = GetComponentInChildren<StatusView>();
            statusView.actor = actor;
            if (this.actor == null)
            {
                return;
            }

            actor.animations = this.animations;
            actorName.text = actor.Name;
            caravanClass.text = actor.Class.ToString();
            health.text = string.Format(healthFormat, actor.CurrentHealth, actor.MaxHealth);

            if(actor.sprite != null)
            {
                this.image.sprite = actor.sprite;
            }
        }

        public void OnMouseEnter()
        {
            if (actor == null)
            {
                return;
            }

            var worldPosition = Input.mousePosition;
            tooltipInstance = Instantiate(tooltipPrefab, new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity, this.transform);
            var tooltip = tooltipInstance.GetComponent<Tooltip>();
            tooltip.Create(actor.GetTooltipDescription());
        }

        public void OnMouseExit()
        {
            if (tooltipInstance == null)
            {
                return;
            }

            Destroy(tooltipInstance);
        }
    }
}
