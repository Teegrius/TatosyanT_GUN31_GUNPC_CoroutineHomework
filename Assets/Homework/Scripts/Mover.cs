using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _leftBound = -14f;
    [SerializeField] private float _rightBound = 14f;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _delay = 1f;

    private Rigidbody _rigidbody;
    private bool _movingRight = true;

    private IEnumerator Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;

        // Начинаем с левой границы
        _rigidbody.position = new Vector3(_leftBound, _rigidbody.position.y, _rigidbody.position.z);

        yield return new WaitForFixedUpdate();

        while (true)
        {
            float targetX = _movingRight ? _rightBound : _leftBound;
            yield return StartCoroutine(MoveToX(targetX));
            yield return new WaitForSeconds(_delay);
            _movingRight = !_movingRight;
        }
    }

    private IEnumerator MoveToX(float targetX)
    {
        float startX = _rigidbody.position.x;
        float distance = Mathf.Abs(targetX - startX);
        float duration = distance / _speed;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.fixedDeltaTime;
            float t = elapsed / duration;
            float newX = Mathf.Lerp(startX, targetX, t);

            Vector3 newPosition = _rigidbody.position;
            newPosition.x = newX;
            _rigidbody.MovePosition(newPosition);

            yield return new WaitForFixedUpdate();
        }

        // Финальная позиция
        Vector3 finalPosition = _rigidbody.position;
        finalPosition.x = targetX;
        _rigidbody.MovePosition(finalPosition);
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(_leftBound, pos.y, pos.z), 0.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector3(_rightBound, pos.y, pos.z), 0.5f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(_leftBound, pos.y, pos.z), new Vector3(_rightBound, pos.y, pos.z));
    }
}