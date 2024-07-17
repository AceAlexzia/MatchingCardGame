using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string mode;
    private InputHandler inputHandler;
    [SerializeField]
    private List<GameObject> cardlist = new List<GameObject>();
    [SerializeField]
    private List<GameObject> selectCard = new List<GameObject>();

    [SerializeField]
    private int cardNumber = 0;
    [SerializeField]
    private List<int> randomNumber = new List<int>();
    [SerializeField]
    private GameObject Card;

    [SerializeField]
    private GameObject cardLayout;

    [SerializeField]
    private List<Sprite> cards_image = new List<Sprite>();

    // Shuffle for list
    void Shuffle<T>(List<T> inputList)
    {
        for (int i = 0; i < inputList.Count - 1; i++)
        {
            T temp = inputList[i];
            int rand = Random.Range(i, inputList.Count);
            inputList[i] = inputList[rand];
            inputList[rand] = temp;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Shuffle(cards_image);

        CreateCard();
        RandomCardID();
    }

    void Update()
    {
        
    }

    private void CreateCard()
    {
        for (int i = 0; i < cardNumber; i++)
        {

            GameObject card = Instantiate(Card);
            card.transform.SetParent(cardLayout.transform);
            cardlist.Add(card);
            
        }

    }

    private void RandomCardID()
    {
        // Put number into the list
        for (int i = 0; i < cardNumber / 2; i++)
        {
            randomNumber.Add(i);
            randomNumber.Add(i);
        }

        // Random Number to the card
        Shuffle(randomNumber);

        // Assign Card id
        for (int i = 0; i < cardlist.Count; i++)
        {
            cardlist[i].GetComponent<Card>().ID = randomNumber[i];
        }

        // Assign Image to cards
        for (int i = 0; i < cardlist.Count; i++)
        {
            cardlist[i].GetComponent<Card>().image.sprite = cards_image[cardlist[i].GetComponent<Card>().ID];
        }

    }

    public void SelectCard(GameObject gameObject)
    {
        Debug.Log("InSelectCard");
        if (!selectCard.Contains(gameObject))
        {
            selectCard.Add(gameObject);
        }
        else
        {
            selectCard.Remove(gameObject);
        }
        if (selectCard.Count == 2)
        {
            CheckCorrection();
        }
    }

    private void CheckCorrection()
    {
        if (selectCard[0].GetComponent<Card>().ID == selectCard[1].GetComponent<Card>().ID)
        {
            Debug.Log("Matching");

            foreach (var card in selectCard)
            {
                cardlist.Remove(card);
            }


            foreach (GameObject obj in selectCard)
            {
                //obj.SetActive(false);

                //Destroy(obj);
                obj.GetComponent<Image>().enabled = false;
                obj.GetComponent<Card>().image.enabled = false;
            
            }
        }
        else
        {
            Debug.Log("Not Matching");
        }

        if (cardlist.Count == 0)
        {
            Debug.Log("Game End");
        }


        selectCard.Clear();
    }

}
