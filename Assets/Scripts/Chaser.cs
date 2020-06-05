using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]

public class Chaser : MonoBehaviour {
	
	private float speed = 30.0f;
	public float minDist = 1f;
	public Transform target;

	// Use this for initialization
	void Start () 
	{
		 speed = Random.Range(10.0f, 50.0f); // get random speed values everytime

		// if no target specified, assume the player
		if (target == null) {

			if (GameObject.FindWithTag ("Player")!=null)
			{
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (target == null)
			return;

		// face the target
		transform.LookAt(target);

		//get the distance between the chaser and the target
		float distance = Vector3.Distance(transform.position,target.position);

		//so long as the chaser is farther away than the minimum distance, move towards it at rate speed.
		if(distance > minDist)
			transform.position += transform.forward * speed * Time.deltaTime;	
	}

	// Set the target of the chaser
	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
	}

}
