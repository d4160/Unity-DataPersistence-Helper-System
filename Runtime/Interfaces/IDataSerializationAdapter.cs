namespace d4160.Systems.DataPersistence
{
    using UnityEngine.GameFoundation.DataPersistence;

    public interface IDataSerializationAdapter
    {
        ISerializableData GetSerializableData();

        void FillFromSerializableData(ISerializableData data);

        void Initialize(ISerializableData data);
    }
}