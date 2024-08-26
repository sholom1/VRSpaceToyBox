using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ShipBrain : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    //private XRGrabInteractable grabInteractable;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //grabInteractable = GetComponent<XRGrabInteractable>();
        //grabInteractable.coll
        //grabInteractable.colliders.Clear();
        //ShipBaseComponent[] children = GetComponentsInChildren<ShipBaseComponent>();
        //foreach (ShipBaseComponent child in children)
        //{
        //    if (child is ShipWindUpComponent)
        //        continue;
        //    grabInteractable.colliders.AddRange(child.GetComponentsInChildren<Collider>());
        //}
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
                Rigidbody childrb = child.GetComponent<Rigidbody>();
                childrb = childrb == null ? child.gameObject.AddComponent<Rigidbody>() : childrb;
                childrb.isKinematic = false;
                childrb.velocity = rb.velocity;
                childrb.useGravity = rb.useGravity;
                childrb.mass = (1f/children.Length) * rb.mass;

                CustomGravity customGravity = child.gameObject.AddComponent<CustomGravity>();
                customGravity.SetGravityType(GravityType.Oribital);

                if (child is ShipWindUpComponent)
                {
                    child.enabled = false;
                    child.GetComponent<LineRenderer>().enabled = false;
                    child.GetComponent<XRGrabInteractable>().enabled = false;
                }
            }
            Destroy(gameObject);
        }
    }
}
