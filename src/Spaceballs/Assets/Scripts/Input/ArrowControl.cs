using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    public float RotationSpeed = 1.0f;
    public float StrengthSpeed = 1.0f;

    public float ImpactStrength = 1.0f;

    public float MinImpactStrength = 1.0f;
    public float MaxImpactStrength = 5.0f;

    public float MinImpactScale = 1.0f;
    public float MaxImpactScale = 2.0f;

    public Rigidbody Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rotationInput = Input.GetAxis("Horizontal");

        if (rotationInput != 0)
        {
            transform.Rotate(Vector3.up, rotationInput * RotationSpeed * Time.deltaTime);
        }

        var strengthInput = Input.GetAxis("Vertical");

        if (strengthInput != 0)
        {
            // Update impact strength
            ImpactStrength += strengthInput * StrengthSpeed * Time.deltaTime;
            ImpactStrength = Mathf.Clamp(ImpactStrength, MinImpactStrength, MaxImpactStrength);

            // Match arrow scale
            var ratio = (ImpactStrength - MinImpactStrength) / (MaxImpactStrength - MinImpactStrength);
            var scale = ratio * (MaxImpactScale - MinImpactScale) + MinImpactScale;
            transform.localScale = new Vector3(1, 1, scale);
        }

        if (Input.GetButton("Jump"))
        {
            Rigidbody.constraints = RigidbodyConstraints.None;
            Rigidbody.AddForce(-transform.forward * ImpactStrength, ForceMode.Impulse);
            gameObject.SetActive(false);
        }
    }
}
