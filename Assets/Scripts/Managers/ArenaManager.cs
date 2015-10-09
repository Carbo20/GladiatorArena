using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Threading;

public class ArenaManager : MonoBehaviour
{

    GameObject lava;
    GameObject arena;

    MeshFilter arenaMesh;

    int[,] arenaMap;

    // Use this for initialization
    void Start()
    {
        BuildArena(10, 10);
    }

    // Update is called once per frame
    void Update()
    {

        PieceDestruction(GameObject.FindGameObjectWithTag("ArenaPiece"));
    }


    private void BuildArena(int height, int width)
    {
        arenaMap = new int [height,width];

        for(int i = 0;i < height; i++)
        {
            for(int j = 0;j < width; j++)
            {
                GameObject piece;
                Vector3 pos;
                if (i == 0 || i == height-1 || j == 0 || j == width-1)
                {
                    piece = Instantiate(Resources.Load("Prefabs/ArenaPiece")) as GameObject;
                    pos = new Vector3(-10 + 2 * i, 0, -10 + 2 * j);
                }
                else
                {
                    piece = Instantiate(Resources.Load("Prefabs/ArenaFlatPiece")) as GameObject;
                    pos = new Vector3(-10 + 2 * i, 1, -10 + 2 * j);
                }
                piece.transform.position = pos;
                arenaMap[i,j] = 1;

            }
        }
    }
    
    private void PieceDestruction(GameObject piece)
    {
        float time = 0f;
        float speed = 1f;

        for (float i = 0f; i < 6f; i += Time.deltaTime)
        {
            time += Time.deltaTime;
            UnityEngine.Debug.Log(i);
            if(time > speed)
            {
                Vector3 pos = piece.transform.position;
                Vector3 newPos = new Vector3(pos.x, pos.y - 0.2f, pos.z);
                piece.transform.position = newPos;
                time = 0;
            }
        }
    }


}
