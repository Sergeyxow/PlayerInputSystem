using Leopotam.Ecs;
using RingBoy.Configs;
using TheSaboteur.Systems;
using UnityEngine;
using PlayerInput = RingBoy.Configs.PlayerInput;

namespace RingBoy.Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private Configuration _configuration;
        private PlayerInput _playerInput;
        private RuntimeData _runtimeData;
        private readonly InputConverter inputConverter = new InputConverter();

        public void Run()
        {
            bool isInEditor = false;

#if UNITY_EDITOR
            isInEditor = true;
#endif

            if (Input.touchCount > 0 || isInEditor)
            {
                Touch touch;

                if (isInEditor)
                    touch = inputConverter.ConvertMouseToTouch();
                else
                    touch = Input.GetTouch(0);


                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _playerInput.AnchorPosition = touch.position;
                        _playerInput.PositionFromAnchor = Vector2.zero;
                        
                        _playerInput.PreviousPosition = touch.position;
                        _playerInput.FrameChange = Vector2.zero;
                        break;
                    case TouchPhase.Moved:
                        SetInput(ref _playerInput, touch.position);
                        break;
                    case TouchPhase.Stationary:
                        _playerInput.FrameChange = Vector2.zero;
                        _playerInput.PreviousPosition = touch.position;
                        break;
                    case TouchPhase.Ended:
                        _playerInput.PositionFromAnchor = Vector2.zero;
                        break;
                        
                }
            }
            // if (_playerInput.Input != Vector2.zero)
            //     Debug.Log(_playerInput.Input);
        }

        private void SetInput(ref PlayerInput playerInput, Vector2 touchPos)
        {
            Vector2 input = Vector2.zero;
            input = (touchPos - playerInput.AnchorPosition) * _configuration.InputSensitivity;

            playerInput.PositionFromAnchor = input;
            playerInput.FrameChange = (touchPos - playerInput.PreviousPosition) * _configuration.InputSensitivity;
            
            playerInput.PreviousPosition = touchPos;
        }
    }
}