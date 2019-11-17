namespace d4160.Systems.DataPersistence
{
    using UnityEngine.GameFoundation.DataPersistence;
    using UnityEngine;
    using System;
    public abstract class RemoteDataPersistence : BaseDataPersistence
    {
        /// <summary>
        /// Use this value to prevent login many times if don't need that
        /// </summary>
        protected bool m_logged;

        public RemoteDataPersistence(IDataSerializer serializer) : base(serializer)
        {
        }

        public abstract void LogIn();
    }
}