using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DetailPointImageGalleryController : MonoBehaviour {
	public float GalleryWidth = 1.0f;
	public float GalleryScale = 1.0f;

	public float ExaminedImageScale = 2.0f;
	public float TransistionSpeed = 1.0f;

	public string GalleryPath;

	private float img_width = 0.9f;
	private float img_height = 0.6f;
	private float img_gutter = 0.9f;

	private GameObject CurrentExaminedImage;

	void Start () {
		if(GalleryPath == null)
			Debug.LogError("No GalleryPath provided. Use GalleryPath to point at the directory of images you want to display.");

		Sprite[] image_sprites = Resources.LoadAll<Sprite>(GalleryPath);

		int sprite_count = 0;
		//setup loop (col/rows) to build image wall
		//converting floats to ints here w/ "*0.1f"
		int col_count = (int)Mathf.Floor(GalleryWidth/(img_width*GalleryScale));
		int row_count = (int)Mathf.Ceil((image_sprites.Length*1.0f)/(col_count*1.0f));
		for (int y = 0; y < row_count; y++) {
			for (int x = 0; x <col_count; x++) {
				//stop building image wall if out of sprites
				//REFACTOR: for-loops into functions so you don't have to break like this
				if((sprite_count+1) > image_sprites.Length)
					break;

				Sprite i_s = image_sprites[sprite_count];
				GameObject ic = Instantiate(Resources.Load ("Prefabs/ImageCanvas"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;

				//make this image_canvas a child of this detail_point_image_gallery
				ic.transform.parent = transform;

				RectTransform rt = ic.GetComponent<RectTransform>();
				Image img = ic.GetComponent<Image>();
				ImageCanvasController img_controller = ic.GetComponent<ImageCanvasController>();

				//set origin_scale for gallery elements
				img_controller.SetOriginScale(GalleryScale);
				//set examine element animation params
				img_controller.SetTargetScale(ExaminedImageScale);
				img_controller.SetTransitionSpeed(TransistionSpeed);
				//load image_sprite n into this image_canvas instance
				img.sprite = i_s;

				//using "*1.0f" to convert int to float
				float newPosX = (x*1.0f) * (img_width * GalleryScale);
				float newPosY = (y*1.0f) * (img_height * GalleryScale);
				
				Vector3 newPos = new Vector3(newPosX, newPosY, 0.0f);
				
				rt.anchoredPosition3D = newPos;
				rt.sizeDelta = new Vector2(img_width, img_height);
				rt.localScale = new Vector3(GalleryScale, GalleryScale, GalleryScale);
				
				sprite_count++;
			}
		}
	}

	void Update () {
	
	}

	public void DoToggleExamineImage(BaseEventData data){
		PointerEventData pointerData = data as PointerEventData;
		GameObject clicked_image = pointerData.pointerCurrentRaycast.gameObject as GameObject;
		ImageCanvasController clicked_image_controller = clicked_image.GetComponent<ImageCanvasController>();

		//set target posx y
		clicked_image_controller.SetTargetX (0.3f);
		clicked_image_controller.SetTargetY (0.1f);

		//toggle the clicked image
		clicked_image_controller.DoToggleIsExamined ();

		//if another image is being examined, close it
		if (CurrentExaminedImage != null && CurrentExaminedImage != clicked_image){
			ImageCanvasController curr_img_controller = CurrentExaminedImage.GetComponent<ImageCanvasController>();
			if(curr_img_controller.GetIsExamined()) {
				curr_img_controller.DoToggleIsExamined();
			}
		}

		CurrentExaminedImage = clicked_image;
	}
}
