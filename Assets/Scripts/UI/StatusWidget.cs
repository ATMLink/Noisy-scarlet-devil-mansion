using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusWidget : MonoBehaviour
{
    [SerializeField] private GameObject statusElementPrefab;
    private readonly List<StatusElementWidget> elements = new List<StatusElementWidget>();

    public void OnStatusChanged(StatusTemplate status, int value)
    {
        var foundElement = false;

        foreach (var element in elements)
        {
            if (element.Type == status.Name)
            {
                if (value > 0)
                {
                    element.UpdateModifier(value);
                    foundElement = true;
                    break;
                }

                elements.Remove(element);
                element.FadeAndDestroy();
                foundElement = true;
                break;
            }            
        }

        if (!foundElement)
        {
            var newElement = Instantiate(statusElementPrefab, transform, false);
            var widget = newElement.GetComponent<StatusElementWidget>();
            widget.Initialize(status, value);
            widget.Show();
            elements.Add(widget);
        }
    }

    public void OnHpchanged(int hp)
    {
        if (hp <= 0)
        {
            foreach (var element in elements)
            {
                element.FadeAndDestroy();
            }
        }
    }
}
