using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningScript : MonoBehaviour
{
    public GameObject currentFloor;
    public float partsSpeed = 10f;
    private Rigidbody rb;
    //private int scoreInitial = 0;
    //private int newScore;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            var children = currentFloor.transform.childCount;
            for (int i = 0; i < children; i++)
            {
                if (currentFloor.transform.GetChild(i).tag == "Allowed" || currentFloor.transform.GetChild(i).tag == "Restricted")
                {
                    StartCoroutine(Push(currentFloor.transform.GetChild(i)));
                }
                if (currentFloor.transform.GetChild(i).tag == "Cleaner")
                {
                    Destroy(currentFloor.transform.GetChild(i).gameObject, 0.5f);
                }
            }
        }
    }

    public IEnumerator Push(Transform partTrans)
    {
        var randVector = new Vector3(Random.Range(-6, 6), partsSpeed, Random.Range(-3, 3));
        rb = partTrans.gameObject.AddComponent<Rigidbody>();
        if (rb != null)
        {
            rb.mass = 0.6f;
            rb.AddForce(Vector3.back * partsSpeed + randVector, ForceMode.Impulse);
            Destroy(partTrans.gameObject, 0.8f);
            yield return null;
        }
        
    }

}
