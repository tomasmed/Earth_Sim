﻿// UnitBehavior.cs
// Contributors: Noah Martin-Ruben

using UnityEngine;
using System.Collections;

public class UnitBehavior : MonoBehaviour {


	// Member variables
	// To add different possible unit states, add to this enum
	public enum State {IDLE, RUN_AWAY, PANIC};
	// updateTime is the time in seconds that the unit takes to update it's priority	
	protected float updateTime = 10;
	// uniteState is the unit's current state
	public State unitState = State.IDLE;

	// Use this for initialization
	void Start () {
		// All Start() does is call initialize()
		initialize ();
	}

	// Added initialize() so child classes wouldn't have to "extend" or "new" start
	protected void initialize() {
		// The InvokeRepeating line makes the updateState function run every updateTime seconds
		InvokeRepeating("updateState", 0, updateTime);
		//print ("parent start");
	}
	
	// Update is called once per frame
	void Update() {
		// All Update() does is call act()
		act ();
	}

	// Added act() so child classes wouldn't have to "extend" or "new" update
	protected void act () {
		switch (unitState) {
			case State.IDLE:
				if (Random.value < 0.02f) {
					randomMove();
				}
				break;
		}
	}

	// randomMove() is empty for a basic unit. Subclasses should implement this function with "protected override randomMove()"
	protected virtual void randomMove() {
		print("WARNING: UnitBehavior.randomMove should never be called!");
	}

	protected void updateState() {
		
		unitState = State.IDLE;
		//print("State being updated");
	}
}
