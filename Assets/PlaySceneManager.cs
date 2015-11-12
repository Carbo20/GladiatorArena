using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class PlaySceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void QuickMatch()
    {
        Debug.Log("Quick Match");
    }

    public void CreateGame()
    {
        Debug.Log("Create Game");
        Application.LoadLevel("GameParameterScene");
    }

    public void ShowInvits()
    {
        Debug.Log("Invitations");
        PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(MultiplayerController.Instance);
    }

    public void Back()
    {
        Debug.Log("Back");
        Application.LoadLevel("OppeningScreen");
    }
}
