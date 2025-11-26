using UnityEngine;

public class Gates : MonoBehaviour
{
    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что это мяч по компоненту Ball
        if (other.GetComponent<Ball>() != null)
        {
            // Уничтожаем мяч
            Destroy(other.gameObject);

            // Увеличиваем счёт
            score++;

            // Выводим счёт в консоль
            Debug.Log($"Goal! Score: {score}");
        }
    }
}