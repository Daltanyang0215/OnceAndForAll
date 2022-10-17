
public class StatesEnforce
{
    // 무기 공격력 배율
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
    // 무기 공격력 배율
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

    // 적 체력 배율
    private static float _enemyHealthGain = 1;
    public static float enemyHealthGain
    {
        get { return _enemyHealthGain; }
        set { _enemyHealthGain = value; }
    }

    // 적 이동속도 배율
    private static float _enemySpeedGain = 1;
    public static float enemySpeedGain
    {
        get { return _enemySpeedGain; }
        set { _enemySpeedGain = value; }
    }
}
