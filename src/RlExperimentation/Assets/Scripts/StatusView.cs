using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class StatusView : MonoBehaviour
    {
        [HideInInspector] public ActorBase actor;
        public GameObject nodePrefab;

        private Dictionary<string, GameObject> statuses = new Dictionary<string, GameObject>();

        public void Update()
        {
            if(this.actor)
            {
                foreach (var status in actor.GetStatus())
                {
                    if(statuses.ContainsKey(status.Name))
                    {
                        var node = statuses[status.Name];
                        var textElements = node.GetComponentsInChildren<Text>();
                        textElements[1].text = " " + status.Intensity.ToString();
                        continue;
                    }

                    var obj = Instantiate(nodePrefab, this.transform);
                    var textNodes = obj.GetComponentsInChildren<Text>();
                    textNodes[0].text = status.Name;
                    statuses.Add(status.Name, obj);
                    Canvas.ForceUpdateCanvases();
                    LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
                }

                var expiredKeys = new List<string>();
                foreach (var key in statuses.Keys)
                {
                    if (!actor.HasStatus(key))
                    {
                        expiredKeys.Add(key);
                    }
                }

                foreach (var key in expiredKeys)
                {
                    Destroy(statuses[key]);
                    statuses.Remove(key);
                }

                Canvas.ForceUpdateCanvases();
                LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
            }
        }
    }
}
