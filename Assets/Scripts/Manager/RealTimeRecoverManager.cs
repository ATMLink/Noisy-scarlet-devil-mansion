using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeRecoverManager : BaseManager
{
    [Header("IntVariables")]
    [SerializeField] private List<IntVariable> enemyHPs;
    [SerializeField] private List<IntVariable> enemyMaxHPs;
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
                if (enemyHPs[i].Value == enemyMaxHPs[i].Value || enemyHPs[i].Value <= 0)
                {
                    continue;
                }
                enemyHPs[i].setValue(enemyHPs[i].Value + 1);
                recoverHPCounter.setValue(recoverHPCounter.Value + 1);
            }
    
            yield return new WaitForSeconds(2.0f);// wait for one second
        }
    }
}
