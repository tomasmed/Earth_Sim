using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {

    public static MainScript S;
    public bool isPaused = false;
    public bool gameOver = false;

    public float cameraScrollSpeed;
    public float cameraScrollZoneSize;

    public GameObject person;
    public GameObject waypoint; //The waypoint prefab to create

    public void awake()
    {
        S = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        getInput();
        moveCamera();

	}

    void getInput()
    {

        if (Input.GetKeyDown("escape"))
        {
            isPaused = !isPaused;
            Time.timeScale = (isPaused) ? 0 : 1;
        }
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(transform.position, ray.direction, out hit, 100))
            {
                waypoint.transform.position = hit.point;
                Person[] p = GameObject.FindObjectsOfType<Person>();
                for (int i = 0; i < p.Length; i++)
                {
                    p[i].setWaypoint(waypoint);
                }
            }
        }
    }

    void moveCamera()
    {
        int width = Screen.width;
        int height = Screen.height;
        Vector3 mousePosition = Input.mousePosition;
        float normalizedX = (width - mousePosition.x) / (float)width;
        float normalizedY = (height - mousePosition.y) / (float)height;
        if (normalizedX > 1 - cameraScrollZoneSize)
        {
            transform.position += new Vector3(Mathf.Abs(0.5f - normalizedX) * -cameraScrollSpeed * Time.deltaTime, 0, 0);
            //transform.Translate(new Vector3(Mathf.Abs(0.5f - normalizedX) * -cameraScrollSpeed * Time.deltaTime, 0, 0));
        }
        else if (normalizedX < cameraScrollZoneSize)
        {
            transform.position += new Vector3(Mathf.Abs(0.5f - normalizedX) * cameraScrollSpeed * Time.deltaTime, 0, 0);
            //transform.Translate(new Vector3(Mathf.Abs(0.5f - normalizedX) * cameraScrollSpeed * Time.deltaTime, 0, 0));
        }

        if (normalizedY > 1 - cameraScrollZoneSize)
        {
            transform.position += new Vector3(0, 0, Mathf.Abs(0.5f - normalizedY) * -cameraScrollSpeed * Time.deltaTime);
            //transform.Translate(new Vector3(0, Mathf.Abs(0.5f - normalizedY) * -cameraScrollSpeed * Time.deltaTime, 0));
        }
        else if (normalizedY < cameraScrollZoneSize)
        {
            transform.position += new Vector3(0, 0, Mathf.Abs(0.5f - normalizedY) * cameraScrollSpeed * Time.deltaTime);
            //transform.Translate(new Vector3(0, Mathf.Abs(0.5f - normalizedY) * cameraScrollSpeed * Time.deltaTime, 0));
        }
    }
}
