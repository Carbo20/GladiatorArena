using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BonusManager : MonoBehaviour {

    private float lastPopTime;
    private float popTime;
    public GameObject bonusGo;
    public int bonusPickedRandomly;
    public bool[] bonusOwnedList;
    public int nbBonus;
    private float timeElapsed;

    private float stunActiveEngage;
    private float stunTime;
    private float expirationBonusTime;
    

    // Use this for initialization
    void Start () {
        timeElapsed = 0;
        lastPopTime = 0;
        popTime = 6;
        nbBonus  =  7;
        expirationBonusTime = 5;
        stunActiveEngage = 4;
        stunTime = 2;
        bonusOwnedList = new bool [nbBonus] ;
        //bonusOwnedList: (0:Stun, 1:BigSpell, 2:InfiniteTir, 3:BoostSpeed, 4:SlowSpeed, 5:GiantSize, 6:DwarfSize)
        //bonusPickedRandomly = Random.Range(0, nbBonus-1);
        bonusPickedRandomly = 6;

        if (bonusGo == null)
        {
            InvokeRepeating("SpawnBonus", popTime, popTime);
        }
      
    }
    void SpawnBonus()
    {
        bonusGo = Instantiate(Resources.Load("Prefabs/Bonus")) as GameObject;
        bonusGo.GetComponent<MeshRenderer>().material = Resources.Load("Bonus/Bonus" + bonusPickedRandomly) as Material;

    }
    // Update is called once per frame
    void Update () {
    }

    public void SetInvisible()
    {
        Destroy(bonusGo);
        InvokeRepeating("ExpireBonus", expirationBonusTime, 0);

        Debug.Log("bonusPickedRandomly: "+ bonusPickedRandomly);
        if (bonusPickedRandomly == 0)
        {
            //TODO wait stunActiveEngage
            bonusOwnedList[bonusPickedRandomly] = true;
        }
        else
        {
            Debug.Log("ajoutListBonusnum:" +bonusPickedRandomly);
            bonusOwnedList[bonusPickedRandomly] = true;
        }

    }

    public void ExpireBonus()
    {
        for (int i = 0; i < nbBonus; i++)
        {
            bonusOwnedList[i] = false;
        }
       
        foreach ( GameObject m in GameObject.FindGameObjectsWithTag("Player") )
        {
            if (m.GetComponent<MoleManager>().PlayerID == GetComponent<GameManager>().playerID)
            {
                m.GetComponent<MoleManager>().transform.localScale = new Vector3(1F, 1F, 1F);
            }
        }
    }
}
