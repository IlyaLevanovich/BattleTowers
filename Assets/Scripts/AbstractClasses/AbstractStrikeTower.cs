using System.Collections;
using UnityEngine;

public abstract class AbstractStrikeTower : MonoBehaviour
{
    protected abstract void Start();
    protected abstract void Update();
    protected abstract IEnumerator SpawnBullet();
    protected virtual void DestroyAllReferences()
    {
        GameplayConfiguration.TargetBuild.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

}
