using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{ Start, PlayerTurn, EnemyTurn, Wait, Sell }

public class StateMachine : MonoBehaviour
{
    public GameState state;

    public GameObject enemyPrefabs;
    public Unit enemyUnit;
    public Transform enemyTransform;

    public BattleHUD enemyHUD;
    private GameObject enemyGO;

    private float buffAttack;

    public void Start()
    {
        state = GameState.Start;
        StartCoroutine(setupBattle());
    }

    private IEnumerator setupBattle()
    {
        yield return new WaitForSeconds(2f);

        enemyGO = Instantiate(enemyPrefabs, enemyTransform);
        enemyUnit = enemyGO.GetComponent<Unit>();

        enemyHUD.setHUD(enemyUnit);

        state = GameState.PlayerTurn;
        playerTurn();
    }

    private void playerTurn()
    {
        //do something basically wait here
    }

    public void OnEndTurn()
    {
        if (state != GameState.PlayerTurn)
        {
            return;
        }

        state = GameState.EnemyTurn;
        StartCoroutine(enemyTurn());
    }

    public void OnDeal()
    {
        if (state != GameState.PlayerTurn)
        {
            return;
        }

        state = GameState.Sell;

        StartCoroutine(sell());
    }

    private IEnumerator sell()
    {
        if (enemyUnit.listOBJ.objList.Count > 0)
        {
            enemyUnit.listOBJ.objList.RemoveAt(enemyUnit.index);
            enemyUnit.player.money += enemyUnit.price;

            enemyHUD.setMoney(enemyUnit.player.money);
            Destroy(enemyGO);
            StartCoroutine(setupBattle());
        }
        else
        {
            Destroy(enemyGO);
            yield break;
        }

        yield return new WaitForSeconds(2f);
        playerTurn();
        state = GameState.PlayerTurn;
    }

    private void AttackPatern(int rng)
    {
        switch (rng)
        {
            case 1:

                enemyUnit.price -= (int)System.Math.Round((10) + (0.5 * (5)) * enemyUnit.mefiance);
                enemyHUD.updatePrice(enemyUnit.price);
                break;

            case 2:
                //atk2 loose 1 patience per card played
                break;

            case 3:
                //next turn atk +5%
                break;

            case 4:
                //add 1 energy to (hopefully) all card
                break;

            case 5:
                enemyUnit.player.charisme -= 1 + (enemyUnit.mefiance / 2);
                break;
        }
    }

    private IEnumerator enemyTurn()
    {
        Debug.Log("ENEMY TURN");
        int rng = Random.Range(1, 7);
        AttackPatern(rng);
        Debug.Log(rng);

        yield return new WaitForSeconds(2f);
        state = GameState.PlayerTurn;
        playerTurn();
    }
}