using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [Serializable]
    [CreateAssetMenu(fileName = "DefaultCard", menuName = "Card", order = 2)]
    public class CardBase : ScriptableObject
    {
        public string Name;
        public Sprite Sprite;
        [TextArea]
        public string Description;
        public List<Effect> Effects;
        [HideInInspector] public GameObject instance;
    }
}
