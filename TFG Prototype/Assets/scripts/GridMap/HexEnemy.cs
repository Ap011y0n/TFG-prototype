using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexEnemy : HexUnit
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Destroy()
    {
        location.Unit = null;
        grid.combatController.units.Remove(this);
        grid.combatController.enemyUnits.Remove(this);
        Destroy(this.gameObject);
    }
}
