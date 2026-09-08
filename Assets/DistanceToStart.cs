using FMODUnity;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class DistanceToStart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private Transform playerTransform;

    private Transform startGateTransform;

    private float maxDistanceToGate;

    private float distanceToGate;

    public float normalizedDistanceToGate;

    [ParamRef] public string distanceToGateParameter;

    void Start()
    {
        maxDistanceToGate = 15f;
        
         GameObject player = GameObject.FindWithTag("Player");

        if (!player)
        {
            Debug.LogError("Could not find game object with tag \"Player\"");
            enabled = false;
            return;
        }
        
        playerTransform = player.transform;


 GameObject startGate = GameObject.FindWithTag("StartGate");

        if (!startGate)
        {
            Debug.LogError("Could not find game object with tag \"Player\"");
            enabled = false;
            return;
        }

        startGateTransform = startGate.transform;

    }

    // Update is called once per frame
    void Update()
    {
        distanceToGate = Vector3.Distance(playerTransform.position,startGateTransform.position);
        normalizedDistanceToGate = (Mathf.Clamp01(distanceToGate / maxDistanceToGate)*100);
        RuntimeManager.StudioSystem.setParameterByName(distanceToGateParameter, normalizedDistanceToGate);

        //player.PlayerMovement.movementSpeed = (2*(normalizedDistanceToGate/100));
    }
}
