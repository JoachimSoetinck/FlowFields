using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 20.0f;
    [SerializeField]
    private float zoomspeed = 20.0f;
  
    private Vector3 OriginalPos;

    private void Start()
    {
        OriginalPos = transform.position;
    }
    public void Update()
    {

        Vector3 pos = transform.position;
      
        if (Input.GetKey("w"))
        {
          
            pos.z += speed * Time.deltaTime;
        }

        if (Input.GetKey("s")  )
        {

            pos.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {

            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {

            pos.x -= speed * Time.deltaTime;
        }

        if (Input.GetKey("r"))
        {

            pos = OriginalPos;
        }
      
        transform.position = pos;
      
    }
}

