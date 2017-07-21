using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instantiator : MonoBehaviour
{
    public List<GameObject> prefablist = new List<GameObject>();
    private List<CasinoRoll> rolledList = new List<CasinoRoll>();

    public GameObject[] objectsBefRoll; // GameObjects that are enabled before rolling
    public GameObject[] objectsAftRoll; // GameObjects that are enabled after roll

    private bool rollInProgress = false;
    private Arrow arrow;
    private GameObject stayedObject;

    public float TokenDelay = 3.0f, TokenSlowdownRate = 1.0f;
    private float currentSpeed, timer;

    public float MinimumSpeed = 10.0f, MaximumSpeed = 20.0f;
    private float randomSpeed;
    // Use this for initialization
    void Start ()
    {
        arrow = (Arrow)FindObjectOfType(typeof(Arrow));
        arrow.instantiator = this;

        ResetCasino();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (rollInProgress) {
            timer += Time.deltaTime;

            if (currentSpeed > 0.0f)
                currentSpeed -= Time.deltaTime * (TokenSlowdownRate * randomSpeed);

            if (timer * (currentSpeed) > TokenDelay) {
                timer = 0;

                if (currentSpeed > 0) {
                    int prefabIndex = UnityEngine.Random.Range(0, prefablist.Count);
                    GameObject newpiece = Instantiate(prefablist[prefabIndex], transform);
                    CasinoRoll casinoRoll = newpiece.AddComponent<CasinoRoll>();
                    casinoRoll.movementSpeed = currentSpeed;
                    rolledList.Add(casinoRoll);

                    if (rolledList.Count >= 9) {
                        Destroy(rolledList[0].gameObject);
                        rolledList.RemoveAt(0);
                    }
                }
            }

            foreach (CasinoRoll roll in rolledList) {
                if (roll.movementSpeed > 0.0f)
                    roll.movementSpeed = currentSpeed;
            }
        }
	}

    public void NewRoll()
    {
        ResetCasino();

        foreach (GameObject go in objectsBefRoll)
            go.SetActive(false);

        foreach (GameObject go in objectsAftRoll)
            go.SetActive(false);

        arrow.DoRoll(); // Get the arrow ready
        rollInProgress = true;
    }

    private void RollDone()
    {
        foreach (GameObject go in objectsAftRoll)
            go.SetActive(true);

        Token token = stayedObject.GetComponent<Token>();
        Debug.Log("Ding Ding! Got a " + token.getTokenType());
    }

    public void ResetCasino()
    {
        randomSpeed = UnityEngine.Random.Range(MinimumSpeed, MaximumSpeed);
        currentSpeed = randomSpeed;

        foreach (CasinoRoll roll in rolledList)
            if (roll != null)
                Destroy(roll.gameObject);

        rolledList.Clear();

        foreach (GameObject go in objectsAftRoll)
            go.SetActive(false);

        foreach (GameObject go in objectsBefRoll)
            go.SetActive(true);
    }

    private void ObjectPassed(GameObject gobject)
    {
        // May add sounds, effects and so on.
    }

    private void ObjectStayed(CasinoRoll roll)
    {
        // Debug.Log("Object stayed: " + roll.gameObject.name);
        foreach (CasinoRoll rolled in rolledList) {
            rolled.movementSpeed = 0.0f; // Stop movement of tokens just to be sure
            if (rolled != roll) {
                rolled.SendMessage("DiscardMe"); // Discard each object, except the one that stayed
            }
        }

        stayedObject = roll.gameObject;

        rollInProgress = false;
        StartCoroutine(FinalToken(roll.gameObject));

        RollDone();
    }

    private IEnumerator FinalToken(GameObject gobject)
    {
        bool keepDoing = true;
        Vector3 targetPos = arrow.gameObject.transform.position + new Vector3(0, 2);
        targetPos.z = 0.0f;
        Vector3 targetScale = (new Vector3(0.25f, 0.25f, 0.25f) + gobject.transform.localScale);
        while (keepDoing) {
            gobject.transform.position = Vector3.MoveTowards(gobject.transform.position, targetPos, 10.0f * Time.deltaTime);
            if (gobject.transform.localScale.x < targetScale.x || gobject.transform.localScale.y < targetScale.y) {
                Vector3 scaleRate = new Vector3(0.05f, 0.05f, 0.0f);
                scaleRate.x = scaleRate.x * (gobject.transform.localScale.x * 0.15f);
                scaleRate.y = scaleRate.y * (gobject.transform.localScale.y * 0.15f);
                gobject.transform.localScale += scaleRate; // Scale while moving
            }
            if (gobject.transform.position == targetPos)
                keepDoing = false; // Token has reached new position, quit while loop

            yield return null;
        }
        yield return null;
    }
}
