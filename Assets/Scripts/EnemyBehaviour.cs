using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public AudioClip explosionSound;

    private GameManager _gameManager;
    private Rigidbody _objectRigidbody;
    private Transform _playerTransform;

    private float _speed = 10.0f;
    private bool _isAlive = true;

    void Start()
    {
        _objectRigidbody = GetComponent<Rigidbody>();
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if (_isAlive)
        {
            Vector3 toPlayer = _playerTransform.position - transform.position;
            _objectRigidbody.AddForce(toPlayer * _speed);
            _objectRigidbody.velocity = Vector3.ClampMagnitude(_objectRigidbody.velocity, _speed);

            transform.LookAt(_playerTransform);
        }

        if (!_gameManager.isGameActive)
        {
            _speed = 0;
            _objectRigidbody.angularVelocity = Vector3.zero;
            GetComponent<AudioSource>().Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && _gameManager.isGameActive)
        {
            _isAlive = false;

            _objectRigidbody.velocity = Vector3.zero;
            _objectRigidbody.angularVelocity = Vector3.zero;

            Instantiate(explosionParticle, transform.position, transform.rotation);

            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Stop();
            audioSource.volume = 1;
            audioSource.PlayOneShot(explosionSound);


            float delay = 4.0f;
            Destroy(gameObject, delay);
        }
    }
}
