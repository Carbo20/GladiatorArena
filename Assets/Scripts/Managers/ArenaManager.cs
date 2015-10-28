using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArenaManager : MonoBehaviour
{
    int[,] arenaMap;
    public Level level;
    GameObject[,] pieces;
    GameManager gameManager;
    float elapsedTime;
    int currentFallStep;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        level = new Level1();
        pieces = new GameObject[level.width, level.height];
        BuildArena();
    }

    // Use this for initialization
    void Start()
    {
        elapsedTime = 0;
        currentFallStep = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.introIsPlaying)
            elapsedTime += Time.deltaTime;
        if (!gameManager.gameOver && currentFallStep < level.GetFallStepCount() &&  level.GetFallTime(currentFallStep) <= elapsedTime)
        {
            TriggerFall(currentFallStep);
            currentFallStep++;
        }
    }

    private void TriggerFall(int step)
    {
        List<Vector2> fallCoord = level.GetFallCoordinates(step);
        for (int i = 0; i < fallCoord.Count; i++)
        {

            if (pieces[(int)fallCoord[i].x, (int)fallCoord[i].y].GetComponent<ArenaPieceScript>() == null)
            {
                Vector3 p = pieces[(int)fallCoord[i].x, (int)fallCoord[i].y].transform.position;
                p.y = 0;
                Destroy(pieces[(int)fallCoord[i].x, (int)fallCoord[i].y].gameObject);

                pieces[(int)fallCoord[i].x, (int)fallCoord[i].y] = Instantiate(Resources.Load("Prefabs/ArenaPiece")) as GameObject;
                pieces[(int)fallCoord[i].x, (int)fallCoord[i].y].transform.position = p;

                

            }

            for (int j = -1; j <= 1; j++)
                for (int k = -1; k <= 1; k++)
                {
                    if (Mathf.Abs(j) != Mathf.Abs(k))
                    {
                        
                        if (
                            (int)fallCoord[i].x + j < level.height &&
                            (int)fallCoord[i].x + j >= 0 &&
                            (int)fallCoord[i].y + k < level.width &&
                            (int)fallCoord[i].y + k >= 0 &&
                            pieces[(int)fallCoord[i].x + j, (int)fallCoord[i].y+ k] != null && 
                            pieces[(int)fallCoord[i].x + j, (int)fallCoord[i].y+ k].GetComponent<ArenaPieceScript>() == null
                            )
                        {
                            Vector3 p = pieces[(int)fallCoord[i].x + j, (int)fallCoord[i].y + k].transform.position;
                            p.y = 0;
                            Destroy(pieces[(int)fallCoord[i].x + j, (int)fallCoord[i].y + k].gameObject);

                            pieces[(int)fallCoord[i].x + j, (int)fallCoord[i].y + k] = Instantiate(Resources.Load("Prefabs/ArenaPiece")) as GameObject;
                            pieces[(int)fallCoord[i].x + j, (int)fallCoord[i].y + k].transform.position = p;



                        }
                    }
                }
        
            pieces[(int)fallCoord[i].x, (int)fallCoord[i].y].GetComponent<MeshRenderer>().material.color = Color.yellow;
            //pieces[(int)fallCoord[i].x, (int)fallCoord[i].y].GetComponent<BoxCollider>().enabled = false;
            pieces[(int)fallCoord[i].x, (int)fallCoord[i].y].GetComponent<ArenaPieceScript>().TriggerFall();

        }
    }

    private void BuildArena()
    {
       
        for (int i = 0; i < level.height; i++)
        {
            for (int j = 0; j < level.width; j++)
            {
                GameObject piece;
                Vector3 pos;
                if (level.GetHeight(i, j) == 0) continue;

                if (i == 0 || i == level.height - 1 || j == 0 || j == level.width - 1 ||
                    level.GetHeight(i-1, j) == 0 || level.GetHeight(i+1, j) == 0 || level.GetHeight(i, j-1) == 0 || level.GetHeight(i, j+1) == 0)
                {
                    piece = Instantiate(Resources.Load("Prefabs/ArenaPiece")) as GameObject;
                    pos = new Vector3(-level.height + 2 * i, 0, -level.width + 2 * j);
                }
                else
                {
                    piece = Instantiate(Resources.Load("Prefabs/ArenaFlatPiece")) as GameObject;
                    pos = new Vector3(-level.height + 2 * i, 1, -level.width + 2 * j);
                }
                piece.transform.position = pos;
                pieces[i, j] = piece;
            }
        }

        for (int i = 0; i < level.obstacles.Count; i++)
        {
            GameObject go =  Instantiate(level.obstacles[i]);
            go.transform.position = level.obstaclesPositions[i];
        }
    }


}
