using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    public List<Target> reachableTargets;
    public float minReachDistance = 70;
    
    public Target currentTarget;
    
   [Range(0.0f, 0.5f)] public float VerticalAimTreshold;
   [Range(0.0f, 0.5f)] public float HorizontalAimTreshold;
   
   
    private void Update()
    {
        if (reachableTargets.Count < 1)
        {
            currentTarget = null;
            return;
        }
        currentTarget = reachableTargets[TargetIndex()];
    }


    public int TargetIndex()
    {
        //Creates an array where the distances between the target and the screen/player will be stored
        float[] distances = new float[reachableTargets.Count];

        //Populates the distances array with the sum of the Target distance from the screen center and the Target distance from the player
        for (int i = 0; i < reachableTargets.Count; i++)
        {

            distances[i] = (Vector3.Distance(transform.position, reachableTargets[i].transform.position));
        }

        //Finds the smallest of the distances
        float minDistance = Mathf.Min(distances);

        int index = 0;

        //Find the index number relative to the target with the smallest distance
        for (int i = 0; i < distances.Length; i++)
        {
            if (minDistance == distances[i])
                index = i;
        }

        return index;

    }
    
}
