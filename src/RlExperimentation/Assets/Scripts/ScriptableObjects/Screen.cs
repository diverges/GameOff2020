
using System.Collections.Generic;
using UnityEngine;

public abstract class Screen : ScriptableObject
{
    public string Title;

    [TextArea]
    public string Description;

    public Screen Next;
}
