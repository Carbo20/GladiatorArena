using UnityEngine;
using System.Collections;

public class OppeningScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    


	}

    public void SwitchToPlayScene()
    {
        //Put The play scene here
    }

    public void SwitchToOptionScene()
    {
        Application.LoadLevel("GameParameterScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
