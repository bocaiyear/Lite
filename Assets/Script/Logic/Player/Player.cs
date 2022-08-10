
using System;
using UnityEngine;

namespace Script.Logic
{
    [Serializable]    
    public class Player : MonoBehaviour
    {
        public static Player Instance;
        
        [SerializeField]
        private float speed = 6f;

        private Vector3 targetPos;
        private Vector3 moveDir;
        private bool isMoving;

        private Animator animator;
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Awake()
        {
            Instance = this;
            animator = GetComponent<Animator>();
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
            animator.SetFloat(Speed, speed);
            CameraMgr.FollowTarget(tf);
        }

        private void Move()
        {
            Transform ts;
            (ts = transform).Translate(moveDir * (speed * Time.deltaTime), Space.World);
            float targetDistance = (ts.position - targetPos).sqrMagnitude;
            if (targetDistance < .1f)
            {
                Stop();
            }
        }

        private void Stop()
        {
            isMoving = false;
            animator.SetFloat(Speed, 0);
            CameraMgr.StopFollow();
        }

        private void OnFootstep(AnimationEvent e)
        {
            
        }
    }
}