using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System;
using GooglePlayGames.BasicApi.Multiplayer;
using System.Text;

public enum MessageCode
{
    MessagePlayer, MessageSpell, GamePref
};

public class MultiplayerController : RealTimeMultiplayerListener
{
    public uint minOpponents = 1;
    public uint maxOpponents = 3;
    public uint gameVariation = 0;
    public bool signedInDone = false;

    public MPLobbyListener lobbyListener;
    public static MultiplayerController _instance = null;

    private System.Action<bool> mAuthCallBack;
    private bool showingWaitingRoom = false;

    

    public MultiplayerController()
    {
        AuthenticationCallBack();
        
       // SignInAndStartMPGame();
    }

    public void MultiplayerConfigAndInit()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            // registers a callback to handle game invitations received while the game is not running.
        .WithInvitationDelegate(InvitationReceived)
            // registers a callback for turn based match notifications received while the
            // game is not running.
        .Build();

        //PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
    }


   public static MultiplayerController Instance //////////////////////////
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
        bool silent = false;

        //Debug.Log("user auth : " + Social.localUser.authenticated);

        if (!signedInDone)
        {
            Debug.Log("Started signin in...");
            PlayGamesPlatform.Instance.Authenticate(mAuthCallBack);

            //////CreateWithInvitationScreen();

            //StartMatchMaking();//to do when the user is correctly logged
            //LoadLevel();
        }
        else
        {
            Debug.Log("You're already signed in.");
            // We could also start our game now
        }
    }

    void AuthenticationCallBack()
    {
        mAuthCallBack = (bool success) =>
        {
            Debug.Log("in auth callback success = " + success);
            signedInDone = true;

            //signedin = false
            if (success)
            {
                Debug.Log("We're signed in! Welcome " + PlayGamesPlatform.Instance.localUser.userName);
                signedInDone = true;
            }
            else
            {
                Debug.Log("Oh... we're not signed in.");
            }
        };
    }


    public void InvitationReceived(GooglePlayGames.BasicApi.Multiplayer.Invitation invitation, bool yesOrNo)
    {
        Debug.Log("Tou have been invited on a game of MOOOOOOLES");
    }

    public void CreateWithInvitationScreen()
    {
        maxOpponents = (uint)(PlayerPrefs.GetInt("nbPlayers", 4) - 1);
        PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(minOpponents, maxOpponents, gameVariation, this);
    }

    void LoadLevel()
    {
        SendPreferences(PlayerPrefs.GetInt("lifeAllowed", 3), PlayerPrefs.GetFloat("timeOfGame", -1),PlayerPrefs.GetInt("nbPlayers", 4));
        Application.LoadLevel("DavidScene");
    }

    /////////////////////////////////////////////////////////////////////////////
    private void SendPreferences(int lifeAllowed, float timeOfGame, int NbPlayers)
    {
        Message msg = new Message(); // GAMEPREF#lifeAllowed#timeOfGame
        bool reliable = true;

        msg.GamePrefMessage(lifeAllowed, timeOfGame, NbPlayers);

        Debug.Log(msg.GetByteMessage());
        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(reliable, msg.GetByteMessage());
    }
    //////////////////////////////////////////////////////////////////////

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
        if (!showingWaitingRoom)
        {
            showingWaitingRoom = true;
            Debug.Log("participant id " + PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId);
            PlayerPrefs.SetInt("playerID", int.Parse(PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId));
            PlayGamesPlatform.Instance.RealTime.ShowWaitingRoomUI();
        }
    }

    //Check when we’ve successfully connected to the room
    public void OnRoomConnected(bool success)
    {
        if (success)
        {
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
        Message msg = new Message();
        msg.SetByteMessage(data);
        msg.ConvertFromByte();

        Debug.Log("Message type " + msg.code + " received");
        switch (msg.code)
        {
            case MessageCode.GamePref:
                PlayerPrefs.SetInt("lifeAllowed", msg.lifeAllowed);
                PlayerPrefs.SetFloat("timeOfGame", msg.timeOfGame);
                PlayerPrefs.SetInt("nbPlayers", msg.nbPlayers);
                break;
            case MessageCode.MessagePlayer:
                GameObject []moles = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject mole in moles)
                {
                    if (msg.PlayerId == mole.GetComponent<MoleManager>().PlayerID)
                    {
                        mole.GetComponent<MoleManager>().SetPosition(msg.playerPos, msg.isShielded);
                    }
                }
                break;
            case MessageCode.MessageSpell:
                GameObject.Find("GameManager").GetComponent<GameManager>().CastSpellReceived(msg.SpellPos, msg.SpellDir);
                break;
        }
    }
}
