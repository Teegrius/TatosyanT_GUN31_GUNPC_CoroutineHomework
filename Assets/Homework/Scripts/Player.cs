using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float shootForce = 10f;
    [SerializeField] private float shootCooldown = 1f;

    private float lastShootTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastShootTime + shootCooldown)
        {
            ShootBall();
            lastShootTime = Time.time;
        }
    }

    private void ShootBall()
    {
        if (ballPrefab != null)
        {
            // Создаём мяч перед игроком
            Vector3 spawnPosition = transform.position + transform.forward * 2f;
            GameObject ball = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);

            // Добавляем силу вперед
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballRigidbody.AddForce(transform.forward * shootForce, ForceMode.Impulse);
            }

            // Уничтожаем мяч через 5 секунд
            Destroy(ball, 5f);
        }
        else
        {
            Debug.LogWarning("Ball prefab is not assigned to Player!");
        }
    }
}