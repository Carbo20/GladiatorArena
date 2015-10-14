using UnityEngine;
using System.Collections;

public class ArenaPieceScript : MonoBehaviour {

    public float timeOfThrill;
    private float timeThrilling;
    public float thrillValue;
    public float fallSpeed;
    public bool isThrilling, isFalling;

    private Vector3 pos;
    
	// Use this for initialization
	void Start () {
        isThrilling = false;
        isFalling = false;
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (isThrilling)
            Thrill();
        if (isFalling)
            Fall();
        if (GetComponent<MeshRenderer>().material.color == Color.yellow && !isThrilling && timeThrilling <= timeOfThrill)
        {
            TriggerFall();
        }
	}

    public void TriggerFall()
    {
        timeThrilling = 0;
        isThrilling = true;
    }

    private void Thrill()
    {
        timeThrilling += Time.deltaTime;

        if (timeThrilling <= timeOfThrill)
        {

            if ((int)(timeThrilling*10) % 2 == 0)
                transform.position = new Vector3(pos.x, pos.y, pos.z - thrillValue);
            else if((int)(timeThrilling*100)%2 == 1)
                transform.position = new Vector3(pos.x, pos.y, pos.z + thrillValue);

        }
        else
        {
            transform.position = pos;
            isThrilling = false;
            isFalling = true;
        }
        
    }

    private void Fall()
    {
        transform.Translate(0, -fallSpeed * Time.deltaTime, 0);

        if (transform.position.y < -4 )
        {
            Destroy(gameObject);
        }
    }
}
