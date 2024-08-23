using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    private static List<CustomGravity> gravitySources = new List<CustomGravity>();

    [SerializeField]
    private GravityType gravityType = GravityType.None;

    [Tooltip("The strength of the gravity force. Only applicable for Planet gravityType")]
    public float gravityStrength = 9.81f;

    private Rigidbody rigidbody;

    private void Start()
    {
        if (gravityType == GravityType.Planet)
        {
            gravitySources.Add(this);
        }
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        if (gravityType == GravityType.Planet)
        {
            gravitySources.Remove(this);
        }
    }

    private void FixedUpdate()
    {
        if (gravityType == GravityType.Oribital)
        {
            foreach (CustomGravity gravitySource in gravitySources)
            {                
                Vector3 gravityDirection = (gravitySource.transform.position - transform.position).normalized;
                rigidbody.AddForce(gravityDirection * gravitySource.gravityStrength * rigidbody.mass, ForceMode.Acceleration);
            }
        }
    }

    public void SetGravityType(GravityType targetType)
    {
        if (gravityType == GravityType.Planet)
        {
            gravitySources.Remove(this);
        }
        if (targetType == GravityType.Planet)
        {
            gravitySources.Add(this);
        }
        gravityType = targetType;
    }
}

public enum GravityType
{
    None,
    Planet,
    Oribital,
}
