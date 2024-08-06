using UnityEngine;
using Utils;

public class CriticalSpot : MonoBehaviour
{
    [SerializeField]
    ColorType critColor;
    [SerializeField]
    private float critDamageMult;

    private EnemyTest enemyToCrit;

    private void Start()
    {
        //Have to be careful with this in the future
        enemyToCrit = GetComponentInParent<EnemyTest>();
    }

    public void CritDamage(float damageAmount, ColorType damageColor)
    {
        if (damageColor == critColor)
        {
            enemyToCrit.DamageHealth(damageAmount * critDamageMult, enemyToCrit.enemyColor); //I use enemyColor so the damage calculation is correct on the other script (although the crit spot is "orange" the damage on the "red" enemy should be 100% of the critdamage)
        }
    }
}
