using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;          // Kéo Player vào đây
    public float mouseSensitivity = 200f; // Tốc độ nhạy của chuột
    public Vector3 offset = new Vector3(0f, 2f, -4f); // Khoảng cách từ camera tới sau lưng Player

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        // Ẩn con trỏ chuột và khóa nó vào giữa màn hình khi chơi game giống PUBG
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Lấy dữ liệu di chuyển của con chuột
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -40f, 60f); // Giới hạn không cho camera ngửa lên/cúi xuống quá đà

        // Tính toán góc xoay của Camera
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0f);

        // Cập nhật vị trí Camera luôn bo theo sau lưng Player dựa trên góc xoay
        transform.position = target.position + rotation * offset;
        
        // Luôn nhìn thẳng vào nhân vật
        transform.LookAt(target.position + Vector3.up * 1f);
    }
}