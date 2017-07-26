using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour 
{
	public GameObject player1;
	public GameObject player2;
	public GameObject TurnController;
	public GameObject multiplayerController;

	public List<Tile> tiles = new List<Tile>();

    public Text minePlcText;

    private List<ParticleSystem> minePlacementParticles = new List<ParticleSystem>();
    private bool minePlacement = false;

    void Awake()
    {
        CreateTiles();
    }

    void Start()
    {
        if (minePlcText.gameObject != null)
            minePlcText.text = "";
    }

    public void CreateTiles()
    {
        if (!multiplayerController.GetComponent<Multiplayer>().isOnline) {
            for (int i = 0; i < tiles.Count; i++) {
                StartCoroutine(CreateTile(i));
            }
        }
    }

    public IEnumerator CreateTile(int i)
    {
        tiles[i].position = tiles[i].gameObject.transform.position;

        TilePlacements tilePlacements = GetComponent<TilePlacements>();
        tiles[i].type = tilePlacements.GetRandom();

        //nappien randomoitu asettelu
        //tiles[i].gameObject.GetComponentInChildren<Text> ().text = ""+tiles[i].type.strength;

        int randomStrength = Random.Range(1, 4 + 1);
        yield return new WaitUntil(() => tilePlacements.CreateTile(tiles[i], i, randomStrength) == true); // Wait until old tile is destroyed and new spawned

        if (minePlacementParticles.Count < tiles.Count)
            minePlacementParticles.Add(tiles[i].gameObject.GetComponentInChildren<ParticleSystem>());

        yield return null;
    }

	public void click(int button)
	{
        GameObject currentPlayer;
        TurnControl turnControl = TurnController.GetComponent<TurnControl>();

        if (turnControl.Player1) {
            currentPlayer = player1;
        }
        else if (turnControl.Player2) {
            if (!multiplayerController.GetComponent<Multiplayer>().isOnline) { // local game
                currentPlayer = player2;
            }
            else { // online game
                Debug.Log("Local player tried to move remote players piece");
                return;
            }
        }
        else {
            Debug.Log("Virhe - Molempien Vuoro");
            return;
        }

        if (!minePlacement) {
            //Moving player to a selected location.
            PlayerAbilities pa = currentPlayer.GetComponent<PlayerAbilities>();
            pa.MoveButton(button);
        }
        else if (minePlacement) {
            tiles[button].type.mine = true;
            minePlacement = false;
            minePlcText.text = "";
            foreach (var ps in minePlacementParticles) {
                ps.Stop();
            }
        }
	}

    public void Mineplacement()
    {
        minePlacement = true;
        minePlcText.text = "Select Tile";
        foreach (var ps in minePlacementParticles) {
            ps.Play();
        }
    }
}