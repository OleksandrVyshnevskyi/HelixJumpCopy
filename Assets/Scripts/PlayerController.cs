using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;
    public float height = 350f;
    private bool tracker = false;
    public GameObject jumpSplat;
    private int score = 0;
    private int best;
    private bool isSmall = false;

    public Text victoryText;
    public Text lostText;
    public Text scoreText;
    public Text bestScoreText;

    public Button replay;
    public Button exit;


    private void Awake()
    {
        best = PlayerPrefs.GetInt("Bestscore");
        scoreText.text = "Score: " + score.ToString();
        bestScoreText.text = "Best: " + best.ToString();

    }

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tracker == true)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Allowed")
        {
            Jumper();
            //Debug.Log(" jump ");
            GetComponent<ParticleSystem>().Play(true);
        }

        if (collision.gameObject.tag == "Restricted")
        {
            //Debug.Log(" lost ");
            lostText.gameObject.SetActive(true);
            tracker = true;
        }

        if (collision.gameObject.tag == "End" && tracker == false)
        {
            //Debug.Log(" Victory ");
            victoryText.gameObject.SetActive(true);
            replay.gameObject.SetActive(true);
            exit.gameObject.SetActive(true);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        var obj = Instantiate(jumpSplat,collision.transform);
        //Debug.Log(collision.gameObject.name);
        var place = collision.transform.position;
        obj.transform.position = new Vector3(place.x, place.y + 0.3f, place.z + 2.4f);
    }

    private void Jumper()
    {
        playerRb.AddForce(Vector3.up * height);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cleaner")
        {
            score += 6;
            scoreText.text = "Score: " +  score.ToString();
            //Debug.Log(score);

            if (score > best)
            {
                best = score;
                PlayerPrefs.SetInt("Bestscore", best);
            }

            bestScoreText.text = "Best: " + best.ToString();

            //lil joy aspect
            if (isSmall != true)
            {
                gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                isSmall = !isSmall;
                gameObject.GetComponent<TrailRenderer>().startWidth /= 2;
                playerRb.mass -= 0.2f;
            }
                
            else if (isSmall == true)
            {
                gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                isSmall = !isSmall;
                gameObject.GetComponent<TrailRenderer>().startWidth *= 2;
                playerRb.mass += 0.2f;
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LevelReload()
    {
        SceneManager.LoadScene(0);
    }
}
