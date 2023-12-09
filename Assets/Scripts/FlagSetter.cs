using UnityEngine;

public class FlagSetter : MonoBehaviour
{
    [SerializeField] private Flag _flag;

    private bool _isPlaced;
    private bool _isTaken; 

    private void OnMouseDown()
    {
        TakeFlag();
    }

    private void TakeFlag()
    {
        _isTaken = true;
        _isPlaced = false;
    }

    private void Put()
    {
        if(!_isPlaced)
        {

            _isPlaced = true; 
            _isTaken = false;
        }
    }
}