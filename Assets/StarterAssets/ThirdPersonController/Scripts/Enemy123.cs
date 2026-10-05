using UnityEngine;
using UnityEngine.AI;

public class Enemy1234 : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private NavMeshAgent self;

    void Update()
    {
        var direction = (target.transform.position - transform.position).normalized;
        self.Move(direction * self.speed * Time.deltaTime);
    }
}
