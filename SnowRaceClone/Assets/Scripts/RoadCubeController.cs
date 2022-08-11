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

    #endregion

    

    void Start()
    {
        snowBall = GameObject.Find("SnowBallCreateEmpty");
        snowBallController =snowBall.GetComponent<SnowBallController>();
        roadCubeMeshRenderer = GetComponent<MeshRenderer>();
        roadCubeMeshRenderer.enabled = false;
    }

   
   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Vector3 fazlalık = snowBall.transform.localScale - new Vector3(0.3f, 0.3f, 0.3f);
            if (fazlalık != Vector3.zero)
            {
                roadCubeMeshRenderer.enabled = true;
                Vector3 endScale = snowBall.transform.localScale - fazlalık;
                snowBall.transform.DOScale(endScale, 3f);
                GetComponent<BoxCollider>().enabled = true;
            }
            else
            {
                GetComponent<BoxCollider>().enabled = false;
            }
           

        }
    }
}
