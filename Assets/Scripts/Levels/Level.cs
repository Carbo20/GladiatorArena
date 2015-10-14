using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Level {

    public int width, height;
    public int[,] heightMap;
    public List<List<Vector2>> fallCoord;
    public List<float> fallTimes;

    public abstract void InitLevel();
    public abstract void SetFallPlanification();

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

}
