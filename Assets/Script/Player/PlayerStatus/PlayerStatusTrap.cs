
namespace MyPlayer
{
    public partial class PlayerStatus
    {
        public void Trap(int damage)
        {
            Hurt(damage);
            Reborn();
        }
    }
}