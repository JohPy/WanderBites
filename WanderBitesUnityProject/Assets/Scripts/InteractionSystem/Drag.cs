using UnityEngine;

/* 
Inspired by: https://www.youtube.com/watch?v=izag_ZHwOtM
*/
public class Drag : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 originalPosition;
    private void Start()
    {
        // Store the original position
        originalPosition = transform.position; 
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        // Remember the offset between the object's center and the click position
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        // Reset to original position when released
        transform.position = originalPosition;
    }
}
