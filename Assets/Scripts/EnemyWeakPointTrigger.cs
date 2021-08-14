using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeakPointTrigger : MonoBehaviour
{
    EnemyLogic enemyLogic;

    private void Start()
    {
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
           EnemyLogic enemyLogic = GetComponentInParent<EnemyLogic>();
            if (enemyLogic)
            {
                enemyLogic.SetState(EnemyState.Dead);

            }
        }
    }
}
