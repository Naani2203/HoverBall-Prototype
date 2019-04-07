using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NetworkPrototype
{


    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : PlayerUnit
    {
        [Header("Movement")]
        [SerializeField]
        private float _Speed=0f;
        [SerializeField]
        private float _MaxSpeed=0f;
        [SerializeField]
        private float _RotationValue=0f;
        [SerializeField]
        private float _InitialSpeedDivider;
        [SerializeField]
        private Transform _Camera;
        [SerializeField]
        private Transform _Ground;
        [SerializeField]
        private float _GroundRadius;
        [SerializeField]
        private float _AdjustSpeed;
        [SerializeField]
        private float _FloatingHeight;
        [SerializeField]
        private float _UturnForce;
        [SerializeField]
        private float _DashDelayTime;
        [SerializeField]
        private float _DashForce;
        [SerializeField]
        private MovementAssist _MoveAssist;

        private Vector3 _Vel;
        private Vector3 _Rot;
        private Rigidbody _RB;
        private Player _Player;
        private Animator _Anim;
        private float _VerticalAxis;
        private float _HorizontalAxis;
        private float _VelocityMag;
        private Vector3 _Force;
        private Vector3 _Forward;
        private Vector3 _CamPosition;
        private bool _IsGrounded;
        private float _TempRot;
        private bool _CanUturn;
        private bool _CanDash=true;
        private float _Delay = 0;
        private float _AngularDrag;
        private float _Drag;

        private PhotonView _PhotonView;
        private Vector3 _TargetPosition;
        private Quaternion _TargetRotation;
        

        private void Awake()
        {
            _RB = GetComponent<Rigidbody>();
            _AngularDrag = _RB.angularDrag;
            _Drag = _RB.drag;
            _Player = GetComponent<Player>();
            _PhotonView = GetComponent<PhotonView>();
           
        
        }
        private void Update()
        {
            if(_PhotonView.IsMine)
            {
                ReadInput();
                AngularMovement();
                _IsGrounded = ReadGround();           
                CheckDashDelay();
               _MoveAssist.RotateToGround();
            }
            else
            {
                //SmoothMove();
                Destroy(GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>());
            }
        }

        private void FixedUpdate()
        {
            if (_IsGrounded == true && _PhotonView.IsMine)
            {
                ApplyMovePhysics();
                Dash();
            }
            if(_PhotonView.IsMine)
            DisableDrag();
        }

        private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
            }
            else
            {
                _TargetPosition = (Vector3)stream.ReceiveNext();
                _TargetRotation = (Quaternion)stream.ReceiveNext();
            }

        }

        private void SmoothMove()
        {
            transform.position = Vector3.Lerp(transform.position, _TargetPosition, 0.25f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _TargetRotation, 500 * Time.deltaTime);
        }

        private void ReadInput()
        {
            _VerticalAxis = Input.GetAxis("Vertical");
            _HorizontalAxis = Input.GetAxis("Horizontal");
            _CamPosition = _Camera.position;
            _CamPosition.y = transform.position.y;
            _Forward = transform.position - _CamPosition;
            _Forward.y = _RB.velocity.y;
            _Force = transform.forward * _Speed;
        }

        private void ApplyMovePhysics()
        {
            if (Input.GetAxis("Fire1")>0&&_IsGrounded==true)
            {
                Debug.Log("Dash");
                if (_RB.velocity.sqrMagnitude < (_MaxSpeed ) * (_MaxSpeed ))
                {
                    _RB.AddForce(_Force);                            
                }
            }
            else
            {
                ApplyInitialMovement();
            }        
        }

        private void ApplyInitialMovement()
        {
            if(_RB.velocity.sqrMagnitude < (_MaxSpeed/_InitialSpeedDivider)* (_MaxSpeed / _InitialSpeedDivider)&&_IsGrounded==true)
            _RB.AddForce(_Force);            
        }

        private void AngularMovement()
        {            
             transform.Rotate(Vector3.up * _HorizontalAxis * _RotationValue );  
        }

        private bool ReadGround()
        {
            Collider[] _Colliders = Physics.OverlapSphere(_Ground.position, _GroundRadius);
            for (int i = 0; i < _Colliders.Length; i++)
            {
                if (_Colliders.Length > 1 && _Colliders[i].tag =="Level")
                {
                    return true;
                }

            }
            return false;
        }
        private void DoAUturn()
        {
           if(Input.GetButtonDown("Fire2"))
            {
                _RB.AddForce(Vector3.up * _UturnForce);
                transform.Rotate(Vector3.right * Mathf.Lerp(_TempRot,_TempRot+180,0.5f));
            }
        } 

        private void Dash()
        {
            if(_CanDash==true && Input.GetButtonDown("Fire2"))
            {
                _RB.AddForce(transform.forward * _DashForce,ForceMode.Impulse);
                _CanDash = false;
            }
            if(_CanDash==false)
            {
                _Delay += Time.deltaTime;

            }
        }
        private void CheckDashDelay()
        {
            if(_Delay>=_DashDelayTime)
            {
                _CanDash = true;
                _Delay = 0;
            }
        }
        private void DisableDrag()
        {
            if(_IsGrounded==false)
            {
                _RB.drag = 0;
                _RB.angularDrag = 0;
            }
            else
            {
                _RB.drag = _Drag;
                _RB.angularDrag = _AngularDrag;
            }
        }
    }
}
