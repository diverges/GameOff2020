using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ForceApplicator : MonoBehaviour
{
    private ForceSource[] forceSources;
    private new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        forceSources = FindObjectsOfType<ForceSource>();
    }

    // FixedUpdate is called with physics update
    void FixedUpdate()
    {
        foreach (var source in forceSources)
        {
            var force = source.GetForce(rigidbody);
            rigidbody.AddForce(force, ForceMode.Force);
        }
    }
}
