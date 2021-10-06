using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Dragging
{
    public interface IDragContainer<T> : IDragDestination<T>, IDragSource<T> where T : class
    {
    }
}