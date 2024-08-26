using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipThrusterComponent : MonoBehaviour
{
    public float thrustDuration;
    private float accelerationProgress;
    private Rigidbody targetRigidbody;
    [SerializeField]
    private MeshRenderer meshRenderer;
    private Color emissionColor;
    [SerializeField]
    private AnimationCurve thrustCurve;
    [SerializeField]
    private float accelerationScalar;
    // Start is called before the first frame update
    void Start()
    {
        targetRigidbody = GetComponentInParent<Rigidbody>();
        emissionColor = meshRenderer.material.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        thrustDuration = Mathf.Max(0, thrustDuration - Time.deltaTime);
        if (thrustDuration > 0)
        {
            accelerationProgress = Mathf.Clamp(accelerationProgress + Time.deltaTime * accelerationScalar, 0, 1);
        }
        else
        {
            accelerationProgress = Mathf.Clamp(accelerationProgress - Time.deltaTime * accelerationScalar, 0, 1);
        }
        meshRenderer.material.SetColor("_EmissionColor", emissionColor * (thrustCurve.Evaluate(accelerationProgress) * 20 - 10));
    }
    private void FixedUpdate()
    {
        if (targetRigidbody == null)
            targetRigidbody = GetComponent<Rigidbody>();
        targetRigidbody.AddForce(transform.forward * thrustCurve.Evaluate(accelerationProgress), ForceMode.Acceleration);
    }
}
