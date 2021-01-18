using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    // script to move the background
    private GameObject player;
    Vector3 vel;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
	}
	
	void Update ()
    {
       // transform.position = new Vector3(Camera.main.transform.position.x+3f, player.transform.position.y,transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position,new Vector3(Camera.main.transform.position.x + 3f, player.transform.position.y, transform.position.z),ref vel,1f);
    }
}
