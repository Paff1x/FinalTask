using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour
{
    [SerializeField] private Image joystickBG;
    [SerializeField] private Image joystick;
    [SerializeField] private Canvas canvas;
    public static float Horizontal { get; private set; }
    public static float Vertical { get; private set; }
    
    private Vector2 inputVector;
    private bool isTouched;
    private Vector2 startedJoystickBGposition;
    private Camera worldCam;

    private void Start()
    {
        startedJoystickBGposition = joystickBG.rectTransform.position;
        worldCam = canvas.worldCamera;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isTouched)
                joystickBG.rectTransform.position = Input.mousePosition;
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform, Input.mousePosition, worldCam, out pos);

            
            pos.x = (pos.x / joystickBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystickBG.rectTransform.sizeDelta.y);
            inputVector = new Vector2(pos.x * 2 , pos.y * 2 );
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
            joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBG.rectTransform.sizeDelta.x / 2), inputVector.y * (joystickBG.rectTransform.sizeDelta.y / 2));
            isTouched = true;
        }
        else
        {
            inputVector = Vector2.zero;
            joystick.rectTransform.anchoredPosition = Vector2.zero;
            isTouched = false;
            joystickBG.rectTransform.position = startedJoystickBGposition;
        }
        Horizontal = inputVector.x;
        Vertical = inputVector.y;
    }
}
