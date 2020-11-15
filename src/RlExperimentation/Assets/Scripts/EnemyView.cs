using System;
using System.Linq;
using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class EnemyView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [HideInInspector] public EnemyBase actor;
        public Text Name;
        public Text Health;
        public Text Intent;
        public Text IntentDescription;

        public GameObject tooltipPrefab;
        [HideInInspector] public GameObject tooltipInstance;

        void Update()
        {
            if(actor != null)
            {
                Name.text = actor.Instance.Name;
                Health.text = $"HP: {Math.Max(actor.Instance.CurrentHealth, 0)}";
                Intent.text = actor.IntentName;
                IntentDescription.text = string.Join(
                    "\r\n",
                    actor.Intent.Select(effect => effect.ToString()));
            }

            if(tooltipInstance != null)
            {
                var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0;
                tooltipInstance.transform.position = worldPosition;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (actor == null && actor.Instance)
            {
                return;
            }

            var worldPosition = eventData.pointerCurrentRaycast.worldPosition;
            tooltipInstance = Instantiate(tooltipPrefab, new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity, this.transform);
            var tooltip = tooltipInstance.GetComponent<Tooltip>();
            tooltip.Create(actor.Instance.GetTooltipDescription());
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
