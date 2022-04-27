using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeckManager : MonoBehaviour
{
    public List<Card> deck;
    public Transform[] handSlot;
    public bool[] slotAvalable;

    [SerializeField] private int nbStartCard;

    public List<Card> discard;

    private void DrawCardFirstTurn()
    {
        if(deck.Count >=1)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];
            for(int i = 0; i < nbStartCard; i++)
            {
                if(slotAvalable[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.index = i;
                    randCard.transform.position = handSlot[i].position;
                    randCard.isPlayed = false;
                    deck.Remove(randCard);
                    slotAvalable[i] = false;
                    return;
                }
            }
        }
    }

    private void DrawCard()
    {
        for(int i = 0; i < slotAvalable.Length; i++)
        {
            if(slotAvalable[i] == true)
            {
                Card cardDraw = deck[Random.Range(0, deck.Count)];
                cardDraw.gameObject.SetActive(true);
                cardDraw.index = i;
                cardDraw.transform.position = handSlot[i].position;
                cardDraw.isPlayed = false;
                deck.Remove(cardDraw);
                slotAvalable[i] = false;
                return;
            }
        }
    }

    void Update()
    {
        DrawCardFirstTurn();
        if(Input.GetKeyDown(KeyCode.K))
        {
            DrawCard();
        }
    }
   
}
