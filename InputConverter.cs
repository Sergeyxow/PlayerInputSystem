using UnityEngine;

namespace TheSaboteur.Systems
{
    public class InputConverter
    {
        private Vector3 _prevPos = Vector3.zero;
        public Touch ConvertMouseToTouch()
        {
            Touch touch = new Touch();

            KeyCode lmb = KeyCode.Mouse0;

            var mousePosition = Input.mousePosition;
            
            if (Input.GetKeyDown(lmb))
            {
                touch.phase = TouchPhase.Began;
                
                _prevPos = Vector3.zero;
            }
            else if (Input.GetKey(lmb))
            {
                if (mousePosition != _prevPos)
                    touch.phase = TouchPhase.Moved;
                else
                    touch.phase = TouchPhase.Stationary;

                _prevPos = mousePosition;
            }
            else if (Input.GetKeyUp(lmb))
            {
                touch.phase = TouchPhase.Ended;
            }
            else
            {
                touch.phase = TouchPhase.Canceled;
            }

            touch.position = mousePosition;

            return touch;
        }
    }
}