using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeRecoverManager : BaseManager
{
    [Header("IntVariables")]
    [SerializeField] private List<IntVariable> enemyHPs;
    [SerializeField] private List<IntVariable> enemyMaxHPs;
    [SerializeField] private List<StatusVariable> enemyStatuses;
    [SerializeField] private IntVariable recoverHPCounter;
    
    public float healthPerSecond = 1.0f;

    public Coroutine recoverHpCoroutine;
    
    public void OnPlayerTurnBegan()
    {
        // if the coroutine already works stop it 
        if(recoverHpCoroutine != null)
            StopCoroutine(recoverHpCoroutine);
        recoverHpCoroutine = StartCoroutine(RecoverEnemyHP());
        
    }

    public void OnEnemyTurnBegan()
    {
        if (recoverHpCoroutine == null)
            return;
        StopCoroutine(recoverHpCoroutine);
        recoverHpCoroutine = null;
    }

    private IEnumerator RecoverEnemyHP()
    {
        while (true)
        {
            for (int i = 0; i < enemyHPs.Count; i++)
            {
                if (enemyHPs[i].Value == enemyMaxHPs[i].Value || enemyHPs[i].Value <= 0 || enemyStatuses[i].GetValue("LockRecover") > 0)
                {
                    continue;
                }
                enemyHPs[i].setValue(enemyHPs[i].Value + 1);
                recoverHPCounter.setValue(recoverHPCounter.Value + 1);
            }
    
            yield return new WaitForSeconds(1.3f);// wait for one second
        }
    }
}


// for multiple enemies

// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
//
// public class RealTimeRecoverManager : BaseManager
// {
//     [Header("IntVariables")]
//     [SerializeField] private List<IntVariable> enemyHPs;
//     [SerializeField] private List<IntVariable> enemyMaxHPs;
//     [SerializeField] private List<StatusVariable> enemyStatuses;
//     [SerializeField] private IntVariable recoverHPCounter;
//
//     public int healthPerSecond = 1;
//     private Dictionary<int, Coroutine> recoverHpCoroutines = new Dictionary<int, Coroutine>(); 
//
//     public void OnPlayerTurnBegan()
//     {
//         // Stop all existing coroutines
//         StopAllCoroutines(); 
//         recoverHpCoroutines.Clear();
//
//         // Start individual coroutines for eligible enemies
//         for (int i = 0; i < enemyHPs.Count; i++)
//         {
//             if (enemyStatuses[i].GetValue("LockRecover") > 0 && enemyHPs[i].Value < enemyMaxHPs[i].Value)
//             {
//                 recoverHpCoroutines[i] = StartCoroutine(RecoverEnemyHP(i));
//             }
//         }
//     }
//
//     public void OnEnemyTurnBegan()
//     {
//         // Stop coroutines for enemies with "LockRecover" status changed
//         foreach (var kvp in recoverHpCoroutines.ToList()) 
//         {
//             int enemyIndex = kvp.Key;
//             if (enemyStatuses[enemyIndex].GetValue("LockRecover") <= 0) 
//             {
//                 StopCoroutine(kvp.Value);
//                 recoverHpCoroutines.Remove(enemyIndex);
//             }
//         }
//     }
//
//     private IEnumerator RecoverEnemyHP(int enemyIndex)
//     {
//         while (enemyStatuses[enemyIndex].GetValue("LockRecover") > 0 && enemyHPs[enemyIndex].Value < enemyMaxHPs[enemyIndex].Value)
//         {
//             enemyHPs[enemyIndex].setValue(Mathf.Min(enemyHPs[enemyIndex].Value + healthPerSecond, enemyMaxHPs[enemyIndex].Value));
//             recoverHPCounter.setValue(recoverHPCounter.Value + healthPerSecond);
//             yield return new WaitForSeconds(1.0f); // adjust the wait time as needed
//         }
//         recoverHpCoroutines.Remove(enemyIndex); // Remove completed coroutine
//     }
// }