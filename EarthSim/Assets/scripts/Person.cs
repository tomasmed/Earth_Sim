using UnityEngine;
using System.Collections;

public class Person : UnitBehavior {
	
	public bool isAlive = true;
	public float speed = 5; //I gave this an arbitrary default value because it was automatically set to 0 when the script was put on a new object
	public int health = 100;
	public float foodLevel = 100;
	public float waterLevel = 100;
	
	private Vector3 waypoint = Vector3.zero;
	
	// Use this for initialization
	void Start () {
		// Change base class variables here before calling initialize
		base.updateTime = 2.0f;
		// Call initialize to call necessary base class functions
		base.initialize ();
	}
	
	// Update is called once per frame
	void Update () {
		if (waypoint != Vector3.zero) {
						unitState = State.MOVING;
						Vector3 targetPoint = new Vector3 (waypoint.x, 0.5f, waypoint.z);
						transform.position = Vector3.MoveTowards (transform.position, targetPoint, Time.deltaTime * speed);
						if (Mathf.Abs ((transform.position - waypoint).magnitude) < 0.1) {
								waypoint = Vector3.zero;
								unitState = State.IDLE;
						}
				}
		base.act();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Consumable"))
			Destroy(other.gameObject);
	}
	
	public void setWaypoint(GameObject waypoint)
	{
		this.waypoint = waypoint.transform.position;
	}

	public void setWaypoint(Vector3 waypoint)
	{
		this.waypoint = waypoint;
	}
	

	public bool hasWaypoint()
	{
		return waypoint != Vector3.zero;
	}

	// Creates a new waypoint GameObject with a random position relatively close to the current position
	// Uses this GameObject to call setWaypoint
	protected override void randomMove() {
		Vector3 newWaypoint = new Vector3();
		newWaypoint = new Vector3(this.transform.position.x + (Random.value * 20) - 10,
		                                             this.transform.position.y,
		                                             this.transform.position.z + (Random.value * 20) - 10);
		setWaypoint(newWaypoint);
	}
}
