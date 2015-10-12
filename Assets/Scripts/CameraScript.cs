using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScript : MonoBehaviour {

    private List<GameObject> moles;
    private float xMin, zMin, xMax, zMax;
    private Camera camera;
    public float zoom;

	// Use this for initialization
	void Start () {
        moles = new List<GameObject>();
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        moles.Clear();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
            moles.Add(go);
        if (moles.Count > 1)
            UpdateCameraPostion();
	}

    void UpdateCameraPostion()
    {
        xMin = moles[0].transform.position.x;
        xMax = moles[0].transform.position.x;
        zMin = moles[0].transform.position.z;
        zMax = moles[0].transform.position.z;
        for (int i = 1; i < moles.Count; i++)
        {
            if (xMin > moles[i].transform.position.x)
                xMin = moles[i].transform.position.x;
            if (xMax < moles[i].transform.position.x)
                xMax = moles[i].transform.position.x;
            if (zMin > moles[i].transform.position.z)
                zMin = moles[i].transform.position.z;
            if (zMax < moles[i].transform.position.z)
                zMax = moles[i].transform.position.z;
        }

        //Debug.Log(moles.Count+") x: " + xMin + " - " + xMax + " z: " + zMin + " - " + zMax);
         
        //the formulas after you found the min and max values:
        float xCenter = (xMax+xMin)/2f;
        float zCenter = (zMax+zMin)/2f;


        float[] height = new float[3];
        height[0] = zoom;
        height[1] = xMax - xMin + zoom;
        height[2] = zMax - zMin + zoom;
        float y = Mathf.Max(height);

        //transform.position = new Vector3(xCenter, 10, zCenter + 10);
        //transform.TransformPoint(new Vector3(xCenter, 10, zCenter - 10));
        transform.position = new Vector3(xCenter, y, zCenter);
    }
}
