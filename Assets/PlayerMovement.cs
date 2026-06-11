using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 720f;
    
    private Transform cameraTransform;

    void Start()
    {
        // Tự động tìm Main Camera trong game để lấy hướng
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDirection.magnitude >= 0.1f && cameraTransform != null)
        {
            // Lấy hướng thẳng phía trước và hướng bên phải của Camera
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            // Triệt tiêu trục Y để nhân vật không bị bay lên trời hoặc lún xuống đất
            camForward.y = 0f;
            camRight.y = 0f;

            // Tính toán hướng di chuyển thực tế dựa trên góc nhìn Camera
            Vector3 moveDirection = camForward.normalized * inputDirection.z + camRight.normalized * inputDirection.x;

            // Dịch chuyển Player
            transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);

            // Xoay mặt chú nhộng về hướng đang di chuyển
            Quaternion toRotation = Quaternion.LookRotation(moveDirection.normalized, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }
}