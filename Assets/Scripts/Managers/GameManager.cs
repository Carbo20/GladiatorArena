using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public class GameManager : MonoBehaviour, RealTimeMultiplayerListener
{

    private int nbPlayers;
    public int playerID;
    private int lifeAllowed;
    private float timeElapsed;
    private float timeOfGame;

    public bool gameOver;
    private GameObject canvas;

    private Text timeTxt;
    private List<Color> colors;
    private List<string> names;

    private Level level;
    private RealTimeMultiplayerListener listener;

    // Use this for initialization
    void Start () {
	    nbPlayers = PlayerPrefs.GetInt("nbPlayers", 4);
        timeOfGame = PlayerPrefs.GetFloat("timeOfGame", -1);
        lifeAllowed = PlayerPrefs.GetInt("lifeAllowed", 3);
        playerID = PlayerPrefs.GetInt("playerID", 0);
        timeElapsed = 0;

        gameOver = false;
        canvas = GameObject.Find("Canvas");
        timeTxt = GameObject.Find("TimeText").GetComponent<Text>();

        level = GameObject.FindGameObjectWithTag("Arena").GetComponent<ArenaManager>().level;

        colors = new List<Color>();
        colors.Add(Color.red);
        colors.Add(Color.blue);
        colors.Add(Color.green);
        colors.Add(Color.yellow);
        names = new List<string>();

       
       //listener = new MultiplayerController();
        //const int MinOpponents = 1, MaxOpponents = 1;//placeholder
       // const int GameVariant = 0;
       // PlayGamesPlatform.Instance.RealTime.CreateQuickGame(MinOpponents, MaxOpponents,
       //             GameVariant, this);

        /*create moles */
        for (int i = 0; i < nbPlayers; i++)
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Mole")) as GameObject;
            if (i == playerID)
            {
                go.AddComponent<MoleController>();
            }

            go.GetComponent<MoleManager>().SetInitPosition(level.GetInitalPosition(nbPlayers, i));
            go.GetComponent<MoleManager>().SetLife(lifeAllowed);
            go.GetComponent<MoleManager>().PlayerID = i;
            go.GetComponent<MoleManager>().Name ="Mole " + i;
            go.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Mole" + i) as Material;

            names.Add(go.GetComponent<MoleManager>().Name);
        }

 

    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            timeElapsed += Time.deltaTime;
            checkEndGame();
            updateTimetext();
        }
	}

    private void updateTimetext()
    {
        if (timeOfGame == -1)
            timeTxt.text = "∞";
        else
        {
            float timeLeft = timeOfGame - timeElapsed;
            if (timeLeft > 10) timeTxt.color = new Color(0, 0, 0);
            else if (timeLeft > 4 && timeLeft <= 10) timeTxt.color = new Color(1, 0.27f, 0);
            else timeTxt.color = new Color(1, 0, 0);
            string timeString = "";
            if ((int)timeLeft / 60 != 0)
            {
                timeString += (int)timeLeft / 60 + ":";
            }
            timeString += ((int)timeLeft % 60).ToString("D2"); ;
            timeTxt.text = timeString;
        }
    }

    private void updateGameOverPanel(MoleManager[] mm)
    {
        GameObject go = Instantiate(Resources.Load("Prefabs/GameOverPanel")) as GameObject;
        go.transform.parent = canvas.transform;
        go.GetComponent<RectTransform>().offsetMax = new Vector2(-20, -125);
        go.GetComponent<RectTransform>().offsetMin = new Vector2(20, 182);
        go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        if (mm.Length != 1)
        {
            GameObject.Find("GameOverText").GetComponent<Text>().text = "Time Over";
        }

        float[] temps = new float[nbPlayers];
        int[] playersId = new int[nbPlayers];

        for (int i = 0; i < nbPlayers; i++)
        {
            playersId[i] = i;
            temps[i] = PlayerPrefs.GetFloat("timeAlive" + i);

        }

        for (int i = 0; i < mm.Length; i++)
        {
            temps[mm[i].PlayerID] = timeElapsed;
        }

        Array.Sort(temps, playersId);
        Array.Reverse(temps);
        Array.Reverse(playersId);
       
        for (int i = 0; i < nbPlayers; i++)
        {
            Text t = GameObject.Find("Mole" + i + "Text").GetComponent<Text>();
            t.color = colors[playersId[i]];
            if (i < mm.Length)
                t.text = "1. " + names[playersId[i]];
            else
                t.text = (i + 1 - (mm.Length - 1)) + ".  " + names[playersId[i]];
        }
        for (int i = nbPlayers; i < 4; i++)
        {
            GameObject.Find("Mole" + i + "Text").SetActive(false);
        }

        GameObject.Find("QuitButton").GetComponent<Button>().onClick.AddListener(QuiButton);
    }

    private void checkEndGame()
    {
        MoleManager[] mm = GameObject.FindObjectsOfType<MoleManager>();

        if ((!gameOver && (mm.Length == 1 || mm.Length == 0)) || (!gameOver && timeOfGame != -1 && timeElapsed >= timeOfGame))
        {
            updateGameOverPanel(mm);
            gameOver = true;
        }
    }

    public void QuiButton()
    {
        Application.LoadLevel("GameParameterScene");
    }

    public void OnRoomSetupProgress(float percent)
    {
        throw new NotImplementedException();
    }

    public void OnRoomConnected(bool success)
    {
        throw new NotImplementedException();
    }

    public void OnLeftRoom()
    {
        throw new NotImplementedException();
    }

    public void OnParticipantLeft(Participant participant)
    {
        throw new NotImplementedException();
    }

    public void OnPeersConnected(string[] participantIds)
    {
        throw new NotImplementedException();
    }

    public void OnPeersDisconnected(string[] participantIds)
    {
        throw new NotImplementedException();
    }

    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
    {
        throw new NotImplementedException();
    }
}
