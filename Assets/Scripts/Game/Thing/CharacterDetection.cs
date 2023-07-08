using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetection : BaseThing<CharacterDetectionConfig>
{

    private int segments = 30; // 圆圈分段数
    private float lineWidth = 0.2f; // 圆圈线宽
    private LineRenderer lineRenderer;
    private Rigidbody2D _rigidbody;

    public override void OnCreate()
    {
        _rigidbody = Instance.GetComponent<Rigidbody2D>();
        if (_rigidbody == null)
        {
            Debug.LogError("Character没有Rigidbody2D");
        }

        if (Config.drawRadius)
        {
            // 创建LineRenderer组件并进行初始化
            lineRenderer = this.Instance.gameObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = segments + 1;
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth; 
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        }
    }

    public override void OnUpdate()
    {
        // 在每一帧更新中心点位置
        Vector2 center = this.Instance.transform.position;

        // 检测范围内的所有碰撞器
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.Instance.transform.position, Config.detectionRadius);

        // 遍历每个检测到的碰撞器
        foreach (Collider2D collider in colliders)
        {
            // 排除当前物体自身
            if (collider.gameObject != this.Instance.gameObject)
            {
                // 处理检测到的其他物体
                //Debug.Log("检测到物体: " + collider.gameObject.name);
            }
        }
        if (Config.drawRadius)
        {
            // 绘制检测半径的可视化表示
            float angleStep = 360f / segments;
            // 更新圆圈的顶点位置
            for (int i = 0; i <= segments; i++)
            {
                float angle = i * angleStep;
                float x = center.x + Mathf.Sin(Mathf.Deg2Rad * angle) * Config.detectionRadius;
                float y = center.y + Mathf.Cos(Mathf.Deg2Rad * angle) * Config.detectionRadius;
                Vector2 position = new Vector3(x, y);
                lineRenderer.SetPosition(i, position);
            }
        }

    }

}
