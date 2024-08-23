using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ShipBrain : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.sqrMagnitude > 1)
        {
            ShipBaseComponent[] children = GetComponentsInChildren<ShipBaseComponent>();
            foreach (ShipBaseComponent child in children)
            {
                child.transform.SetParent(null, true);
                Debug.Log(child.name);
                Rigidbody childrb = child.gameObject.AddComponent<Rigidbody>();
                childrb.velocity = rb.velocity;
                childrb.useGravity = rb.useGravity;
                childrb.mass = (1f/children.Length) * rb.mass;

                CustomGravity customGravity = child.gameObject.AddComponent<CustomGravity>();
                customGravity.SetGravityType(GravityType.Oribital);
            }
            Destroy(gameObject);
        }
    }
}
