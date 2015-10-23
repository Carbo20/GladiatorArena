using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System;

public class MultiplayerController
{

    public static MultiplayerController _instance = null;

    public MultiplayerController()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
               /* // enables saving game progress.
                .EnableSavedGames()
                // registers a callback to handle game invitations received while the game is not running.
                .WithInvitationDelegate(< callback method >)
                // registers a callback for turn based match notifications received while the
                // game is not running.
                .WithMatchDelegate(< callback method >)*/
                .Build();

        PlayGamesPlatform.InitializeInstance(config);
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
            LoadLevel();
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

}
