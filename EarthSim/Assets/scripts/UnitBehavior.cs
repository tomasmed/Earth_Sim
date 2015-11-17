// UnitBehavior.cs
// Contributors: Noah Martin-Ruben

using UnityEngine;
using System.Collections;

public class UnitBehavior : MonoBehaviour {


	// Member variables
	// To add different possible unit states, add to this enum
	public enum State {IDLE, MOVING, RUN_AWAY, PANIC, PLAYER_MOVE};
	// updateTime is the time in seconds that the unit takes to update it's priority	
	protected float updateTime = 10;
	// uniteState is the unit's current state
	public State unitState = State.IDLE;
	// Chance that a unit will randomly move, gets checked every frame
	// 0.02f is pretty frequent
	protected float randomMoveChance = 0.02f;

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
				if (Random.value < randomMoveChance) {
					randomMove();
				}
				break;
			case State.PLAYER_MOVE: break;
		}
	}

	// randomMove() is empty for a basic unit. Subclasses should implement this function with "protected override randomMove()"
	protected virtual void randomMove() {
		print("WARNING: UnitBehavior.randomMove should never be called!");
	}

	// 
	protected void updateState() {
		if (unitState == State.PLAYER_MOVE) return;
		unitState = State.IDLE;
		//print("State being updated");
	}

	public State getState() {
		return unitState;
	}
}
