using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchController : MonoBehaviour {

    // Use this for initialization
    public float radius = 50.0F;
    public float power = 100.0F;


    

    void Start () {

    }

    // Update is called once per frame
    void Update () {
        Vector3 explosionPos = GameObject.Find("BOD").transform.position;

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("You pressed the button");
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
            }
        }
    }
}


