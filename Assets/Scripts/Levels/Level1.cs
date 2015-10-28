using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Level1 : Level
{

    public override void InitLevel()
    {
        SetDimension(21, 21);
        heightMap = new int[21, 21] 
        {
            {0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,1,1,1},
            {0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,1,1,1,1,1},
            {0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0},
            {0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
            {0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
            {0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
            {0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
            {0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0},
            {0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,1,1,1,1,1},
            {0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,1,1,1},
            {0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0}
             
        };

    }

    public override void SetInitialPositions()
    {
        //formule pour les positions en fonction de la heightMap
        // x = -level.height + 2 * i y = 2, z =-level.width + 2 * j

        //position inititales pour 2 joueurs
        initialPositions[0].Add(new Vector3(-height + 2 * 3, 2, -width + 2 * 9));
        initialPositions[0].Add(new Vector3(-height + 2 * 16, 2, -width + 2 * 19));

        //position inititales pour 3 joueurs
        initialPositions[1].Add(new Vector3(-height + 2 * 10, 2, -width + 2 * 3));
        initialPositions[1].Add(new Vector3(-height + 2 * 4, 2, -width + 2 * 19));
        initialPositions[1].Add(new Vector3(-height + 2 * 16, 2, -width + 2 * 19));

        //position inititales pour 4 joueurs
        initialPositions[2].Add(new Vector3(-height + 2 * 3, 2, -width + 2 * 9));
        initialPositions[2].Add(new Vector3(-height + 2 * 17, 2, -width + 2 * 9));
        initialPositions[2].Add(new Vector3(-height + 2 * 4, 2, -width + 2 * 19));
        initialPositions[2].Add(new Vector3(-height + 2 * 16, 2, -width + 2 * 19));

    }

    public override void SetFallPlanification()
    {
        InitFallVariables();
        /*step 0 */
        fallTimes.Add(10);

        fallCoord.Add(new List<Vector2>());
        fallCoord[0].Add(new Vector2(8, 7));
        fallCoord[0].Add(new Vector2(9, 7));
        fallCoord[0].Add(new Vector2(10, 7));
        fallCoord[0].Add(new Vector2(11, 7));
        fallCoord[0].Add(new Vector2(12, 7));
        fallCoord[0].Add(new Vector2(8, 6));
        fallCoord[0].Add(new Vector2(9, 6));
        fallCoord[0].Add(new Vector2(10, 6));
        fallCoord[0].Add(new Vector2(11, 6));
        fallCoord[0].Add(new Vector2(12, 6));
        fallCoord[0].Add(new Vector2(8, 5));
        fallCoord[0].Add(new Vector2(9, 5));
        fallCoord[0].Add(new Vector2(10, 5));
        fallCoord[0].Add(new Vector2(11, 5));
        fallCoord[0].Add(new Vector2(12, 5));
        fallCoord[0].Add(new Vector2(9, 4));
        fallCoord[0].Add(new Vector2(10, 4));
        fallCoord[0].Add(new Vector2(11, 4));
        fallCoord[0].Add(new Vector2(9, 3));
        fallCoord[0].Add(new Vector2(10, 3));
        fallCoord[0].Add(new Vector2(11, 3));
        fallCoord[0].Add(new Vector2(9, 2));
        fallCoord[0].Add(new Vector2(10, 2));
        fallCoord[0].Add(new Vector2(11, 2));
        fallCoord[0].Add(new Vector2(10, 1));
        fallCoord[0].Add(new Vector2(10, 0));

        fallTimes.Add(15);
        fallCoord.Add(new List<Vector2>());
        fallCoord[1].Add(new Vector2(8, 11));
        fallCoord[1].Add(new Vector2(8, 12));
        fallCoord[1].Add(new Vector2(12, 11));
        fallCoord[1].Add(new Vector2(12, 12));
        fallCoord[1].Add(new Vector2(0, 8));
        fallCoord[1].Add(new Vector2(1, 8));
        fallCoord[1].Add(new Vector2(2, 8));
        fallCoord[1].Add(new Vector2(3, 8));
        fallCoord[1].Add(new Vector2(4, 8));
        fallCoord[1].Add(new Vector2(5, 8));
        fallCoord[1].Add(new Vector2(6, 8));
        fallCoord[1].Add(new Vector2(14, 8));
        fallCoord[1].Add(new Vector2(15, 8));
        fallCoord[1].Add(new Vector2(16, 8));
        fallCoord[1].Add(new Vector2(17, 8));
        fallCoord[1].Add(new Vector2(18, 8));
        fallCoord[1].Add(new Vector2(19, 8));
        fallCoord[1].Add(new Vector2(20, 8));
        fallCoord[1].Add(new Vector2(1, 9));
        fallCoord[1].Add(new Vector2(2, 9));
        fallCoord[1].Add(new Vector2(3, 9));
        fallCoord[1].Add(new Vector2(4, 9));
        fallCoord[1].Add(new Vector2(5, 9));
        fallCoord[1].Add(new Vector2(15, 9));
        fallCoord[1].Add(new Vector2(16, 9));
        fallCoord[1].Add(new Vector2(17, 9));
        fallCoord[1].Add(new Vector2(18, 9));
        fallCoord[1].Add(new Vector2(19, 9));
        fallCoord[1].Add(new Vector2(2, 10));
        fallCoord[1].Add(new Vector2(3, 10));
        fallCoord[1].Add(new Vector2(4, 10));
        fallCoord[1].Add(new Vector2(16, 10));
        fallCoord[1].Add(new Vector2(17, 10));
        fallCoord[1].Add(new Vector2(18, 10));
        fallCoord[1].Add(new Vector2(3, 11));
        fallCoord[1].Add(new Vector2(4, 11));
        fallCoord[1].Add(new Vector2(16, 11));
        fallCoord[1].Add(new Vector2(17, 11));
        fallCoord[1].Add(new Vector2(4, 12));
        fallCoord[1].Add(new Vector2(16, 12));

        fallTimes.Add(20);
        fallCoord.Add(new List<Vector2>());
        fallCoord[2].Add(new Vector2(10, 14));
        fallCoord[2].Add(new Vector2(5, 20));
        fallCoord[2].Add(new Vector2(15, 20));
        fallCoord[2].Add(new Vector2(4, 16));
        fallCoord[2].Add(new Vector2(4, 17));
        fallCoord[2].Add(new Vector2(4, 18));
        fallCoord[2].Add(new Vector2(4, 19));
        fallCoord[2].Add(new Vector2(4, 20));
        fallCoord[2].Add(new Vector2(16, 16));
        fallCoord[2].Add(new Vector2(16, 17));
        fallCoord[2].Add(new Vector2(16, 18));
        fallCoord[2].Add(new Vector2(16, 19));
        fallCoord[2].Add(new Vector2(16, 20));
        fallCoord[2].Add(new Vector2(3, 18));
        fallCoord[2].Add(new Vector2(3, 19));
        fallCoord[2].Add(new Vector2(3, 20));
        fallCoord[2].Add(new Vector2(17, 18));
        fallCoord[2].Add(new Vector2(17, 19));
        fallCoord[2].Add(new Vector2(17, 20));

    }

    public override void SetObstacles()
    {
        //formule pour les positions en fonction de la heightMap
        // x = -level.height + 2 * i y = 2, z =-level.width + 2 * j

    }
}
