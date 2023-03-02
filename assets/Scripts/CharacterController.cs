using Wiwa;
using WiwaApp;

namespace Game
{
    [Component]
    public struct CharacterControllerCmp
    {
        public float velocity;
        public float camYOffset;
        public float camYAngle;

        public float fireInterval;
        public float fireTimer;
    }
    class CharacterController : Behaviour
    {
        void Update()
        {
            ref CharacterControllerCmp character = ref GetComponent<CharacterControllerCmp>();
            ref Transform3D transform = ref GetComponent<Transform3D>();

            UpdateCamera(ref character, ref transform);
            UpdateInput(ref character, ref transform);

            Fire(ref character);
        }
        private void Fire(ref CharacterControllerCmp character)
        {
            float shootX = Input.GetAxis(Gamepad.GamePad1, GamepadAxis.RightX);
            float shootY = Input.GetAxis(Gamepad.GamePad1, GamepadAxis.RightY);

            character.fireTimer += Time.DeltaTime();

            if (character.fireTimer >= character.fireInterval)
            {
                character.fireTimer = 0.0f;

                if (System.Math.Abs(shootX) > 0.5f || System.Math.Abs(shootY) > 0.5f)
                {
                    Vector3 bulletDir = new Vector3(shootX, 0, shootY);
                    SpawnBullet(new Vector3(0, 0, 0), bulletDir, 0);
                }
            }
        }
        private void UpdateInput(ref CharacterControllerCmp character, ref Transform3D transform)
        {
            Vector3 forward = Math.CalculateForward(ref transform);
            Vector3 right = Math.CalculateRight(ref transform);
            //ref Rigidbody rigidbody = ref GetComponent<Rigidbody>();
            float translation = character.velocity * Time.DeltaTime();

            Vector3 direction = new Vector3(0, 0, 0);
            direction += GetInputFromKeyboard(forward, right, translation);
            direction += GetInputGamepad(forward, right, translation);

            transform.LocalPosition += direction;
            //PhysicsManager.SetLinearVelocity(m_EntityId, direction);
        }
        private Vector3 GetInputGamepad(Vector3 forward, Vector3 right, float translation)
        {
            Vector3 direction = new Vector3(0, 0, 0);

            direction += forward * translation * Input.GetAxis(Gamepad.GamePad1, GamepadAxis.LeftX);
            direction += right * translation * Input.GetAxis(Gamepad.GamePad1, GamepadAxis.LeftY);


            return direction;
        }
        private Vector3 GetInputFromKeyboard(Vector3 forward, Vector3 right, float translation)
        {
            Vector3 direction = new Vector3(0, 0, 0);

            if (Input.IsKeyDown(KeyCode.W))
            {
                direction += forward * translation;
            }
            else if (Input.IsKeyDown(KeyCode.S))
            {
                direction -= forward * translation;
            }
            if (Input.IsKeyDown(KeyCode.A))
            {
                direction += right * translation;
            }
            else if (Input.IsKeyDown(KeyCode.D))
            {
                direction -= right * translation;
            }

            return direction;
        }

        private void UpdateCamera(ref CharacterControllerCmp character, ref Transform3D transform)
        {
            System.UInt64 cam_id = CameraManager.GetActiveCamera();

            Vector3 campos = transform.Position;
            campos.y = transform.Position.y + character.camYOffset;

            CameraManager.SetPosition(cam_id, campos);
            CameraManager.SetCameraRotation(cam_id, new Vector3(transform.LocalRotation.y, character.camYAngle, 0));
        }


        void SpawnBullet(Vector3 bullet_offset, Vector3 direction, float rot)
        {
            ref Transform3D parent = ref GetComponent<Transform3D>(m_EntityId);

            ulong entity = CreateEntity();

            // Take components of entity
            ref Transform3D t3d = ref GetComponent<Transform3D>(entity);
            ref BulletComponent bc = ref AddComponent<BulletComponent>(entity);
            AddMesh(entity, "bullet", "assets/03_mat_addelements.wimaterial");

            // Change position and scale
            //t3d.LocalPosition.x += 0.58f;
            //t3d.LocalPosition.y += 1.74f;
            //t3d.LocalPosition.z += 1.82f;
            t3d.LocalPosition += parent.LocalPosition;
            t3d.LocalRotation.y = 90.0f + rot;
            t3d.LocalScale.x = t3d.LocalScale.y = t3d.LocalScale.z = 0.1f;

            // Add bullet component
            bc.Velocity = 20.0f;
            bc.TimeToDestroy = 1.5f;
            bc.direction = direction;

            // Activate controller
            ApplySystem<BulletController>(entity);
            ApplySystem<MeshRenderer>(entity);
        }
    }
}