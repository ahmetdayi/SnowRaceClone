using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Invector.vCharacterController;
using Unity.VisualScripting;

public class SnowBallController : MonoBehaviour
{
    #region Variables

    [SerializeField]  GameObject player;
    [SerializeField]  GameObject snowBall;
    private Vector3 distanceBetweenPlayerandSnowBall;
    private float momentScale;
    private float startScale;
    private vThirdPersonInput vThirdPersonInput;
    private ArrayList roadCubes;
    private PlayerController playerController;
    private ArrayList oldRoadCubes;
    public bool isPlayerCollid;
    public bool isActiveRoad = true;
    

    #endregion
    

    void Start()
    {
        vThirdPersonInput = GameObject.FindWithTag("Player").GetComponent<vThirdPersonInput>();
        roadCubes = new ArrayList();
        oldRoadCubes = new ArrayList();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        AddRoadCubeInArray();
        distanceBetweenPlayerandSnowBall = snowBall.transform.position - player.transform.position;
        StartCoroutine(SnowBallVisibilityRoutine());
        startScale = transform.localScale.x;
        isActiveRoad = true;
        oldRoadCubes.Add(123341111);
    }

    private void AddRoadCubeInArray()
    {
        foreach (var roadCube in GameObject.FindGameObjectsWithTag("RoadCube"))
        {
            roadCubes.Add(roadCube);
        }
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

        if(momentScale <= startScale)
        {
           
            if (isPlayerCollid == true)
            { 
                decreaseSnowBallScale();
            
            }
            
        }
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
    public void IncreaseSnowBallScale()
    {
        if (vThirdPersonInput.isMove && momentScale <1.5f)
        {
            startScale += 0.2f;
            if (startScale >= 1.5f)
            {
                return;
            }
            transform.DOScale(new Vector3(startScale,startScale,startScale), 1f);
            
        }
    }

    public void decreaseSnowBallScale()
    {
        startScale -= 0.6f/4;
            if (startScale <= 0.6f)
            {
                isActiveRoad = false;
                return;
            }
           
            isActiveRoad = true;
            foreach (GameObject roadCube in roadCubes)
            {
                
                foreach (float oldRoadCube in oldRoadCubes)
                {
                    if (roadCube.gameObject.GetInstanceID() != oldRoadCube)
                    {
                        transform.DOScale(new Vector3(startScale,startScale,startScale), .5f);
                        oldRoadCubes.Add(roadCube.gameObject.GetInstanceID());
                        return;
                    }
                 
                }
            }
            
            
    }
    
}
