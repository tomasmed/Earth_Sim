using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public float contactTime;
	public bool isColliding;

	// Use this for initialization
	void Start () {
		contactTime = 0;
		isColliding = false;
	}

	void OnTriggerEnter(Collider col) {
		if(col.gameObject.CompareTag("Player")) {
			isColliding = true;
		}
	}

	void OnTriggerExit(Collider col) {
		if(col.gameObject.CompareTag("Player")) {
			isColliding = false;
			contactTime = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//If the player is within 5 units of the enemy, it will move towards the player at a speed of 2 units per second
		//These are just example values, they can/should be changed later
		if ((float)Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 5) {
			transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, 2*Time.deltaTime);
		}
		//Tracks how long the enemy has been in contact with the player
		if (isColliding) {
			contactTime += Time.deltaTime;
		}
		//For every 1 second the player remains in contact with the enemy, the tribe loses 1 population
		//These are example values, adjust later
		if (contactTime >= 1) {
				equation.population--;
				contactTime = 0;
		}
	}
}
