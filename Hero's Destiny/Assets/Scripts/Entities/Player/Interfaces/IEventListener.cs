
public interface IEventListener
{
    public void OnRun();
    public void OnIdle();
    public void OnJump();
    public void OnCrouch(bool isCrouch);
    public void OnHurt(HealthInfo healthInfo);
    public void OnDie();
    public void OnAttack();
    public void OnHeavyAttack();
}
