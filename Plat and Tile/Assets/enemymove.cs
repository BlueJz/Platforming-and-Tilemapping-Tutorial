using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemymove : MonoBehaviour
{

    public Vector2 destination;
    public Vector2 Starting;

    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;
    // Start is called before the first frame update
    void Start()
    {
        //Starting = GetComponent<Vector3>();
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector2.Distance(Starting, destination);

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector2.Lerp(Starting, destination, Mathf.PingPong(fractionOfJourney, 1));
            
        }
    }


}
