using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalcollide : MonoBehaviour
{
    public GameObject canvas_Goal;
    public int counter=0;
    // Start is called before the first frame update
    void Start()
    {
        canvas_Goal = GameObject.Find("goal");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("inside OnCollisionEnter");
        if (collision.gameObject.name == "golfBall")
        {
            Debug.Log("inside collision.gameObject.name IF");

            counter++;
            canvas_Goal.GetComponent<TMPro.TextMeshProUGUI>().SetText(counter.ToString());//change the goal value.
            
        }
    }
}
