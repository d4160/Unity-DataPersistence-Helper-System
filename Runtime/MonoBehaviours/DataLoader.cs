namespace d4160.Systems.DataPersistence
{
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;
    using NaughtyAttributes;

    public abstract class DataLoader : MonoBehaviour
    {
        [SerializeField] protected DataPersistenceType m_persistenceType = DataPersistenceType.Local;
        [SerializeField] protected bool m_encrypted;

        [ShowIf("IsDataPersistenceLocal")]
        [SerializeField] protected SaveDataPath m_saveDataFolder;

        [ShowIf(ConditionOperator.And, "IsDataPersistenceLocal", "IsNotSavePathPlayerPrefs")]
        [SerializeField] protected string m_fileName;

        [ShowIf(ConditionOperator.And, "IsDataPersistenceLocal", "IsNotSavePathPlayerPrefs")]
        [SerializeField] protected string m_fileExtension;

        [ShowIf("IsDataPersistenceRemote")]
        [SerializeField] protected RemotePersistenceType m_remotePersistenceType = RemotePersistenceType.PlayFab;

        [ShowIf("IsDataPersistenceRemote")]
        [SerializeField] protected string m_remoteId;

        [ShowIf("IsDataPersistenceRemote")]
        [Tooltip("Otherwise split in many entries like playerprefs")]
        [SerializeField] protected bool m_storageInOneEntry;

        [ShowIf("AvailableToUseKey")]
        [SerializeField] protected string m_key;

        [ShowIf("AvailableToUseSerializer")]
        [SerializeField] protected DataSerializerType m_serializerType = DataSerializerType.Odin;

        [ShowIf("AvailableToChoiceAdapter")]
        [SerializeField] protected DataSerializationAdapterType m_adapterType = DataSerializationAdapterType.Generic;

        protected IDataPersistence m_dataPersistence;

        #region Editor Only
#if UNITY_EDITOR
        protected bool IsDataPersistenceLocaOrRemote => IsDataPersistenceLocal || IsDataPersistenceRemote;
        protected bool AvailableToUseSerializer => (IsDataPersistenceRemote && m_storageInOneEntry)
                                            || (IsDataPersistenceLocal);

        protected bool AvailableToChoiceAdapter => IsNotSerializerJsonUtility &&
                                            (IsDataPersistenceLocal ||
                                            (IsDataPersistenceRemote && m_storageInOneEntry));
#endif
        #endregion

        protected bool IsDataPersistenceLocal => m_persistenceType == DataPersistenceType.Local;
        protected bool IsDataPersistenceRemote => m_persistenceType == DataPersistenceType.Remote;
        protected bool IsNotSerializerJsonUtility => m_serializerType != DataSerializerType.JsonUtility;
        protected bool IsNotSavePathPlayerPrefs => m_saveDataFolder != SaveDataPath.PlayerPrefs;
        protected bool AvailableToUseKey => (!IsNotSavePathPlayerPrefs && IsDataPersistenceLocal)
                                            || (IsDataPersistenceRemote && m_storageInOneEntry);

        protected string Identifier
        {
            get
            {
                if (AvailableToUseKey)
                    return m_key;
                else
                    return DataPath.GetPath(m_saveDataFolder, m_fileName, m_fileExtension);
            }
        }

        protected bool SaveToPlayerPrefs => m_saveDataFolder == SaveDataPath.PlayerPrefs;

        public string RemoteId { get =>  m_remoteId; set => m_remoteId = value; }

        public abstract void Initialize();

        public abstract void Save();

        public abstract void Load();
    }
}