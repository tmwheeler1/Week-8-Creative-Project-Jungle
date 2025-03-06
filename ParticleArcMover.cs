using UnityEngine;

public class ParticleArcMover : MonoBehaviour
{
    public float speed = 5f; // Horizontal movement speed
    public float amplitude = 2f; // Height of the arc
    public float frequency = 1f; // Frequency of the arc
    public Vector3 startPosition;
    public Vector3 endPosition;

    private float journeyLength;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition, endPosition);
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;

        Vector3 newPos = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
        newPos.y += Mathf.Sin(fractionOfJourney * Mathf.PI) * amplitude;

        transform.position = newPos;

        if (fractionOfJourney >= 1f)
        {
            Destroy(gameObject); // Destroy the particle when it reaches the end
        }
    }
}

