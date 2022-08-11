
using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Script.Logic
{
    [Serializable]    
    public class Player : MonoBehaviour
    {
        public static Player Instance;

        [SerializeField] private float targetSpeed = 6f;
        
        private float speed;
        private Vector3 targetPos;
        private Vector3 moveDir;
        private TweenerCore<float,float,FloatOptions> moveTween;

        private Animator animator;
        private static readonly int SpeedId = Animator.StringToHash("Speed");

        private void Awake()
        {
            Instance = this;
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (speed > 0)
            {
                Move();
            }
        }

        public void MoveTo(Vector3 targetPos)
        {
            this.targetPos = targetPos;
            var tf = transform;
            moveDir = (targetPos - tf.position).normalized;
            tf.forward = moveDir;
            CameraMgr.FollowTarget(tf);
            
            if (speed < .01f)
            {
                speed = 1f;
                moveTween = DOTween.To(() => speed, x =>
                {
                    speed = x;
                    animator.SetFloat(SpeedId, speed);
                }, targetSpeed, 1);
            }
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
            speed = 0;
            animator.SetFloat(SpeedId, speed);
            CameraMgr.StopFollow();
            moveTween?.Kill();
        }

        private void OnFootstep(AnimationEvent e)
        {
            
        }
    }
}