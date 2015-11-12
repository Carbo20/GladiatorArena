using UnityEngine;
using System.Collections;
using System.Text;

public class OppeningScreen : MonoBehaviour {

    private bool connect;

	// Use this for initialization
	void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        connect = false;
    }
	
	// Update is called once per frame
	void Update () {
	    


	}

    public void SwitchToPlayScene()
    {
        if (!connect)
        {
            Debug.Log("tentative de connexion...");
            MultiplayerController.Instance.SignInAndStartMPGame();


            if (MultiplayerController.Instance.signedInDone == true)
            {
                Debug.Log("Connexion réussie");
                connect = true;
                Application.LoadLevel("PlayScene");
            }
        }
        
        
    }

    public void SwitchToOptionScene()
    {
        Application.LoadLevel("OptionScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
