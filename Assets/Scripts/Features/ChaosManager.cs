using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ChaosManager : MonoBehaviour
{
    //Create this as instance
    public static ChaosManager Instance;

    public CameraShake camshake;

    #region Inputs

    public Transform player;
    private bool hasWon = false;

    [Header("Chaos_Settings")]
    public float maxChaos = 100f;
    public float chaosIncreaseDistance = 10f;
    public float chaosDecreaseRate = 5f;
    public float chaosIncreaseMultiplier = 10f;

    [Header("Win_Factor")]
    public float safeChaosThreshold = 30f;
    public float survivalTime = 20f;

    public float CurrentChaos;
    private float SafeTimer;

    //Get the Enemies List from enemies Script.
    private List<EnemyLogic> enemies = new List<EnemyLogic> ();

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterEnemy(EnemyLogic enemy)
    {
        enemies.Add(enemy);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateChaos();
        HandleWinCondition();
        HandleCameraShake();
    }

    #region CalculatingChaos
    void CalculateChaos()
    {
        float chaos = 0f;

        //Iterate through all the enemies.
        foreach(var enemy in enemies)
        {
            if (enemy == null) continue;

            //Calculate the distance
            float distance = Vector3.Distance(player.transform.position, enemy.transform.position);
            if(distance < chaosIncreaseDistance)
            {
                chaos += (chaosIncreaseDistance - distance);
            }
        }

        CurrentChaos += chaos * chaosIncreaseMultiplier * Time.deltaTime;
        CurrentChaos -= chaosDecreaseRate * Time.deltaTime;

        CurrentChaos = Mathf.Clamp(CurrentChaos,0,maxChaos);

    }
    #endregion

    #region WinCondition
    void HandleWinCondition()
    {

        if (hasWon)
        {
            return;
        }

        if(CurrentChaos < safeChaosThreshold)
        {
            SafeTimer += Time.deltaTime;
        }
        else
        {
            SafeTimer = 0;
        }

        if(SafeTimer >= survivalTime)
        {
            //Win_Logic
            hasWon = true;
            print("WON");
        }
    }
    #endregion

    #region CameraShake
    void HandleCameraShake()
    {
        if (CurrentChaos > safeChaosThreshold)
        {
            float value = CurrentChaos / maxChaos;
            camshake.Shake(value);
        }
    }
    #endregion
}
