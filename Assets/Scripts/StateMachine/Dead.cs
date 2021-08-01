using UnityEngine;

public class Dead : IState
{
    private float _despawnTime;
    private Entity _entity;
    const float DESPAWN_DELAY = 5f;
    public Dead(Entity entity)
    {
        _entity = entity;
    }
    public void Tick()
    {
        if (Time.time >= _despawnTime)
        {
            GameObject.Destroy(_entity.gameObject);
        }
    }

    public void OnEnter()
    {
        // drop loot
        
        _despawnTime = Time.time + DESPAWN_DELAY;
    }

    public void OnExit()
    {
    }
}