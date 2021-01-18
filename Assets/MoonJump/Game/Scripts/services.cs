using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.Native.Cwrapper;

public class services : MonoBehaviour
{
    private string leaderboardID;
    private object msg;
    public Image topBar;
    public Text topbarText;
    public GameObject loading;
    public GameObject sidebarMenu;
    AudioSource audi;

    public Sprite[] audioSprite;
    public Button audioBtn;
    public Button sfxBtn;

    // share
    string subject = "";
    string body = "";

    // Use this for initialization
    void Start()
    {
        
       // LeaderboardManager.Login();
        //PlayGamesPlatform.DebugLogEnabled = true;
       // PlayGamesPlatform.Activate();

        leaderboardID = GPGSIds.leaderboard_high_score;
        AudioStart();
        DontDestroyOnLoad(this.transform);

        audi = GetComponent<AudioSource>();
        topBar.color = new Color(0.73f, 0.73f, 0.81f, 1f);
        topbarText.text = "Connecting...";
        Login();
    }

    public void Login()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        if (Social.localUser.authenticated)
        {
            topBar.color = new Color(0.73f, 0.73f, 0.81f, 1f);
            topbarText.text = "Connected...";
            Debug.Log("User is authetifiacted");
            return;
        }

        Social.localUser.Authenticate((bool sucess) => {
            if (sucess)
            {
                topBar.color = new Color(0.73f, 0.73f, 0.81f, 1f);
                topbarText.text = "Connected...";
                Debug.Log("User is authetifiacted!!");
            }
            else
            {
                topBar.color = new Color(0.9622642f, 0.3826201f, 0.1134745f, 1f);
                topbarText.text = "Network Problem!";
                Debug.Log("User is not authetifiacted");
            }

        });
    }
    // leaderBoard
    public void ShowLeaderBoard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_high_score);
       // ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("666690433826");
       // Social.ShowLeaderboardUI();
    }

    public void ReportScore(int score)
    {
        Debug.Log("Das is Service Score: " + score);
        Social.ReportScore(score, GPGSIds.leaderboard_high_score, (bool sucess) => {
            if (sucess)
            {
                Debug.Log("reported Score");
            }
            else
            {
                Debug.Log("Failed report Score");
            }
        });
    }

    public void PlayGame()
    {
        audi.Play();
        ShowLoading();
        SceneManager.LoadScene(1);
    }

    public void ShowLoading()
    {
        loading.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void OnClick_SidebarButton()
    {
        Animator sideAnim = sidebarMenu.GetComponent<Animator>();

        if (sidebarMenu.activeSelf)
        {

            sideAnim.SetTrigger("hide");
            StartCoroutine(wait());
        }
        else
        {
            sidebarMenu.SetActive(true);
        }

        audi.Play();
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.35f);
        sidebarMenu.SetActive(false);

    }

    public void OnClick_AudioBtn()
    {
        int onOff = PlayerPrefs.GetInt("audio");
        if (onOff == 1)
        {
            //sound already onn
            audioBtn.image.sprite = audioSprite[1];
            PlayerPrefs.SetInt("audio", 0);
            PlayerPrefs.Save();
        }
        else
        {
            // sound is off
            audioBtn.image.sprite = audioSprite[0];
            PlayerPrefs.SetInt("audio", 1);
            PlayerPrefs.Save();
        }

    }
    public void OnClick_SFXBtn()
    {
        int onOff = PlayerPrefs.GetInt("audiosfx");
        if (onOff == 1)
        {
            //sound already onn
            sfxBtn.image.sprite = audioSprite[1];
            PlayerPrefs.SetInt("audiosfx", 0);
            PlayerPrefs.Save();
        }
        else
        {
            // sound is off
            sfxBtn.image.sprite = audioSprite[0];
            PlayerPrefs.SetInt("audiosfx", 1);
            PlayerPrefs.Save();
        }

    }
    void AudioStart()
    {
        if (!PlayerPrefs.HasKey("audio"))
        {
            PlayerPrefs.SetInt("audio", 1);
            PlayerPrefs.Save();
        }
        else
        {
            int state = PlayerPrefs.GetInt("audio");
            //if sound is off
            if (state == 0)
            {

                audioBtn.image.sprite = audioSprite[1];
            }
            else
            {
                audioBtn.image.sprite = audioSprite[0];

            }
        }


        if (!PlayerPrefs.HasKey("audiosfx"))
        {
            PlayerPrefs.SetInt("audiosfx", 1);
            PlayerPrefs.Save();
        }
        else
        {
            int state = PlayerPrefs.GetInt("audiosfx");
            //if sound is off
            if (state == 0)
            {

                sfxBtn.image.sprite = audioSprite[1];
            }
            else
            {
                sfxBtn.image.sprite = audioSprite[0];

            }
        }
    }
    public void OnAndroidSharingClick()
    {

        StartCoroutine(ShareAndroidText());

    }
    IEnumerator ShareAndroidText()
    {
        yield return new WaitForEndOfFrame();
        //execute the below lines if being run on a Android device
#if UNITY_ANDROID
        //Reference of AndroidJavaClass class for intent
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        //Reference of AndroidJavaObject class for intent
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        //call setAction method of the Intent object created
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        //set the type of sharing that is happening
        intentObject.Call<AndroidJavaObject>("setType", "text/plain");
        //add data to be passed to the other activity i.e., the data to be sent
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
        //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Text Sharing ");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
        //get the current activity
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        //start the activity by sending the intent data
        AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
        currentActivity.Call("startActivity", jChooser);
#endif
    }


}
