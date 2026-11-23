using UnityEngine;
using System.Collections;

public class Pocket : MonoBehaviour
{
    [Header("Pocket settings")]
    public Transform dropPoint;
    
    public float maxCaptureSpeed = 15f;
    public float minSinkSpeed = 0.0008f;
    public float maxSinkSpeed = 0.035f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PoolBall")) return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;

        if (rb.linearVelocity.magnitude > maxCaptureSpeed)
        {
            rb.AddForce((Vector3.up + transform.forward) * 5f, ForceMode.Impulse);
            return;
        }

        StartCoroutine(PocketBallRoutine(other.gameObject, rb));
    }
    
    private IEnumerator PocketBallRoutine(GameObject ball, Rigidbody rb)
    {
        ball.tag = "PocketedBall";
    
        float ballSpeed = rb.linearVelocity.magnitude;

        float speedPercentage = Mathf.Clamp01(ballSpeed / maxCaptureSpeed);

        float sinkSpeed = Mathf.Lerp(minSinkSpeed, maxSinkSpeed, speedPercentage);
    
        Collider ballCol = ball.GetComponent<Collider>();
        if (ballCol != null) ballCol.enabled = false;
    
        rb.isKinematic = true;
    
        Vector3 dropTarget = dropPoint != null ? dropPoint.position : transform.position + Vector3.down * 0.25f;
    
        Vector3 holeCenter = new Vector3(dropTarget.x, ball.transform.position.y, dropTarget.z);
    
        while (ball != null && Vector3.Distance(ball.transform.position, holeCenter) > 0.01f)
        {
            ball.transform.position = Vector3.MoveTowards(
                ball.transform.position,
                holeCenter,
                sinkSpeed * Time.deltaTime
            );
            yield return null;
        }
    


        while (ball != null && Vector3.Distance(ball.transform.position, dropTarget) > 0.01f)
        {
            ball.transform.position = Vector3.MoveTowards(
                ball.transform.position,
                dropTarget,
                sinkSpeed * Time.deltaTime
            );
            yield return null;
        }
    
    
        if (ball != null)
        {
            ball.SetActive(false);
        }
    }
}