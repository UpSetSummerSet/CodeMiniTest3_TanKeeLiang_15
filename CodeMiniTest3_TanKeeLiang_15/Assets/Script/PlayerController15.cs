using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController15 : MonoBehaviour
{
    float speed = 6.00f;
    float Timer = 10;
    int PowerUpsCollected = 4;
    bool ConeActivated = false;
    float CountDown = 1;
    float JumpForce = 8.0f;
    float gravityModifier = 2.00f;
    int SpaceTrack = 2;

    public Animator playerAnim;
    public GameObject TimeCounter;
    public Animator PlaneBAnim;
    public bool isHitBox = false;

    public Rigidbody playerRb;
    public Renderer playerRen;
    public Material[] playerMtrs;

    // Start is called before the first frame update
    void Start()
    {
        if (ConeActivated == true)
        {
            TimeCounter.GetComponent<Text>().text = "Time Left: " + Timer.ToString("0");
        }
        Debug.Log(PowerUpsCollected);

        Physics.gravity *= gravityModifier;
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
        /*
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
        else if(Timer >= 0)
        {
            PlaneBAnim.SetBool("ConeActivated", false);
            TimeCounter.GetComponent<Text>().text = "" + "";
        }

        Jumpforce();
    }

    void StartRun()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    private void Jumpforce()
    {
        if (Input.GetKeyDown(KeyCode.Space) && SpaceTrack > 1)
        {
            SpaceTrack--;
            playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            playerAnim.SetBool("IsJump", true);

            playerRen.material.color = playerMtrs[0].color;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            playerAnim.SetBool("IsJump", false);
        }
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
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HitBox"))
        {
            isHitBox = true;
        }
        if (collision.gameObject.CompareTag("PlayPlane"))
        {
            SpaceTrack = 2;
            playerRen.material.color = playerMtrs[1].color;
        }
    }
}
