using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;

public class PlayerController15 : MonoBehaviour
{
    float speed = 5.00f;

    public Animator playerAnim;
    public GameObject TimeCounter;
    int Counter = 10;
    // Start is called before the first frame update
    void Start()
    {
        TimeCounter.GetComponent<Text>().text = "Coin Collected: " + Counter.ToString("0");
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
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetBool("IsJump", true);
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            playerAnim.SetBool("IsJump", false);
        }
        */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Counter--;
            TimeCounter.GetComponent<Text>().text = "Coin Collected: " + Counter.ToString("0");
        }
    }
    void StartRun()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ConeTag"))
        {
            Debug.Log("Actived PlaneB 90deg rotation");
        }
    }
}
