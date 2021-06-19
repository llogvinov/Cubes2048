using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedZone : MonoBehaviour
{
    private float coolDown = 1f;

    private void OnTriggerStay(Collider other)
    {
        Cube cube = other.GetComponent<Cube>();
        if (cube != null)
        {
            if (!cube.isMainCube && cube.cubeRigidbody.velocity.magnitude < 0.1f)
            {
                Invoke("ReloadScene", coolDown);    
            }
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
