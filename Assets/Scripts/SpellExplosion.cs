using UnityEngine;
using System.Collections;

/// <summary>
/// made to destroy the explosion particles after a while so they don't stay forever
/// </summary>
public class SpellExplosion : MonoBehaviour {

    private float explosionDuration;
    private float explosionLaunched;

    // Use this for initialization
    void Start () {
        explosionDuration = 2f;
        explosionLaunched = 0;
    }
	
	// Update is called once per frame
	void Update () {
        explosionLaunched += Time.deltaTime;
        if (explosionLaunched >= explosionDuration)
            EndExplosion();
    }

    private void EndExplosion()
    {
        Destroy(this.gameObject);
    }
}
