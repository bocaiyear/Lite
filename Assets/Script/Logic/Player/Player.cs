
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.Logic
{
    [Serializable]    
    public class Player : MonoBehaviour
    {
        public static Player Instance;
        
        [SerializeField]
        private float speed = 10f;

        private Vector3 targetPos;
        private Vector3 moveDir;
        private bool isMoving;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (isMoving)
            {
                Move();
            }
        }

        public void MoveTo(Vector3 targetPos)
        {
            this.targetPos = targetPos;
            isMoving = true;
            var tf = transform;
            moveDir = (targetPos - tf.position).normalized;
            tf.forward = moveDir;
            
            CameraMgr.FollowTarget(tf);
        }

        private void Move()
        {
            Transform ts;
            (ts = transform).Translate(moveDir * (speed * Time.deltaTime), Space.World);
            if ((ts.position - targetPos).sqrMagnitude < .001f)
            {
                isMoving = false;
                CameraMgr.StopFollow();
            }
        }
    }
}