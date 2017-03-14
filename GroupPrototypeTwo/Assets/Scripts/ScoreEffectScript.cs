using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEffectScript : MonoBehaviour {

    private Rigidbody rb;
    public float speed;
    public float seconds;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * Random.Range(150.0f, 250.0f));
        StartCoroutine(destroyObjectAfter(seconds));
    }

    IEnumerator destroyObjectAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
