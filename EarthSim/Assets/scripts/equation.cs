﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class equation : MonoBehaviour {

	public int foodStored=50; //These values are initialized just for testing
	public int waterStored=40;
	public int population=1;
	public float birthThreshold;
	System.Random rng = new System.Random();
	public double timeElapsed = 0;
	int days = 0;

	//The UI component where the results of this script will be displayed
	Text textUI;

	void resourceComp() {
		foodStored -= population;
		waterStored -= population;
		if (foodStored < 0) { //To do: optimize
			foodStored = 0;
		}
		if (waterStored < 0) {
			waterStored = 0;
		}
	}

	//As of right now, the lesser of either food or water is treated as the "limiting" resource
	//All calculations will use this value and disregard the greater one
	int popChange() {
		//First, if there is no food/water, you will lose about a tenth of your population per day
		if (Mathf.Min(foodStored, waterStored) <= 0) {
			birthThreshold = 0;
			return -1-population/10;
		}else { //Otherwise, if you do have food/water...
			//If you have less food/water than population, your population will neither increase nor decrease
			if (Mathf.Min(foodStored, waterStored) < population) {
				return 0;
			}else { //If you have more food/water than population...
				//First, a birth threshold is calculated.
				//This is a number between 0 and 100, given by the formula:
				birthThreshold = (Mathf.Min((float)foodStored, (float)waterStored)-(float)population)/(float)population*100;
				//The threshold is capped at 100, this is attained when you have twice as much food/water as population
				if (birthThreshold > 100)
					birthThreshold = 100;
				//Next, a random number between 0 and 100 is generated
				//If the number is below the birth threshold, then you will gain population equal to about a tenth of your current population
				//You are guaranteed a population gain if you have at least twice as much food/water as population
				if (rng.NextDouble()*100 < birthThreshold) {
					return 1+population/10;
				}else { //Otherwise, you won't gain any population that day.
					return 0;
				}
			}
		}
	}
	
	// Use this for initialization
	void Start () {
		textUI = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
		//Right now, a day ticks every 2 seconds, this is just for testing
		if (timeElapsed >= 2) {
			timeElapsed=0;
			days++;
			population+=this.popChange(); //The population change function is called once a day
			this.resourceComp();
			textUI.text = "Population: " + population + "\n"
				+ "Birth threshold: " + birthThreshold + "\n"
					+ "Food: " + foodStored + "\n"
					+ "Water: " + waterStored + "\n";
		}


	}
}
