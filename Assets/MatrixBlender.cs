using UnityEngine;
using System.Collections;
using Assets.LogicSystem;

public class MatrixBlender : MonoBehaviour {

    public Camera ViewCamera;
    public bool Working { get; set; }



    private IEnumerator LerpFromTo(Matrix4x4 src, Matrix4x4 dest, float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            ViewCamera.projectionMatrix = MatrixLerp(src, dest, (Time.time - startTime) / duration);
            yield return 1;
        }
        ViewCamera.projectionMatrix = dest;
        Working = false;
    }

    public Coroutine BlendToMatrix(Matrix4x4 targetMatrix, float duration)
    {
        Working = true;
        return StartCoroutine(LerpFromTo(ViewCamera.projectionMatrix, targetMatrix, duration));
    }

    public static Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float time)
    {
        Matrix4x4 ret = new Matrix4x4();
        for (int i = 0; i < 16; i++)
            ret[i] = Mathf.Lerp(from[i], to[i], time);
        return ret;
    }

}
