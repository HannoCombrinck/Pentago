using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
    [System.Serializable]
    public class BoardClickEvent : UnityEvent<GameObject> {}
    [SerializeField]
    public BoardClickEvent onClick;

	public void Clicked()
    {
        onClick.Invoke(gameObject);
    }
}
