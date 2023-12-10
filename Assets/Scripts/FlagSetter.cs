using UnityEngine;

[RequireComponent (typeof(Base))]
public class FlagSetter : MonoBehaviour
{
    [SerializeField] private Flag _flag;

    private bool _isTaken;
    private Flag _currentFlag;
    private Base _base;

    private void Awake()
    {
        _base = GetComponent<Base>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0) && _isTaken)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.TryGetComponent(out Ground ground))
                {
                    Put(hit.point);
                }
            }
        }
    }

    private void OnMouseDown()
    {
        TakeFlag();
    }

    private void TakeFlag()
    {
        _isTaken = true;
    }

    private void Put(Vector3 position)
    {
        if (_currentFlag != null)
        {
            Destroy(_currentFlag.gameObject);
        }

        _isTaken = false;
        _currentFlag = Instantiate(_flag, position, Quaternion.identity);
        _base.SendCreateNewBatabase(_currentFlag);
    }
}