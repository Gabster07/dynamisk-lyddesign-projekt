using FMODUnity;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using System.Collections;
using System.Collections.Generic;

public class DistanceToEnd : MonoBehaviour
{
    public SplineContainer spline;
    [ParamRef] public string distanceToEndParameter;

    private Transform playerTransform;

    [SerializeField] private CanvasGroup canvasgroup;

    private void Start()
    {
        if (!spline)
        {
            enabled = false;
            return;
        }
        
        GameObject player = GameObject.FindWithTag("Player");

        if (!player)
        {
            Debug.LogError("Could not find game object with tag \"Player\"");
            enabled = false;
            return;
        }
        
        playerTransform = player.transform;
        RuntimeManager.StudioSystem.setParameterByName(distanceToEndParameter, 1);

    }
    
    private void Update()
    {
        Vector3 point = playerTransform.position;
        float3 localPoint = spline.transform.InverseTransformPoint(point);
        SplineUtility.GetNearestPoint(spline.Spline, localPoint, out float3 _, out float t);
        RuntimeManager.StudioSystem.setParameterByName(distanceToEndParameter, Mathf.Clamp01(1 - t));
        if (Mathf.Clamp01(1 - t)<=0.25)
        {
            canvasgroup.alpha = (((1f - (Mathf.Clamp01(1 - t)))*4f) -3f);
        }
        else
        {
            canvasgroup.alpha = 0f;
        }
    }
}