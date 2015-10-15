using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level0 : Level {

    

    public override void InitLevel()
    {
        SetDimension(21, 21);
        heightMap = new int[21 , 21] 
        {
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0},
            { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
            { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
            { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
            { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
            { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
            { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
            { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
            { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
            { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
            { 0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        };
    }

    public override void SetFallPlanification()
    {
        InitFallVariables();
        /*step 0 */
        fallTimes.Add(10);
        
        fallCoord.Add(new List<Vector2>());
        fallCoord[0].Add(new Vector2(1, 8));
        fallCoord[0].Add(new Vector2(1, 9));
        fallCoord[0].Add(new Vector2(1, 10));
        fallCoord[0].Add(new Vector2(1, 11));
        fallCoord[0].Add(new Vector2(1, 12));
        fallCoord[0].Add(new Vector2(2, 6));
        fallCoord[0].Add(new Vector2(2, 7));
        fallCoord[0].Add(new Vector2(2, 8));
        fallCoord[0].Add(new Vector2(2, 9));
        fallCoord[0].Add(new Vector2(2, 10));
        fallCoord[0].Add(new Vector2(2, 11));
        fallCoord[0].Add(new Vector2(2, 12));
        fallCoord[0].Add(new Vector2(2, 13));
        fallCoord[0].Add(new Vector2(2, 14));
        fallCoord[0].Add(new Vector2(3, 4));
        fallCoord[0].Add(new Vector2(3, 5));
        fallCoord[0].Add(new Vector2(3, 6));
        fallCoord[0].Add(new Vector2(3, 7));
        fallCoord[0].Add(new Vector2(3, 8));
        fallCoord[0].Add(new Vector2(3, 11));
        fallCoord[0].Add(new Vector2(3, 12));
        fallCoord[0].Add(new Vector2(3, 13));
        fallCoord[0].Add(new Vector2(3, 14));
        fallCoord[0].Add(new Vector2(3, 15));
        fallCoord[0].Add(new Vector2(3, 16));
        fallCoord[0].Add(new Vector2(4, 5));
        fallCoord[0].Add(new Vector2(4, 15));
        fallCoord[0].Add(new Vector2(16, 5));
        fallCoord[0].Add(new Vector2(16, 15));
        fallCoord[0].Add(new Vector2(17, 4));
        fallCoord[0].Add(new Vector2(17, 5));
        fallCoord[0].Add(new Vector2(17, 6));
        fallCoord[0].Add(new Vector2(17, 7));
        fallCoord[0].Add(new Vector2(17, 8));
        fallCoord[0].Add(new Vector2(17, 11));
        fallCoord[0].Add(new Vector2(17, 12));
        fallCoord[0].Add(new Vector2(17, 13));
        fallCoord[0].Add(new Vector2(17, 14));
        fallCoord[0].Add(new Vector2(17, 15));
        fallCoord[0].Add(new Vector2(17, 16));
        fallCoord[0].Add(new Vector2(18, 6));
        fallCoord[0].Add(new Vector2(18, 7));
        fallCoord[0].Add(new Vector2(18, 8));
        fallCoord[0].Add(new Vector2(18, 9));
        fallCoord[0].Add(new Vector2(18, 10));
        fallCoord[0].Add(new Vector2(18, 11));
        fallCoord[0].Add(new Vector2(18, 12));
        fallCoord[0].Add(new Vector2(18, 13));
        fallCoord[0].Add(new Vector2(18, 14));
        fallCoord[0].Add(new Vector2(19, 8));
        fallCoord[0].Add(new Vector2(19, 9));
        fallCoord[0].Add(new Vector2(19, 10));
        fallCoord[0].Add(new Vector2(19, 11));
        fallCoord[0].Add(new Vector2(19, 12));

        /*step 1*/
        fallTimes.Add(15);
        fallCoord.Add(new List<Vector2>());
        fallCoord[1].Add(new Vector2(8, 1));
        fallCoord[1].Add(new Vector2(9, 1));
        fallCoord[1].Add(new Vector2(10, 1));
        fallCoord[1].Add(new Vector2(11, 1));
        fallCoord[1].Add(new Vector2(12, 1));
        fallCoord[1].Add(new Vector2(6, 2));
        fallCoord[1].Add(new Vector2(7, 2));
        fallCoord[1].Add(new Vector2(8, 2));
        fallCoord[1].Add(new Vector2(9, 2));
        fallCoord[1].Add(new Vector2(11, 2));
        fallCoord[1].Add(new Vector2(12, 2));
        fallCoord[1].Add(new Vector2(13, 2));
        fallCoord[1].Add(new Vector2(14, 2));
        fallCoord[1].Add(new Vector2(4, 3));
        fallCoord[1].Add(new Vector2(5, 3));
        fallCoord[1].Add(new Vector2(6, 3));
        fallCoord[1].Add(new Vector2(7, 3));
        fallCoord[1].Add(new Vector2(13, 3));
        fallCoord[1].Add(new Vector2(14, 3));
        fallCoord[1].Add(new Vector2(15, 3));
        fallCoord[1].Add(new Vector2(16, 3));
        fallCoord[1].Add(new Vector2(4, 4));
        fallCoord[1].Add(new Vector2(5, 4));
        fallCoord[1].Add(new Vector2(15, 4));
        fallCoord[1].Add(new Vector2(16, 4));
        fallCoord[1].Add(new Vector2(8, 19));
        fallCoord[1].Add(new Vector2(9, 19));
        fallCoord[1].Add(new Vector2(10, 19));
        fallCoord[1].Add(new Vector2(11, 19));
        fallCoord[1].Add(new Vector2(12, 19)); 
        fallCoord[1].Add(new Vector2(6, 18));
        fallCoord[1].Add(new Vector2(7, 18));
        fallCoord[1].Add(new Vector2(8, 18));
        fallCoord[1].Add(new Vector2(9, 18));
        fallCoord[1].Add(new Vector2(11, 18));
        fallCoord[1].Add(new Vector2(12, 18));
        fallCoord[1].Add(new Vector2(13, 18));
        fallCoord[1].Add(new Vector2(14, 18));
        fallCoord[1].Add(new Vector2(4, 17));
        fallCoord[1].Add(new Vector2(5, 17));
        fallCoord[1].Add(new Vector2(6, 17));
        fallCoord[1].Add(new Vector2(7, 17));
        fallCoord[1].Add(new Vector2(13, 17));
        fallCoord[1].Add(new Vector2(14, 17));
        fallCoord[1].Add(new Vector2(15, 17));
        fallCoord[1].Add(new Vector2(16, 17));
        fallCoord[1].Add(new Vector2(4, 16));
        fallCoord[1].Add(new Vector2(5, 16));
        fallCoord[1].Add(new Vector2(15, 16));
        fallCoord[1].Add(new Vector2(16, 16));

        /*step 2*/
        fallTimes.Add(25);
        fallCoord.Add(new List<Vector2>());
        fallCoord[2].Add(new Vector2(10, 2));
        fallCoord[2].Add(new Vector2(8, 3));
        fallCoord[2].Add(new Vector2(9, 3));
        fallCoord[2].Add(new Vector2(10, 3));
        fallCoord[2].Add(new Vector2(11, 3));
        fallCoord[2].Add(new Vector2(12, 3));
        fallCoord[2].Add(new Vector2(6, 4));
        fallCoord[2].Add(new Vector2(7, 4));
        fallCoord[2].Add(new Vector2(8, 4));
        fallCoord[2].Add(new Vector2(9, 4));
        fallCoord[2].Add(new Vector2(10, 4));
        fallCoord[2].Add(new Vector2(11, 4));
        fallCoord[2].Add(new Vector2(12, 4));
        fallCoord[2].Add(new Vector2(13, 4));
        fallCoord[2].Add(new Vector2(14, 4));
        fallCoord[2].Add(new Vector2(5, 5));
        fallCoord[2].Add(new Vector2(6, 5));
        fallCoord[2].Add(new Vector2(7, 5));
        fallCoord[2].Add(new Vector2(8, 5));
        fallCoord[2].Add(new Vector2(9, 5));
        fallCoord[2].Add(new Vector2(10, 5));
        fallCoord[2].Add(new Vector2(11, 5));
        fallCoord[2].Add(new Vector2(12, 5));
        fallCoord[2].Add(new Vector2(13, 5));
        fallCoord[2].Add(new Vector2(14, 5));
        fallCoord[2].Add(new Vector2(15, 5));
        fallCoord[2].Add(new Vector2(3, 9));
        fallCoord[2].Add(new Vector2(17, 9));
        fallCoord[2].Add(new Vector2(10, 18));
        fallCoord[2].Add(new Vector2(8, 17));
        fallCoord[2].Add(new Vector2(9, 17));
        fallCoord[2].Add(new Vector2(10, 17));
        fallCoord[2].Add(new Vector2(11, 17));
        fallCoord[2].Add(new Vector2(12, 17));
        fallCoord[2].Add(new Vector2(6, 16));
        fallCoord[2].Add(new Vector2(7, 16));
        fallCoord[2].Add(new Vector2(8, 16));
        fallCoord[2].Add(new Vector2(9, 16));
        fallCoord[2].Add(new Vector2(10, 16));
        fallCoord[2].Add(new Vector2(11, 16));
        fallCoord[2].Add(new Vector2(12, 16));
        fallCoord[2].Add(new Vector2(13, 16));
        fallCoord[2].Add(new Vector2(14, 16));
        fallCoord[2].Add(new Vector2(5, 15));
        fallCoord[2].Add(new Vector2(6, 15));
        fallCoord[2].Add(new Vector2(7, 15));
        fallCoord[2].Add(new Vector2(8, 15));
        fallCoord[2].Add(new Vector2(9, 15));
        fallCoord[2].Add(new Vector2(10, 15));
        fallCoord[2].Add(new Vector2(11, 15));
        fallCoord[2].Add(new Vector2(12, 15));
        fallCoord[2].Add(new Vector2(13, 15));
        fallCoord[2].Add(new Vector2(14, 15));
        fallCoord[2].Add(new Vector2(15, 15));
        fallCoord[2].Add(new Vector2(3, 10));
        fallCoord[2].Add(new Vector2(17, 10));

    }

    public override void SetInitialPositions()
    {
        //formule pour les positions en fonction de la heightMap
        // x = -level.height + 2 * i y = 2, z =-level.width + 2 * j

        //position inititales pour 2 joueurs
        initialPositions[0].Add(new Vector3(-height + 2 * 5, 2, -width + 2 * 4));
        initialPositions[0].Add(new Vector3(-height + 2 * 16, 2, -width + 2 * 17));

        //position inititales pour 3 joueurs
        initialPositions[1].Add(new Vector3(-height + 2 * 5, 2, -width + 2 * 4));
        initialPositions[1].Add(new Vector3(-height + 2 * 16, 2, -width + 2 * 17));
        initialPositions[1].Add(new Vector3(-height + 2 * 16, 2, -width + 2 * 4));

        //position inititales pour 4 joueurs
        initialPositions[2].Add(new Vector3(-height + 2 * 5, 2, -width + 2 * 4));
        initialPositions[2].Add(new Vector3(-height + 2 * 16, 2, -width + 2 * 17));
        initialPositions[2].Add(new Vector3(-height + 2 * 16, 2, -width + 2 * 4));
        initialPositions[2].Add(new Vector3(-height + 2 * 5, 2, -width + 2 * 17));

    }

    public override void SetObstacles()
    {
        //formule pour les positions en fonction de la heightMap
        // x = -level.height + 2 * i y = 2, z =-level.width + 2 * j

        //model creation bumper en (7,13)
        obstacles.Add(Bumper);
        obstaclesPositions.Add(new Vector3(-height + 2 * 7, obstacles[0].transform.position.y, -width + 2 * 13));

        obstacles.Add(Bumper);
        obstaclesPositions.Add(new Vector3(-height + 2 * 13, obstacles[0].transform.position.y, -width + 2 * 7));

        obstacles.Add(Bumper);
        obstaclesPositions.Add(new Vector3(-height + 2 * 7, obstacles[0].transform.position.y, -width + 2 * 7));

        obstacles.Add(Bumper);
        obstaclesPositions.Add(new Vector3(-height + 2 * 13, obstacles[0].transform.position.y, -width + 2 * 13));

    }
}
