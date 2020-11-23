using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Tooltip : MonoBehaviour
    {
        public GameObject tooltipNodePrefab;

        /// <summary>
        /// Expect (header, description) tuple
        /// </summary>
        /// <param name="nodes"></param>
        public void Create(List<Tuple<string, string>> nodes)
        {
            foreach(var node in nodes)
            {
                var instance = Instantiate(tooltipNodePrefab, new Vector3(0, 0, -80), Quaternion.identity, transform);
                instance.GetComponent<TooltipNode>().Set(node.Item1, node.Item2);
            }
            var layout = this.GetComponent<VerticalLayoutGroup>();
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
        }
    }
}
