using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class warrior : hero
{
	float startTime;
	void Start()
	{
		characterStart();
		sightRadius.baseStat = 4;
	}
	void Update()
	{
		decide();
		doSomething();
	}
	public override void doSomething()
	{
		switch (doing)
		{
			case action.Move:
				move();
				break;

			case action.combat:
				if(Vector2.Distance(transform.position, target.transform.position)>sightRadius.totalStat()){
					move();
				}
				else if (!cooldownOn)
				{
					cooldownOn = true;
					StartCoroutine(attack());
				}
				else
				{
					moveToEnemy();
				}
				break;

			default:
				return;
		}
	}
	IEnumerator attack()
	{
		if (scanList("monster", 1f,transform).Count > 0)
		{
			enemy.GetComponent<monster>().takeDamage(strength.totalStat());
			yield return new WaitForSeconds(1.5f / (1 + 0.1f * abilityHaste.totalStat()));
		}
		else
		{
			moveToEnemy();
		}
		cooldownOn = false;
	}
}