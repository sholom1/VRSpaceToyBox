using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRRayInteractor))]
public class ControlShip : MonoBehaviour
{
    private XRRayInteractor rayInteractor;
    private void Start()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
    }

    private void Update()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            //Debug.Log(hit.collider.name);
            ShipBrain shipBrain = hit.collider.GetComponentInParent<ShipBrain>();
            if (shipBrain != null)
            {
                Debug.Log(shipBrain.name);
            }
        }
    }
}
