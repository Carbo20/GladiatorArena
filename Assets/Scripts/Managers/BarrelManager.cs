using UnityEngine;
using System.Collections;

public class BarrelManager : MonoBehaviour {

    private bool isTriggered;
    public float TimeBeforeExplode;
    private float timeSinceTriggered;
    public static float range;
    private GameObject prefabExplosion;
	// Use this for initialization
	void Start () {
        range = 5;
        isTriggered = false;
        prefabExplosion = Resources.Load("Prefabs/ExplosionParticles") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (isTriggered && timeSinceTriggered < TimeBeforeExplode)
        {
            timeSinceTriggered += Time.deltaTime;
        }
        else if (isTriggered && timeSinceTriggered >= TimeBeforeExplode)
        {
            Explode();
            Destroy(gameObject);
        }

	}

    private void Explode()
    {
        GameObject[] Moles = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject mole in Moles)
        {
            if ((transform.position - mole.transform.position).magnitude <= range)
            {
                mole.GetComponent<MoleManager>().HitByBarrelForce(transform.position);
            }
        }

        Instantiate(prefabExplosion, transform.position, transform.rotation);

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Spell")
        {
            isTriggered = true;
            timeSinceTriggered = 0;
            collision.gameObject.GetComponent<Spell>().explode();
        }
    }
}
