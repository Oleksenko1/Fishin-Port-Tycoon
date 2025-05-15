using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZone : MonoBehaviour
{
    public virtual void OnPlayerEnter()
    {
        
    }
    public virtual void OnPlayerExit()
    {
        
    }
    public virtual void OnPlayerStay()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        OnPlayerEnter();
    }
    void OnTriggerExit(Collider other)
    {
        OnPlayerExit();
    }
    void OnTriggerStay(Collider other)
    {
        OnPlayerStay();
    }
}
