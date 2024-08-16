using UnityEngine;
public class PlayerController : MonoBehaviour, IHealth
{

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public PlayerGunController playerGunController;
    public Transform playerGraphics;

    [SerializeField] private PlayerModel _playerModel;

    private Rigidbody2D m_rigidbody;
    private float inputHorizontal;
    private float inputVertical;


    public void Init(PlayerModel playermodel, EnemiesController enemiesController)
    {
        _playerModel = playermodel;
        playerGunController.Initialize(_playerModel, enemiesController, playermodel.BulletPrefab);
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputHorizontal = SimpleInput.GetAxis(horizontalAxis);
        inputVertical = SimpleInput.GetAxis(verticalAxis);

        float f = playerGunController.ShootTarget.transform.localPosition.x - transform.position.x;
        if (f < 0)
            playerGraphics.localScale = new Vector3(-1, 1, 1);
        else
            playerGraphics.localScale = new Vector3(1, 1, 1);
    }

    void FixedUpdate()
    {
        if (inputHorizontal != 0 || inputVertical != 0)
            m_rigidbody.velocity = new Vector3(inputHorizontal, inputVertical, 0f) *
                _playerModel.MoveSpeed;
        else
            m_rigidbody.velocity = Vector3.zero;
    }


    public void TakeDamage(float damage)
    {
        //PlayerModel._health -= (int)damage;
        _playerModel.TakeDamage((int)damage);
        if (!_playerModel.IsPlayerAlive())
            Debug.Log("player is dead");
    }

    public bool isPlayerAlive()
    {
        return _playerModel.IsPlayerAlive();
    }
}
