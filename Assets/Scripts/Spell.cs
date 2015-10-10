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
    /// Sound played when the spell explode
    /// </summary>
    public AudioSource SpellExplosionSound;
    /// <summary>
    ///     used to store my transform to avoid any unecessary lookup. Saves performance.
    /// </summary>
    public Transform myTransform;


    // Use this for initialization
    void Start () {
        SpellExplosionSound = Resources.Load("Audio/FireExplosion1") as AudioSource;
        myTransform = transform;
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
        if (SpellExplosionSound != null)
            SpellExplosionSound.Play();
        
        Destroy(this.gameObject);
    }
}
