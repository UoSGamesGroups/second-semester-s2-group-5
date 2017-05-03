using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript : MonoBehaviour {

    public GameObject[] waypoints;
    public float speed;
    private int currentPoint;

	// Use this for initialization
	void Start () {
        currentPoint = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if(waypoints != null)
        {
            moveToWaypoint();
        }
	}

    private void moveToWaypoint()
    {
        if((this.transform.position - waypoints[currentPoint].transform.position).magnitude < 0.50f){
            currentPoint++;
            if(currentPoint == waypoints.Length)
            {
                currentPoint = 0;
            }
        }

        this.transform.position = Vector3.Lerp(this.transform.position, waypoints[currentPoint].transform.position, Time.deltaTime * speed);
    }
}
