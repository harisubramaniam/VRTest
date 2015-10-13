using UnityEngine;
using System.Collections;

public class ImageCanvasController : MonoBehaviour {

	private float originScale = 1.0f;
	private float targetScale = 2.0f;
	private float originX = 0.0f;
	private float originY = 0.0f;
	private float originZ = 0.0f;
	private float targetX = 0.0f;
	private float targetY = 0.0f;
	private float targetZ = -0.5f;
	private float transistionSpeed = 0.9f;

	private bool IsExaminded = false;

	void Start () {
		//make references to images origin pos
		//always manipulate image via RectTransform
		RectTransform rt = gameObject.GetComponent<RectTransform>();
		originX = rt.anchoredPosition3D.x;
		originY = rt.anchoredPosition3D.y;
	}

	void Update () {
		RectTransform rt = gameObject.GetComponent<RectTransform>();

		//convert simple user properties to vector3s
		Vector3 t_s = new Vector3 (targetScale, targetScale, targetScale);
		Vector3 o_s = new Vector3 (originScale, originScale, originScale);

		if (IsExaminded && transform.localScale != t_s) {
			rt.localScale = Vector3.Lerp (rt.localScale, t_s, Time.deltaTime * transistionSpeed);
		} 
		else if(!IsExaminded && transform.localScale != o_s) {
			rt.localScale = Vector3.Lerp (rt.localScale, o_s, Time.deltaTime * transistionSpeed);
		}


		if (IsExaminded && rt.anchoredPosition3D.z != targetZ) {
			Vector3 targetPos = new Vector3(targetX, targetY, targetZ);
			rt.anchoredPosition3D = Vector3.Lerp (rt.anchoredPosition3D, targetPos, Time.deltaTime * transistionSpeed);
		} 
		else if(!IsExaminded && rt.anchoredPosition3D.z != originZ) {
			Vector3 originPos = new Vector3(originX, originY, originZ);
			rt.anchoredPosition3D = Vector3.Lerp (rt.anchoredPosition3D, originPos, Time.deltaTime * transistionSpeed);
		}
	}

	public void DoToggleIsExamined(){
		IsExaminded = (IsExaminded == false) ? true : false;

	}

	public void SetOriginScale(float os){
		originScale = os;
	}

	public void SetTargetScale(float ts){
		targetScale = ts;
	}

	public void SetTransitionSpeed(float ts){
		transistionSpeed = ts;
	}

	public void SetTargetX(float tx){
		targetX = tx;
	}

	public void SetTargetY(float ty){
		targetY = ty;
	}

	public void SetTargetZ(float tz){
		targetZ = tz;
	}

	public bool GetIsExamined(){
		return IsExaminded;
	}

}
