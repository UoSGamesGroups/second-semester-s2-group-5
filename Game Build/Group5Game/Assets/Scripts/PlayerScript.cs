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

    public GameObject TrajectoryPointPrefeb;
    private GameObject[] trajectoryPoints;
    public int numOfTrajectoryPoints = 10;

    public GameObject petal;
    public bool petalSpawn;


    // Use this for initialization
    void Start () {
        petalSpawn = true;

        levelController = GameObject.FindGameObjectWithTag("LevelController");
        rigidbody = GetComponent<Rigidbody>();
        fired = false;
        mouseDown = false;

        trajectoryPoints = new GameObject[numOfTrajectoryPoints];
        // TrajectoryPoints are instatiated
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            GameObject dot = (GameObject)Instantiate(TrajectoryPointPrefeb);
            dot.GetComponent<Renderer>().enabled = false;
            trajectoryPoints[i] = dot;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (mouseDown && !fired)
        {
            Vector2 force = GetForceFrom(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            force = force * playerMovementScale;
            float angle = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
            
            placeTrajectoryPoints(transform.position, force / GetComponent<Rigidbody>().mass);
        }

        if (fired && petalSpawn && rigidbody.velocity.magnitude > 2.5f)
        {
            StartCoroutine(petalDelay(0.05f));
            Instantiate(petal, new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), 0.5f), Quaternion.Euler(0.0f, 0.0f, Random.Range(-90.0f, 90.0f)));
            petalSpawn = false;
        }

    }

    IEnumerator petalDelay(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        petalSpawn = true;
    }

    private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        return (new Vector2(fromPos.x, fromPos.y) - new Vector2(toPos.x, toPos.y));
    }

    void OnMouseDown()
    {
        if (!fired)
        {
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
            clearTrajectoryPoints();
            rigidbody.isKinematic = false;
            mMouseUpPos = Input.mousePosition;
            mMouseUpPos.z = 0;

            Vector3 direction = mMouseDownPos - mMouseUpPos;
            float magnitude = direction.magnitude * playerMovementScale;
            direction.Normalize();

            rigidbody.AddForce(direction * magnitude);
            fired = true;

            StartCoroutine(levelController.GetComponent<LevelController>().spawnAfter(5.0f));
            StartCoroutine(changeObject(5.0f));
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
            float dx = velocity * gameTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * gameTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics.gravity.magnitude * gameTime * gameTime / 2.0f);
            Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, 2);
            trajectoryPoints[i].transform.position = pos;
            trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - Physics.gravity.magnitude * gameTime, pVelocity.x) * Mathf.Rad2Deg);
            trajectoryPoints[i].GetComponent<Renderer>().enabled = true;
            gameTime += 0.1f;
        }

    }
    void clearTrajectoryPoints()
    {
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            Destroy(trajectoryPoints[i].gameObject);
        }
    }

    IEnumerator changeObject(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        GameObject flower = Instantiate(scorePrefab, transform.position, transform.rotation);
        flower.GetComponent<CollisionScript>().playerNum = player;
        flower.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        flower.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
        Destroy(this.gameObject);
    }
}
