using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {

    //Renderer renderer;
    SpriteRenderer sr;
    public Sprite currSprite;
    BoxCollider box;

    public int hit;
    public bool changed;

    // Use this for initialization
    void Start () {
        //renderer = GetComponent<Renderer>();
        sr = GetComponent<SpriteRenderer>();
        currSprite = sr.sprite;
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
            onChange(col.gameObject.GetComponent<SpriteRenderer>().sprite, col.gameObject.GetComponent<PlayerScript>().player);
        }

        if (col.gameObject.tag.Equals("Score") && !changed)
        {
            onChange(col.gameObject.GetComponent<SpriteRenderer>().sprite, col.gameObject.GetComponent<CollisionScript>().hit);
        }
    }

    public void onChange(Sprite newSprite, int newPlayer)
    {
        sr.sprite = newSprite;
        hit = newPlayer;
        changed = true;
        ChangeNeighbors();
    }

    public void ChangeNeighbors()
    {
        Collider[] cols = Physics.OverlapBox(transform.position, box.bounds.size*0.60f, Quaternion.Euler(new Vector3()));
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag.Equals("Score"))
            {
                cols[i].gameObject.GetComponent<CollisionScript>().changed = true;

                if (cols[i].gameObject.GetComponent<CollisionScript>().hit != hit)
                {
                    cols[i].gameObject.GetComponent<CollisionScript>().onChange(GetComponent<SpriteRenderer>().sprite,hit);
                }
            }
        }
    }
}
