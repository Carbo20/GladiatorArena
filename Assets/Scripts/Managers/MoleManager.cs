﻿using UnityEngine;
using System.Collections;

public class MoleManager : MonoBehaviour {

    private int life;
    private Vector3 initPosition;
    private int playerID;

    public int PlayerID
    {
        get { return playerID; }
        set { playerID = value; }
    }

	// Use this for initialization
	void Start () {
        Repop();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetLife(int value)
    {
        life = value;
    }

    public void LifeDown()
    {
        life--;

        if (life == 0)
            Death();
        else
            Repop();
    }

    public void lifeUp()
    {
        life++;
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void Repop()
    {
        transform.position = initPosition;
    }

    public void SetInitPosition(Vector3 position)
    {
        initPosition = position;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            LifeDown();
        }
    }
}