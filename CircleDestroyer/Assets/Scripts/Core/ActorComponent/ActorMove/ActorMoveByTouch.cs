﻿using System.Collections.Generic;
using System.Linq;
using Core.InputModule;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Core.ActorModel
{
    public class ActorMoveByTouch : ActorComponent
    {
        [Inject] private InputProcess _inputProcess;
        
        private const float StartSpeed = 10f;
        private const float SpeedLossByTime = 1f;
        private const float MinSpeed = 6f;
        
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
        
        private void Update()
        {
            Move();
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
            
            var speed = Time.deltaTime * _currentSpeed;
            
            ComponentActor.transform.position = 
                Vector2.MoveTowards(ComponentActor.transform.position, firstPos, speed);

            if (_currentSpeed > MinSpeed)
            {
                _currentSpeed -= Time.deltaTime * SpeedLossByTime;
            }
        }
    }
}