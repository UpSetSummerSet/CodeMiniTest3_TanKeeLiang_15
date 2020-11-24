using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlane : MonoBehaviour
{
    bool isMoveBack = true;
    bool isMoveFwd = false;

    float zLowerLimit = 37.53f;
    float zUpperLimit = 57.47f;

    public GameObject playerGO;

    float moveSpeed = 5.00f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGO.GetComponent<PlayerController15>().isHitBox)
        {
            if (isMoveBack && !isMoveFwd)
            {
                if (transform.position.z >= zLowerLimit)
                {
                    transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
                }
                else
                {
                    isMoveBack = !isMoveBack;
                    isMoveFwd = !isMoveFwd;
                }
            }

            if (isMoveFwd && !isMoveBack)
            {
                if (transform.position.z <= zUpperLimit)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
                }
                else
                {
                    isMoveBack = !isMoveBack;
                    isMoveFwd = !isMoveFwd;
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        collision.transform.parent = null;
    }
}
