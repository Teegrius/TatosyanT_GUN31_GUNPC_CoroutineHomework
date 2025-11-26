using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float shootForce = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBall();
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
        }
        else
        {
            Debug.LogWarning("Ball prefab is not assigned to Player!");
        }
    }
}