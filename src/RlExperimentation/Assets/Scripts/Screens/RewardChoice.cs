using Assets.Scripts.ScriptableObjects;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Screens
{
    public class RewardChoice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Text Name;
        public Text Class;
        public Text Health;
        public Text Backpack;

        [HideInInspector] public Scenarios scenario;
        [HideInInspector] public ActorBase actor;
        [HideInInspector] public GameObject container;

        public GameObject tooltipPrefab;
        [HideInInspector] private GameObject tooltipInstance;

        public void Set(GameObject parent, Scenarios scenario, ActorBase actor)
        {
            this.actor = actor;
            this.scenario = scenario;
            this.container = parent;

            this.Health.text = $"HP: {actor.CurrentHealth}/{actor.MaxHealth}";
            this.Name.text = actor.Name;
            this.Class.text = actor.Name;
            this.Backpack.text = string.Join(", ", actor.Backpack
                .GroupBy(card => card.Name)
                .Select(group => $"{group.Key} (x{group.Count()})"));
        }

        public void Start()
        {
            if(actor == null || scenario == null)
            {
                this.gameObject.SetActive(false);
            }
        }

        public void OnMouseClick()
        {
            scenario.OnRewardPick(this.actor);
            Destroy(this.container);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (actor == null)
            {
                return;
            }

            var worldPosition = eventData.pointerCurrentRaycast.worldPosition;
            tooltipInstance = Instantiate(tooltipPrefab, new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity, this.transform);
            var tooltip = tooltipInstance.GetComponent<Tooltip>();
            tooltip.Create(actor.GetTooltipDescription());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (tooltipInstance == null)
            {
                return;
            }

            Destroy(tooltipInstance);
        }
    }
}
