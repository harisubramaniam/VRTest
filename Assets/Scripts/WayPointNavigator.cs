using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class WayPointNavigator : MonoBehaviour {

	public float moveSpeed = 2.1f;

	protected bool doMove = false;
	protected Vector3 targetPos;
	protected CharacterController PlayerController;
	protected GameObject CurrentWaypoint;
	protected GameObject CurrentWaypointMarker;
	
	void Start () {
		PlayerController = gameObject.GetComponent<CharacterController>();
	}

	void Update () {
		if (doMove) {
			Vector3 player_pos = transform.position;//new Vector3 (targetX, transform.position.y, targetZ);
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
	}

	public bool IsCurrentWaypoint(GameObject go){
		return go == CurrentWaypoint ? true : false;
	}
}
