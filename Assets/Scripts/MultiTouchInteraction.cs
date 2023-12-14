using UnityEngine;

public class MultiTouchInteraction : MonoBehaviour
{
    private Vector3 touchStartPos;
    private Vector3 touchOffset;
    private bool isDragging = false;
    private float initialDistance;
    private Vector3 initialScale;
    private Vector3 initialRotation;

    private void Update()
    {
        // Check for touch input
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Check if the touch started on the prefab
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                    {
                        touchStartPos = touch.position;
                        touchOffset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(touchStartPos.x, touchStartPos.y, transform.position.z));
                        isDragging = true;
                    }
                    break;

                case TouchPhase.Moved:
                    // Drag the prefab with one finger (restrict to X and Z axes)
                    if (isDragging)
                    {
                        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, transform.position.z)) + touchOffset;
                        transform.position = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
                    }
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    break;
            }
        }
        else if (Input.touchCount == 2)
        {
            // Handle two-finger rotation and scaling
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touch0.position, touch1.position);
                initialScale = transform.localScale;
                initialRotation = transform.rotation.eulerAngles;
            }
            else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(touch0.position, touch1.position);
                float scaleFactor = currentDistance / initialDistance;

                // Scale the prefab
                transform.localScale = initialScale * scaleFactor;

                // Calculate rotation angle based on initial and current positions
                Vector3 currentRotation = transform.rotation.eulerAngles;
                float rotationAngle = Mathf.DeltaAngle(initialRotation.z, currentRotation.z);

                // Rotate the prefab
                transform.Rotate(Vector3.forward, rotationAngle);
            }
        }
    }
}
