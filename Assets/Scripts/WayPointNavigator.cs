using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class WayPointNavigator : MonoBehaviour {

	public float moveSpeed = 2.1f;

	protected bool doMove = false;
	protected Vector3 targetPos;
	protected CharacterController PlayerController;
	protected GameObject PlayerCameras;
	protected GameObject CurrentWaypoint;
	protected GameObject CurrentWaypointMarker;
	protected GameObject CurrentDetailPoint;
	protected float targetY = 0.0f;
	protected float cameraYMoveSpeed = 2.5f;
	
	void Start () {
		PlayerController = gameObject.GetComponent<CharacterController>();
		PlayerCameras = GameObject.Find ("CardboardMain");
	}

	void Update () {

		if (targetY != 0.0f) {
			//move the camera y position
			Vector3 target_cameras_pos = new Vector3(PlayerCameras.transform.localPosition.x, targetY, PlayerCameras.transform.localPosition.z);
			PlayerCameras.transform.localPosition = Vector3.Lerp(PlayerCameras.transform.localPosition, target_cameras_pos, cameraYMoveSpeed * Time.deltaTime); 
		}

		if (doMove) {
			//move the player
			Vector3 player_pos = transform.position;
			Vector3 target_pos = new Vector3(targetPos.x, player_pos.y, targetPos.z);
			Vector3 offset = target_pos - player_pos;

			if (offset.magnitude > 0.5f) {
				offset = offset.normalized * moveSpeed;
				PlayerController.Move(offset * Time.deltaTime);
			} else {
				doMove = false;
			}
		}

	}

	public void MoveToWayPoint(BaseEventData data){
		doMove = true;
		PointerEventData pointerData = data as PointerEventData;
		GameObject CurrentWaypointMarker = pointerData.pointerCurrentRaycast.gameObject as GameObject;
		CurrentWaypoint = CurrentWaypointMarker.transform.parent.gameObject;
		targetPos = CurrentWaypointMarker.transform.position;

		targetY = 13.0f;

		//reset CurrentDetailPoint
		CurrentDetailPoint = null;
	}

	public void MoveToDetailPoint (Vector3 tPos, GameObject cDP){
		doMove = true;
		CurrentDetailPoint = cDP;
		targetPos = tPos;
		targetY = 3.0f;
	}

	public bool IsCurrentWaypoint(GameObject go){
		return go == CurrentWaypoint ? true : false;
	}

	public bool IsCurrentDetailPoint(GameObject go){
		return go == CurrentDetailPoint ? true : false;
	}
}
