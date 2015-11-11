using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using GooglePlayGames.BasicApi.Multiplayer;

public class MainMenu : MonoBehaviour, MPLobbyListener
{
    private float ready;
    public GUISkin guiSkin;

    private bool _showLobbyDialog;
    private string _lobbyMessage;
    private bool connect;
    MultiplayerController multiplayerController;

    public void SetLobbyStatusMessage(string message)
    {
        _lobbyMessage = message;
    }

    void Start ()
    {
        // Authenticate localUser for multi network
        ready = 0;
        connect = false;

        multiplayerController = new MultiplayerController();
        MultiplayerController.Instance.MultiplayerConfigAndInit();
    }

    public void HideLobby()
    {

    }

    void Update()
    {
        if (connect)
        {
            GameObject.Find("Button").GetComponent<Button>().interactable = false;
        }
    }

    public void Connect()
    {
        if (!connect)
        {
            Debug.Log("tentative de connexion...");
            MultiplayerController.Instance.SignInAndStartMPGame();


            if (MultiplayerController.Instance.signedInDone == true)
            {
                Debug.Log("Connexion réussie");
                connect = true;
                
            }
        }


        //////if (!connect)
        //////{
        //////    // MultiplayerController.Instance.mainMenu = this;
        //////    MultiplayerController.Instance.lobbyListener = this;
        //////    MultiplayerController.Instance.SignInAndStartMPGame();

        //////    ////_lobbyMessage = "Starting a multi-player game...";
        //////    ////_showLobbyDialog = true;
        //////    ////if (_showLobbyDialog)
        //////    ////{
        //////    ////    GUI.skin = guiSkin;
        //////    ////    GUI.Box(new Rect(Screen.width * 0.25f, Screen.height * 0.4f, Screen.width * 0.5f, Screen.height * 0.5f), _lobbyMessage);
        //////    ////}
        //////    connect = true;
        //////}
    }

    public void CreateInvitGame()
    {
        MultiplayerController.Instance.CreateWithInvitationScreen();
    }

    public void ShowInvit()
    {
        PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(MultiplayerController.Instance);
    }

}