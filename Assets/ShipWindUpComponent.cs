using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (LineRenderer))]
public class ShipWindUpComponent : ShipBaseComponent
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private Transform attachPoint;
    private ShipBrain shipBrain;
    private ShipThrusterComponent[] shipThrusters;
    private float deviation;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        shipBrain = GetComponentInParent<ShipBrain>();
        shipThrusters = shipBrain.GetComponentsInChildren<ShipThrusterComponent>();
    }
    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPositions(new Vector3[] { transform.position, attachPoint.position });
        deviation = Vector3.Distance(transform.position, attachPoint.position);
    }
    public void Release()
    {
        transform.position = attachPoint.position;
        transform.rotation = attachPoint.rotation;
        foreach (ShipThrusterComponent thruster in shipThrusters)
        {
            thruster.thrustDuration += deviation * 10;
        }
    }
}
