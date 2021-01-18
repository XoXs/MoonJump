using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject particle;

    // Player script  
    public AudioSource audioSource;

    public GameObject dustParticle;
    public float fallMultiplier = 4.5f;
    public float lowJumpMultiplier = 2f;
    public bool firstJump = false;
    public float dropTileTime = 1f;

    //UI
    public Text scoreText;
    public Text gameOver_scoreText;
    public Text AchText;

    public GameObject GameOverScreen;


    Rigidbody2D rb;
    bool Grounded = true;
    private Animator anim;
    private char lastJump = 'N';
    int jumpTarget = 30;
    int level_one = 10;
    float lastYpos;
    Color[] standColor = new Color[] {Color.red,Color.green,Color.blue,Color.yellow };
    int standColorIndex = 0;
    // add
    private float ShowAddAfter = 3;
    private int audioState;
    void Awake()
    {

        // audio and adds count
    
        if (!PlayerPrefs.HasKey("audio"))
        {
            PlayerPrefs.SetInt("audio", 1);
            PlayerPrefs.SetInt("addCount", 0);
            PlayerPrefs.Save();
        }
        audioState = PlayerPrefs.GetInt("audio");
        audioSource = this.GetComponent<AudioSource>();
       
        
        Time.timeScale = 2f;
       
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        lastYpos = -1000f;

        //if(Application.platform == RuntimePlatform.Android)
       // AdManager.instance.RequestInterstitial();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Jump(true);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Jump(false);
        }

        if (transform.position.y+5f < lastYpos)
            GameOver();
    }
    void FixedUpdate()
    {

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (Grounded)
                Jump(true);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }
    public void Jump(bool longjump)
    {
        firstJump = true;
        audioSource.Play();

        if (!Grounded)
            return;

        if (longjump)
        {
            StartCoroutine(longJump());
        }
        else
        {
            lastJump = 'S';

            rb.AddForce(new Vector2(9.8f * 12f, 9.8f * 20f));
        }
        anim.SetTrigger("jump");
        Grounded = false;
       
        
    }
    IEnumerator longJump()
    {
        lastJump = 'B';
        rb.AddForce(new Vector2(0, 9.8f * 29f));
      
        yield return new WaitForSeconds(0.15f);
        
        rb.AddForce(new Vector2(9.8f * 12f,0));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        string smallTag = "smallPlatform";
        string bigTag = "bigPlatform";

        if (lastJump == 'B' && col.gameObject.tag == bigTag)
        {
            lastYpos = transform.position.y;
        }
        else if (lastJump == 'S' && col.gameObject.tag == smallTag)
        {
            lastYpos = transform.position.y;
        }
        else if (lastJump == 'N')
        {
            lastYpos = transform.position.y;
        }
        else
        {
            GameOver();
           // AdManager.instance.show();
            return;
        }

        if (col.gameObject.tag.Contains("Platform") )
        {
           
            //**********
            rb.velocity = Vector3.zero;

            //********* COLOR of jumped Player
            GameObject cube = col.gameObject;
            Renderer cr = cube.GetComponent<Renderer>();
            cr.material.SetColor("_Color" ,standColor[standColorIndex]);
            //********* Effects
            GameObject dustTemp = Instantiate(dustParticle, new Vector3(transform.position.x, transform.position.y-0.5f, transform.position.z), Quaternion.identity);
           Destroy(dustTemp,1.5f);
                Grounded = true;

            transform.position = new Vector2
                (
                col.gameObject.transform.position.x-0.20f,
                transform.position.y
                );

            if (!firstJump)
                return;


            //update the score
            ScoreUpdate();
           
          StartCoroutine(droptile(col.gameObject));

        }

    }
    IEnumerator droptile(GameObject tile)
    {
        yield return new WaitForSeconds(dropTileTime);
        if(tile != null)
        tile.gameObject.AddComponent<Rigidbody2D>();
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag.Contains("Platform"))
        {
            Grounded = false;    
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag.Contains("Platform"))
        {
            Grounded = true;
        }   
    }

    void ScoreUpdate()
    {
        int score = 0;
        int lowjump = score + 1;
        int jumps = int.Parse(scoreText.text) + 50;
        int jumps2 = int.Parse(gameOver_scoreText.text) + 50;
        scoreText.text = (jumps).ToString();
        gameOver_scoreText.text = (jumps2).ToString();


        if (jumps == jumpTarget)
        {
            jumpTarget = jumpTarget * 2;
            AcheivementAchive(jumps);
        }

        if (jumps == 10000)
        {


        }
/*
        if (jumps == 500)
        {
            // Instantiate(particle, transform.position += Vector3.up * 10.0f, Quaternion.identity);
            Instantiate(particle, transform.position, Quaternion.identity);
            Debug.Log("Coinfix");
        }

        if (jumps == 1000)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            Debug.Log("Coinfix");
        }

        if (jumps == 1500)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            Debug.Log("Coinfix");
        }

        if (jumps == 2000)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            Debug.Log("Coinfix");
        }

        if (jumps == 2500)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            Debug.Log("Coinfix");
        }

        if (jumps == 3000)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            Debug.Log("Coinfix");
        }
*/
    }
    void AcheivementAchive(int jumps)
    {
        // acheivement code here...
        AchText.text = jumps.ToString();
        //tile color
        if (standColorIndex <= standColor.Length - 1)
            standColorIndex++;
        else
            standColorIndex = 0;
        //drop tile time
        if (dropTileTime <= 0.35f) {
            dropTileTime = 0.356f;
            return;
        }
        dropTileTime -= 0.15f;
    }

    void GameOver()
    {
        if (GameOverScreen.activeSelf)
            return;
        Handheld.Vibrate();

        GameOverScreen.SetActive(true);
        Camera.main.GetComponent<CameraFollow>().follow = false;
        GameObject.Find("Background").GetComponent<BackgroundMove>().enabled = false;
        
        //leaderBoard score updation
   
       GameObject.Find("Services").GetComponent<services>().ReportScore(int.Parse(scoreText.text));
        Debug.Log(scoreText.text);


        // ads programming
        int addcount = PlayerPrefs.GetInt("addCount");
        PlayerPrefs.SetInt("addCount", addcount+1);
        PlayerPrefs.Save();
        // show add after "ShowAddAfter" deaths
        if (addcount >= ShowAddAfter)
        {
            print("show add...");
            PlayerPrefs.SetInt("addCount", 0);
            PlayerPrefs.Save();

        }

       
    }
  
}
