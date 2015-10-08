using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private int nbPlayers;
    private int playerID;
    private int lifeAllowed;
    private float timeElapsed;
    private float timeOfGame;

    
	// Use this for initialization
	void Start () {
	    nbPlayers = PlayerPrefs.GetInt("nbPlayers", 4);
        timeOfGame = PlayerPrefs.GetFloat("timeOfGame", -1);
        lifeAllowed = PlayerPrefs.GetInt("lifeAllowed", 3);
        playerID = PlayerPrefs.GetInt("playerID", 0);
        timeElapsed = 0;

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

        if (mm.Length == 1)
        {
            Debug.Log("Player " + mm[0].PlayerID + " wins!");
        }
    }
}
