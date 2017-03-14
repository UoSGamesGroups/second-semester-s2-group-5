using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {

    Renderer renderer;
    BoxCollider box;

    public int hit;
    public bool changed;

    // Use this for initialization
    void Start () {
        renderer = GetComponent<Renderer>();
        box = GetComponent<BoxCollider>();
        changed = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            renderer.material = col.gameObject.GetComponent<Renderer>().material;
            hit = col.gameObject.GetComponent<PlayerScript>().player;
            ChangeNeighbors();
            changed = true;
        }

        if (col.gameObject.tag.Equals("Score") && changed)
        {
            col.gameObject.GetComponent<Renderer>().material = renderer.material;
            col.gameObject.GetComponent<CollisionScript>().hit = hit;
            ChangeNeighbors();
        }
    }

    public void ChangeNeighbors()
    {
        Collider[] cols = Physics.OverlapBox(transform.position, box.bounds.size, Quaternion.Euler(new Vector3()));
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag.Equals("Score"))
            {
                cols[i].gameObject.GetComponent<Renderer>().material = renderer.material;
                cols[i].gameObject.GetComponent<CollisionScript>().hit = hit;
                cols[i].gameObject.GetComponent<CollisionScript>().changed = true;

                if (cols[i].gameObject.GetComponent<CollisionScript>().hit != hit)
                {
                    cols[i].gameObject.GetComponent<CollisionScript>().ChangeNeighbors();
                }
            }
        }
    }
}
