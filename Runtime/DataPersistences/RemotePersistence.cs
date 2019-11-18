namespace d4160.Systems.DataPersistence
{
    using UnityEngine.GameFoundation.DataPersistence;
    using UnityEngine;
    using System;
    public abstract class RemoteDataPersistence : BaseDataPersistence
    {
        protected ILoginProvider m_loginProvider;

        public RemoteDataPersistence(IDataSerializer serializer, ILoginProvider loginProvider) : base(serializer)
        {
            m_loginProvider = loginProvider;

            if (m_loginProvider != null)
                m_loginProvider.Login();
        }
    }
}