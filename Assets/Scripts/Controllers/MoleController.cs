using UnityEngine;
using System.Collections;

public class MoleController : MonoBehaviour {

    private Vector2 direction;
    private float speed;
    private GameObject spellPrefab;
    private Spell spell;


    /// <summary>
    /// used for the swipe to remember where it started
    /// </summary>
    private Vector2 fingerStartPos = Vector2.zero;
    private bool isShielded;
    public float shieldDuration;
    private float shieldRemainingDuration;
    private float shieldCooldown;
    private float shieldRemainingCooldown;
    private Material mNoShield;
    private Material mShield;
    public float spellCooldown;
    private float spellRemainingCooldown;
    private bool spellLaunched;
    private float minSwipeDist;
    private GameObject gameManager;
    private MoleManager moleManager;

    // Use this for initialization
    void Start () {
        minSwipeDist = 50.0f;
        speed = 10f;
        shieldDuration = 0.5f;
        shieldRemainingDuration = 0;
        shieldCooldown = 3f;
        spellCooldown = 0.5f;
        spellRemainingCooldown = 0;
        isShielded = false;
        spellLaunched = false;
        spellPrefab = Resources.Load("Prefabs/Spell") as GameObject;
        mShield = Resources.Load("Materials/Shield") as Material;
        mNoShield = GetComponent<MeshRenderer>().material;
        gameManager = GameObject.Find("GameManager");
        moleManager = GetComponent<MoleManager>(); ;
    }
	
	// Update is called once per frame
	void Update () {

        GameObject bonusMan;
        bonusMan = GameObject.Find("GameManager");

        if (CanMove() && !bonusMan.GetComponent<BonusManager>().bonusOwnedList[0])
            Move();

        if (shieldRemainingDuration > 0)
            shieldRemainingDuration -= Time.deltaTime;
        else
        {
            isShielded = false;
            moleManager.ShieldUpdate(isShielded);
            GetComponent<MeshRenderer>().material = mNoShield;
        }
        
        if (CanSpell())
            Spell();

        if (!spellLaunched && CanShield())
            Shield();

        spellLaunched = false;
    }

    private void Move()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        dir.z = Input.acceleration.y;
        /* if (dir.sqrMagnitude > 1)
             dir.Normalize();*/
        dir *= Time.deltaTime;

        GameObject bonusMan;
        bonusMan = GameObject.Find("GameManager");
        if (bonusMan.GetComponent<BonusManager>().bonusOwnedList[3] == true)
        {//boost speed
           transform.Translate(dir * 2 * speed);
            Debug.Log("we boost");

        }
        else if(bonusMan.GetComponent<BonusManager>().bonusOwnedList[4] == true)
        {//slow speed
            transform.Translate(dir * speed / 2);
        }
        else
        {//normal speed
            transform.Translate(dir * speed);
            Debug.Log("normalspeed");
        }

    }

    private bool CanMove()
    {
        

        return (!gameManager.GetComponent<GameManager>().gameOver);
    }

    private bool CanSpell()
    {
        if (gameManager.GetComponent<GameManager>().gameOver) return false;

        GameObject bonus;
        bonus = GameObject.Find("GameManager");

        if (!bonus.GetComponent<BonusManager>().bonusOwnedList[2])
        {
            //Cooldown active if the player doesn't own infinite spell bonus
            if (spellRemainingCooldown > 0 )
            {
                spellRemainingCooldown -= Time.deltaTime;
                return false;
            }
       }
        return true;
    }

    private bool CanShield()
    {
        if (gameManager.GetComponent<GameManager>().gameOver) return false;
        if (shieldRemainingCooldown > 0)
        {
            shieldRemainingCooldown -= Time.deltaTime;
            return false;
        }

        return true;
    }

    private void Shield()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        /* this is a new touch */
                        fingerStartPos = touch.position;
                        break;

                    case TouchPhase.Ended:
                        float gestureDist = (touch.position - fingerStartPos).magnitude;

                        if (gestureDist < minSwipeDist)
                        {
                            RaycastHit hit;
                            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.GetComponent<MoleManager>() != null && hit.transform.gameObject.GetComponent<MoleManager>().PlayerID == gameManager.GetComponent<GameManager>().playerID)
                            {
                                isShielded = true;
                                moleManager.ShieldUpdate(isShielded);
                                shieldRemainingDuration = shieldDuration;
                                shieldRemainingCooldown = shieldCooldown;
                                mNoShield = GetComponent<MeshRenderer>().material;
                                GetComponent<MeshRenderer>().material = mShield;
                            }
                        }
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Deal with th input "swipe" to launch a spell
    /// </summary>
    private void Spell()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        /* this is a new touch */
                        fingerStartPos = touch.position;
                        break;

                    case TouchPhase.Ended:
                        float gestureDist = (touch.position - fingerStartPos).magnitude;
                        if (gestureDist > minSwipeDist)
                        {
                            spell = spellPrefab.GetComponent<Spell>();
                            spell.direction.x = (touch.position - fingerStartPos).normalized.x;
                            spell.direction.y = 0;
                            spell.direction.z = (touch.position - fingerStartPos).normalized.y;
                            Instantiate(spellPrefab, transform.position + spell.direction*2, transform.rotation);
                            spellRemainingCooldown = spellCooldown;
                            spellLaunched = true;
                        }
                        break;
                }
            }
        }
    }
}
