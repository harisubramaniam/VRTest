using UnityEngine;
using System.Collections;

public class DetailPointMarkerController : MonoBehaviour {
	private WayPointNavigator WPNavigator;
	private GameObject Flame;
	private GameObject FlameLight;
	private bool IS_GAZED = false;

	void Start () {
		Transform flame_t = transform.FindChild ("Candle_Flame");
		Flame = flame_t.gameObject;
		Flame.SetActive (false);

		Transform light_t = transform.FindChild ("Candle_Light");
		FlameLight = light_t.gameObject;
		FlameLight.SetActive (false);

		WPNavigator = GameObject.Find ("Player").GetComponent<WayPointNavigator> ();
	}

	void Update () {
		bool dp_selected = WPNavigator.IsCurrentDetailPoint (transform.parent.gameObject);
		if (IS_GAZED || dp_selected) {
			Flame.SetActive (true);
			FlameLight.SetActive (true);
		} else {
			Flame.SetActive (false);
			FlameLight.SetActive (false);
		}
	}
	
	public void SetIsGazed(bool g) {
		IS_GAZED = g;
	}
}
