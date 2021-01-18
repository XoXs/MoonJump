using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candel : MonoBehaviour
{
    float ypos;
    newCandleGenerator LevelGenerator;
    Player _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        LevelGenerator = GameObject.Find("LevelGenerator").GetComponent<newCandleGenerator>();
        ypos = transform.position.y;
    }

	void FixedUpdate ()
    {
        if (!_player.firstJump)
            return;

        if (transform.position.y < ypos - 10f)
        { 
            Destroy(this.gameObject);
            LevelGenerator.generateCandle();
        }
    }
}

