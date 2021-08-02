using UnityEngine;

/// <summary>
/// <para> This Allow the entity to look at its current focus </para>
/// <param name="entity">The Entity Object</param>
/// <param name="animator">The Animator controller component</param>
/// <param name="lookAtPosition"></param>
/// <param name="imaginaryTarget"></param>
/// </summary>
[RequireComponent(typeof(Animator))]
public class Tracker: ITrack
{
    private Transform _imaginaryTarget;
    private Transform _player;
    private Entity _entity;
    private Vector3 _target;
    private Vector3 _imaginaryStartPos;
    private Vector3 _lookAtPosition;
    private Vector3 imaginaryStartPos;

    public float lookSpeed = 3.0f;
    private float distanceToTaget = Mathf.Infinity;
    private float lookWeight;

    public Tracker(Transform player,Entity entity)
    {
        _entity = entity;
        _player = player;
        _imaginaryTarget = _entity.currentFocus;
        _imaginaryStartPos = _entity.currentFocus.position;
        _target = _entity.currentFocus.position;
        _lookAtPosition = _target;
    }
    public void Tick()
    {
        DetectPlayer();
    }

    public void DetectPlayer()
    {
        _entity.distanceToTaget = Vector3.Distance(_entity.transform.position, _player.position);
        _entity.lookWeight = Mathf.Lerp(lookWeight, 1f, Time.deltaTime);
        _entity._lookAtPosition = _imaginaryTarget.position;
        
        if (_entity.distanceToTaget <= _entity.threatRange)
        {
            _entity._target = new Vector3(_player.position.x,_player.position.y + _entity.targetOffset,_player.position.z);
            _imaginaryTarget.position = Vector3.Lerp(_imaginaryTarget.position, _entity._target, Time.deltaTime * lookSpeed);
            
            var targetAngle = Vector3.Angle(_entity.transform.forward, _entity._target);
            if (targetAngle >= _entity._maxAngle)
            {
                _entity._target = _entity._resetPosition.position;
                _imaginaryTarget.position = Vector3.Lerp(_imaginaryTarget.position, _entity._target, Time.deltaTime * lookSpeed);    
            }
        }
        else
        {
            _entity._target = _entity._resetPosition.position;
            _imaginaryTarget.position = Vector3.Lerp(_imaginaryTarget.position, _entity._target, Time.deltaTime * lookSpeed);
        }
    }

    
}