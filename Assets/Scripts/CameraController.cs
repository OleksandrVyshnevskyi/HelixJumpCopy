using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
       
        offset = new Vector3(0f, 2f, 6.5f);
#else
        offset = new Vector3(0f, 1.5f, 5.5f);
#endif
        transform.localPosition = target.transform.localPosition + offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.transform.localPosition.y + offset.y < transform.localPosition.y)
        {
            transform.localPosition = target.transform.localPosition + offset;
        }
    }
}
