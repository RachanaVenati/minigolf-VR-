using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonactions : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Golfball;
    public GameObject Golfclub1;
    public GameObject Golfclub2;
    public GameObject play_button;
    public GameObject stop_button;
    public GameObject reset_button;
    public Vector3 initial_position;
    public Vector3 initial_golfclub;
    void Start()
    {
        initial_position = Golfball.transform.position;
        initial_golfclub = Golfclub2.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play_executeOnClick()
    {
        Debug.Log("Play_executeOnClick");
        
        Golfball.SetActive(true);
        // Golfclub1.SetActive(true);
        Golfclub2.SetActive(true);
    }
    public void Stop_executeOnClick()
    {
        Debug.Log("Stop_executeOnClick");
        Golfball.SetActive(false);
        // Golfclub1.SetActive(true);
        Golfclub2.SetActive(false);
    }
    public void Reset_executeOnClick()
    {

        Debug.Log("Reset_executeOnClick");
        Golfball.transform.position = initial_position;
        Golfclub2.transform.position = initial_golfclub;
    }

}
