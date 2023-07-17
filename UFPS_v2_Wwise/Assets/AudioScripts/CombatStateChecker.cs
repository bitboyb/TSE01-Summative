using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateChecker : MonoBehaviour
{
    #region Singleton

    public static CombatStateChecker Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private bool bInCombat;

    //Limits Combatcheck to every 0.2 seconds
    private float checkCooldown = 0.2f;
    private float localCheckCooldown;

    private int engagedEnemies;

    //Wwise State references
    public AK.Wwise.State inCombatState;
    public AK.Wwise.State outOfCombatState;

    void Start()
    {
        engagedEnemies = 0;
    }

    void Update()
    {
        if (Time.time - checkCooldown >= localCheckCooldown)
        {
            localCheckCooldown = Time.time;
            CombatCheck();
        }
    }

    void CombatCheck()
    {
        if (engagedEnemies > 0)
        {
            inCombatState.SetValue();
        }
        else
        {
            outOfCombatState.SetValue();
        }
    }


    public void AddEnemy()
    {
        engagedEnemies++;
    }

    public void RemoveEnemy()
    {
        engagedEnemies--;

        if (engagedEnemies < 0)
        {
            engagedEnemies = 0;
        }
    }
}
