
public class StatesEnforce
{
    // ���� ���ݷ� ����
    private static float _weaponDamageGain = 1;
    public static float weaponDamageGain
    {
        get
        {
            return _weaponDamageGain;
        }
        set
        {
            _weaponDamageGain = value;
        }
    }
    // ���� ���ݷ� ����
    private static float _playerMoveSpeedGain = 1;
    public static float PlayerMoveSpeedGain
    {
        get
        {
            return _playerMoveSpeedGain;
        }
        set
        {
            _playerMoveSpeedGain = value;
        }
    }

    // �� ü�� ����
    private static float _enemyHealthGain = 1;
    public static float enemyHealthGain
    {
        get { return _enemyHealthGain; }
        set { _enemyHealthGain = value; }
    }

    // �� �̵��ӵ� ����
    private static float _enemySpeedGain = 1;
    public static float enemySpeedGain
    {
        get { return _enemySpeedGain; }
        set { _enemySpeedGain = value; }
    }
}
