namespace d4160.Systems.DataPersistence
{
    using System;
    using NaughtyAttributes;
    using UnityEngine;
    using UnityEngine.Events;

    public abstract class AuthenticatorControllerBase : MonoBehaviour
    {
        [SerializeField] protected AuthenticationType m_authenticationType = AuthenticationType.Local;
        [ShowIf("IsAuthenticationRemote")]
        [SerializeField] protected RemotePersistenceType m_remotePersistenceType = RemotePersistenceType.PlayFab;

        [SerializeField] protected string m_username;
        [SerializeField] protected bool m_loginAtStart;
        [SerializeField] protected UnityEvent m_onLoginCompleted;
        [SerializeField] protected UnityEvent m_onLoginFailed;


        protected IAuthenticator m_authenticator;
        protected bool m_authenticated;

        #region Editor Only
#if UNITY_EDITOR
        protected bool IsAuthenticationRemote => m_authenticationType == AuthenticationType.Remote;
#endif
        #endregion

        public AuthenticationType AuthenticationType => m_authenticationType;
        public RemotePersistenceType RemotePersistenceType => m_remotePersistenceType;
        public string AuthenticationId => m_authenticator != null ? m_authenticator.Id : string.Empty;
        public bool Authenticated => m_authenticated;
        public string Username { get =>  m_username; set => m_username = value; }

        protected virtual void Start()
        {
            if (m_loginAtStart)
                Login();
        }

        public virtual void Login()
        {
            Login(null);
        }

        public virtual void Login(Action onCompleted, Action onFailed = null)
        {
            if(m_authenticated) return;

            m_authenticator = CreateAuthenticator(
                () => {
                    onCompleted?.Invoke();
                    m_onLoginCompleted?.Invoke();

                    m_authenticated = true;
                }, () => {
                    onFailed?.Invoke();
                    m_onLoginFailed?.Invoke();
                });

            if(m_authenticator != null)
                m_authenticator.Login();
        }

        public virtual void Logout()
        {
            if(!m_authenticated) return;

            if (m_authenticator != null)
            {
                m_authenticator.Logout();

                m_authenticated = false;
            }
        }

        protected abstract IAuthenticator CreateAuthenticator(Action onCompleted = null, Action onFailed = null);
    }
}