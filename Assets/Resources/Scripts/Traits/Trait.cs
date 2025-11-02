using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Trait : ScriptableObject
{
    public bool effectUsed;
    public Food addedTo;
    public virtual void OnAdd(Food _addedTo)
    {
        addedTo = _addedTo;
    }
    
    public virtual void OnGiveToCustomer()
    {
        
    }
}
