using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Healthbar Settings")]
    [SerializeField] private Image healthBarBackground;
    [SerializeField] private Image healthBarHP;
    [SerializeField] private float healthReducingSpeed = 2f;
    [SerializeField] private float scaleFactor = 1.0f;
    [SerializeField] private float alphaFactor = 2f;
    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = 3f;

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

        ScaleHealthBar();

        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position); //Billboard effect
    }

    private void ScaleHealthBar()
    {
        if (mainCamera != null)
        {
            //Distance between player and enemy
            float distance = Vector3.Distance(transform.position, mainCamera.transform.position);

            //Sets new scale depending on distance
            float newScale = Mathf.Clamp((scaleFactor / distance), minScale, maxScale);
            float newAlpha = Mathf.Clamp(((scaleFactor / alphaFactor) / distance), 0.1f, 1f);

            //This probably could be done better
            healthBarBackground.transform.localScale = new Vector3(newScale, newScale, newScale);
            healthBarHP.transform.localScale = new Vector3(newScale, newScale, newScale);

            healthBarBackground.color = new Color(healthBarBackground.color.r, healthBarBackground.color.g, healthBarBackground.color.b, newAlpha);
            healthBarHP.color = new Color(healthBarHP.color.r, healthBarHP.color.g, healthBarHP.color.b, newAlpha);
        }
    }
}
