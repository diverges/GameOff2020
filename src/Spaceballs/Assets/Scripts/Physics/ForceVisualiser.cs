using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody))]
public class ForceVisualiser : MonoBehaviour
{
    private ForceSource[] forceSources;
    private new Rigidbody rigidbody;
    private float lengthScalar = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        forceSources = FindObjectsOfType<ForceSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var totalForce = Vector3.zero;
        
        foreach (var source in forceSources)
        {
            totalForce += source.GetForce(rigidbody);
        }

        Debug.DrawLine(transform.position, transform.position + (lengthScalar * totalForce));
    }
}
