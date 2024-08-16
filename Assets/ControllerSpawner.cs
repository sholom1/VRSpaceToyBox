using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class ControllerSpawner : MonoBehaviour
{
    [SerializeField]
    private XRInputButtonReader inputButtonReader;
    [SerializeField]
    private GameObject shipPrefab;

    private void Update()
    {
        if (inputButtonReader.ReadWasPerformedThisFrame())
        {
            SpawnShip();
        }
    }

    private void SpawnShip()
    {
        Debug.Log("Spawned ship");
        Instantiate(shipPrefab, gameObject.transform.position, transform.rotation, null);
    }
}
