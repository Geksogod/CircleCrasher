﻿using System.Collections.Generic;
using System.Linq;
using Core.InputModule;
using UnityEngine;
using Zenject;

namespace Core.ActorModel
{
    public class ActorMoveByTouch : ActorComponent
    {
        [Inject] private InputProcess _inputProcess;
        
        private const float StartSpeed = 0.9f;
        private const float SpeedLossByTime = 0.999f;
        private const float MinSpeed = 0.6f;
        
        private readonly List<Vector2> _cashedPos = new();
        private float _currentSpeed;
        private bool _isMove;
        private bool _isSkipNewMove;
        
        public override void Configurate(Actor actor)
        {
            base.Configurate(actor);
            _inputProcess.OnDrag += OnDrag;
            _inputProcess.OnBeginDrag += OnBeginDrag;
            _inputProcess.OnEndDrag += OnEndDrag;
        }
        
        private void FixedUpdate()
        {
            Move();
        }

        private void OnEndDrag(Vector3 obj)
        {
            _isSkipNewMove = false;
        }

        private void OnBeginDrag(Vector3 obj)
        {
            if (_isMove)
            {
                _isSkipNewMove = true;
                return;
            }
            _currentSpeed = StartSpeed;
        }

        private void OnDrag(Vector3 dragPos)
        {
            if (_isSkipNewMove)
            {
                return;
            }
            
            if (_cashedPos.Count <= 0)
            {
                _cashedPos.Add(dragPos);
                return;
            }

            var lastPos = _cashedPos.Last();
            
            if (Vector2.Distance(dragPos, lastPos) >= 0.5f)
            {
                _cashedPos.Add(dragPos);
            }
        }

        private void Move()
        {
            _isMove = _cashedPos.Count > 0;
            if (!_isMove)
            {
                return;
            }
            var firstPos = _cashedPos.First();

            if (Vector2.Distance(firstPos, transform.position) <= 0.01f)
            {
                _cashedPos.RemoveAt(0);
                Move();
            }
            
            var moveStep = (_currentSpeed * Time.deltaTime ) * 25f;
            
            ComponentActor.transform.position = 
                Vector2.MoveTowards(ComponentActor.transform.position, firstPos, moveStep);

            if (_currentSpeed > MinSpeed)
            {
                _currentSpeed *= SpeedLossByTime;
            }
        }

        public void Stop()
        {
            _cashedPos.Clear();
        }
    }
}