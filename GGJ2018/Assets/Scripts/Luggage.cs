using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luggage : MonoBehaviour {

    public static string Tag { get { return "Luggage"; } }
    public static Dictionary<LuggageType, int> ValueTable;

    public enum LuggageType {
        Default, 
        Common, 
        Valuable, 
        Luxury
    }

    public LuggageType Type;

    private void OnTriggerEnter(Collider other) {
        
    }

    private void Awake() {
        if (ValueTable == null) {
            ValueTable = new Dictionary<LuggageType, int>();
            ValueTable.Add(LuggageType.Default, 5);
            ValueTable.Add(LuggageType.Common, 5);
            ValueTable.Add(LuggageType.Valuable, 10);
            ValueTable.Add(LuggageType.Luxury, 25);
        }
    }
}
