using UnityEngine;
using System.Collections;

public class Person : UnitBehavior {
	
	public bool isAlive = true;
	public float speed = 5; //I gave this an arbitrary default value because it was automatically set to 0 when the script was put on a new object
	public int health = 100;
	public float foodLevel = 100;
	public float waterLevel = 100;
	
	private GameObject waypoint = null;
	
	// Use this for initialization
	void Start () {
		// Change base class variables here before calling initialize
		base.updateTime = 2.0f;
		// Call initialize to call necessary base class functions
		base.initialize ();
	}
	
	// Update is called once per frame
	void Update () {
		if(waypoint != null)
		{
			unitState = State.MOVING;
			Vector3 targetPoint = new Vector3(waypoint.transform.position.x, 0.5f, waypoint.transform.position.z);
			transform.position = Vector3.MoveTowards(transform.position, targetPoint, Time.deltaTime * speed);
			if (Mathf.Abs((transform.position - waypoint.transform.position).magnitude) < 0.1)
			{
				waypoint = null;
				unitState = State.IDLE;
			}
		} else //TODO I believe this else clause is unnecessary. It's corresponding if checks to see if waypoint is not null, so there's no need to set waypoint to null again
		{
			waypoint = null;
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
		this.waypoint = waypoint;
	}
	
	//TODO fix this to work with new waypoint system
	public bool hasWaypoint()
	{
		return waypoint != null;
	}

	// Creates a new waypoint GameObject with a random position relatively close to the current position
	// Uses this GameObject to call setWaypoint
	protected override void randomMove() {
		GameObject newWaypoint = new GameObject(); //TODO For some reason this GameObject is not being deleted when player reaches waypoint
		newWaypoint.transform.position = new Vector3(this.transform.position.x + (Random.value * 20) - 10,
		                                             this.transform.position.y,
		                                             this.transform.position.z + (Random.value * 20) - 10);
		setWaypoint(newWaypoint); // Can also be this.waypoint = newWaypoint
		//UnityEngine.Object.Destroy(newWaypoint);
	}
}
