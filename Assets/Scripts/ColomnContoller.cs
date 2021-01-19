using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColomnContoller : MonoBehaviour
{
    private Transform colomnTransform;
    float swipe;
    private float speed = 5f;


    void Start()
    {
        colomnTransform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        UserInputs();
    }

    private void UserInputs()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector3 Rotation = Input.GetTouch(0).deltaPosition;
            transform.Rotate(0, Rotation.x * speed * Time.deltaTime, 0);
        }
#else
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                swipe = Input.GetAxis("Horizontal") * speed;
                colomnTransform.Rotate(Vector3.down, swipe);
            }
    #endif

    }
}
