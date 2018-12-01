using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    //Get all map light objects
    // GameObject[] mapLights = GameObject.FindGameObjectsWithTag("mapLights");
    public GameObject[] mapLights;

    public ConstantForce gravity;
    private Vector3 gForce = new Vector3(0.0f, -6.81f, 0.0f);
    // Use this for initialization
    private readonly float radius = 0.9f;
    private float power = 150000;

    public Vector3 startPosition;
    public Vector3 endPosition;

    float lerpTime = 1f;
    float currentLerpTime;
    private float moveDistance = -.05f;

    //public Transform parentButton = transform.parent;

    private int isTriggered = 0;

    private float secondCounter;
    private float secondCounter2;

    void Start()
    {
        //Get list of all map lights



        mapLights = GameObject.FindGameObjectsWithTag("mapLights");

        foreach (GameObject light in mapLights)
        {
            light.transform.Find("Point Light").gameObject.GetComponent<Light>().color = Color.white;
            light.transform.Find("Point Light").gameObject.GetComponent<Light>().intensity = 0.8f;
            light.transform.Find("Point Light").gameObject.GetComponent<Light>().range = 5;
        }


        startPosition = transform.parent.position;
        endPosition = transform.parent.position+Vector3.up*moveDistance;
        //endPosition = new Vector3(transform.parent.position.x, (transform.parent.position.y - 7), transform.parent.position.z);
    }


    // Update is called once per frame
    void Update()
    {
        secondCounter += Time.deltaTime;
        secondCounter2 += Time.deltaTime;
    }

    void OnTriggerEnter(Collider col)
    {
        if (secondCounter < 2)
        {
            return;
        }
        if (secondCounter2 < 0.3)
        {
            return;
        }
        secondCounter2 = 0;
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
                Debug.Log("explosion hit");
                //Explosions and all things included
               hit.attachedRigidbody.useGravity = true;            
          
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
            }
            
        }

        //Turn a random light on
        int oneOrTwo = Random.Range(0, 2);
        int lightRange = Random.Range(0, mapLights.Length);
        Debug.Log(mapLights[0]);

        if (oneOrTwo == 0)
        {
            mapLights[lightRange].transform.Find("Point Light").gameObject.GetComponent<Light>().color = Color.red;
            //mapLights[lightRange].transform.GetChild(3).gameObject.GetComponent<Light>().intensity = 100;
           // mapLights[lightRange].GetComponent<Light>().intensity = 100;
            //mapLights[lightRange].GetComponent<Light>().color =Color.red;
        }
        if(oneOrTwo == 1)
        {
            mapLights[lightRange].transform.Find("Point Light").gameObject.GetComponent<Light>().color = Color.white;
            //mapLights[lightRange].transform.Find("Point Light").gameObject.GetComponent<Light>().intensity = 100;
            //mapLights[lightRange].transform.GetChild(3).Intensity = 100;
            //mapLights[lightRange].GetComponent<Light>().intensity = 100;
            //mapLights[lightRange].GetComponent<Light>().color = Color.white;


        }
        
        transform.parent.position=endPosition;

    }
    
   
    void OnTriggerExit(Collider col)
    {
        transform.parent.position = startPosition;
    }
}






