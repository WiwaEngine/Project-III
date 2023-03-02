using Wiwa;

namespace WiwaApp
{
    [Component]
    public struct PingPongComp
    {
        public float speed;
        public float turnTime;
        public float turnTimer;
        public int direction;
    }
    class MovePingpong : Behaviour
    {
        void Update()
        {
            ref Transform3D t3d = ref GetComponent<Transform3D>();
            ref PingPongComp ppc = ref GetComponent<PingPongComp>();

            ppc.turnTimer += Time.DeltaTime();

            Vector3 forward = Math.CalculateForward(ref t3d);

            if(ppc.turnTimer >= ppc.turnTime)
            {
                ppc.turnTimer = 0.0f;

                ppc.direction *= -1;
            }

            t3d.LocalPosition += forward * ppc.speed * Time.DeltaTime() * ppc.direction;
        }
    }
}
