using UnityEngine;
using System.Collections;

public class Person : UnitBehavior {
	
	public bool isAlive = true;
	public float speed;
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
			Vector3 targetPoint = new Vector3(waypoint.transform.position.x, 0.5f, waypoint.transform.position.z);
			transform.position = Vector3.MoveTowards(transform.position, targetPoint, Time.deltaTime * speed);
			if (Mathf.Abs((transform.position - waypoint.transform.position).magnitude) < 0.1)
				waypoint = null;
		} else
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
	
	protected override void randomMove() {
		//TODO at this point a new waypoint should be added in a random direction not too far away from the person
		// I'm not sure how to do this, so I'm leaving it blank for now.
		//print("subclass move");
	}
}
