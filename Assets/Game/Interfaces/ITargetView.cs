namespace Game.Interfaces
{
    public interface ITargetView
    {
        bool IsActive { get; }
        
        string Title { get; }
        
        string Description { get; }

        void Show();

        void Hide();
    }
}