using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHurtbox : MonoBehaviour
{
    public static PlayerHurtbox Instance;
    public GameObject hurtPanel;        
    public float feedbackDuration = 0.1f;

    [Header("Camera Shake")]
    public Transform cameraTransform;      // arrastra la camara aqui
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 0.2f;

    public UnityAction<float> onHurt;
    void Awake() 
    {
        if (Instance == null) Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectile") || other.CompareTag("Returnable"))
        {
            onHurt?.Invoke(other.GetComponent<EnemyProjectile>().speedReduction);
            RankSystem.Instance.OnHurt();
            StartCoroutine(HurtFeedbackCoroutine());
            StartCoroutine(CameraShakeCoroutine());
        }
    }

    private IEnumerator HurtFeedbackCoroutine()
    {
        hurtPanel.SetActive(true);
        yield return new WaitForSeconds(feedbackDuration);
        hurtPanel.SetActive(false);
    }

    private IEnumerator CameraShakeCoroutine()
    {
        Vector3 originalPos = cameraTransform.localPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            // posicion aleatoria cerca de la original
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            cameraTransform.localPosition = new Vector3(
                originalPos.x + x,
                originalPos.y + y,
                originalPos.z
            );
            elapsed += Time.deltaTime;
            yield return null;
        }

        // volver a la posicion original
        cameraTransform.localPosition = originalPos;
    }
}
