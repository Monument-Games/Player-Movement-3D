using UnityEngine;

namespace MonumentGames.PlayerMovement3D
{
    using Config;

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 10f;
        [SerializeField] private float sprintSpeed = 15f;
        [SerializeField] private float horizontalMouseMovement = 1f;
        [SerializeField] private float verticalMouseMovement = 1f;
        [SerializeField] private bool invertVerticalMouse;
        [SerializeField] private bool invertHorizontalMouse;
	    private Rigidbody rb;

        public Camera cam;

	    public void Awake() {
	        rb = GetComponent<Rigidbody>();
	    }

        public void Start() {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Update()
        {
            float movSpeed = 0;

            // Look if Sprint Key is pressed and set movementSpeed accordingly
            if (Input.GetKeyDown(Config.cfg.sprintKey))
                movSpeed = sprintSpeed;
            else
                movSpeed = movementSpeed;

            // Calculate the added distance of the different axis based on the move speed
            var xMovement = Input.GetAxis("Vertical") * Time.deltaTime * movSpeed;
            var yMovement = Input.GetAxis("Horizontal") * Time.deltaTime * movSpeed;

            // Adding the walked distance to the current position
            // and telling the Rigidbody to move to that new position
	        Vector3 newPos = Quaternion.Euler(0, -90, 0) * transform.rotation * new Vector3(xMovement, 0, -yMovement);
            rb.MovePosition(transform.position + newPos);

            // Calculate the rotation for the mouse movement
            var h = horizontalMouseMovement * Input.GetAxis("Mouse X") * (invertHorizontalMouse ? -1 : 1);
            var v = verticalMouseMovement * Input.GetAxis("Mouse Y") * (invertVerticalMouse ? -1 : 1);

            // Add the horizontal movement to the rotation of the camera
            transform.eulerAngles += new Vector3(0, h, 0);

            // Clamp the vertical movement between 90 and 270°
            // and apply the rotation to the camera
            if(cam.transform.eulerAngles.x + v > 90 && cam.transform.eulerAngles.x + v < 270) v = 0;
            cam.transform.eulerAngles += new Vector3(v, 0, 0);
        } 

        public void SetMovementSpeed(float speed) => movementSpeed = speed;

        public float GetMovementSpeed() => movementSpeed;

        public void SetSprintSpeed(float speed) => sprintSpeed = speed;

        public float GetSprintSpeed() => sprintSpeed;

        public void SetHorizontalSpeed(float speed) => horizontalMouseMovement = speed;

        public float GetHorizontalSpeed() => horizontalMouseMovement;

        public void SetVerticalSpeed(float speed) => verticalMouseMovement = speed;

        public float GetVerticalSpeed() => verticalMouseMovement;

        public void SetInvertHorizontal(bool toggle) => invertHorizontalMouse = toggle;

        public bool GetInvertHorizontal() => invertHorizontalMouse;

        public void SetInvertVertical(bool toggle) => invertVerticalMouse = toggle;

        public bool GetInvertVertical() => invertVerticalMouse;
    }
}
