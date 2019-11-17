namespace d4160.Systems.DataPersistence
{
    public interface IPlayerPrefsActions
    {
        void Save(bool encrypted = false);

        void Load(bool encrypted = false);
    }
}