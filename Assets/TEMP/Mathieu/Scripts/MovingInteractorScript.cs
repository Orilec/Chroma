using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingInteractorScript : InteractorScript
{
    [SerializeField] private Transform finalPoint; 

    public override void SetInteractorActive()
    {

        base.SetInteractorActive();

        if (finalPoint != null)
        {
            StartCoroutine(MoveInteractor());
        }
        else
        {
            Debug.LogError("Renseignez un final point"); 
        }

    }


    IEnumerator MoveInteractor()
    {
        float timeElapsed = 0;
        Vector3 startPosition = transform.position; 

        while (timeElapsed < colorationTime)
        {
            var normalizedProgress = timeElapsed / colorationTime; // 0-1
            var easing = easeColorationCurve.Evaluate(normalizedProgress);

            transform.position = Vector3.Lerp(startPosition, finalPoint.transform.position, easing);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = finalPoint.transform.position; // Set final position

    }


    void OnDrawGizmosSelected()
    {


        // Draw a sphere and wireframe at the transform's position
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawSphere(finalPoint.position, maxRadius);
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawWireSphere(finalPoint.position, maxRadius);



    }

}
