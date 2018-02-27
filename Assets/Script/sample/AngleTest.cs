using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTest : MonoBehaviour {

    /// <summary>位置関係を知りたい相手</summary>
    [SerializeField] GameObject m_target;
    /// <summary>位置関係を表示する UI</summary>
    [SerializeField] UnityEngine.UI.Text m_console;
    float m_angleBuffer;
    /// <summary>これ以上角度が変わると変わったとする値</summary>
    [SerializeField] float m_precision = .1f;

    // Use this for initialization
    void Start()
    {
        m_angleBuffer = GetAngle();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = GetAngle();

        // 角度が変わっていたらログを出力する
        if (Mathf.Abs(angle - m_angleBuffer) > Mathf.Abs(m_precision))
        {
            m_angleBuffer = angle;
            string message = string.Format("Angle: {0} degree from {1} to {2}", angle, gameObject.name, m_target.name);
            Debug.Log(message);
            if (m_console)
                m_console.text = message;
        }
    }

    /// <summary>
    /// 自分自身と m_target の Y 軸に対する角度を求める
    /// </summary>
    /// <returns>The angle.</returns>
    float GetAngle()
    {
        Vector3 targetDirection = m_target.transform.position - transform.position;
        Vector3 angleAxis = Vector3.Cross(transform.forward, targetDirection);
        float angle = Vector3.Angle(transform.forward, targetDirection) * (angleAxis.y < 0 ? -1 : 1);
        return angle;
    }
}
