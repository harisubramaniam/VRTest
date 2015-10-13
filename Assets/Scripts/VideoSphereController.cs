
using UnityEngine;
using System.Collections;

public class VideoSphereController : MonoBehaviour {	
	
	public MediaPlayerCtrl srcMediaPlayer;
	public string srcMedia;
	
	public float yOpen = 2.7f;
	public float yClosed = 0.12f;
	public Vector3 scaleClosed = new Vector3 (0.5f, 0.5f, 0.5f);
	public Vector3 scaleOpen = new Vector3 (9.0f, 9.0f, 9.0f);
	public float scaleSpeed = 0.9f;
	
	protected bool isRunContent = false;

	void Start () {
		srcMediaPlayer.UnLoad();
	}
	
	void Update () {
		
		/*
	 * srcMediaPlayer.Load("EasyMovieTexture.mp4");
	 * srcMediaPlayer.Play();
	 * srcMediaPlayer.Stop();
	 * srcMediaPlayer.Pause();
	 * srcMediaPlayer.UnLoad();
	 * 
	 * */

		if (isRunContent) {
			srcMediaPlayer.Play ();
		} else {
			srcMediaPlayer.Stop ();
		}
		
		if (isRunContent && transform.localScale != scaleOpen) {
			transform.localScale = Vector3.Lerp (transform.localScale, scaleOpen, Time.deltaTime * scaleSpeed);
		} 
		else if(!isRunContent && transform.localScale != scaleClosed) {
			transform.localScale = Vector3.Lerp (transform.localScale, scaleClosed, Time.deltaTime * scaleSpeed);
		}
		
		if(isRunContent && transform.localPosition.y < yOpen){
			transform.Translate(Vector3.up * Time.deltaTime * scaleSpeed, Space.World);
		} 
		else if(!isRunContent && transform.localPosition.y > yClosed) {
			transform.Translate(Vector3.down * Time.deltaTime * scaleSpeed, Space.World);
		}
	}
	
	public void DoToggleVideo(){
		if (isRunContent) {
			isRunContent = false;
			srcMediaPlayer.UnLoad();
		} else if (!isRunContent) {
			isRunContent = true;
			srcMediaPlayer.Load(srcMedia);
		}
	}
}
