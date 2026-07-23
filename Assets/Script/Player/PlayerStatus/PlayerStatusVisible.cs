
namespace MyPlayer
{
    public partial class PlayerStatus
    {
       bool visible  = false;

       public bool Visible
        {
            set
            {
                visible = value;
            }
            get
            {
                return visible;
            }
        }
    }
}