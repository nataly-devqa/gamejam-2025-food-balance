using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private int lives = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            lives--;
            Debug.Log("monster" + lives);
        }

        if (lives < 1)
        {
            Die();
        }     
    }

    private void Die()
    {
        Destroy(gameObject); // Уничтожает монстра
    }
}
