using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CreepAI.Behaviour
{
    public class ChoseRandomWaypoint : Action
    {
        public SharedTransformList waypoints;
        public SharedInt activeWaypointIndex;
        public float strictness = 0.5f;
        public float cooldown = 10f;
        public float selectionRange = 25f;
        private float timer;

        public override TaskStatus OnUpdate()
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                return TaskStatus.Success;
            }

            timer = cooldown;

            if (Random.Range(0f, 1f) > strictness || activeWaypointIndex.Value == -1)
                activeWaypointIndex.Value = SelectRandomNearWaypointIndex();

            return TaskStatus.Success;
        }
        
        private int SelectRandomNearWaypointIndex()
        {
            List<int> validWaypointIndices = new List<int>();

            for (int i = 0; i < waypoints.Value.Count; i++)
            {
                float distance = Vector3.Distance(waypoints.Value[i].position, transform.position);
                if (distance <= selectionRange && distance > 3f)
                    validWaypointIndices.Add(i);
            }
            
            if (validWaypointIndices.Count == 0)
                return (int) Mathf.Floor(Random.Range(0, waypoints.Value.Count));

            int randomIndexForSelectingIndex = (int) Mathf.Floor(Random.Range(0, validWaypointIndices.Count));
            return validWaypointIndices[randomIndexForSelectingIndex];
        }
    }
}
