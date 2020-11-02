using UnityEngine;

public class GravitySource : ForceSource
{
    public float Mass;

    //private static float G = 6.67e-11f;
    private static float G = 1;

    private void Start()
    {
    }

    public override Vector3 GetForce(Rigidbody target)
    {
        var to = transform.position - target.transform.position;
        var radius = to.magnitude;

        if (radius == 0)
        {
            return Vector3.zero;
        }

        var scalar = ((G * Mass * target.mass) / (radius * radius * radius));
        
        return to * scalar;
    }
}
