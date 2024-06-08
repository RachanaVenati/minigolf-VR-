using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManipulationSelector : NetworkBehaviour
{
    #region Member Variables

    private NetworkVariable<bool> isGrabbed = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server);
    public HitBall hitvar;
    public GameObject canvas_score_BFV;
    public GameObject canvas_goal_BVF;
    public GameObject canvas_Distance;
    public ButtonFollowVisual BFV;
    public BallOwnership BO;
    public BallOwnership playB;
    public BallOwnership stopB;
    public BallOwnership resetB;
    public BallOwnership farB;
    public BallOwnership closeB;
    public BallOwnership inc_velo;
    public BallOwnership dec_velo;
    public BallOwnership goal;
    #endregion

    #region Selector Methods
    public void GoalSync(int value)
    {

        if(IsServer)
        {
           // Debug.Log("Visibilitysync IsServer");
            ChangeScoreClientClientRpc(value,2);
        }
        else
        {
            ChangeScoreServerServerRpc(value,2);
        }
    }
    public void ScoreBoardSync(int value)
    {
        if(IsServer)
        {
            ChangeScoreClientClientRpc(value,1);
        }
        else
        {
            ChangeScoreServerServerRpc(value,1);
        }
    }
   public void DistanceSync(string distance)
    {
        if(IsServer)
        {
            ChangeDistanceClientClientRpc(distance);
        }
        else
        {
            ChangeDistanceServerServerRpc(distance);
        }
    }
    public bool RequestGrab()
    {
        if (!isGrabbed.Value)
        {
            if (!IsOwner)
                TransferOwnershipServerRpc(NetworkManager.Singleton.LocalClientId);

            SetIsGrabbedServerRpc(true);
            return true;
        }
        
        return false;
    }

    public void Release()
    {
        SetIsGrabbedServerRpc(false);
    }

    #endregion

    #region RPCs

    [ServerRpc(RequireOwnership = false)]
    private void TransferOwnershipServerRpc(ulong clientId)
    {
        GetComponent<NetworkObject>().ChangeOwnership(clientId);
        BO.TransferOwnership(clientId);
        playB.TransferOwnership(clientId);
        stopB.TransferOwnership(clientId);
        resetB.TransferOwnership(clientId);
        inc_velo.TransferOwnership(clientId);
        dec_velo.TransferOwnership(clientId);
        farB.TransferOwnership(clientId);
        closeB.TransferOwnership(clientId);
        goal.TransferOwnership(clientId);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetIsGrabbedServerRpc(bool grabbed)
    {
        isGrabbed.Value = grabbed;
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeScoreServerServerRpc(int value,int decide)
    {
        if(decide==1)
        {
            hitvar.counter = value;
            canvas_score_BFV.GetComponent<TMPro.TextMeshProUGUI>().SetText(value.ToString());

        }
        if(decide==2)
        {
            hitvar.gc = value;
            canvas_goal_BVF.GetComponent<TMPro.TextMeshProUGUI>().SetText(value.ToString());

        }
    }

    [ClientRpc]
    public void ChangeScoreClientClientRpc(int value,int decide)
    {
        if (decide == 1)
        {
            hitvar.counter = value;
            canvas_score_BFV.GetComponent<TMPro.TextMeshProUGUI>().SetText(value.ToString());

        }
        if (decide == 2)
        {
            hitvar.gc = value;
            canvas_goal_BVF.GetComponent<TMPro.TextMeshProUGUI>().SetText(value.ToString());

        }

    }
    [ServerRpc(RequireOwnership = false)]
    public void ChangeDistanceServerServerRpc(string distance)
    {
        canvas_Distance.GetComponent<TMPro.TextMeshProUGUI>().SetText(distance);  

    }
    [ClientRpc]
    public void ChangeDistanceClientClientRpc(string distance)
    {
        canvas_Distance.GetComponent<TMPro.TextMeshProUGUI>().SetText(distance);
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeVisibilityServerServerRpc(bool value)
    {
        BFV.Golfball.SetActive(value);
        BFV.Golfclub2.SetActive(value);
    }
    [ClientRpc]
    public void ChangeVisibilityClientClientRpc(bool value)
    {
        Debug.Log("ChangeVisibilityClientClientRpc"+value);

        BFV.Golfball.SetActive(value);
        BFV.Golfclub2.SetActive(value);
    }
    #endregion
}
