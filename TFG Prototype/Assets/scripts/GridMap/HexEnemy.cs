using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexEnemy : HexUnit
{
    public Image unitCard;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void SetUnitCard(Sprite sprite)
    {
        unitCard.sprite = sprite;

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
