using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
    [SerializeField] private float cubeMaxPosX;

    [Space]
    [SerializeField] private TouchSlider touchSlider;

    private Cube mainCube;

    private bool canMove;
    private bool isPointerDown;
    private Vector3 cubePos;

    private void Start()
    {
        canMove = true;

        //spawn new cube
        SpawnCube();

        //listen to slider event
        touchSlider.OnPointerDownEvent += OnPointerDown;
        touchSlider.OnPointerDragEvent += OnPointerDrag;
        touchSlider.OnPointerUpEvent += OnPointerUp;
    }

    private void Update()
    {
        if (isPointerDown)
        {
            mainCube.transform.position = Vector3.Lerp(
                mainCube.transform.position, 
                cubePos, 
                moveSpeed * Time.deltaTime
            );
        }
    }

    private void OnPointerDown()
    {
        isPointerDown = true;
    }

    private void OnPointerDrag(float xMovement)
    {
        if (isPointerDown)
        {
            cubePos = mainCube.transform.position;
            cubePos.x = xMovement * cubeMaxPosX;
        }
    }

    private void OnPointerUp()
    {
        if (isPointerDown && canMove)
        {
            isPointerDown = false;
            canMove = false;

            //push the cube
            mainCube.cubeRigidbody.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);

            //spawn a new Cube after some time
            Invoke("SpawnNewCube", 0.3f);
        }
    }

    private void SpawnNewCube()
    {
        mainCube.isMainCube = false;
        canMove = true;
        SpawnCube();
    }

    private void SpawnCube()
    {
        mainCube = CubeSpawner.Instance.SpawnRandom();
        mainCube.isMainCube = true;

        //reset cubePos variable
        cubePos = mainCube.transform.position;
    }

    private void OnDestroy()
    {
        //remove listeners
        touchSlider.OnPointerDownEvent -= OnPointerDown;
        touchSlider.OnPointerDragEvent -= OnPointerDrag;
        touchSlider.OnPointerUpEvent -= OnPointerUp;
    }




}
