namespace d4160.Systems.DataPersistence
{
    using System;
    using UnityEngine.GameFoundation.DataPersistence;

    /// <summary>
    /// Modified version of UnityEngine.GameFoundation.DataPersistence.LocalPersistence.cs
    /// to allow use a full path instead of an identifier only, so we can choose the location folder, for example
    /// also allow us to choose if we use encryption
    /// </summary>
    public class DefaultPlayerPrefsPersistence : BaseDataPersistence
    {
        protected bool m_encrypted = false;
        /// <summary>
        /// The default object as a recipient for send load data
        /// </summary>
        protected IPlayerPrefsActions m_loadDataContainer = null;

        /// <inheritdoc />
        public DefaultPlayerPrefsPersistence(IPlayerPrefsActions emptyDataContainer, bool encrypted) : base(null)
        {
            m_loadDataContainer = emptyDataContainer;
            m_encrypted = encrypted;
        }

        /// <summary>
        /// Ignore identifier, don't need here
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="content"></param>
        /// <param name="onSaveCompleted"></param>
        /// <param name="onSaveFailed"></param>
        public override void Save(string identifier, ISerializableData content, Action onSaveCompleted = null, Action onSaveFailed = null)
        {
            var playerPrefsPairs = content as IPlayerPrefsActions;

            if (playerPrefsPairs != null)
            {
                playerPrefsPairs.Save(m_encrypted);

                onSaveCompleted?.Invoke();
            }
            else
            {
                onSaveFailed?.Invoke();
            }
        }

        /// <summary>
        /// Ignore identifierFullPath, don't need here
        /// </summary>
        /// <param name="identifierFullPath"></param>
        /// <param name="onLoadCompleted"></param>
        /// <param name="onLoadFailed"></param>
        /// <typeparam name="T"></typeparam>
        public override void Load<T>(string identifierFullPath, Action<ISerializableData> onLoadCompleted = null, Action onLoadFailed = null)
        {
            if (m_loadDataContainer != null)
            {
                m_loadDataContainer.Load(m_encrypted);

                onLoadCompleted?.Invoke(m_loadDataContainer as ISerializableData);
            }
            else
            {
                onLoadFailed?.Invoke();
            }
        }
    }
}