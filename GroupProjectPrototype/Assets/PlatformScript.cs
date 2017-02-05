using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //GetComponent<Rigidbody>().AddForce(0, -300, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody>().velocity = new Vector3(0, -1.0f);
        Vector3 pos = GetComponent<Transform>().position;
        pos.y = pos.y - 0.01f;
        transform.position = pos;
        if (transform.position.y < -10.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
