using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

namespace NetworkPrototype
{
    public class LockOnCamera : MonoBehaviourPun
    {
        [SerializeField]
        private GameObject _VirtualCamera;
        [SerializeField]
        private Transform _TargetPosition;
        [SerializeField]
        private string _AxisName;
        [SerializeField]
        private float _LockOnRange;
        private Transform _TeamMate;
        private GameObject[] _PlayersInScene;
        private CinemachineTargetGroup _TargetGroup;
        private CinemachineTargetGroup.Target[] _Targets = new CinemachineTargetGroup.Target[2];
        private int _MyTeam;

        void Awake()
        {
            _TargetGroup = GetComponentInChildren<CinemachineTargetGroup>();
            _MyTeam = GetComponentInChildren<PlayerMovement>()._TeamNumber;
            if(photonView.IsMine)
            {
            _Targets[1].target = this.gameObject.transform;

            }
        }

        void Update()
        {
            if (Input.GetButton(_AxisName) && _VirtualCamera != null)
            {
                //_VirtualCamera.SetActive(true);
                //_Targets[0].target = FindTeamMate();
            }

            else
            {
                _VirtualCamera.SetActive(false);
            }
        }

        private Transform FindTeamMate()
        {
            _PlayersInScene = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in _PlayersInScene)
            {
                if (player.GetComponent<PlayerMovement>()._TeamNumber == _MyTeam)
                {
                    if (player != this.gameObject)
                {
                    _VirtualCamera.GetComponent<CinemachineVirtualCamera>().LookAt = player.transform;
                    return player.GetComponentInChildren<Target>().transform;
                }
                 }
            }

            return null;
        }
    }
}
