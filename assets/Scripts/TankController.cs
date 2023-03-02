using Wiwa;

namespace WiwaApp
{
    [Component]
    public struct TankComponent
    {
        public float velocity;
        public float rotateVelocity;
        public Vector3 camOffset;
        public Vector3 camRot;
    }
    class TankController : Behaviour
    {
        void Awake()
        {

        }

        void Init()
        {

        }

        void Update()
        {
            ref TankComponent tank = ref GetComponent<TankComponent>();
            ref Transform3D transform = ref GetComponent<Transform3D>();
            float transAmount = tank.velocity * Time.DeltaTime();
            float rotAmount = tank.rotateVelocity * Time.DeltaTime();

            System.UInt64 cam_id = CameraManager.GetActiveCamera();
            
            Vector3 forward = Math.CalculateForward(ref transform);
            
            Vector3 campos = transform.Position + (tank.camOffset * -forward);
            campos.y = transform.Position.y + tank.camOffset.y;

            CameraManager.SetPosition(cam_id, campos);
            
            Vector3 camrot = tank.camRot;
            camrot.x -= transform.Rotation.y;
            
            CameraManager.SetCameraRotation(cam_id, camrot);

            if (Input.IsKeyDown(KeyCode.W))
            {
                transform.LocalPosition += forward * transAmount;
            }
            else if (Input.IsKeyDown(KeyCode.S))
            {
                transform.LocalPosition -= forward * transAmount;
            }
            if (Input.IsKeyDown(KeyCode.A))
            {
                transform.LocalRotation.y += rotAmount;
            }
            else if (Input.IsKeyDown(KeyCode.D))
            {
                transform.LocalRotation.y -= rotAmount;
            }
        }
    }
}
