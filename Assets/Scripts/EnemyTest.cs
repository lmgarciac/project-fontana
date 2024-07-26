using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fontana = System.Drawing;

public class EnemyTest : MonoBehaviour
{
    [SerializeField]
    float health = 100f;
    [SerializeField]
    ColorType enemyColor;

    public enum ColorType
    {
        Red,
        Blue,
        Yellow,
        Green,
        Purple,
        Orange,
        White,
        Black
    }

    public void DamageHealth(float damageReceived, ColorType damageColor)
    {
        switch (damageColor) //There must be a better way to do this apart from this and a damage values matrix
        {
            case ColorType.Red:
                if (enemyColor == ColorType.Red)
                    health -= damageReceived;
                if (enemyColor == ColorType.Orange || enemyColor == ColorType.Purple)
                    health -= (damageReceived * 0.5f);
                if (enemyColor == ColorType.White)
                    health -= (damageReceived * 0.33f);
                break;

            case ColorType.Yellow:
                if (enemyColor == ColorType.Yellow)
                    health -= damageReceived;
                if (enemyColor == ColorType.Orange || enemyColor == ColorType.Green)
                    health -= (damageReceived * 0.5f);
                if (enemyColor == ColorType.White)
                    health -= (damageReceived * 0.33f);
                break;

            case ColorType.Blue:
                if (enemyColor == ColorType.Blue)
                    health -= damageReceived;
                if (enemyColor == ColorType.Green || enemyColor == ColorType.Purple)
                    health -= (damageReceived * 0.5f);
                if (enemyColor == ColorType.White)
                    health -= (damageReceived * 0.33f);
                break;

            case ColorType.Orange:
                if (enemyColor == ColorType.Orange)
                    health -= damageReceived;
                if (enemyColor == ColorType.Red || enemyColor == ColorType.Yellow)
                    health -= (damageReceived * 0.5f);
                if (enemyColor == ColorType.White)
                    health -= (damageReceived * 0.33f);
                break;

            case ColorType.Purple:
                if (enemyColor == ColorType.Purple)
                    health -= damageReceived;
                if (enemyColor == ColorType.Red || enemyColor == ColorType.Blue)
                    health -= (damageReceived * 0.5f);
                if (enemyColor == ColorType.White)
                    health -= (damageReceived * 0.33f);
                break;

            case ColorType.Green:
                if (enemyColor == ColorType.Green)
                    health -= damageReceived;
                if (enemyColor == ColorType.Blue || enemyColor == ColorType.Yellow)
                    health -= (damageReceived * 0.5f);
                if (enemyColor == ColorType.White)
                    health -= (damageReceived * 0.33f);
                break;

            case ColorType.White:
                if (enemyColor == ColorType.White)
                    health -= damageReceived;
                if (enemyColor == ColorType.Red || enemyColor == ColorType.Yellow || enemyColor == ColorType.Blue)
                    health -= (damageReceived * 0.33f);
                if (enemyColor == ColorType.Orange || enemyColor == ColorType.Green || enemyColor == ColorType.Purple)
                    health -= (damageReceived * 0.66f);
                break;

            case ColorType.Black:
                Debug.Log("INDESTRUCTIBLE... RUN...");
                break;
        }

        CheckHealth();
    }

    void CheckHealth()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"Health Remaining: {health}HP");
        }
    }
}
