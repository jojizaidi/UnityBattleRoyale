using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkController : NetworkBehaviour
{
    CharacterController controller;
    PlayerMovement playerMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            controller = GetComponent<CharacterController>();
            playerMovement = GetComponent<PlayerMovement>();

            StartCoroutine(AssignPosition());
        }
    }

    IEnumerator AssignPosition()
    {
        yield return new WaitForEndOfFrame();
        controller.enabled = false;
        playerMovement.enabled = false;
        yield return new WaitForEndOfFrame();

        NetworkManager nm = FindObjectOfType<NetworkManager>();
        int totalPlayers = nm.numPlayers;

        GameObject spawnParent = GameObject.Find("SpawnPoints");
        transform.position = spawnParent.transform.GetChild(totalPlayers - 1).position;
        yield return new WaitForEndOfFrame();
        controller.enabled = true;
        playerMovement.enabled = true;
    }

}
