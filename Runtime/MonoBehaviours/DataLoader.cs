namespace d4160.Systems.DataPersistence
{
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public abstract class DataLoader : MonoBehaviour
    {
        [SerializeField] protected SaveDataPath m_saveDataFolder;
        [SerializeField] protected string m_fileName;
        [SerializeField] protected string m_fileExtension;
        [SerializeField] protected bool m_encrypted;

        protected IDataPersistence m_dataPersistence;

        protected string SaveDataPathFull => DataPath.GetPath(m_saveDataFolder, m_fileName, m_fileExtension);

        protected bool SaveToPlayerPrefs => m_saveDataFolder == SaveDataPath.PlayerPrefs;

        public abstract void Initialize();

        public abstract void Save();

        public abstract void Load();
    }
}