using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public class DetailPointController : MonoBehaviour {
	private GameObject DetailPointMarker;
	private CharacterController PlayerController;
	private WayPointNavigator WPNavigator;
	private DetailPointMarkerController DPMController;
	private GameObject MemorialLabel;
	private GameObject MemorialPOV;


	void Start () {
		//get DetailPointMarker by tag name
		foreach(Transform tr in transform)
		{
			if(tr.tag == "DetailPointMarker")
			{
				DetailPointMarker = tr.gameObject;
			}
		}

		GameObject p = GameObject.FindWithTag ("Player");
		GameObject mc = GameObject.FindWithTag ("MainCamera");
		if (p) {
			PlayerController = p.GetComponent<CharacterController> () as CharacterController;
		} else {
			Debug.LogError("No GameObject with tag 'Player' exists. Make sure you have a Player GameComponent and it is tagged, 'Player'.");
		}

		DPMController = DetailPointMarker.GetComponent<DetailPointMarkerController> ();
		WPNavigator = PlayerController.GetComponent<WayPointNavigator>();

		MemorialPOV = transform.Find ("Memorial_POV").gameObject;

		//hide detailpoint label
		MemorialLabel = transform.Find ("Memorial_Label").gameObject;
		MemorialLabel.SetActive (false);

		//set-up gaze and click event for detailmarker
		EventTrigger trigger = DetailPointMarker.GetComponentInParent<EventTrigger>();
		EventTrigger.Entry entryClick = new EventTrigger.Entry();
		EventTrigger.Entry entryGazeOn = new EventTrigger.Entry ();
		EventTrigger.Entry entryGazeOff = new EventTrigger.Entry ();

		entryGazeOn.eventID = EventTriggerType.PointerEnter;
		entryGazeOff.eventID = EventTriggerType.PointerExit;
		entryClick.eventID = EventTriggerType.PointerClick;


		entryClick.callback.AddListener( (eventData) => { HandleClick(eventData); } );
		trigger.triggers.Add(entryClick);

		entryGazeOn.callback.AddListener( (eventData) => { HandleGazeOn(eventData); } );
		trigger.triggers.Add(entryGazeOn);
		
		entryGazeOff.callback.AddListener( (eventData) => { HandleGazeOff(eventData); } );
		trigger.triggers.Add(entryGazeOff);
	}

	void Update () {
		if (WPNavigator.IsCurrentDetailPoint (gameObject)) {
			MemorialLabel.SetActive (true);
		} else {
			MemorialLabel.SetActive (false);
		}
	}

	void HandleClick(BaseEventData data){
		WPNavigator.MoveToDetailPoint (MemorialPOV.transform.position, gameObject);
	}

	void HandleGazeOn(BaseEventData data){
		DPMController.SetIsGazed (true);
		//WPNavigator.MoveToWayPoint (data);
	}

	void HandleGazeOff(BaseEventData data){
		DPMController.SetIsGazed (false);
		//WPNavigator.MoveToWayPoint (data);
	}
}
