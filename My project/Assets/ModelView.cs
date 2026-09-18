using UnityEngine;

public class ModelView : MonoBehaviour
{
    [Header("绑定动态生成的模型")]
    public GameObject targetModel;

    public float rotateSpeed = 0.6f;
    public float zoomSpeed = 8f;

    void Update()
    {
        if (targetModel == null) return;

        if (Input.GetMouseButton(0))
        {
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");
            targetModel.transform.Rotate(Vector3.up, h * rotateSpeed, Space.World);
            targetModel.transform.Rotate(Vector3.right, -v * rotateSpeed, Space.Self);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.fieldOfView -= scroll * zoomSpeed;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 15, 90);
    }
}