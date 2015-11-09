using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

    /// <summary>
    ///     Speed of the projectile
    /// </summary>
    public float speed;
    /// <summary>
    ///     Amount of time the spell is gonna last, we keep track of it so that when lifeTime reaches 0 the spell should be destroyed.
    /// </summary>
    public float lifeTime ;
    /// <summary>
    ///     Indicate the force with wish the explosion of the spell is gonna push the other players
    /// </summary>
    public float Force;
    /// <summary>
    /// Indicate what radius will be concerned at the explosion of the spell
    /// </summary>
    public float radius;
    /// <summary>
    ///     Direction the spell is gonna follow
    /// </summary>
    public Vector3 direction;
    /// <summary>
    ///     used to store my transform to avoid any unecessary lookup. Saves performance.
    /// </summary>
    public Transform myTransform;
    
    private GameObject prefabExplosion;

    // Use this for initialization
    void Start () {
        myTransform = transform;
        //BonusManager bonusMan ;bonusMan = gameObject.GetComponent<BonusManager>();
        
        GameObject bonus;
        bonus = GameObject.Find("GameManager");
        Debug.Log("avant if");
        if (bonus.GetComponent<BonusManager>().bonusGo != null)
        {
            if (bonus.GetComponent<BonusManager>().bonusOwnedList[1] == true)
            {
                Debug.Log("dans big explo");
                prefabExplosion = Resources.Load("Prefabs/BigExplosionParticles") as GameObject;
            }
            else
            {
                Debug.Log("dans normal explo");
                prefabExplosion = Resources.Load("Prefabs/ExplosionParticles") as GameObject;
            }
        }
        else
        {
            prefabExplosion = Resources.Load("Prefabs/ExplosionParticles") as GameObject;
        }
        
    }


	
	// Update is called once per frame
	void Update () {
        float amountToMove = speed * Time.deltaTime;
        myTransform.Translate(direction * amountToMove);

        // we keep track on how long the spell has lived, if it has reach its end of life we kill it
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
            explode();
    }

    /// <summary>
    ///     Used to make the spell move in the direction you choose.
    /// </summary>
    public void Move(Vector3 moveDirection, float moveSpeed)
    {
        direction = moveDirection;
        speed = moveSpeed;
        //modifier velocity over lifetime en fonction
    }

    /// <summary>
    ///     Used to make the spell move in the direction you choose.
    /// </summary>
    public void Move(Vector3 moveDirection)
    {
        direction = moveDirection;
        //modifier velocity over lifetime en fonction
    }

    /// <summary>
    ///     Used right before we destroy the spell
    /// </summary>
    public void explode()
    {
        Debug.Log("before explode");
        Instantiate(prefabExplosion, myTransform.position, myTransform.rotation);
        Debug.Log("after explode");
        Destroy(this.gameObject);
    }
}
