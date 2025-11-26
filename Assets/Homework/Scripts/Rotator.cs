using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _rotate = new Vector3(0, 30, 0);
    private Rigidbody _rigidbody;

    private IEnumerator Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        yield return new WaitForFixedUpdate();

        while (true)
        {
            Quaternion deltaRotation = Quaternion.Euler(_rotate * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
            yield return new WaitForFixedUpdate();
        }
    }
}