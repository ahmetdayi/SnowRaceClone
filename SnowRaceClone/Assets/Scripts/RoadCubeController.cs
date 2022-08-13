using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RoadCubeController : MonoBehaviour
{
    #region Variables

    private SnowBallController snowBallController;
    public MeshRenderer roadCubeMeshRenderer;
    private GameObject snowBall;
    private PlayerController playerController;
    public bool isEnabledBoxCollider;
    public bool isSataration = false;
    

    #endregion

    

    void Start()
    {
        snowBall = GameObject.Find("Player/SnowBallCreateEmpty");
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
               
                Debug.Log("else");
                if (roadCubeMeshRenderer.enabled == false)
                {
                    isEnabledBoxCollider = true;
                    
                    GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }
}
