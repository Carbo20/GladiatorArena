using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour, MPLobbyListener
{
    private float ready;
    public GUISkin guiSkin;

    private bool _showLobbyDialog;
    private string _lobbyMessage;
    private bool connect;

    public void SetLobbyStatusMessage(string message)
    {
        _lobbyMessage = message;
    }

    void Start ()
    {
        // Authenticate localUser for multi network
        
        ready = 0;
        connect = false;

    }

    //Instructs the Main Menu to stop showing the lobby interface
    public void HideLobby()
    {
        _lobbyMessage = "";
        _showLobbyDialog = false;
    }

    void Update()
    {
        /*if (ready < 1f)
            ready += Time.deltaTime;
        else
            MultiplayerController.Instance.SignInAndStartMPGame();*/
    }

    public void OnGUI()
    {
        if (!connect)
        {
            _lobbyMessage = "Starting a multi-player game...";
            _showLobbyDialog = true;
            // MultiplayerController.Instance.mainMenu = this;
            MultiplayerController.Instance.lobbyListener = this;
            MultiplayerController.Instance.SignInAndStartMPGame();

            if (_showLobbyDialog)
            {
                GUI.skin = guiSkin;
                GUI.Box(new Rect(Screen.width * 0.25f, Screen.height * 0.4f, Screen.width * 0.5f, Screen.height * 0.5f), _lobbyMessage);
            }
            connect = true;
        }
    }

}