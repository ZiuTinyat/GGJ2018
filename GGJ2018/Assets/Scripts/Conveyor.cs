using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour {

    // Real-time dragging not supported yet!

    public float Speed;
    public bool Direction; // true: forward

    [SerializeField] Transform StartGearTransform, EndGearTransform;

    private List<ConveyorUnit> ConveyorUnits = new List<ConveyorUnit>();

    [SerializeField] Transform UnitContainerTransform;
    [SerializeField] GameObject SpawnUnit;

	// Use this for initialization
	void Start () {
        StartCoroutine(ConveyorUnitProcessCoroutine());
	}
	
	// Update is called once per frame
	void Update () {
		if (ConveyorUnits.Count > 0 && ConveyorUnits[ConveyorUnits.Count - 1].x > 0.2f)
        {
            StartCoroutine(ConveyorUnitProcessCoroutine());
        }
	}

    IEnumerator ConveyorUnitProcessCoroutine()
    {
        GameObject unitObj = Instantiate(SpawnUnit, StartGearTransform.position, Quaternion.identity, UnitContainerTransform);
        ConveyorUnit unit = unitObj.GetComponent<ConveyorUnit>();
        ConveyorUnits.Add(unit);
        do
        {
            yield return null;
            unit.totX = Vector3.Distance(EndGearTransform.position, StartGearTransform.position);
            unit.x += Speed * Time.deltaTime;
            //unitObj.transform.position = Vector3.Lerp(StartGearTransform.position, EndGearTransform.position, unit.percent);
            //Vector3 dir= (EndGearTransform.position - StartGearTransform.position).normalized;
            unit.rigidbody.MovePosition(Vector3.Lerp(StartGearTransform.position, EndGearTransform.position, unit.percent));
        }
        while (unit.percent < 1f);
        ConveyorUnits.Remove(unit);
        Destroy(unitObj);
    }
}
