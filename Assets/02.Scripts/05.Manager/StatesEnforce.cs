
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
