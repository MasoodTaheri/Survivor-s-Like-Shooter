using UnityEngine;

[System.Serializable]
public class ObjectWithChance<T>
{
    public T item;
    [Range(0f, 1f)]
    public float chance;
    private float _min;
    private float _max;
    public void SetRandomRange(float min, float max)
    {
        _min = min;
        _max = max;
    }

    public bool CheckChanse(float randomValue)
    {
        if ((randomValue > _min) && (randomValue < _max))
            return true;
        else return false;
    }
}


