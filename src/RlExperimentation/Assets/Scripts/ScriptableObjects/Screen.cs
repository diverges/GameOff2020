
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public abstract class ScreenBase : ScriptableObject
    {
        public string Title;

        [TextArea]
        public string Description;

        public ScreenBase Next;
    }
}

