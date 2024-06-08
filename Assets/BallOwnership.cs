using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BallOwnership : NetworkBehaviour
{
    private NetworkVariable<bool> BallOwner = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone,
       NetworkVariableWritePermission.Server);
    public void TransferOwnership(ulong clientId)
    {
        Debug.Log("TransferOwnership"+ clientId);
            TransferOwnerServerRpc(clientId);


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ServerRpc(RequireOwnership = false)]
    private void TransferOwnerServerRpc(ulong clientId)
    {
        Debug.Log("TransferOwnerServerRpc");
        GetComponent<NetworkObject>().ChangeOwnership(clientId);
    }
}
