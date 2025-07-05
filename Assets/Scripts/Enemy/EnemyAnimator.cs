using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    public void OnAttack()
    {
        anim.SetBool("attack_b", true);
    }
    public void StopAttack()
    {
        anim.SetBool("attack_b", false);
    }

    public void OnDeath()
    {
        anim.SetBool("dead_b", true);
    }
    public void OnAlert()
    {
        anim.SetBool("alert_b", true);
    }
    public void StopAlert()
    {
        anim.SetBool("alert_b", false);
    }
    public void OnWander()
    {
        anim.SetBool("wander_b", true);
    }
    public void StopWander()
    {
        anim.SetBool("wander_b", false);
    }
    private void Update()
    {
        anim.SetFloat("speed_f", agent.velocity.magnitude);
    }
}
