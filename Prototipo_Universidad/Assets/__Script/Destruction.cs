using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{

	Transform t;
	Rigidbody2D r;
	public GameObject grid;
	bool end=false;
	
    void Start()
	{
		t = grid.transform;
		r = grid.GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
		if (end){
			r.MovePosition( new Vector3 (t.position.x,t.position.y-0.01f,t.position.z));
		}
    }

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag.Equals("Player")){
			end = true;
			Destroy(col.gameObject);
		}
	}

}
