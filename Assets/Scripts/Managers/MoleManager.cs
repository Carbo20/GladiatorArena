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
    private bool shield;

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
        shield = false;
        timeAlive = 0;
        isBeingPushed = false;
        timeOfPush = 0.5f;

        SpellExplosionSound = Resources.Load("Audio/FireExplosion1") as AudioClip;
        if (SpellExplosionSound == null)
            Debug.Log("erreur son");
        transform.position = initPosition;
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
        transform.position = new Vector3(0, 2, 0);
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

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            LifeDown();
        }
        else if (collision.gameObject.tag == "Spell")
        {
            Spell spell;
            spell = collision.gameObject.GetComponent<Spell>();

            BonusManager bonusMan;
            bonusMan = gameObject.GetComponent<BonusManager>();

            if (bonusMan.bonusPickedRandomly == 1)
            {
                spell.Force = 10;
            }

            // envoyer l'explosion de la boule de feu
            spell.explode();

            if (SpellExplosionSound != null)
            {
                AudioSource audioSource = GetComponent<AudioSource>();
                audioSource.clip = SpellExplosionSound;
                audioSource.Play();
            }

            if (!shield)
            {
                isBeingPushed = true;
                timeBeingPushed = 0;
                pushForce = spell.Force;
                pushDirection = spell.direction;
            }
        }

        else if (collision.gameObject.tag == "Bumper")
        {
            isBeingPushed = true;
            timeBeingPushed = 0;
            pushDirection = (transform.position - collision.gameObject.transform.position).normalized;
            pushForce = 7; //force des bumpers
        }

       
        else if (collision.gameObject.CompareTag("Bonus"))
        {
            //Destroy(collision.gameObject);
             BonusManager bonusMan;
             bonusMan = collision.gameObject.GetComponent<BonusManager>();
            /*  Destroy(bonusMan);*/
            //collision.gameObject.SetActive(false);
            //collision.gameObject.GetComponent<MeshRenderer>().material = null;
            //bonusMan.bonusGo.SetActive(false);
            // bonusMan.GetComponent<MeshRenderer>().material = null;

            bonusMan.SetInvisible();

        }
        
    }

    public void HitByBarrelForce(Vector3 barrelPos)
    {
        isBeingPushed = true;
        timeBeingPushed = 0;
        pushDirection = (transform.position - barrelPos).normalized;
        
        pushForce = (BarrelManager.range - (new Vector2(transform.position.x, transform.position.z) - new Vector2(barrelPos.x, barrelPos.z)).magnitude) * 3;
    }

    public void ShieldUpdate(bool shieldUpdate)
    {
        shield = shieldUpdate;
    }
}
