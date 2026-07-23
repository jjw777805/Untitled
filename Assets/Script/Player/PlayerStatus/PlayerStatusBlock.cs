
namespace MyPlayer
{
    public partial class PlayerStatus
    {
        public bool CanBlock()
        {
            return !isinjury;   
        }

        bool isBlock=false;
        public bool IsBlock()
        {
            return isBlock;
        }
        public void BlockBegin()
        {
            isBlock = true;
        }

        public void BlockEnd()
        {
            isBlock = false;
        }
    }
}