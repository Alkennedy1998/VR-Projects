using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    //Get all map light objects
    GameObject[] mapLights = GameObject.FindGameObjectsWithTag("mapLights");

    public ConstantForce gravity;
    private Vector3 gForce = new Vector3(0.0f, -6.81f, 0.0f);
    // Use this for initialization
    private readonly float radius = 0.9f;
    private float power = 150000;

    public Vector3 startPosition;
    public Vector3 endPosition;

    private float distance = -.05f;

    private float currentLerpTime = 0;
    private float lerpTime = 5;
    //public Transform parentButton = transform.parent;

    private int isTriggered = 0;


    void Start()
    {
        //Get list of all map lights




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
        //Randomly select explosion position from an array of 14 different positions
       

        GameObject parent = GameObject.Find("Explosions");
        List<Vector3> locations = new List<Vector3>();
        foreach(Transform child in parent.transform)
        {
            //Debug.Log(child.gameObject.name);
            locations.Add(child.transform.position);
        }

        Vector3 explosionPos = locations[Random.Range(0, parent.transform.childCount)];

        //Array of all colliders within the radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);



  
        //Perform action on each ragdoll
        foreach (Collider hit in colliders)
        {
            if (hit.gameObject!=null&&hit.gameObject.CompareTag("ragdoll"))
            { 
                //Explosions and all things included
               hit.attachedRigidbody.useGravity = true;            
          
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
            }
            
        }

        //Turn a random light on

        int oneOrTwo = Random.Range(0, 1);
        int lightRange = Random.Range(0, mapLights.Count());
        if (oneOrTwo == 0)
        {
            mapLights[lightRange].transform.Find("Point Light").gameObject.Light.color = Color.red;
        }
        else
        {
            mapLights[lightRange].transform.Find("Point Light").gameObject.Light.color = Color.white;
        }

        isTriggered = 1;

    }

   
    void OnTriggerExit(Collider col)
    {
        isTriggered = 2;
    }
}






