using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class ArenaManager : MonoBehaviour
{

    GameObject lava;
    GameObject arena;

    MeshFilter arenaMesh;

    int[,] arenaMap;

    // Use this for initialization
    void Start()
    {
        Stopwatch watch = Stopwatch.StartNew();
        buildArena(10, 10);
        watch.Stop();
        UnityEngine.Debug.Log(watch.ElapsedMilliseconds);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void buildArena(int height, int width)
    {
        arenaMap = new int [height,width];

        for(int i = 0;i < height; i++)
        {
            for(int j = 0;j < width; j++)
            {
                GameObject Piece;
                Vector3 pos;
                if (i == 0 || i == height-1 || j == 0 || j == width-1)
                {
                    Piece = Instantiate(Resources.Load("Prefabs/ArenaPiece")) as GameObject;
                    pos = new Vector3(-10 + 2 * i, 0, -10 + 2 * j);
                }
                else
                {
                    Piece = Instantiate(Resources.Load("Prefabs/ArenaFlatPiece")) as GameObject;
                    pos = new Vector3(-10 + 2 * i, 1, -10 + 2 * j);
                }
                Piece.transform.position = pos;
                arenaMap[i,j] = 1;

            }
        }
    }
    
    private void arenaDestruction()
    {

    }


}
