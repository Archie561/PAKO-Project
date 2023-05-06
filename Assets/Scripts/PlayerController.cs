using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public ParticleSystem pickupParticle;
    public AudioClip explosionSound;
    public AudioClip pickupSound;

    private Rigidbody _playerRigidbody;
    private GameManager _gameManager;
    private Vector3 _rotationVelocity = new Vector3(0, 75, 0);

    private float _speed = 1600.0f;
    private float _maxSpeed = 8.0f;
    private float _boostedSpeed = 2.0f;
    private float _speedupDuration = 10.0f;

    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (_gameManager.isGameActive)
        {
            _playerRigidbody.AddForce(transform.forward * _speed);
            _playerRigidbody.velocity = Vector3.ClampMagnitude(_playerRigidbody.velocity, _maxSpeed);

            float horiznotanInput = Input.GetAxis("Horizontal");
            Quaternion deltaRotation = Quaternion.Euler(_rotationVelocity * Time.deltaTime * horiznotanInput);
            _playerRigidbody.MoveRotation(_playerRigidbody.rotation * deltaRotation);
        }
    }

    IEnumerator SpeedBoost()
    {
        _maxSpeed += _boostedSpeed;
        yield return new WaitForSeconds(_speedupDuration);
        _maxSpeed -= _boostedSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Enemy")) && _gameManager.isGameActive)
        {
            _playerRigidbody.constraints = RigidbodyConstraints.FreezeAll;

            Instantiate(explosionParticle, transform.position, transform.rotation);
            GetComponent<AudioSource>().PlayOneShot(explosionSound);
            _gameManager.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Speedup"))
        {
            GetComponent<AudioSource>().PlayOneShot(pickupSound);
            Instantiate(pickupParticle, transform.position, transform.rotation);
            StartCoroutine(SpeedBoost());
            Destroy(other.gameObject);
        }
    }
}
