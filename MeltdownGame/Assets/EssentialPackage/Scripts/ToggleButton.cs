using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public abstract class ToggleButton : MonoBehaviour, IPointerClickHandler
{
    public static List<ToggleButton> currentActives;
    public string ID;
    public bool activeOnStart;

    protected virtual void Start()
    {
        if (currentActives == null)
        {
            currentActives = new List<ToggleButton>();
        }
        else
        {
            for(int i = 0; i < currentActives.Count; i++)
            {
                if (currentActives[i] == null)
                {
                    currentActives.RemoveAt(i);
                    i--;
                }
            }
        }
        if (activeOnStart)
        {
            OnActivate();
            currentActives.Add(this);
        }
        else
        {
            OnDeactivate();
            currentActives.Remove(this);
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleButton current = currentActives.Find(x => x.ID == ID);
        if (current != null)
        {
            current.OnDeactivate();
            currentActives.Remove(current);
        }
        currentActives.Add(this);
        OnActivate();
    }
    public abstract void OnActivate();
    public abstract void OnDeactivate();
}