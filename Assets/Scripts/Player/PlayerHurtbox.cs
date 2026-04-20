using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class PlayerHurtbox : MonoBehaviour
{
    public static PlayerHurtbox Instance;

    void Awake() //Singleton para acceder al código desde otros scripts
    {
        if (Instance == null)
            Instance = this;
    }

    public UnityAction<float> onHurt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            onHurt?.Invoke(other.GetComponent<EnemyProjectile>().speedReduction);
        }
    }
}
