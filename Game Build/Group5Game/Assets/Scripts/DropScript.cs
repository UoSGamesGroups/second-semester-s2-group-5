using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour {

	void Update () {
		if(this.transform.position.y < 0)
        {
            Destroy(this.gameObject);
        }
	}
}
