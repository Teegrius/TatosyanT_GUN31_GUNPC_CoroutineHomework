using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Vector3 _start;
    [SerializeField] private Vector3 _end;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _delay = 1f;

    private Rigidbody _rigidbody;

    private IEnumerator Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        yield return new WaitForFixedUpdate();

        while (true)
        {
            yield return StartCoroutine(MoveToPosition(_start, _end));
            yield return StartCoroutine(MoveToPosition(_end, _start));
            
        }
    }

    private IEnumerator MoveToPosition(Vector3 localFrom, Vector3 localTo)
    {
        Vector3 worldFrom = transform.TransformPoint(localFrom);
        Vector3 worldTo = transform.TransformPoint(localTo);

        float distance = Vector3.Distance(worldFrom, worldTo);
        float duration = distance / _speed;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.fixedDeltaTime;
            float t = elapsed / duration;

            Vector3 newPosition = Vector3.Lerp(worldFrom, worldTo, t);
            _rigidbody.MovePosition(newPosition);
            yield return new WaitForFixedUpdate();
        }

        _rigidbody.MovePosition(worldTo);
    }

    // Визуализация точек в редакторе
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.TransformPoint(_start), 0.3f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.TransformPoint(_end), 0.3f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.TransformPoint(_start), transform.TransformPoint(_end));
    }
}