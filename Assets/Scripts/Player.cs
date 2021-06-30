using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _pushForce;
    [SerializeField] private float _cubeMaxPosX;

    [Space]
    [SerializeField] private TouchSlider _touchSlider;

    private Cube _mainCube;

    private bool _canMove;
    private bool _isPointerDown;
    private Vector3 _cubePos;

    private void Start()
    {
        _canMove = true;

        SpawnCube();

        //listen to slider event
        _touchSlider.OnPointerDownEvent += OnPointerDown;
        _touchSlider.OnPointerDragEvent += OnPointerDrag;
        _touchSlider.OnPointerUpEvent += OnPointerUp;
    }

    private void Update()
    {
        if (_isPointerDown)
        {
            _mainCube.transform.position = Vector3.Lerp(
                _mainCube.transform.position, 
                _cubePos, 
                _moveSpeed * Time.deltaTime
            );
        }
    }

    private void OnPointerDown()
    {
        _isPointerDown = true;
    }

    private void OnPointerDrag(float xMovement)
    {
        if (_isPointerDown)
        {
            _cubePos = _mainCube.transform.position;
            _cubePos.x = xMovement * _cubeMaxPosX;
        }
    }

    private void OnPointerUp()
    {
        if (_isPointerDown && _canMove)
        {
            _isPointerDown = false;
            _canMove = false;

            //push the cube
            _mainCube.cubeRigidbody.AddForce(Vector3.forward * _pushForce, ForceMode.Impulse);

            //spawn a new Cube after some time
            Invoke("SpawnNewCube", 0.3f);
        }
    }

    private void SpawnNewCube()
    {
        _mainCube.isMainCube = false;
        _canMove = true;
        SpawnCube();
    }

    private void SpawnCube()
    {
        _mainCube = CubeSpawner.Instance.SpawnRandom();
        _mainCube.isMainCube = true;

        //reset _cubePos variable
        _cubePos = _mainCube.transform.position;
    }

    private void OnDestroy()
    {
        //remove listeners
        _touchSlider.OnPointerDownEvent -= OnPointerDown;
        _touchSlider.OnPointerDragEvent -= OnPointerDrag;
        _touchSlider.OnPointerUpEvent -= OnPointerUp;
    }

}
