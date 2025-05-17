using UnityEngine;

public class FoodCollector : MonoBehaviour
{
    public float vitamin = 0f;
    public float calcium = 0f;
    public float iron = 0f;
    public float junk = 0f;

    [SerializeField] private AudioSource crunchySource;

    void OnTriggerEnter2D(Collider2D other)
    {
        Food food = other.GetComponent<Food>();
        if (food != null)
        {
            switch (food.type)
            {
                case FoodType.Vitamin:
                    vitamin += 1f;
                    break;
                case FoodType.Calcium:
                    calcium += 1f;
                    break;
                case FoodType.Iron:
                    iron += 1f;
                    break;
                case FoodType.Junk:
                    junk += 1f;
                    break;
            }
            // 🔒 Ограничение до 5
            vitamin = Mathf.Clamp(vitamin, 0f, 5f);
            calcium = Mathf.Clamp(calcium, 0f, 5f);
            iron = Mathf.Clamp(iron, 0f, 5f);
            junk = Mathf.Clamp(junk, 0f, 5f);

            // Проигрываем звук
        if (crunchySource != null)
        {
           crunchySource.Play();
        }

            Destroy(other.gameObject); // удаляем еду после сбора
        }
    }
}
