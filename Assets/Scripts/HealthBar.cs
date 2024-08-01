using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarHP;
    [SerializeField] private float healthReducingSpeed = 2f;

    private float targetHealth;

    private Camera mainCamera;
    
    public void UpdateHealthBar(float maxHP, float currentHP)
    {
        targetHealth = currentHP / maxHP;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        healthBarHP.fillAmount = Mathf.MoveTowards(healthBarHP.fillAmount, targetHealth, healthReducingSpeed * Time.deltaTime); //Slow anim

        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position); //Billboard effect
    }
}
