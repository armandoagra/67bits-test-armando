using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform backgroundRect;
    [SerializeField] private RectTransform handleRect;

    private Vector2 inputVector;


    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundRect, eventData.position, eventData.pressEventCamera, out position))
        {
            position.x = (position.x / backgroundRect.sizeDelta.x) * 2;
            position.y = (position.y / backgroundRect.sizeDelta.y) * 2;

            inputVector = new Vector2(position.x, position.y);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            handleRect.anchoredPosition = new Vector2(inputVector.x * (backgroundRect.sizeDelta.x / 2), inputVector.y * (backgroundRect.sizeDelta.y / 2));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        backgroundRect.gameObject.SetActive(true);
        backgroundRect.position = eventData.position;
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handleRect.anchoredPosition = Vector2.zero;
        backgroundRect.gameObject.SetActive(false);
    }

    public Vector3 GetInputVector()
    {
        return inputVector;
    }

}
