using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HexUnit : MonoBehaviour
{

	public List<HexCell> path;
	public int faction = 0;
	public HexGrid grid;
	[HideInInspector]
	public bool movement = true;
	[HideInInspector]
	public bool action = true;

	public TextMeshProUGUI text;
	public GameObject damageTextPrefab;
	public GameObject unitCanvas;

	public int meleAttack;
	public int meleDefense;
	public int weaponPen;
	public int armor;
	public int weaponDamage;
	public int entityHp;
	public int entityNum;
	int totalHP;

	public enum unitType
    {
		HumanPlayer,
		SmallMonster,
		BigMonster,
    }
	public HexCell Location
	{
		get
		{
			return location;
		}
		set
		{
			location = value;
			value.Unit = this;
			transform.localPosition = value.Position;
		}
	}

	protected HexCell location;

    private void Start()
    {
		path = new List<HexCell>();
	}
	public void setStats(unitType type, int unitSize)
    {
		entityNum = unitSize;
		text.text = entityNum.ToString();
		switch (type)
        {
			default:
				break;
			case unitType.HumanPlayer:
                {
					meleAttack = 50;
					meleDefense = 40;
					weaponPen = 0;
					armor = 4;
					weaponDamage = 1;
					entityHp = 1;
					totalHP = entityNum * entityHp;
				}
				break;
			case unitType.SmallMonster:
                {
					meleAttack = 50;
					meleDefense = 40;
					weaponPen = 0;
					armor = 5;
					weaponDamage = 1;
					entityHp = 1;
					totalHP = entityNum * entityHp;
				}
				break;
			case unitType.BigMonster:
                {
					meleAttack = 90;
					meleDefense = 80;
					weaponPen = 3;
					armor = 3;
					weaponDamage = 20;
					entityHp = 20;
					totalHP = entityNum * entityHp;
				}
				break;

		}
    }

	public void takeDamage(int damage, HexUnit enemy, bool retaliation)
    {
		totalHP -= damage;
		GameObject damageText = Instantiate(damageTextPrefab, unitCanvas.transform);
		damageText.GetComponent<DamageText>().text.text = damage.ToString();
		
		if (totalHP <= 0)
        {
			entityNum = 0;
			Destroy();
		}
		else
        {
			entityNum -= damage / entityHp;
			text.text = entityNum.ToString();
			if (isInRange(enemy) && retaliation)
			{
				Attack(enemy, false);
			}

		}
	}
    public virtual void Destroy()
    {

		location.Unit = null;
		grid.combatController.playerUnits.Remove(this);
		grid.combatController.units.Remove(this);
		Destroy(this.gameObject);
	
	}
	public void Move()
    {
		if (path != null)
		{
			Location.Unit = null;
			Location = path[0];
			grid.disableAllHighlights();
			path = null;
			movement = false;

		}
		else
			Debug.LogWarning(gameObject.name + " path is null!");
    }
	public bool isInRange(HexUnit enemy)
    {
		bool ret = false;
		for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
        {
			if (location.GetNeighbor(d) == enemy.location)
            {
				Debug.Log("Enemy in range");
				ret = true;
            }


		}


		return ret;
    }
	public bool HasEnemyInRange()
	{
		bool ret = false;
		for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
		{
			if (location.GetNeighbor(d).Unit && location.GetNeighbor(d).Unit.faction != faction)
			{
				Debug.Log("Enemy in range");
				ret = true;
			}


		}


		return ret;
	}
	public void Attack(HexUnit enemy, bool enemyCanRetaliate = true)
    {

		int totalDamage = 0;
		for(int i = 0; i < entityNum; i++)
        {
			int attack = Random.Range(0, meleAttack);
			int def = Random.Range(0, enemy.meleDefense);
			if(attack > def)
            {
				int armorSave = Random.Range(1, 7);
				if(armorSave < enemy.armor + weaponPen)
                {
					totalDamage += weaponDamage;
                }
            }

		}
		
		enemy.takeDamage(totalDamage, this, enemyCanRetaliate);
		action = false;
    }
	public void ResetActions()
    {
		action = true;
		movement = true;
    }
	public void EndActions()
    {
		action = false;
		movement = false;
	}
}
