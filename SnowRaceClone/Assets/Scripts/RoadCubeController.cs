using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RoadCubeController : MonoBehaviour
{
    #region Variables

    private SnowBallController snowBallController;
    private MeshRenderer roadCubeMeshRenderer;
    private GameObject snowBall;
    private PlayerController playerController;
    public bool isEnabledBoxCollider;
    

    #endregion

    

    void Start()
    {
        snowBall = GameObject.Find("SnowBallCreateEmpty");
        snowBallController = snowBall.GetComponent<SnowBallController>();
        roadCubeMeshRenderer = GetComponent<MeshRenderer>();
        roadCubeMeshRenderer.enabled = false;
    }

    


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (snowBallController.isActiveRoad)
            {
                roadCubeMeshRenderer.enabled = true;
                GetComponent<BoxCollider>().enabled = true;
                isEnabledBoxCollider = true;
            }
            else
            {
                if (roadCubeMeshRenderer.enabled == false)
                {
                    isEnabledBoxCollider = false;
                    GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }
}
