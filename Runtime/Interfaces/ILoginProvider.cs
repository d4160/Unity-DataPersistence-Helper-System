namespace d4160.Systems.DataPersistence
{
    public interface ILoginProvider
    {
        string Id { get; }
        /// <summary>
        /// Use this value to prevent login many times if don't need that
        /// </summary>
        bool Logged { get; }

        void Login();

        void Logout();
    }
}