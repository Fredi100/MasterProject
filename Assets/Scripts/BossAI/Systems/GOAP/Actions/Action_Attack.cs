using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Attack : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(Goal_Attack) });

    Goal_Attack _attackGoal;
    [SerializeField] private DamageTrigger damageTrigger;
    [SerializeField] 
    private float firerate = 1f;
    [SerializeField] private float nexTimeToFire = 0f;

    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public override float GetCost()
    {
        return 0;
    }

    public override void OnActivated(Goal_Base _linkedGoal)
    {
        base.OnActivated(_linkedGoal);
        
        //choose attack animation to play
        //Animator anim = GetComponent<GameObject>().GetComponent<Animator>();
        //AudioSource audio = GetComponent<AudioSource>();
        //audio.PlayOneShot(MeleeAudio);
        //anim.SetTrigger("Attack");
        
        //spawn collider here
        if (Time.time >= nexTimeToFire)
        {
            nexTimeToFire = Time.time + 1f / firerate;
            if (StatTracker.Instance.getMoreRangedAttacksPerformed())
            {
                damageTrigger.CreateDamageThingForSeconds();
            }
            else if (StatTracker.Instance.getMoreMeleeAttacksPerformed())
            {
                Debug.Log("ranged attack");
                damageTrigger.createRangedAttack();
            }
        }
        
        
        
        
        
            
        //Actually Attack here
        //then change AttackPriority
        //Debug.Log("Attack");
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        
        _attackGoal = null;
    }

    public override void OnTick()
    {
        //var agentPos = Agent.transform.position;
        //var distanceBetween = Vector3.Distance(_attackGoal.MoveTarget, agentPos);
        //if enemy is in range repeat is possible here
        //if (distanceBetween <= _attackGoal.attackRange)
             OnActivated(LinkedGoal);
        
    }
}