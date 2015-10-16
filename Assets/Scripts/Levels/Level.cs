using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Level {

    private int NB_PLAYER_COUNT__MODE = 3;
    public int width, height;
    public int[,] heightMap;
    public List<List<Vector2>> fallCoord;
    public List<float> fallTimes;
    public List<List<Vector3>> initialPositions;
    public List<GameObject> obstacles;
    public List<Vector3> obstaclesPositions;

    public abstract void InitLevel();
    public abstract void SetFallPlanification();
    public abstract void SetInitialPositions();
    public abstract void SetObstacles();

    public GameObject Bumper;
    public GameObject Barrel;

    public Level()
    {
        InitLevel();
        SetFallPlanification();
        initialPositions = new List<List<Vector3>>(NB_PLAYER_COUNT__MODE);
        for (int i = 0; i < NB_PLAYER_COUNT__MODE; i++)
        {
            initialPositions.Add(new List<Vector3>());
        }
        SetInitialPositions();

        InitObstacles();
        SetObstacles();
    }

    public void InitObstacles()
    {
        obstacles = new List<GameObject>();
        obstaclesPositions = new List<Vector3>();
        Bumper = Resources.Load<GameObject>("Prefabs/Bumper");
        Barrel = Resources.Load<GameObject>("Prefabs/Barrel");
    }

    public void SetDimension(int _width, int _height)
    {
        width = _width;
        height = _height;
    }

    public void InitFallVariables()
    {
        fallCoord = new List<List<Vector2>>();
        fallTimes = new List<float>();
        
    }

    public int GetHeight(int x, int y)
    {
        return heightMap[x, y];
    }

    public List<Vector2> GetFallCoordinates(int step)
    {
        return fallCoord[step];
    }

    public float GetFallTime(int _step)
    {
        return fallTimes[_step];
    }

    public int GetFallStepCount()
    {
        return fallTimes.Count;
    }

    public Vector3 GetInitalPosition(int nbPlayers, int playerId)
    {
        return initialPositions[nbPlayers - 2][playerId];
    }
}
