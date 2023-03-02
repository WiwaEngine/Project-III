using System;
using Wiwa;

namespace WiwaApp
{
    using EntityId = System.UInt64;

    [Component]
    public struct TankTurretComp
    {
        public float rotY;
        public float velX;
        public Vector3 bulletOffset;
        public float bulletZOffset;

        public float fireInterval;
        public float fireTimer;
    }
    public class TankTurret : Behaviour
    {
        //Called the first frame
        void Awake()
        {
        }
        //Called after Awake()
        void Init()
        {
        }
        //Called every frame
        void Update()
        {
            ref TankTurretComp tank = ref GetComponent<TankTurretComp>();

            tank.fireTimer += Time.DeltaTime();

            CameraControls(ref tank);

            ref Transform3D transform = ref GetComponent<Transform3D>();

            Transform3D transform_forward = transform;
            transform_forward.Rotation.x = 0.0f;
            transform_forward.Rotation.y += transform_forward.LocalRotation.z;
            Vector3 forward = Wiwa.Math.CalculateForward(ref transform_forward);

            if (Input.IsKeyDown(KeyCode.Space) && tank.fireTimer >= tank.fireInterval)
            {
                tank.fireTimer = 0.0f;

                Vector3 bullet_offset = transform.Position + tank.bulletOffset + forward * tank.bulletZOffset;

                SpawnBullet(bullet_offset, forward, transform_forward.Rotation.y);
            }
        }

        void SpawnBullet(Vector3 bullet_offset, Vector3 direction, float rot)
        {
            EntityId entity = CreateEntityNamed("bullet");

            // Take components of entity
            ref Transform3D t3d = ref GetComponent<Transform3D>(entity);
            //ref BulletComponent bc = ref AddComponent<BulletComponent>(entity);
            AddMesh(entity, "bullet", "assets/03_mat_addelements.wimaterial");

            ref Rigidbody rb = ref AddComponent<Rigidbody>(entity);
            rb.scalingOffset.x = 1;
            rb.scalingOffset.y = 1;
            rb.scalingOffset.z = 1;
            rb.mass = 1;
            rb.gravity.x = 0;
            rb.gravity.y = -10.0f;
            rb.gravity.z = 0;
            rb.isTrigger = false;
            rb.isSensor = false;
            rb.selfTag = 0;
            rb.filterBits ^= (-0 ^ rb.filterBits) & (1 << 32);

            ref ColliderCube collCube = ref AddComponent<ColliderCube>(entity);
            collCube.halfExtents.x = 3;
            collCube.halfExtents.y = 3;
            collCube.halfExtents.z = 3;

            
            // Change position and scale
            //t3d.LocalPosition.x += 0.58f;
            //t3d.LocalPosition.y += 1.74f;
            //t3d.LocalPosition.z += 1.82f;
            t3d.LocalPosition += bullet_offset;
            t3d.LocalPosition.y += 5;
            //t3d.LocalRotation.y = 90.0f + rot;
            t3d.LocalScale.x = t3d.LocalScale.y = t3d.LocalScale.z = 0.1f;

            // Add bullet component
            //bc.Velocity = 20.0f;
            //bc.TimeToDestroy = 1.5f;
            //bc.direction = direction;

            // Activate controller
            //ApplySystem<BulletController>(entity);
            ApplySystem<MeshRenderer>(entity);
            ApplySystem<PhysicsSystem>(entity);
            PhysicsManager.SetLinearVelocity(entity, direction * 100);
            Console.WriteLine("entity: " + entity + "direction " + direction.x + " " + direction.y + " " + direction.z);
            PhysicsManager.AddBodyToLog(entity);
        }

        private void CameraControls(ref TankTurretComp tank)
        {
            ref Transform3D transform = ref GetComponent<Transform3D>();
            tank.rotY -= Input.GetMouseXDelta() * tank.velX * Time.DeltaTime();

            transform.LocalRotation.z = tank.rotY;
        }
    }
}
