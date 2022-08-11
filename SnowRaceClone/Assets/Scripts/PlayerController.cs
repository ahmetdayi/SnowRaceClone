using System;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private RaycastHit raycastHit;
    public GameObject snowBall;
    private float rotateSpeed = 30;
    private float snowBallScale;
    private float reSnowBallScale =1.1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        snowBallScale = snowBall.transform.localScale.x;
        Debug.Log(snowBallScale);
        IncreaseSnowBallScale();
        

    }

    void IncreaseSnowBallScale()
    {
        if (snowBallScale < 1.5f)
        {
            snowBall.transform.DOScale(new Vector3(reSnowBallScale,reSnowBallScale, reSnowBallScale), 3f);
            reSnowBallScale += 0.1f;
           
        }
        else
        {
            
        }
    }
}
