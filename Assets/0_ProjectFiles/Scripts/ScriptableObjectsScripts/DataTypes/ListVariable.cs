using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ListVariable", menuName = "Variables/ListVariable", order = 5)]
public class ListVariable : ScriptableObject {

    public List<Player> list;

    public List<Player> List
    {
        get
        { 
            if (this.list == null)
            {
                this.list = new List<Player>();
            }
            return this.list;
        }
        set
        {
            if (this.list == null)
            {
                this.list = new List<Player>();
            }
            this.list = value;
        }
    }
}
