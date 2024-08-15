using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ShipController : MonoBehaviour
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
            foreach (ShipBaseComponent child in GetComponentsInChildren<ShipBaseComponent>())
            {
                child.transform.SetParent(null, true);
                Debug.Log(child.name);
                Rigidbody childrb = child.gameObject.AddComponent<Rigidbody>();
                childrb.velocity = rb.velocity;
                childrb.useGravity = rb.useGravity;
            }
            Destroy(gameObject);
        }
    }
}
