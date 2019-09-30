using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{

    public List<Observer> observerList = new List<Observer>();

    public void addNewObserver(Observer observer) {
        observerList.Add(observer);
    }

    public void notify(Object value, NotificationType notification) {
        foreach (Observer observer in observerList) {
            observer.onNotify(value, notification);
        }
    }
}
