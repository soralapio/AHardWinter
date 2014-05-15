using UnityEngine;
using System.Collections;

public class GameLoader : MonoBehaviour {

	private float startTimer;
	public Texture2D loadingPic;

	// Use this for initialization
	void Start () {

		//Do actual game start. For now...
	
	}
	
	// Update is called once per frame
	void Update () {

		startTimer += Time.deltaTime;

		if (startTimer > 3.0f)
		{

			Application.LoadLevel ("FarmGame");

		}
	
	}

	public void OnGUI()
	{

		GUI.DrawTexture (new Rect(Screen.width / 2 - loadingPic.width / 2, Screen.height / 2 - loadingPic.height / 2,
		                          loadingPic.width, loadingPic.height), loadingPic);

	}
}
