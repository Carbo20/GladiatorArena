using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System;
using GooglePlayGames.BasicApi.Multiplayer;

public class MultiplayerController : RealTimeMultiplayerListener
{
    private uint minOpponents = 1;
    private uint maxOpponents = 1;
    private uint gameVariation = 0;

    public MPLobbyListener lobbyListener;
    public static MultiplayerController _instance = null;
    public MultiplayerController()
    {
        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
               /* // enables saving game progress.
                .EnableSavedGames()
                // registers a callback to handle game invitations received while the game is not running.
                .WithInvitationDelegate(< callback method >)
                // registers a callback for turn based match notifications received while the
                // game is not running.
                .WithMatchDelegate(< callback method >)*/
         //       .Build();

        //PlayGamesPlatform.InitializeInstance(config);
        //Debug mode
        PlayGamesPlatform.DebugLogEnabled = true;
        
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

       // SignInAndStartMPGame();

    }

   public static MultiplayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MultiplayerController();
            }
            return _instance;
        }
    }
    
    // Authenticate localUser for multi network
    public void SignInAndStartMPGame()
    {
       
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) => {
                if (success)
                {
                    Debug.Log("We're signed in! Welcome " + PlayGamesPlatform.Instance.localUser.userName);
                    // We could start our game now
                   
                    Application.LoadLevel("DavidSceneWithNetwork");
                }
                else
                {
                    Debug.Log("Oh... we're not signed in.");
                } 
            });

            //StartMatchMaking();//to do when the user is correctly logged
            //LoadLevel();

        }
        else
        {
            Debug.Log("You're already signed in.");
            // We could also start our game now
        }
    }

    void LoadLevel()
    {
        Application.LoadLevel("DavidSceneWithNetwork");
    }

    private void StartMatchMaking()
    {
        PlayGamesPlatform.Instance.RealTime.CreateQuickGame(minOpponents, maxOpponents, gameVariation, this);
    }

    private void ShowMPStatus(string message)
    {
        Debug.Log(message);
        if (lobbyListener != null)
        {
            lobbyListener.SetLobbyStatusMessage(message);
        }
    }
    //Indicates the progress of setting up your room
    public void OnRoomSetupProgress(float percent)
    {
        ShowMPStatus("We are " + percent + "% done with setup");
    }

    //Check when we’ve successfully connected to the room
    public void OnRoomConnected(bool success)
    {
        if (success)
        {
            ShowMPStatus("We are connected to the room! I would probably start our game now.");
            lobbyListener.HideLobby();
            lobbyListener = null;
            LoadLevel();
        }
        else
        {
            ShowMPStatus("Uh-oh. Encountered some error connecting to the room.");
        }
    }

    //Signals that your player has successfully exited a multiplayer room
    public void OnLeftRoom()
    {
        ShowMPStatus("We have left the room. We should probably perform some clean-up tasks.");
    }

    public void OnParticipantLeft(Participant participant)
    {
        throw new NotImplementedException();
    }

    //Signals when one or more players joins the room to which the local player is currently connected
    public void OnPeersConnected(string[] participantIds)
    {
        foreach (string participantID in participantIds)
        {
            ShowMPStatus("Player " + participantID + " has joined.");
        }
    }

    //Signals that one or more players have left the room
    public void OnPeersDisconnected(string[] participantIds)
    {
        foreach (string participantID in participantIds)
        {
            ShowMPStatus("Player " + participantID + " has left.");
        }
    }

    //Receives gameplay data from any player in the room, this handles all of the multiplayer traffic
    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
    {
        ShowMPStatus("We have received some gameplay messages from participant ID:" + senderId);
    }
}
