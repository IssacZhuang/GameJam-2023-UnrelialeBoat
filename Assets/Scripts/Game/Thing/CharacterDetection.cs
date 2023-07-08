using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vocore;

public class CharacterDetection : BaseThing<CharacterDetectionConfig>
{

    private int segments = 30; // ԲȦ�ֶ���
    private float lineWidth = 0.2f; // ԲȦ�߿�
    private LineRenderer lineRenderer;
    private Rigidbody2D _rigidbody;

    public override void OnCreate()
    {
        _rigidbody = Instance.GetComponent<Rigidbody2D>();
        if (_rigidbody == null)
        {
            Debug.LogError("Characterû��Rigidbody2D");
        }

        if (Config.drawRadius)
        {
            // ����LineRenderer��������г�ʼ��
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
        // ��ÿһ֡�������ĵ�λ��
        Vector2 center = this.Instance.transform.position;

        // ��ⷶΧ�ڵ�������ײ��
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.Instance.transform.position, Config.detectionRadius);

        // ����ÿ����⵽����ײ��
        foreach (Collider2D collider in colliders)
        {
            // �ų���ǰ��������
            if (collider.gameObject != this.Instance.gameObject)
            {
                // ������⵽����������
                // Debug.Log("��⵽����: " + collider.gameObject.name);
                var eventBridge = collider.gameObject.GetComponent<EventBridge>();
                if (eventBridge != null)
                {
                    // Debug.Log("������ " + collider.gameObject.name+"���������¼�.");
                    EventBridge.SendEventByGameObject(collider.gameObject, EventHoverObject.eventHoverObject);
                }

            }
        }
        if (Config.drawRadius)
        {
            // ���Ƽ��뾶�Ŀ��ӻ���ʾ
            float angleStep = 360f / segments;
            // ����ԲȦ�Ķ���λ��
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
