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

    // Use this for initialization
    void Start () {
        speed = 10f;
        spellPrefab = Resources.Load("Prefabs/Spell") as GameObject;
    }
	
	// Update is called once per frame
	void Update () {

        if(CanMove())
            Move();

        Swipe();
    }

    private void Move()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        dir.z = Input.acceleration.y;
        /* if (dir.sqrMagnitude > 1)
             dir.Normalize();*/
        dir *= Time.deltaTime;
        transform.Translate(dir * speed);
    }

    private bool CanMove()
    {
        return true;
    }

    /// <summary>
    ///     Push the mole from an explosion
    /// </summary>
    private void Pushed(Vector3 pushDirection, float pushForce)
    {

    }

    /// <summary>
    /// Deal with th input "swipe" to launch a spell
    /// </summary>
    private void Swipe()
    {
        float minSwipeDist = 0.0f;

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        /* this is a new touch */
                        fingerStartPos = touch.position;
                        //Debug.Log("touch.position "  + touch.position + " fingerStartPos  " + fingerStartPos);
                        break;

                    case TouchPhase.Ended:
                        //Debug.Log("touch.position " + touch.position + " fingerStartPos  " + fingerStartPos);
                        float gestureDist = (touch.position - fingerStartPos).magnitude;
                        //Debug.Log("   && gestureDist  " + gestureDist + "   > minSwipeDist  " + minSwipeDist);
                        if (gestureDist > minSwipeDist)
                        {
                            Vector2 direction = touch.position - fingerStartPos;
                            Vector2 swipeType = Vector2.zero;
                            spell = spellPrefab.GetComponent<Spell>();
                            spell.direction.x = (touch.position - fingerStartPos).normalized.x;
                            //Debug.Log("(touch.position - fingerStartPos).normalized.x  " + (touch.position - fingerStartPos).normalized.x);
                            //Debug.Log("touch.position  " + touch.position + " fingerStartPos  " + fingerStartPos);
                            spell.direction.y = 0;
                            spell.direction.z = (touch.position - fingerStartPos).normalized.y;
                            //Debug.DrawLine(transform.position, transform.position + spell.direction * 10, Color.red, Mathf.Infinity);
                            Instantiate(spellPrefab, transform.position + spell.direction, transform.rotation);
                        }
                        break;
                }
            }
        }

    }
}
