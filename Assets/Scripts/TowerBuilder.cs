using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuilder : MonoBehaviour
{
    public ScenarioController scenarioController;
    public Color AllowColor;
    public Color DenyColor;

    private TowerData towerData;

    private void Awake()
    {
        Events.OnTowerSelected += TowerSelected;
    }

    private void OnDestroy()
    {
        Events.OnTowerSelected -= TowerSelected;
    }

    private void TowerSelected(TowerData selectedTower) 
    {
        towerData = selectedTower;
        TintSprite(AllowColor);
    }
    //Reposition the gameobject to mouse coordinates. 
    //Round the coordinates to make it snap to a grid.
    //Verify that building area is free of other towers. 
    //By using a static overlap method from Physics2D class we can make this work without collider and a 2d rigidbody.
    //Tint the sprite to green or red accordingly.
    //Call the build method when the player presses left mouse button
    void Update()
    {
        if (towerData != null)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Pool ühikut offset 0.5f
            mouseWorldPos = new Vector2(Mathf.Round(mouseWorldPos.x - 0.5f) + 0.5f, Mathf.Round(mouseWorldPos.y - 0.5f) + 0.5f);

            transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);

            Color tintColor = IsFree(transform.position) ? AllowColor : DenyColor;
            TintSprite(tintColor);

            if (Input.GetMouseButton(0))
            {
                Build();
            }
            if (Input.GetMouseButton(1))
            {
                towerData = null;
                TintSprite(Color.clear);
            }
        }
    }

    private void TintSprite(Color color)
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

        foreach(SpriteRenderer rend in renderers)
        {
            rend.color = color;
        }
    }

    private bool IsFree(Vector2 pos)
    {
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(pos, 0.45f);
        foreach (Collider2D overlap in overlaps) 
        {
        if (!overlap.isTrigger)
            {
                return false;
            }
        }

        if (EventSystem.current.IsPointerOverGameObject()) return false;

        return true;
    }
    
    
    void Build() {
        //Verify that building area is free of other towers. (Turn this into a method)
        if (!IsFree(transform.position))
        {
            return;
        }

        //Make a note to remove gold from player later when gold is implemented
        if (!scenarioController.SpendGold(towerData.Cost))
        {
            return;
        }

        //Instantiate a tower prefab at the current position
        Instantiate<Tower>(towerData.Prefab, transform.position, Quaternion.identity);

        towerData = null;
        TintSprite(Color.clear);

    }
}
