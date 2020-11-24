using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController15 : MonoBehaviour
{
    float speed = 10.00f;

    public Animator playerAnim;
    public GameObject TimeCounter;
    float Timer = 10;
    int PowerUpsCollected = 4;
    public Animator PlaneBAnim;
    bool ConeActivated = false;
    float CountDown = 1;
    public bool isHitBox = false;

    // Start is called before the first frame update
    void Start()
    {
        if (ConeActivated == true)
        {
            TimeCounter.GetComponent<Text>().text = "Time Left: " + Timer.ToString("0");
        }
        Debug.Log(PowerUpsCollected);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            StartRun();
            transform.rotation = Quaternion.Euler(0, 0, 0);
            playerAnim.SetBool("IsSprint", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            StartRun();
            transform.rotation = Quaternion.Euler(0, 180, 0);
            playerAnim.SetBool("IsSprint", true);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.SetBool("IsSprint", false);
            playerAnim.SetFloat("IsRun", 0.25f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            StartRun();
            transform.rotation = Quaternion.Euler(0, -90, 0);
            playerAnim.SetBool("IsSprint", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            StartRun();
            transform.rotation = Quaternion.Euler(0, 90, 0);
            playerAnim.SetBool("IsSprint", true);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.SetBool("IsSprint", false);
            playerAnim.SetFloat("IsRun", 0.25f);
        }
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetBool("IsJump", true);
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            playerAnim.SetBool("IsJump", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Timer--;
            TimeCounter.GetComponent<Text>().text = "Coin Collected: " + Counter.ToString("0");
        }
        */
        if (ConeActivated == true && Timer >= 0)
        {
            TimeCounter.GetComponent<Text>().text = "Time Left: " + Timer.ToString("0");
            Timer -= CountDown * Time.deltaTime;
        }
        else
        {
            TimeCounter.GetComponent<Text>().text = "" + "";
            PlaneBAnim.SetBool("ConeActivated", false);
        }
    }

    void StartRun()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TagCone"))
        {
            ConeActivated = true;
            PlaneBAnim.SetBool("ConeActivated", true);
            Debug.Log("Actived PlaneB 90deg rotation");
        }
        if (other.gameObject.CompareTag("PowerUp"))
        {
            PowerUpsCollected--;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Drop"))
        {
            SceneManager.LoadScene("YouLose");
        }
        if (other.gameObject.CompareTag("HitBox"))
        {
            isHitBox = true;
        }
    }
}
