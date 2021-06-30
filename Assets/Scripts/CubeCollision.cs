using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    private float _pushForce = 2.5f;
    private float _overlapRadius = 2f;
    private float _explosionForce = 400f;
    private float _explosionRadius = 1.5f;
    
    private Cube _cube;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Cube otherCube = collision.gameObject.GetComponent<Cube>();

        if (otherCube != null && _cube.cubeID > otherCube.cubeID)
        {
            if (_cube.cubeNumber == otherCube.cubeNumber)
            {
                Vector3 contactPosition = collision.contacts[0].point;
                
                if (otherCube.cubeNumber < CubeSpawner.Instance.maxCubeNumber)
                {
                    //spawn a new cube
                    Cube newCube = CubeSpawner.Instance.Spawn(_cube.cubeNumber * 2, contactPosition + Vector3.up * 1.6f);

                    //push the new cube up and forward
                    newCube.cubeRigidbody.AddForce(new Vector3(0, 0.3f, 1f) * _pushForce, ForceMode.Impulse);

                    //add random torque
                    float randomTorque = Random.Range(-20, 20);
                    Vector3 randomDirection = Vector3.one * randomTorque;
                    newCube.cubeRigidbody.AddTorque(randomDirection);
                }

                Collider[] surroundedCubes = Physics.OverlapSphere(contactPosition, _overlapRadius);
                foreach (Collider collider in surroundedCubes)
                {
                    if (collider.attachedRigidbody != null)
                    {
                        collider.attachedRigidbody.AddExplosionForce(_explosionForce, contactPosition, _explosionRadius);
                    }
                }

                FX.Instance.PlayCubeExplosionFX(contactPosition, _cube.cubeColor);

                //destroy the two cubes
                CubeSpawner.Instance.DestroyCube(_cube);
                CubeSpawner.Instance.DestroyCube(otherCube);

            }
        }
    }

}
