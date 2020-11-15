﻿using Assets.Scripts.Core;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CaravanMember : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const string healthFormat = "HP: {0}/{1}";
        [HideInInspector] public ActorBase actor;
        public Text actorName;
        public Text caravanClass;
        public Text health;

        public GameObject tooltipPrefab;
        [HideInInspector] public GameObject tooltipInstance;

        public void Update()
        {
            if (this.actor == null)
            {
                return;
            }

            health.text = string.Format(healthFormat, actor.CurrentHealth, actor.MaxHealth);
        }

        public void SetCaravanMember(ActorBase actor)
        {
            this.actor = actor;
            if (this.actor == null)
            {
                return;
            }

            actorName.text = actor.Name;
            caravanClass.text = actor.Class.ToString();
            health.text = string.Format(healthFormat, actor.CurrentHealth, actor.MaxHealth);
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
