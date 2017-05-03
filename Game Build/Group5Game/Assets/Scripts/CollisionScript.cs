using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {

    SpriteRenderer sr;
    public Sprite currSprite;
    BoxCollider box;

    public int playerNum;
    public bool changeThisRound;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        currSprite = sr.sprite;
        box = GetComponent<BoxCollider>();
        changeThisRound = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            onChange(col.gameObject.GetComponent<SpriteRenderer>().sprite, col.gameObject.GetComponent<PlayerScript>().player);
        }

        if (col.gameObject.tag.Equals("Score"))
        {
            if (!changeThisRound && col.gameObject.GetComponent<CollisionScript>().changeThisRound)
            {
                onChange(col.gameObject.GetComponent<SpriteRenderer>().sprite, col.gameObject.GetComponent<CollisionScript>().playerNum);
            }
        }
    }

    public void onChange(Sprite newSprite, int newPlayer)
    {
        sr.sprite = newSprite;
        playerNum = newPlayer;
        changeThisRound = true;
        ChangeNeighbors();
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "MovingPlatform")
        {
            transform.parent = col.transform;
            Debug.Log("yes");
        }
        Debug.Log("yes");
    }


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    public void ChangeNeighbors()
    {
        Collider[] cols = Physics.OverlapBox(transform.position, box.bounds.size*0.60f, Quaternion.Euler(new Vector3()));
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.tag.Equals("Score"))
            {
                cols[i].gameObject.GetComponent<CollisionScript>().changeThisRound = true;

                if (cols[i].gameObject.GetComponent<CollisionScript>().playerNum != playerNum)
                {
                    cols[i].gameObject.GetComponent<CollisionScript>().onChange(GetComponent<SpriteRenderer>().sprite,playerNum);
                }
            }
        }
    }
}
