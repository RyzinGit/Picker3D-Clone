using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform player;
    void LateUpdate()
    {
        transform.position = new Vector3(0, 4f, player.position.z - 2.7f );
    }
}

