using UnityEngine;
using System.Collections;

public class ImageTileController : MonoBehaviour {

	public TextAsset imageAsset;


	void Start () {
		// Create a texture. Texture size does not matter, since
		// LoadImage will replace with with incoming image size.
		Texture2D tex = new Texture2D(2, 2);
		tex.LoadImage(imageAsset.bytes);
		GetComponent<Renderer>().material.mainTexture = tex;
	}
	

	void Update () {
	
	}
}
