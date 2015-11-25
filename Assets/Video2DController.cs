using UnityEngine;
using System.Collections;

public class Video2DController : MonoBehaviour {

	public MediaPlayerCtrl srcMediaPlayer;
	public string srcMedia;

	protected bool isRunContent = false;

	void Start () {
		srcMediaPlayer.UnLoad();
	}

	void Update () {
		if (isRunContent) {
			srcMediaPlayer.Play ();
		} else {
			srcMediaPlayer.Stop ();
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
