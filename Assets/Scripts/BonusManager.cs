using UnityEngine;
using System.Collections;

public class BonusManager : MonoBehaviour {

    private float lastPopTime;
    public GameObject bonusGo;
    public int bonusPickedRandomly;

    // Use this for initialization
    void Start () {
        lastPopTime = 5;
        bonusPickedRandomly = Random.Range(1, 5);

        bonusGo = GameObject.Find("Bonus");
        bonusGo.SetActive(false);
        bonusGo.GetComponent<MeshRenderer>().material = Resources.Load("Bonus/Bonus" + bonusPickedRandomly) as Material;
       
    }

    // Update is called once per frame
    void Update () {
     
        /*create Bonus */
        lastPopTime -= Time.deltaTime;

        if ( lastPopTime <= 0 && !(bonusGo.activeSelf) )
        {
            bonusGo.SetActive(true);
            //bonusGo = Instantiate(Resources.Load("Prefabs/Bonus")) as GameObject;
            bonusGo.GetComponent<MeshRenderer>().material = Resources.Load("Bonus/Bonus" + bonusPickedRandomly) as Material;

        }
       
    }

    public void SetInvisible()
    {
        Destroy(bonusGo);
        //bonusGo.GetComponent<MeshRenderer>().material = null;
        //bonusGo.SetActive(false);
    }
}
