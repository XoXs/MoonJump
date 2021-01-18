using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTrail : MonoBehaviour
{
    public float delay;
    public float delayConstant;
    public GameObject Trailprefab;

	void Update ()
    {
        if (delay <= 0)
        {
           GameObject temp =  Instantiate(Trailprefab, transform.position, Quaternion.identity);
            Destroy(temp, 2f);
            delay = delayConstant;
        }
        else {
            delay -= Time.deltaTime;
        }
	}
}
