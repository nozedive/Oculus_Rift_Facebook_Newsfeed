using UnityEngine;
using System.Collections;

public class NewsFeedFabrication : MonoBehaviour {

	private static GameObject[] _images;
	private Texture2D[] _textList;

	private string[] _files;
	private string pathPrefix;

	private Vector3 input;

	// Use this for initialization
	void Start () {

		pathPrefix = @"file://";

		//change this to change pictures folder
		string path = @"/Users/derekseaton/Downloads/unity_images/";

		_files = System.IO.Directory.GetFiles (path, "*.png");
		Debug.Log (_files [1]);
		_images = new GameObject[_files.Length];

		Vector3 spawnspot;
		Debug.Log (_files.Length);
		for (int i = 0; i < _files.Length; i++) {
			float temp;
			float negative = 1;
			if (i%2 == 0)
				temp = i*10;
			else {
				temp = (i-1)*10;
				negative = -1;
			}
			spawnspot = new Vector3( temp, (float)2.5, (float)4.8*negative);
			GameObject picture = GameObject.CreatePrimitive(PrimitiveType.Cube);
			_images[i] = picture;
			_images[i].transform.localScale = new Vector3 (4f, 4f,0.2f);
			_images[i].transform.position += spawnspot;

			//Debug.Log ("it got made");
		}

		StartCoroutine (LoadImages ());
	}
	
	// Update is called once per frame
	void Update () {
		input = new Vector3( Input.GetAxis("Horizontal"), 0f, 0f);
		for (int i = 0; i < _images.Length; i++)
			_images [i].transform.Translate (input);
	}

	private IEnumerator LoadImages() {
		//load all images in default folder as textures and apply dynamically to plane game objects.
		_textList = new Texture2D[_files.Length];
		Debug.Log (_textList.Length);

		int dummy = 0;
		foreach(string tstring in _files) {
			string pathTemp = pathPrefix + tstring;
			WWW www = new WWW(pathTemp);
			yield return www;
			Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.ARGB32, false);

			www.LoadImageIntoTexture(texTmp);
			_textList[dummy] = texTmp;
			Debug.Log (texTmp);
			_images[dummy].renderer.material.mainTexture = _textList[dummy];
			if(dummy%2 == 0){
				_images[dummy].transform.Rotate(180, -180, 180);
			}

			dummy++;


		}
		Debug.Log ("textures used");
	}
}
