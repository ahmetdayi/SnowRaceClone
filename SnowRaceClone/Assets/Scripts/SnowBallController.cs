using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallController : MonoBehaviour
{
    #region Variables

    [SerializeField]  GameObject player;
    [SerializeField]  GameObject snowBall;
    private Vector3 distanceBetweenPlayerandSnowBall;
    

    #endregion

    private void Awake()
    {
       
    }

    void Start()
    {
        distanceBetweenPlayerandSnowBall = snowBall.transform.position - player.transform.position;
        StartCoroutine(SnowBallVisibilityRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
          
        
    }

    private void LateUpdate()
    {
        snowBall.transform.position = player.transform.position + distanceBetweenPlayerandSnowBall;
    }

    public IEnumerator SnowBallVisibilityRoutine()
    {
        snowBall.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        snowBall.gameObject.SetActive(true);
        
    }
}
