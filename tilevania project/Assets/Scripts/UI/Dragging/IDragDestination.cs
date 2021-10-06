using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Dragging
{
    public interface IDragDestination<T> where T : class
    {
        bool Acceptable(T item);
        void AddItems(T item, int number);
    }
}