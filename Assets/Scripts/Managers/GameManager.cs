using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {

    private int nbPlayers;
    private int playerID;
    private int lifeAllowed;
    private float timeElapsed;
    private float timeOfGame;

    private bool gameOver;
    private GameObject canvas;

	// Use this for initialization
	void Start () {
	    nbPlayers = PlayerPrefs.GetInt("nbPlayers", 4);
        timeOfGame = PlayerPrefs.GetFloat("timeOfGame", -1);
        lifeAllowed = PlayerPrefs.GetInt("lifeAllowed", 3);
        playerID = PlayerPrefs.GetInt("playerID", 0);
        timeElapsed = 0;

        gameOver = false;
        canvas = GameObject.Find("Canvas");

        /*create moles */
        for (int i = 0; i < nbPlayers; i++)
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Mole")) as GameObject;
            if (i == playerID)
            {
                go.AddComponent<MoleController>();
            }
            go.GetComponent<MoleManager>().SetInitPosition(new Vector3(i *1.5f, 1, i*2f)); // TODO: position de depart dans le terrain
            go.GetComponent<MoleManager>().SetLife(lifeAllowed);
            go.GetComponent<MoleManager>().PlayerID = i;
        }
	}

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        checkEndGame();
	}

    private void checkEndGame()
    {
        MoleManager[] mm = GameObject.FindObjectsOfType<MoleManager>();

        if (!gameOver && mm.Length == 1)
        {
            /*pannel game over */
            GameObject go = Instantiate(Resources.Load("Prefabs/GameOverPanel")) as GameObject;
            go.transform.parent = canvas.transform;
            go.GetComponent<RectTransform>().offsetMax = new Vector2(-31, -206.5f);
            go.GetComponent<RectTransform>().offsetMin = new Vector2(31, 312.5f);
            
            float[] temps = new float[nbPlayers];
            int[] playersId = new int[nbPlayers];
            for (int i = 0; i < nbPlayers; i++)
            {
                playersId[i] = i;
                if (i == mm[0].PlayerID)
                    temps[i] = timeElapsed;
                else
                    temps[i] = PlayerPrefs.GetFloat("timeAlive" + i);

            }
            Array.Sort(temps, playersId);
            GameObject.Find("WinText").GetComponent<Text>().text = "Player " + playersId[0] + " wins !";
            string looseString = "";
            for (int i = 1; i < nbPlayers; i++)
            {
                looseString += (i+1) + "e. Player " + playersId[i] + "\n"; 
            }
            GameObject.Find("LooseText").GetComponent<Text>().text = looseString;
            GameObject.Find("QuitButton").GetComponent<Button>().onClick.AddListener(QuiButton);
            
            gameOver = true;

        }
    }

    public void QuiButton()
    {
        Application.LoadLevel("GameParameterScene");
    }
}
