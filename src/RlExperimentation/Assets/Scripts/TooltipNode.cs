using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TooltipNode : MonoBehaviour
    {
        public Text header;
        public Text description;

        public void Set(string header, string description)
        {
            this.header.text = header;
            this.description.text = description;
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
        }
    }
}
