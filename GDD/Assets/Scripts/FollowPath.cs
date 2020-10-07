using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class FollowPath : MonoBehaviour
{
    [HideInInspector]
    public PathCreator pathCreator;
    [HideInInspector]
    public EndOfPathInstruction endOfPathInstruction;
    [HideInInspector]
    public bool slowed = false;

    public float speed = 5;

    private float distanceTravelled;

    void Start()
    {
        slowed = false;
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }
    }

    void Update()
    {
        if(distanceTravelled >= pathCreator.path.length)
        {
            Destroy(gameObject);
            GameObject chateau = GameObject.FindGameObjectWithTag("Player");
            if(chateau != null)
            {
                chateau.GetComponent<Castle>().life -= 50;
            }
        }

        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }

    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}
