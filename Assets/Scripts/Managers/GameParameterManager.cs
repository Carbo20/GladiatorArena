using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameParameterManager : MonoBehaviour {

    private int nbPlayers;
    private int nbPlayersMax;

    private int nbLife;
    private int nbLifeMax;

    private int timeID;
    private List<float> times;
    private int nbTimeOption;
    private string[] timeTxts = {"∞", "30s", "1min", "2min", "5min"};

    private Text playerText, lifeText, timeText;

    // Use this for initialization
	void Start () {
        times = new List<float>();
        times.Add(-1);
        times.Add(30);
        times.Add(60);
        times.Add(120);
        times.Add(300);

        nbPlayersMax = 4;
        nbLifeMax = 5;
        nbTimeOption = 4;

        nbPlayers = PlayerPrefs.GetInt("nbPlayers", 2);
        nbLife = PlayerPrefs.GetInt("lifeAllowed", 1);
        timeID = times.IndexOf(PlayerPrefs.GetFloat("timeOfGame", -1));

        playerText = GameObject.Find("NbPlayerSelecter/ValueText").GetComponent<Text>();
        lifeText = GameObject.Find("LifeSelecter/ValueText").GetComponent<Text>();
        timeText = GameObject.Find("TimeSelecter/ValueText").GetComponent<Text>();

        updateLifeText();
        updateTimeText();
        updateNbPlayerText();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void updateLifeText()
    {
        lifeText.text = nbLife.ToString();
    }

    public void updateTimeText()
    {
        timeText.text = timeTxts[timeID];
    }

    public void updateNbPlayerText()
    {
        playerText.text = nbPlayers.ToString();
    }

    public void MoreNbPlayers()
    {
        if (nbPlayers < nbPlayersMax)
        {
            nbPlayers++;
            updateNbPlayerText();
        }

    }

    public void LessNbPlayer()
    {
        if (nbPlayers > 2)
        {
            nbPlayers--;
            updateNbPlayerText();
        }

    }

    public void MoreNbLife()
    {
        if (nbLife < nbLifeMax)
        {
            nbLife++;
            updateLifeText();
        }

    }
    
    public void LessNbLife()
    {
        if (nbLife > 1)
        {
            nbLife--;
            updateLifeText();
        }

    }

    public void MoreTime()
    {
        if (timeID < nbTimeOption)
        {            
            timeID++;
            updateTimeText();
        }
    }

    public void LessTime()
    {
        if (timeID > 0)
        {
            timeID--;
            updateTimeText();
        }
    }

    public void Play()
    {
        PlayerPrefs.SetFloat("timeOfGame", times[timeID]);
        PlayerPrefs.SetInt("lifeAllowed", nbLife);
        PlayerPrefs.SetInt("nbPlayers", nbPlayers);
        MultiplayerController.Instance.CreateWithInvitationScreen();
        
    }
}
