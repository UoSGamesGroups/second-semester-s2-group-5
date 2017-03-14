using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetalScript : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    private Vector3 endScale = new Vector3(0.25f,0.25f,0.25f);

	// Use this for initialization
	void Start () {
        StartCoroutine(destroyAfter(0.5f));
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(Random.Range(0.0f,1.0f), Random.Range(0.0f, 1.0f), 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.localScale = Vector3.Lerp(transform.localScale, endScale, speed * Time.deltaTime);
    }

    IEnumerator destroyAfter(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        Destroy(this.gameObject);
    }
}
