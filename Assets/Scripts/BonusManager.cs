using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BonusManager : MonoBehaviour {

    private float lastPopTime;
    private float popTime;
    public GameObject bonusGo;
    public int bonusPickedRandomly;
    public bool[] bonusOwnedList;
    int nbBonus;
    private float timeElapsed;

    // Use this for initialization
    void Start () {
        timeElapsed = 0;
        lastPopTime = 0;
        popTime = 6;
        nbBonus  =  5;
        bonusOwnedList = new bool [nbBonus] ;
       
        bonusPickedRandomly = Random.Range(1, nbBonus);
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
        bonusOwnedList[bonusPickedRandomly] = true;
        Destroy(bonusGo);
    }
}
