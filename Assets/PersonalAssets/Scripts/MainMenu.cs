using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class MainMenu : MonoBehaviour {

	public int screenIndex;
	public Texture[] screens, buttonTextures;
	public Rect[] buttonRects;
	public GUIStyle emptyStyle;
	
	void OnGUI(){
		GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), screens[screenIndex]);
		if (screenIndex == 0) {
			if(GUI.Button(buttonRects[0], buttonTextures[0], emptyStyle)){
				Application.LoadLevel(Application.loadedLevel+1);
			}
			if(GUI.Button(buttonRects[1], buttonTextures[1], emptyStyle)){
				screenIndex = 2;
			}
			if(GUI.Button(buttonRects[2], buttonTextures[2], emptyStyle)){
				screenIndex = 1;
			}
			if(GUI.Button(buttonRects[3], buttonTextures[3], emptyStyle)){
				Application.Quit();
			}
		}
		else if(screenIndex != 0){
			if(GUI.Button(buttonRects[3], buttonTextures[3], emptyStyle)){
				screenIndex = 0;
			}
		}
	}

}
