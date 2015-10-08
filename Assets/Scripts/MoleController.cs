using UnityEngine;
using System.Collections;

public class MoleController : MonoBehaviour {

    private Vector2 direction;
    private float speed;

	// Use this for initialization
	void Start () {
        speed = 10f;
	}
	
	// Update is called once per frame
	void Update () {

        Move();

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

    public void OnGUI()
    {
        GUI.TextArea(new Rect(5, 5, 120, 20), Input.acceleration.ToString());
    }
}
