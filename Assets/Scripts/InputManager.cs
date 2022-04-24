using UnityEngine;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    public event OnTouchInput TouchAction;
    public delegate void OnTouchInput(Vector2 position);

    private Vector2 touchStartPosition;
    private bool isTouching = false;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isTouching == false)
        {
            touchStartPosition = Input.GetTouch(0).position;
            isTouching = true;

            TouchAction?.Invoke(touchStartPosition);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isTouching = false;
        }
    }
}
