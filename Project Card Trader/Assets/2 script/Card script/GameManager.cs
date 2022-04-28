using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Card> deck;
    public List<Card> initialDeck;
    public List<Card> hand;

    public Transform[] handSlot;
    public bool[] slotAvalable;

    [SerializeField] private int nbStartCard;

    public List<Card> discard;
    public List<Card> banCard;

    public int nbCardDraw;
    public int nbCardBan;

    public bool discardHandEndTurn = true;

    private void Start()
    {
        initialDeck = deck;
    }
    private void DrawCardFirstTurn()
    {
        if (deck.Count >= 1)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];
            for (int i = 0; i < nbStartCard; i++)
            {
                if (slotAvalable[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.index = i;
                    randCard.transform.position = handSlot[i].position;
                    randCard.isPlayed = false;
                    deck.Remove(randCard);
                    hand.Add(randCard);
                    slotAvalable[i] = false;
                    return;
                }
            }
        }
    }
    public void DiscardHand()
    {
        foreach (Card card in hand)
        {
            discard.Add(card);
        }
        hand.Clear();
    }


    public void DrawCard()
    {
        for (int i = 0; i < slotAvalable.Length; i++)
        {
            if (slotAvalable[i] == true)
            {
                Card cardDraw = deck[Random.Range(0, deck.Count)];
                nbCardDraw += 1;
                cardDraw.gameObject.SetActive(true);
                cardDraw.index = i;
                cardDraw.transform.position = handSlot[i].position;
                cardDraw.isPlayed = false;
                deck.Remove(cardDraw);
                hand.Add(cardDraw);
                slotAvalable[i] = false;
                return;
            }
        }
    }

    private void DiscardToDeck()
    {
        foreach (Card card in discard)
        {
            deck.Add(card);
        }
        discard.Clear();
    }
    void Update()
    {
        DrawCardFirstTurn();

        if (Input.GetKeyDown(KeyCode.J))
        {
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            DrawCard();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            DiscardToDeck();
        }
    }
}
