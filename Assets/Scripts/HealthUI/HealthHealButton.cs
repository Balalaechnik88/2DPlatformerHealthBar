public class HealthHealButton : HealthButtonBase
{
    protected override void Apply(Health health, int amount)
    {
        health.Heal(amount);
    }
}
