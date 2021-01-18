using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newCandleGenerator : MonoBehaviour {

    public GameObject[] tiles;
    public List<GameObject> candleList;
    private GameObject _player;
    private Player _script;

    private float xDiff = 1.1f;
    private float yDiffSmall = 0.5f;
    private float yDiffBig = 1.4f;

    private float xpos = -2.5f;
    private float Ypos = -4.5f;

    //currentCube property
    string smallTag = "smallPlatform";
    string bigTag = "bigPlatform";

	void Start ()
    {
        candleList = new List<GameObject>();
        _player = GameObject.FindGameObjectWithTag("Player");
        for(int i=0;i<15;++i)
            generateCandle();
    }
	
  public void generateCandle()
    {
        int random = Random.Range(0, 3);

        if (random < 2)
        {
            generatesmallCube();
        }
        else
        {
            generatebigCube();
        }

    }
    void generatesmallCube()
    {
        xpos += xDiff;
        Ypos += yDiffSmall;

        int tileNo = Random.Range(0, tiles.Length - 1);
        Vector3 pos = new Vector3(xpos, Ypos);

        GameObject cube = Instantiate(tiles[tileNo], pos, tiles[tileNo].transform.rotation);
        cube.tag = smallTag;
        
    }

    void generatebigCube()
    {
        xpos += xDiff;
        Ypos += yDiffBig;

        int tileNo = Random.Range(0, tiles.Length - 1);
        Vector3 pos = new Vector3(xpos, Ypos);

        GameObject cube = Instantiate(tiles[tileNo], pos, tiles[tileNo].transform.rotation);
        cube.tag = bigTag;

    }
}
