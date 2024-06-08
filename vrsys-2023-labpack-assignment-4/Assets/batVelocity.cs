using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batVelocity : MonoBehaviour
{
    public int counter=1;
    public Vector3 lastposition;
    public Rigidbody batrigidbody;
    // Start is called before the first frame update
    void Start()
    {
        batrigidbody = GetComponent<Rigidbody>();
        lastposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public Vector3 GetBatVelocity()
    {
        lastposition = transform.position;
        Vector3 velocity = (transform.position - lastposition) / Time.deltaTime;
        
        Debug.Log("velocity" + velocity + "counter" + counter);
        counter++;
        return velocity;
    }
}
