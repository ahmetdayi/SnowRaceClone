﻿using UnityEngine;
using DG.Tweening;
namespace Invector.vCharacterController
{
    public class vThirdPersonInput : MonoBehaviour
    {
        #region Variables

        [Header("Controller Input")] 
        public VariableJoystick joystick = null;
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;
        public bool isMove = false;
        private RoadCubeController roadCubeController;
        private GameObject[] roadCubes;
        public float reachMaxZ;
        
        [Header("Camera Input")]
        public string rotateCameraXInput = "Mouse X";
        public string rotateCameraYInput = "Mouse Y";

        [HideInInspector] public vThirdPersonController cc;
        [HideInInspector] public vThirdPersonCamera tpCamera;
         public Camera cameraMain;

        #endregion

        protected virtual void Start()
        {
            InitilizeController();
            InitializeTpCamera();
            roadCubes = GameObject.FindGameObjectsWithTag("RoadCube");
            roadCubeController = roadCubes[0].GetComponent<RoadCubeController>();
          
        }

        protected virtual void FixedUpdate()
        {
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
        }

        protected virtual void Update()
        {
            InputHandle();                  // update the input methods
            cc.UpdateAnimator();            // updates the Animator Parameters
            FindDisabledRoadCube();
        }

        public virtual void OnAnimatorMove()
        {
            cc.ControlAnimatorRootMotion(); // handle root motion animations 
        }

        #region Basic Locomotion Inputs

        protected virtual void InitilizeController()
        {
            cc = GetComponent<vThirdPersonController>();

            if (cc != null)
                cc.Init();
        }

        protected virtual void InitializeTpCamera()
        {
            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<vThirdPersonCamera>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }
        }

        protected virtual void InputHandle()
        {
            MoveInput();
            CameraInput();
            SprintInput();
            StrafeInput();
            JumpInput();
        }

        public virtual void MoveInput()
        {
            cc.input.x = joystick.Horizontal;
            cc.input.z = joystick.Vertical;

            if (cc.input.x != 0 || cc.input.z != 0)
            {
                isMove = true;
            }
            else
            {
                isMove = false;
            }
            
           

           
        }

        protected virtual void CameraInput()
        {
            if (!cameraMain)
            {
                if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
                else
                {
                    cameraMain = Camera.main;
                    cc.rotateTarget = cameraMain.transform;
                }
            }

            if (cameraMain)
            {
                cc.UpdateMoveDirection(cameraMain.transform);
            }

            if (tpCamera == null)
                return;

            // var Y = Input.GetAxis(rotateCameraYInput);
            // var X = Input.GetAxis(rotateCameraXInput);
            //
            // tpCamera.RotateCamera(X, Y);
        }

        protected virtual void StrafeInput()
        {
            if (Input.GetKeyDown(strafeInput))
                cc.Strafe();
        }

        protected virtual void SprintInput()
        {
            if (Input.GetKeyDown(sprintInput))
                cc.Sprint(true);
            else if (Input.GetKeyUp(sprintInput))
                cc.Sprint(false);
        }

        /// <summary>
        /// Conditions to trigger the Jump animation & behavior
        /// </summary>
        /// <returns></returns>
        protected virtual bool JumpConditions()
        {
            return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
        }

        /// <summary>
        /// Input to trigger the Jump 
        /// </summary>
        protected virtual void JumpInput()
        {
            if (Input.GetKeyDown(jumpInput) && JumpConditions())
                cc.Jump();
        }

        #endregion

        void FindDisabledRoadCube()
        {
            foreach (var roadCube in roadCubes)
            {
                if (roadCube.GetComponent<RoadCubeController>().isEnabledBoxCollider == false )
                {
                    reachMaxZ = roadCube.transform.localPosition.z - (roadCube.transform.localScale.z / 2);
                    
                    return;
                }
            }
        }
    }
}