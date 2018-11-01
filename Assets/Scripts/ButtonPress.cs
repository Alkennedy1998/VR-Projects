using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{


    // Use this for initialization
    public float radius = 50.0F;
    public float power = 100.0F;

    public Vector3 startPosition;
    public Vector3 endPosition;

    private float distance = -.05f;

    private float currentLerpTime = 0;
    private float lerpTime = 5;
    //public Transform parentButton = transform.parent;

    private int isTriggered = 0;

    void Start()
    {
        startPosition = transform.parent.position;
        endPosition = transform.parent.position+Vector3.up*distance;
        //endPosition = new Vector3(transform.parent.position.x, (transform.parent.position.y - 7), transform.parent.position.z);
    }


    // Update is called once per frame
    void Update()
    {
        if (isTriggered == 1||isTriggered==2)
        {


            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float Perc = currentLerpTime / lerpTime;

            if (isTriggered == 1)
            {
                transform.parent.position = endPosition;
                //transform.parent.position = Vector3.Lerp(startPosition, endPosition, Perc);
            }
            if(isTriggered == 2)
            {
                transform.parent.position = startPosition;
                //transform.parent.position = Vector3.Lerp(endPosition, startPosition, Perc);
            }
 
            isTriggered = 0;
        }
    }




    void OnTriggerEnter(Collider col)
    {
        //Begin button y transform


        Vector3 explosionPos = GameObject.Find("BOD").transform.position;

        Debug.Log("You pressed the button");
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }

        isTriggered = 1;
        //transform.parent.position = Vector3.Lerp(startPosition, endPosition, Time.deltaTime*0.1f);

    }

    void OnTriggerExit(Collider col)
    {
        isTriggered = 2;
    }
}






