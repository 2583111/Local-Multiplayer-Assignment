using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Transform bM;
    public int hitCount = 5;

    public Mesh buildingState1;
    public Mesh buildingState2;
    public Mesh buildingState3;

    public float defaultScore = 50;

    private GameObject player1;
    private GameObject player2;


    void Start()
    {
        bM = transform.parent;

        GetComponent<MeshRenderer>().material = bM.GetComponent<BuildingManager>().Full_Mat;

        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
    }

    private void Update()
    {
        if (hitCount == 0)
        {
            bM.GetComponent<BuildingManager>().CheckDestruction();
        }

        if (Time.time >= colliderWait)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    float colliderWait = 0;

    public void TakeDamage(int thisPlayer)
    {
        GetComponent<BoxCollider>().enabled = false;
        Debug.Log("Taken damage");
        hitCount -= 1;

        if (Time.time > colliderWait)
        {
            colliderWait = Time.time + 0.5f;
        }

        if (hitCount == 3)
        {
            //gameObject.GetComponent<MeshFilter>().mesh = buildingState1;
            GetComponent<MeshRenderer>().material = bM.GetComponent<BuildingManager>().Half_Mat;
        }

        if (hitCount == 0)
        {
            if (thisPlayer == 0)
            {
                PlayerController p1 = player1.GetComponent<PlayerController>();
                p1.playerScore += defaultScore;
                p1.updateScore();
            }

            else
            {
                PlayerController p2 = player2.GetComponent<PlayerController>();
                p2.playerScore += defaultScore;
                p2.updateScore();
            }

            GetComponent<MeshRenderer>().material = bM.GetComponent<BuildingManager>().Zero_Mat;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

}
