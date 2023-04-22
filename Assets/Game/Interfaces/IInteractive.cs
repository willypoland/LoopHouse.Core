namespace Game.Interfaces
{
    public interface IInteractive
    {
        string Title { get; }

        string Description { get; }

        bool CanUse(object user);

        bool Use(object user);
    }
}