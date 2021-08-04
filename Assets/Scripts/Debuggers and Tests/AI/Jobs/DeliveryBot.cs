using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryBot : MonoBehaviour
{
    private Queue<Stop> _queuedStops = new Queue<Stop>();
    public enum BotType
    {
        PRODUCE,WATER,BREAD
    }

    [SerializeField] private BotType type;

    private Stop[] _allStops;
    private Queue<Stop> testQueue;

    void Start()
    {
        _allStops = FindObjectsOfType<Stop>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            testQueue = GetQueuedStops(_queuedStops, _allStops,transform.position);
            while (testQueue.Count > 0)
            {
                Stop currentStop = testQueue.Dequeue();
                Debug.Log($"Current Stop: {currentStop._name}");
            }
        }
    }

    private Queue<Stop> GetQueuedStops(Queue<Stop> _queuedStops,Stop[] _allStops,Vector3 lastStop)
    {
        if (_queuedStops.Count == _allStops.Length)
        {
            return _queuedStops;
        }

        if (_allStops.Length == 0)
        {
            return null;
        }
        float nearestDistance = Single.MaxValue;
        Stop closestStop = null;
        int closestStopIdx = -1;
        foreach (Stop _stop in _allStops)
        {
            if (_stop != null)
            {
                var currentDistance = Vector3.Distance(lastStop, _stop.dropOffLocation.transform.position);
                if ( currentDistance < nearestDistance)
                {
                    nearestDistance = currentDistance;
                    closestStop = _stop;
                    closestStopIdx = Array.IndexOf(_allStops,_stop);
                }
            }
        }
        _queuedStops.Enqueue(closestStop);
        _allStops[closestStopIdx] = null;
        return GetQueuedStops(_queuedStops, _allStops,closestStop.dropOffLocation.transform.position);
    }
}
