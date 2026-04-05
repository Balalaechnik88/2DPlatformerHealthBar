public class HealthDamageButton : HealthButtonBase
{
    protected override void Apply(Health health, int amount)
    {
        health.TakeDamage(amount);
    }
}
