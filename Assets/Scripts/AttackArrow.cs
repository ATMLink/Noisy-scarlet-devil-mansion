using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackArrow : MonoBehaviour
{
    [SerializeField] private GameObject _bodyPrefab;
    [SerializeField] private GameObject _heaadPrefab;

    private const int _attackArrowPartsNumber = 17;
    private readonly List<GameObject> _arrow = new List<GameObject>(_attackArrowPartsNumber);

    private Camera mainCamera;

    private bool isArrowEnable;

    private void Start()
    {
        for (var i = 0; i < _attackArrowPartsNumber - 1; i++)
        {
            var body = Instantiate(_bodyPrefab, gameObject.transform);
            _arrow.Add(body);
        }

        var head = Instantiate(_heaadPrefab, gameObject.transform);
        _arrow.Add(head);

        foreach (var part in _arrow)
        {
            part.SetActive(false);
        }
        mainCamera = Camera.main;
    }

    public void enableArrow(bool arrowEnabled)
    {
        isArrowEnable = arrowEnabled;
        foreach (var part in _arrow)
        {
            part.SetActive(arrowEnabled);
        }
    }
    
    private void LateUpdate()
    {
        if (!isArrowEnable)
        {
            return;
        }
        
        var mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var mouseX = mousePos.x;
        var mouseY = mousePos.y;
        
        const float centerX = 0.0f;
        const float centerY = -1.0f;

        //计算贝塞尔曲线控制点
        var controlAx = centerX - (mouseX - centerX) * 0.3f;
        var controlAy = centerY + (mouseY - centerY) * 0.8f;
        var controlBx = centerX + (mouseX - centerX) * 0.1f;
        var controlBy = centerY + (mouseY - centerY) * 1.4f;

        for (var i = 0; i < _arrow.Count; i++)
        {
            var part = _arrow[i];
            
            //calculate each part of arrow position
            
            //不同箭身部分根据索引值得到对应位置的t值
            //越靠近arrow尾部t越小
            //越靠近arrow头部t越大
            var t = (i + 1) * 1.0f / _arrow.Count;
            
            // t的平方
            var tt = t * t;
            //t三次方
            var ttt = tt * t;

            var u = 1.0f - t;
            var uu = u * u;
            var uuu = uu * u;
            //贝塞尔三阶立方公式：B(t) = P0 * uuu + 3 * P1 * t * uu + 3 * P2 * tt * u + P3 * ttt, 0<= t <= 1
            //其中曲线起始于P0，终止于P1, 从p2方向来到p3，一般不会经过怕p1，p2；p1,p2只负责提供方向信息
            //p3就是centerX/centerY, P0是mouseX/mouseY, P1是controlAx/controlAy, p2 is controlBx/controlBy
            //arrowX is the value of X's position which from the new arrow component. Similarly arrowY is the position of arrow component

            var arrowX = uuu * centerX +
                         3 * uu * t * controlAx +
                         3 * u * tt * controlBx +
                         ttt * mouseX;
            var arrowY = uuu * centerY +
                         3 * uu * t * controlAy +
                         3 * u * tt * controlBy +
                         ttt * mouseY;

            _arrow[i].transform.position = new Vector3(arrowX, arrowY, 0.0f);
            
            //calculate each part of arrow spirit direction
            float directionX;
            float directionY;

            if (i > 0)
            {
                //expected the tail of arrow
                directionX = _arrow[i].transform.position.x - _arrow[i - 1].transform.position.x;
                directionY = _arrow[i].transform.position.y - _arrow[i - 1].transform.position.y;
            }
            else
            {
                //concentrate the tail of arrow
                directionX = _arrow[i + 1].transform.position.x - _arrow[i].transform.position.x;
                directionY = _arrow[i + 1].transform.position.y - _arrow[i].transform.position.y;
            }

            part.transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(directionX, directionY) * Mathf.Rad2Deg);

            part.transform.localScale = new Vector3(
                1.0f - 0.03f * (_arrow.Count - 1 - i),
                1.0f - 0.03f * (_arrow.Count - 1 - i),
                0);
        }
    }
}
