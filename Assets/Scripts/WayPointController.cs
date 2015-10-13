using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class WayPointController : MonoBehaviour {
	
	private GameObject WaypointMarker;
	private Camera GazeCam;
	private CharacterController PlayerController;
	
	private float lastCamDistance = 0.0f;
	//private Renderer rend = null;
	private WayPointNavigator WPNavigator;
	//private Material wpMaterialInactive;
	//private Material wpMaterialActive;
	private List<GameObject> DetailPoints = new List<GameObject>();

	
	
	void Start () {
		//get this waypoint's marker
		WaypointMarker = transform.Find ("WaypointMarker").gameObject;

		GameObject p = GameObject.FindWithTag ("Player");
		GameObject mc = GameObject.FindWithTag ("MainCamera");
		if (p) {
			PlayerController = p.GetComponent<CharacterController> () as CharacterController;
		} else {
			Debug.LogError("No GameObject with tag 'Player' exists. Make sure you have a Player GameComponent and it is tagged, 'Player'.");
		}

		if (mc) {
			GazeCam = mc.GetComponent<Camera>() as Camera;
		} else {
			Debug.LogError("No GameObject with tag 'MainCamera' exists. Make sure you have a MainCamera GameComponent and it is tagged, 'MainCamera'.");
		}

		WPNavigator = PlayerController.GetComponent<WayPointNavigator>();

		//set all child DetailPoints invisible
		foreach(Transform child in transform){
			if(child.CompareTag("DetailPoint")){
				DetailPoints.Add(child.gameObject);
				child.gameObject.SetActive(false);
			}
		}

		//set-up click eventfor waypointMarker
		EventTrigger trigger = WaypointMarker.GetComponentInParent<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerClick;
		entry.callback.AddListener( (eventData) => { HandleClick(eventData); } );
		trigger.triggers.Add(entry);
	}
	
	void Update () {
		//round cam_distance to hundredths
		float cam_distance = Mathf.Round (Vector3.Distance (WaypointMarker.transform.position, GazeCam.transform.position) * 100f) / 100;
		
		//update scale relative to distance from gazeCam
		if(cam_distance != lastCamDistance){
			float scale_multiplier = cam_distance / 10;
			float new_scale = 1 + scale_multiplier;
			
			WaypointMarker.transform.localScale = new Vector3 (new_scale, new_scale, 1);
		}
		
		//update position to face camera
		WaypointMarker.transform.LookAt (GazeCam.transform.position);
		WaypointMarker.transform.Rotate (0.0f, 180.0f, 0.0f);
		lastCamDistance = cam_distance;
		
		/*
		//if Player headed this way
		*/
		if (WPNavigator.IsCurrentWaypoint (gameObject)) {
			//rend.material = wpMaterialActive;
			foreach (GameObject dp in DetailPoints) {
				dp.SetActive (true);
			}
		} else {
			foreach (GameObject dp in DetailPoints) {
				dp.SetActive (false);
			}
		}
	}

	void HandleClick(BaseEventData data){
		WPNavigator.MoveToWayPoint (data);
	}
	
}
