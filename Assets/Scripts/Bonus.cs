using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

    public bool isDroppedBigSpell;
    private GameObject bonusGo;
    private float lastPopTime;
    private int nbBonus;

    // Use this for initialization
    void Start () {
        nbBonus = 4;
        lastPopTime = 5;
        isDroppedBigSpell = true;
        bonusGo = GameObject.Find("Bonus");
        Debug.Log("Hello");
        bonusGo.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        lastPopTime -= Time.deltaTime;
        Debug.Log("lastPopTime"+ lastPopTime);
        if (lastPopTime <= 0)
        {
            Debug.Log("Hi");
            bonusGo.SetActive(true);
        }
    }
}
