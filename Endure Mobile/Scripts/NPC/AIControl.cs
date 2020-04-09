
namespace Endure
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public class AIControl : MonoBehaviour
    {
        public bool usedByZombie = false;

        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public NPC character { get; private set; }  
        public ZombieNPC z_character { get; private set; }// the character we are controlling
        public Vector3 target;                                                     // target to aim for
        
        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();

            if (!usedByZombie)
                character = GetComponent<NPC>();
            else
                z_character = GetComponent<ZombieNPC>();
            

            agent.updateRotation = false;
            agent.updatePosition = true;
        }
        
        private void Update()
        {
            if (target != Vector3.zero)
                agent.SetDestination(target);

            if (agent.remainingDistance > agent.stoppingDistance) {
                if (!usedByZombie) {
                    character.Move(agent.desiredVelocity, false, false);
                }
                else
                {
                    z_character.Move(agent.desiredVelocity, false, false);
                }
                    }
            else {
                if(!usedByZombie)
                character.Move(Vector3.zero, false, false);
                else
                    z_character.Move(Vector3.zero, false, false);
            }
        }
        
        public void SetTarget(Vector3 target)
        {
            this.target = target;
        }
    }
}
