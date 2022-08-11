using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Veriables

    [SerializeField] private GameObject player;
    private Vector3 distance;

    #endregion
    void Start()
    {
       distance = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        CameraFollow();
    }

    void CameraFollow()
    {
       

        transform.position = player.transform.position + distance;
    }
}
