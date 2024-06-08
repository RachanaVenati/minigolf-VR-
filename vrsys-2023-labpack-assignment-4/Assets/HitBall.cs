using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class HitBall : MonoBehaviour
{
    private batVelocity batswingvelocity;
    private Rigidbody ballRigidbody;
    private Rigidbody batvelocityrigidbody;
    public int counter = 0;
    public GameObject canvas_score;
    public GameObject canvas_Goal;
    public GameObject bat;
    public GameObject ball;
    public int gc = 0;
    public float velocity=20f;
    public ManipulationSelector ms;
  //  public GameObject Goal_text;
    public Vector3 initial_position_HB;
    public Vector3 initial_golfclub_HB;
    public Quaternion initial_rotation_HB;
    public Quaternion initial_rotation_club_HB;
    // Start is called before the first frame update
    void Start()
    {
        ballRigidbody = transform.GetComponent<Rigidbody>();
       // batvelocityrigidbody = bat.GetComponent<Rigidbody>();    

        canvas_score = GameObject.Find("score");
        canvas_Goal = GameObject.Find("goal");
       // Goal_text = GameObject.Find("gol_reset");
        initial_position_HB = ball.transform.position;
        initial_rotation_HB= ball.transform.rotation;
        initial_golfclub_HB = bat.transform.position;
        Debug.Log("initial_golfclub_HB HITBALL" + initial_golfclub_HB);
        initial_rotation_club_HB = bat.transform.rotation;
        Debug.Log("initial_rotation_club_HB HITBALL" + initial_rotation_club_HB);

    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("batvelocity rigidbody " + batvelocityrigidbody.velocity.magnitude + " counter " + counter)
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("private void OnTriggerEnter(Collider other)");
        if(other.CompareTag("Bat"))
        {
           // Debug.Log("other.CompareTag IF");

            counter++;
            canvas_score.GetComponent<TMPro.TextMeshProUGUI>().SetText(counter.ToString());//change the strikes value.
           
             Vector3 hitDirection = (transform.position - other.transform.position).normalized;

            Vector3 hitVelocityVector = hitDirection * velocity;
            hitVelocityVector.y = ballRigidbody.velocity.y;
            Debug.Log("velocity" + velocity);
            ms.ScoreBoardSync(counter);
            ballRigidbody.velocity= hitVelocityVector;
           
        }

        if(other.CompareTag("Goal"))
        {
          //  Debug.Log("other.CompareTag");
            gc++;
            canvas_Goal.GetComponent<TMPro.TextMeshProUGUI>().SetText(gc.ToString());//change the strikes value.
            ball.transform.SetPositionAndRotation(initial_position_HB, initial_rotation_HB);
           //bat.transform.SetPositionAndRotation(initial_golfclub_HB, initial_rotation_club_HB);
            Debug.Log("bat.transform.SetPositionAndRotation Position HITBALL " + bat.transform.position + " rotation " + bat.transform.position);
            ms.GoalSync(gc);
        }

    }
}
