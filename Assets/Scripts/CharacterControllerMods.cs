using UnityEngine;
using System.Collections;

public class CharacterControllerMods : MonoBehaviour {

	public float gravityMod = -1.1f;

	//private CharacterController controller;

	void Start () {
		//controller = GetComponent<CharacterController>();
	}

	void Update () {
		//Vector3 moveVec = transform.position;
		//moveVec.y = -(1.1f * gravityMod);//-= gravityMod * Time.deltaTime;
		//controller.Move (moveVec * Time.deltaTime);
		transform.Translate(new Vector3(0f, -1.2f, 0f), Space.World);
		if (Terrain.activeTerrain != null) {
			float terrainHeight = Terrain.activeTerrain.SampleHeight(transform.position);
			if (transform.position.y < terrainHeight) transform.position = new Vector3(transform.position.x, terrainHeight, transform.position.z);
		}
	}
}
