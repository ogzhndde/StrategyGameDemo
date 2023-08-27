using ArmyFactoryStatic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The class on the UI Button that provides the soldier production.
/// </summary>

public class SoldierMenu : MonoBehaviour, ISoldier
{
    [Header("Definitons")]
    [SerializeField] private TextMeshProUGUI TMP_Name;
    [SerializeField] private Image IMA_Soldier;
    [SerializeField] private Image IMA_TeamFlag;
    [SerializeField] private Button BTN_SoldierUI;


    [Space(10)]
    #region Main Variables of SoldierUI
    [SerializeField] private string _name;
    [SerializeField] private Sprite _soldierSprite;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _cellSize;
    [SerializeField] private SoldierType _soldierType;
    [SerializeField] private TeamTypes _teamTypes;
    #endregion

    #region Interface Variables
    public string Name => _name;
    public Sprite SoldierSprite => _soldierSprite;
    public int Health => _health;
    public int Damage => _damage;
    public int CellSize => _cellSize;
    public SoldierType SoldierType => _soldierType;
    #endregion

    //Set soldier properties when he/she created
    public void SetSoldierProperties(string name, Sprite soldierSprite, int health, int damage, int cellSize, SoldierType soldierType, TeamTypes teamTypes)
    {
        _name = name;
        _soldierSprite = soldierSprite;
        _health = health;
        _damage = damage;
        _cellSize = cellSize;
        _soldierType = soldierType;
        _teamTypes = teamTypes;
    }

    //Set visual properties of the soldier
    public void SetVisualProperties()
    {
        TMP_Name.text = _name;
        IMA_Soldier.sprite = _soldierSprite;
        IMA_TeamFlag.color = _teamTypes switch
        {
            TeamTypes.Red => Color.red,
            TeamTypes.Blue => Color.blue,
            TeamTypes.Green => Color.green,
            _ => Color.white
        };
    }

    void Start()
    {
        BTN_SoldierUI.onClick.AddListener(ButtonSoldierSpawn);
    }

    //Get current building from Information Panel Manager and spawn the soldier
    private void ButtonSoldierSpawn()
    {
        InformationPanelManager infPanelMan = InformationPanelManager.Instance;

        Building currentBuilding = infPanelMan.CurrentBuilding.GetComponent<Building>();
        ArmyFactory.CreateSoldier(_soldierType, _teamTypes, currentBuilding.FindValidSpawnPoint());
    }
}
