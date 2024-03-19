using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class Trail : MonoBehaviour
{
    [SerializeField] private CinemachineDollyCart dollyCart;
    [SerializeField] private CinemachineSmoothPath dollyTrack;
    [SerializeField] private KioskObject kioskObject;
    

    private float travelTime;
    [SerializeField] private AnimationCurve easeColorationCurve;

    private float pathLength;



    private void Awake()
    {
        SetParameters();
    }


    public void SetTrailActive()
    {
        StartCoroutine(StartPath());
    }



    IEnumerator StartPath()
    {
        float timeElapsed = 0;


        while (timeElapsed < travelTime)
        {
            var normalizedProgress = timeElapsed / travelTime; // 0-1
            var easing = easeColorationCurve.Evaluate(normalizedProgress);
            dollyCart.m_Position = Mathf.Lerp(0, pathLength, easing);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        dollyCart.m_Position = pathLength; // Set final position

    }


    public void SetParameters()
    {
        pathLength = dollyTrack.PathLength;
        dollyCart.m_Position = 0f;
        travelTime = kioskObject.DelayBeforeAppear; 
    }
}
