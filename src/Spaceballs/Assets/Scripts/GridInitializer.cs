using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridInitializer : MonoBehaviour
{
    public Vector3 Size;
    public Vector3Int Count;

    public GameObject Prefab;

    private GameObject[] nodes;
    private bool dirty = true;

    // Start is called before the first frame update
    void OnValidate()
    {
        dirty = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(dirty)
        {
            DeleteAllChildren();
            CreateGridChildren();

            dirty = false;
        }
    }

    void DeleteAllChildren()
    {
        if (nodes != null)
        {
            foreach (var node in nodes)
            {
                GameObject.DestroyImmediate(node);
            }

            nodes = null;
        }
    }

    void CreateGridChildren()
    {
        var newNodes = new List<GameObject>();

        for (int i = 0; i < Count.x; i++)
        {
            for (int j = 0; j < Count.y; j++)
            {
                for (int k = 0; k < Count.z; k++)
                {
                    var node = GameObject.Instantiate(Prefab, transform);

                    var x = (Size.x * i) / Count.x;
                    var y = (Size.y * j) / Count.y;
                    var z = (Size.z * k) / Count.z;

                    node.transform.localPosition = new Vector3(x, y, z);
                    newNodes.Add(node);
                }
            }
        }

        nodes = newNodes.ToArray();
    }
}
