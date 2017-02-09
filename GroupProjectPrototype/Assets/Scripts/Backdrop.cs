using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backdrop : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        Vector3 pos = GetComponent<Transform>().position;
        pos.y = pos.y - 0.01f;
        transform.position = pos;
        if (transform.position.y < -20.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
