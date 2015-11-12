using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
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

    /*intro anim*/
    private int introStep;
    private int introStepCount;
    public bool introIsPlaying;
    private float[] introStepTimes = {7f, 11f, 13f, 17f };
    private GameObject nacelle, nacelleFloor;
    private List<Vector3> initPosOnnacelle;
    private List<GameObject> moles;
    private QuickCutsceneController QCSController;
    private bool cutScenePlayed;
    GameObject getReadyImg, goImg;

    private Level level;

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

        /*intro anim*/
        introStep = 0;
        introStepCount = 4;
        introIsPlaying = true;
        nacelle = GameObject.Find("nacelle");
        nacelleFloor = GameObject.Find("nacellefloor");
        initPosOnnacelle = new List<Vector3>();
        initPosOnnacelle.Add(new Vector3(2.5f, 30.8f, 2.5f));
        initPosOnnacelle.Add(new Vector3(2.5f, 30.8f, -2.5f));
        initPosOnnacelle.Add(new Vector3(-2.5f, 30.8f, 2.5f));
        initPosOnnacelle.Add(new Vector3(-2.5f, 30.8f, -2.5f));
        moles = new List<GameObject>();
        QCSController = GameObject.Find("BaseCutscene").GetComponent<QuickCutsceneController>();
        cutScenePlayed = false;
        getReadyImg = GameObject.Find("GetReadyImage");
        goImg = GameObject.Find("GoImage");
        getReadyImg.SetActive(false);
        goImg.SetActive(false);

        /*create moles */
        for (int i = 0; i < nbPlayers; i++)
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Mole")) as GameObject;
            if (i == playerID)
            {
                go.AddComponent<MoleController>();
            }

            go.GetComponent<MoleManager>().SetInitPosition(initPosOnnacelle[i]);

            go.GetComponent<Rigidbody>().Sleep();
            //go.GetComponent<MoleManager>().SetInitPosition(level.GetInitalPosition(nbPlayers, i));
            go.GetComponent<MoleManager>().SetLife(lifeAllowed);
            go.GetComponent<MoleManager>().PlayerID = i;
            go.GetComponent<MoleManager>().Name = "Mole " + i;
            go.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Mole" + i) as Material;

            moles.Add(go);
            names.Add(go.GetComponent<MoleManager>().Name);
        }

        updateTimetext();
	}

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (introIsPlaying)
        {
            PlayIntro();
        }
        /*else
        {
            if (goImg.activeInHierarchy)
                goImg.SetActive(false);
        }*/

        if(!introIsPlaying && !gameOver)
        {
            
            checkEndGame();
            updateTimetext();
        }
	}

    private void PlayIntro()
    {
        if (!cutScenePlayed)
        {
            QCSController.ActivateCutscene();
            cutScenePlayed = true;
        }
        switch (introStep)
        {
            case 0:
                
                nacelle.transform.position = Vector3.Lerp(new Vector3(0, 30, 0), new Vector3(0, -10, 0), timeElapsed / introStepTimes[introStep]);
                nacelle.transform.Rotate(Vector3.up, 10);
                break;
            case 1:
                for (int i = 0; i < nbPlayers; i++)
                {

                    moles[i].transform.position = Vector3.Lerp(new Vector3(initPosOnnacelle[i].x, 2, initPosOnnacelle[i].z), level.GetInitalPosition(nbPlayers, i), (timeElapsed - introStepTimes[introStep - 1]) / (introStepTimes[introStep] - introStepTimes[introStep - 1]));
                }
                nacelle.transform.Rotate(Vector3.up, 10);
                break;
            case 2:
                nacelle.transform.position = Vector3.Lerp(new Vector3(0, -10, 0), new Vector3(0, -15, 0), (timeElapsed - introStepTimes[introStep - 1]) / (introStepTimes[introStep] - introStepTimes[introStep - 1]));
                break;
            case 3:
                Destroy(nacelle);
                if (goImg.activeInHierarchy)
                    goImg.SetActive(false);
                introIsPlaying = false;
                timeElapsed = 0;
                break;
        }

        if (timeElapsed >= 6.65 && timeElapsed < 11f)
        {
            if (!getReadyImg.activeInHierarchy)
                getReadyImg.SetActive(true);
        }
        if (timeElapsed >= 11f && timeElapsed < 12.1)
        {
            if (getReadyImg.activeInHierarchy)
                getReadyImg.SetActive(false);
            if (!goImg.activeInHierarchy)
                goImg.SetActive(true);
       }
       if (timeElapsed >= 11.9)
           goImg.GetComponent<Image>().color = new Color(goImg.GetComponent<Image>().color.r, goImg.GetComponent<Image>().color.g, goImg.GetComponent<Image>().color.b, goImg.GetComponent<Image>().color.a - Time.deltaTime*4);
 
        if (introStep < introStepCount)
        {
            if (timeElapsed >= introStepTimes[introStep])
            {
                introStep++;
            }
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
        go.GetComponent<RectTransform>().offsetMax = new Vector2(-120, 0);
        go.GetComponent<RectTransform>().offsetMin = new Vector2(120, 0);
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

}
