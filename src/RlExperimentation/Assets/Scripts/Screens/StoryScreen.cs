using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Screens
{
    public class StoryScreen : MonoBehaviour
    {
        public Text title;
        public Text body;
        public Text submitText;

        [HideInInspector] public Scenarios scenario;

        public void Set(string title, string description, string submitText)
        {
            this.title.text = title;
            this.body.text = description;
            this.submitText.text = submitText;
        }

        public void OnConfirm()
        {
            scenario.OnStoryScreenContinue();
            Destroy(this.gameObject);
        }

    }
}
