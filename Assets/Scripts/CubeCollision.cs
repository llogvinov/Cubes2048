using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    private Cube cube;

    private float pushForce = 2.5f;
    private float overlapRadius = 2f;
    private float explosionForce = 400f;
    private float explosionRadius = 1.5f;

    private void Awake()
    {
        cube = GetComponent<Cube>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Cube otherCube = collision.gameObject.GetComponent<Cube>();

        if (otherCube != null && cube.cubeID > otherCube.cubeID)
        {
            if (cube.cubeNumber == otherCube.cubeNumber)
            {
                Vector3 contactPosition = collision.contacts[0].point;
                
                if (otherCube.cubeNumber < CubeSpawner.Instance.maxCubeNumber)
                {
                    //spawn a new cube
                    Cube newCube = CubeSpawner.Instance.Spawn(cube.cubeNumber * 2, contactPosition + Vector3.up * 1.6f);

                    //push the new cube up and forward
                    newCube.cubeRigidbody.AddForce(new Vector3(0, 0.3f, 1f) * pushForce, ForceMode.Impulse);

                    //add random torque
                    float randomTorque = Random.Range(-20, 20);
                    Vector3 randomDirection = Vector3.one * randomTorque;
                    newCube.cubeRigidbody.AddTorque(randomDirection);
                }

                Collider[] surroundedCubes = Physics.OverlapSphere(contactPosition, overlapRadius);
                foreach (Collider collider in surroundedCubes)
                {
                    if (collider.attachedRigidbody != null)
                    {
                        collider.attachedRigidbody.AddExplosionForce(explosionForce, contactPosition, explosionRadius);
                    }
                }

                FX.Instance.PlayCubeExplosionFX(contactPosition, cube.cubeColor);

                //destroy the two cubes
                CubeSpawner.Instance.DestroyCube(cube);
                CubeSpawner.Instance.DestroyCube(otherCube);

            }
        }
    }





}
