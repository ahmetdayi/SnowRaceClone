using System;
using UnityEngine;
using DG.Tweening;
using Invector.vCharacterController;

public class PlayerController : MonoBehaviour
{
    private SnowBallController snowBallController;
    private vThirdPersonInput vThirdPersonInput;
    public string aboveRoadCube;
    

    private void Start()
    {
        snowBallController = GameObject.Find("Player/SnowBallCreateEmpty").GetComponent<SnowBallController>();
        vThirdPersonInput = GetComponent<vThirdPersonInput>();
    }

    private void Update()
    {
        PlayerLastBorder();
    }

    private void PlayerLastBorder()
    {
        if (transform.position.z >= vThirdPersonInput.reachMaxZ && !snowBallController.isActiveRoad )
        {
            transform.position =
                new Vector3(transform.position.x, transform.position.y, vThirdPersonInput.reachMaxZ - 0.1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RoadCube"))
        {
            snowBallController.isPlayerCollid = true;
            aboveRoadCube = collision.gameObject.tag;
            snowBallController.decreaseSnowBallScale(collision);
        }

        if (collision.gameObject.CompareTag("Cube"))
        {
            aboveRoadCube = collision.gameObject.tag;
            snowBallController.isPlayerCollid = false;
        }
    }
}
