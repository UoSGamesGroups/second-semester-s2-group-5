using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public int player;
    public GameObject levelController;
    public GameObject scorePrefab;

    private Vector3 mMouseDownPos;
    private Vector3 mMouseUpPos;
    private Rigidbody rigidbody;
    bool fired;
    private bool mouseDown;

    public float speed = .1f;
    public float playerMovementScale = 1.0f;
    public float gravScale = 1.0f;

    public GameObject TrajectoryPointPrefeb;
    private GameObject[] tPoints;
    public int numOfTrajectoryPoints = 10;

    public bool debugMode = false;


    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        fired = false;
        mouseDown = false;

        tPoints = new GameObject[numOfTrajectoryPoints];
        // TrajectoryPoints are instatiated
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            GameObject dot = (GameObject)Instantiate(TrajectoryPointPrefeb);
           // dot.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
            dot.GetComponent<Renderer>().enabled = false;
            tPoints[i] = dot;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (mouseDown && !fired)
        {
            Vector2 force = GetForceFrom(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            float angle = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
            placeTrajectoryPoints(transform.position, force / GetComponent<Rigidbody>().mass);
        }
    }

    private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        return (new Vector2(fromPos.x, fromPos.y) - new Vector2(toPos.x, toPos.y)) * (playerMovementScale-rigidbody.drag);
    }

    void OnMouseDown()
    {
        if (!fired)
        {
            mMouseDownPos = Input.mousePosition;
            mMouseDownPos = Input.mousePosition;
            mMouseDownPos.z = 0;
        }
        mouseDown = true;
    }

    void OnMouseUp()
    {
        mouseDown = false;
        if (!fired)
        {
            rigidbody.isKinematic = false;
            mMouseUpPos = Input.mousePosition;
            mMouseUpPos = Input.mousePosition;
            mMouseUpPos.z = 0;
            Vector3 direction = mMouseDownPos - mMouseUpPos;
            float magnitude = direction.magnitude * playerMovementScale;
            direction.Normalize();
            //float moveSpeed = Mathf.Min(speed, magnitude);

            rigidbody.AddForce(direction * magnitude);
            fired = true;
            if (!debugMode)
            {
                clearTrajectoryPoints();
            }
            StartCoroutine(levelController.GetComponent<LevelController>().spawnAfter(4.0f));
            StartCoroutine(changeObject(4.0f));
        }
    }

    void placeTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity)
    {
        // calc hypoth velocity
        float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float gameTime = 0;
        gameTime += 0.1f;

        //place each of the points, increasing game time each loop
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            float dx = (velocity-(rigidbody.drag * gameTime)) * gameTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * gameTime * Mathf.Sin(angle * Mathf.Deg2Rad) - ((Physics.gravity.magnitude* gravScale) * gameTime * gameTime / 2.0f);
            Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, 2);
            tPoints[i].transform.position = pos;
            tPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude * gravScale) * gameTime, pVelocity.x) * Mathf.Rad2Deg);
            tPoints[i].GetComponent<Renderer>().enabled = true;
            gameTime += 0.1f;
        }

    }
    void clearTrajectoryPoints()
    {
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            Destroy(tPoints[i].gameObject);
        }
    }

    IEnumerator changeObject(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        GameObject scoreCrate = Instantiate(scorePrefab, transform.position, transform.rotation);
        scoreCrate.GetComponent<CollisionScript>().hit = player;
        scoreCrate.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        scoreCrate.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
        Destroy(this.gameObject);
    }
}
