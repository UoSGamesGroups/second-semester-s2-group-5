using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public int player;
    public GameObject levelController;
    public GameObject scorePrefab;

    Vector3 mMouseDownPos;
    Vector3 mMouseUpPos;
    Rigidbody rigidbody;
    bool fired;
    bool moving;

    public float speed = .1f;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        fired = false;
        moving = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (fired)
        {
            StartCoroutine(isMoving());
        }

        if (fired && !moving)
        {
            levelController.GetComponent<LevelController>().SpawnPlayer(player);
            GameObject scoreCrate = Instantiate(scorePrefab, transform.position, transform.rotation);
            scoreCrate.GetComponent<CollisionScript>().hit = player;
            scoreCrate.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
            Destroy(this.gameObject);
        }
    }

    void OnMouseDown()
    {
        if (!fired)
        {
            mMouseDownPos = Input.mousePosition;
            mMouseDownPos = Input.mousePosition;
            mMouseDownPos.z = 0;
        }
    }

    void OnMouseUp()
    {
        if (!fired)
        {
            rigidbody.isKinematic = false;
            mMouseUpPos = Input.mousePosition;
            mMouseUpPos = Input.mousePosition;
            mMouseUpPos.z = 0;
            var direction = mMouseDownPos - mMouseUpPos;
            direction.Normalize();
            rigidbody.AddForce(direction * speed);
            moving = true;
            fired = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (fired)
        {

        }
    }

    IEnumerator isMoving()
    {
        Vector3 sek0pos = transform.position;
        yield return new WaitForSeconds(1.0f);
        Vector3 sek1pos = transform.position;
        
        if(sek0pos == sek1pos)
        {
            moving = false;
        }
    }
}
