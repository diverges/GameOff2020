using UnityEngine;

public abstract class ForceSource : MonoBehaviour
{
    public abstract Vector3 GetForce(Rigidbody target);
}
