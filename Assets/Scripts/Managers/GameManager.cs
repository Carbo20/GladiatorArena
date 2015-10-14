﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {

    private int nbPlayers;
    public int playerID;
    private int lifeAllowed;
    private float timeElapsed;
    private float timeOfGame;

    private bool gameOver;
    private GameObject canvas;

    private Text timeTxt;
    private List<Color> colors;
    private List<string> names;

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

        colors = new List<Color>();
        colors.Add(Color.red);
        colors.Add(Color.blue);
        colors.Add(Color.green);
        colors.Add(Color.yellow);
        names = new List<string>();

        /*create moles */
        for (int i = 0; i < nbPlayers; i++)
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Mole")) as GameObject;
            if (i == playerID)
            {
                go.AddComponent<MoleController>();
            }
            go.GetComponent<MoleManager>().SetInitPosition(new Vector3(i *1.5f, 2, i*2f)); // TODO: position de depart dans le terrain
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
        go.GetComponent<RectTransform>().offsetMax = new Vector2(-31, -206);
        go.GetComponent<RectTransform>().offsetMin = new Vector2(31, 248);

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

        if (!gameOver && mm.Length == 1)
        {
            updateGameOverPanel(mm);
            gameOver = true;
        }

        if (!gameOver && timeOfGame != -1 && timeElapsed >= timeOfGame)
        {
            updateGameOverPanel(mm);
            gameOver = true;

        }
    }

    public void QuiButton()
    {
        Application.LoadLevel("GameParameterScene");
    }
}
