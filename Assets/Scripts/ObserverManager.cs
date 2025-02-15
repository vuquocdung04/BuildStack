using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverManager : Singleton<ObserverManager>
{
    [Header("ObserverManager")]
    [SerializeField] protected List<IObserver> observers = new List<IObserver>();

    public virtual void AddListened(IObserver observer)
    {
        this.observers.Add(observer);
    }
    public virtual void RemoveListened(IObserver observer)
    {
        this.observers.Remove(observer);
    }
    //
    public virtual void UpdateScore()
    {
        foreach (IObserver observer in this.observers)
        {
            observer.UpdateScoreText();
        }
    } 
}
