using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Invector.vCharacterController;
using Unity.VisualScripting;
using UnityEngine.Animations;

public class SnowBallController : MonoBehaviour
{
    #region Variables

    [SerializeField]  GameObject player;
    [SerializeField]  GameObject snowBall;
    public float momentScale;
    public float startScale;
    private vThirdPersonInput vThirdPersonInput;
    private PlayerController playerController;
    public bool isPlayerCollid;
    public bool isActiveRoad = true;
    private float decreaseValueAmount = 0.6f/4;
    
    

    #endregion
    

    void Start()
    {
        vThirdPersonInput = GameObject.FindWithTag("Player").GetComponent<vThirdPersonInput>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        StartCoroutine(SnowBallVisibilityRoutine());
        startScale = transform.localScale.x;
        isActiveRoad = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        momentScale = transform.localScale.x;
        
        if (momentScale >= startScale)
        {
            if (isPlayerCollid == false && playerController.aboveRoadCube.Equals("Cube"))
            {
                IncreaseSnowBallScale();
            }
        }
        
    }

    private void LateUpdate()
    {
        transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, player.transform.eulerAngles.y,
            player.transform.eulerAngles.z);
    }

    public IEnumerator SnowBallVisibilityRoutine()
    {
        snowBall.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        snowBall.gameObject.SetActive(true);
        
    }
    public void IncreaseSnowBallScale()
    {
        if (vThirdPersonInput.isMove && momentScale <1.5f)
        {
            startScale += 0.2f;
            if (startScale >= 1.5f)
            {
                return;
            }
            transform.DOScale(new Vector3(startScale,startScale,startScale), .4f);
            if (ControlPassTheBridge().Equals("isActive is true"))
            {
                isActiveRoad = true;
            }
        }
    }

    public void decreaseSnowBallScale(Collision collision)
    {
        ControlPassTheBridge();

        isActiveRoad = true;
        if (collision.gameObject.GetComponent<RoadCubeController>().isEnabledBoxCollider == false &&
            collision.gameObject.GetComponent<RoadCubeController>().isSataration == false)
        {
            transform.DOScale(new Vector3(startScale, startScale, startScale), .5f);
            collision.gameObject.GetComponent<RoadCubeController>().isSataration = true;
        }
    }

    private String ControlPassTheBridge()
    {
        startScale -= decreaseValueAmount;
        if (startScale <= 0.6f)
        {
            isActiveRoad = false;
            return "isActive is false";
        }
        else
        {
            return "isActive is true";
        }
    }
}
