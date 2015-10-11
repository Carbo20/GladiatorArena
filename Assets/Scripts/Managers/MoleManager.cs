using UnityEngine;
using System.Collections;

public class MoleManager : MonoBehaviour
{

    private int life;
    private Vector3 initPosition;
    private int playerID;
    private float timeAlive;
    private string name;
    private bool isBeingPushed;
    private Vector3 pushDirection;
    public float timeOfPush;
    private float timeBeingPushed;
    private float pushForce;

    /// <summary>
    /// Sound played when the spell explode
    /// </summary>
    public AudioClip SpellExplosionSound;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int PlayerID
    {
        get { return playerID; }
        set { playerID = value; }
    }

    // Use this for initialization
    void Start()
    {
        timeAlive = 0;
        isBeingPushed = false;
        timeOfPush = 0.5f;

        SpellExplosionSound = Resources.Load("Audio/FireExplosion1") as AudioClip;
        if (SpellExplosionSound == null)
            Debug.Log("erreur son");
        Repop();
    }


    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;

        if(isBeingPushed)
        {
            if (timeBeingPushed < timeOfPush)
                Pushed();
            else
                isBeingPushed = false;
        }
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
        PlayerPrefs.SetFloat("timeAlive" + playerID, timeAlive);
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

    /// <summary>
    ///     Push the mole from an explosion
    /// </summary>
    public void Pushed()
    {
        timeBeingPushed += Time.deltaTime;
        Vector3 dir = Vector3.zero;
        dir.x = pushDirection.x;
        dir.z = pushDirection.z;
        transform.Translate(dir * pushForce * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            LifeDown();
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Spell")
        {
            // envoyer l'explosion de la boule de feu
            Spell spell;
            spell = collision.gameObject.GetComponent<Spell>();
            spell.explode();

            if (SpellExplosionSound != null)
            {
                AudioSource audioSource = GetComponent<AudioSource>();
                audioSource.clip = SpellExplosionSound;
                audioSource.Play();
            }

            isBeingPushed = true;
            timeBeingPushed = 0;
            pushForce = spell.Force;
            pushDirection = spell.direction;
        }
    }
}
