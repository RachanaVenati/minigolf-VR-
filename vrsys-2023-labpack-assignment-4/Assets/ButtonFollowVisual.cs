using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
public class ButtonFollowVisual : MonoBehaviour
{
    
    private Transform pokeAttachTransform;
    private XRBaseInteractable interactable;
    private bool isfollowing = false;
    public Transform visualtarget;
    public Vector3 localaxis;
    private Vector3 offset;
    private Vector3 InitialLocalPosition;
    public float speed = 5f;
    public GameObject Golfball;
    public GameObject Golfclub2;
    public Vector3 initial_position;
    public Vector3 initial_golfclub;
    public Quaternion initial_rotation;
    public Quaternion initial_rotation_club;
    public Vector3 initial_pos_user;
    public Quaternion initial_rot_user;
    public HitBall hitball;
    public GameObject canvas_score_BFV;
    public GameObject canvas_Goal_BFV;
    public GameObject user_reset;
    public GameObject canvas_velocity;
    public GameObject canvas_distance;
    public Vector3 close;
    public Vector3 far;
    public GameObject Goal;
    public ManipulationSelector Mani;
    // Start is called before the first frame update
    void Start()
    {
        InitialLocalPosition = visualtarget.localPosition;
        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(Reset);
        initial_position = Golfball.transform.position;
        initial_rotation = Golfball.transform.rotation;
        initial_golfclub = Golfclub2.transform.position;
        initial_rotation_club = Golfclub2.transform.rotation;
        initial_pos_user = user_reset.transform.position;
        initial_rot_user = user_reset.transform.rotation;
        far = new Vector3(100f,0.31f,-90f);
        close = new Vector3(10f,0.31f,45f);
        canvas_velocity.GetComponent<TMPro.TextMeshProUGUI>().SetText("20");
        canvas_distance.GetComponent<TMPro.TextMeshProUGUI>().SetText("FAR");
        // Debug.Log("initial_position in START" + initial_position + " rotation "+ initial_rotation);
        // Debug.Log("initial_golfclub in START" + initial_golfclub + " rotation "+initial_rotation_club);
        Debug.Log(" user_reset.transform.position start " + initial_pos_user + " user_reset.transform.rotation START" + initial_rot_user);
    }


    void Update()
    {
        if(isfollowing)
        {
            
            //Debug.Log("if(isfollowing)");

            Vector3 localtargetposition = visualtarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrained_local_targetposition = Vector3.Project(localtargetposition, localaxis);
            visualtarget.position = visualtarget.TransformPoint(constrained_local_targetposition);
        }
        else
        {
           // Debug.Log("else");

            visualtarget.localPosition = Vector3.Lerp(visualtarget.localPosition,InitialLocalPosition,Time.deltaTime*speed);
          //  Debug.Log("visualtarget.localPosition else "+visualtarget.localPosition);

        }

    }
    public void Reset(BaseInteractionEventArgs hover)
    {
       // Debug.Log(" Reset(BaseInteractionEventArgs hover)");
       

        isfollowing = false;
        

    }
    public void Follow(BaseInteractionEventArgs hover)
    {
  

        if (hover.interactorObject is XRPokeInteractor)
        {
          //  Debug.Log("hover.interactorObject is XRPokeInteractor");
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;
            //Debug.Log("interactor" + interactor.name);
            pokeAttachTransform = interactor.attachTransform;
            offset = visualtarget.position - pokeAttachTransform.position;

            isfollowing = true;

        }
       // Debug.Log("Play_executeOnClick");
        if(transform.parent.name == "PLAY")
        {
            Golfball.SetActive(true);
            Golfclub2.SetActive(true);
           // Mani.Visibilitysync(true);
           // Goal.SetActive(true);

        }
        if (transform.parent.name == "STOP")
        {
            Golfball.SetActive(false);
           Golfclub2.SetActive(false);
           // Mani.Visibilitysync(false);
           // Goal.SetActive(false);

        }
        if (transform.parent.name == "RESET")
        {
            hitball.counter = 0;
            hitball.gc = 0;
            canvas_score_BFV.GetComponent<TMPro.TextMeshProUGUI>().SetText("0");//change the strikes value.
            canvas_Goal_BFV.GetComponent<TMPro.TextMeshProUGUI>().SetText("0");//change the strikes value.
            user_reset.transform.SetLocalPositionAndRotation(initial_pos_user, initial_rot_user);
            Debug.Log("user_reset.transform RESET " + user_reset.transform.position + " rotation " + user_reset.transform.rotation);
            //Debug.Log("Reset_executeOnClick");
            Golfball.transform.SetPositionAndRotation(initial_position, initial_rotation);
            Golfclub2.transform.SetPositionAndRotation(initial_golfclub, initial_rotation_club);
            //Debug.Log("transform.parent.name RESET Ball" + Golfball.transform.position + Golfball.transform.rotation);
            //Debug.Log("transform.parent.name RESET club" + Golfclub2.transform.position + Golfclub2.transform.rotation);
            
        }
        if (transform.parent.name == "Inc_velocity")
        {
           // Debug.Log("Inc_velocity IF " + hitball.velocity);
            hitball.velocity = hitball.velocity + 1f;
            canvas_velocity.GetComponent<TMPro.TextMeshProUGUI>().SetText(hitball.velocity.ToString());//change the velocity value on canvas.
           // Debug.Log("Inc_velocity IF AFTER " + hitball.velocity);
        }
        if (transform.parent.name == "Dec_velocity")
        {
           // Debug.Log("Dec_velocity IF " + hitball.velocity);
            hitball.velocity = hitball.velocity - 1f;
            canvas_velocity.GetComponent<TMPro.TextMeshProUGUI>().SetText(hitball.velocity.ToString());//change the velocity value on canvas.
           // Debug.Log("Dec_velocity IF AFTER " + hitball.velocity);

        }
        if (transform.parent.name == "Inc_distance")
        {
            Goal.transform.position = far;
            canvas_distance.GetComponent<TMPro.TextMeshProUGUI>().SetText(" FAR");
            Mani.DistanceSync(" FAR");
        }
        if (transform.parent.name == "Dec_distance")
        {
            Goal.transform.position = close;
            canvas_distance.GetComponent<TMPro.TextMeshProUGUI>().SetText(" CLOSE");
            Mani.DistanceSync(" CLOSE");
        }


    }
}
