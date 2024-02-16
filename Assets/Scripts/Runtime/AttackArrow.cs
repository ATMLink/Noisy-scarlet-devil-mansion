using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackArrow : MonoBehaviour
{
    [SerializeField] private GameObject _bodyPrefab;
    [SerializeField] private GameObject _heaadPrefab;
    
    //敌人包围框预制体
    [SerializeField] private GameObject topLeftFrame;
    [SerializeField] private GameObject topRightFrame;
    [SerializeField] private GameObject bottomLeftFrame;
    [SerializeField] private GameObject bottomRightFrame;

    private GameObject topLeftPoint;
    private GameObject topRightPoint;
    private GameObject bottomLeftPoint;
    private GameObject bottomRightPoint;

    private const int _attackArrowPartsNumber = 17;
    private readonly List<GameObject> _arrow = new List<GameObject>(_attackArrowPartsNumber);

    private Camera mainCamera;

    [SerializeField] private LayerMask enemyLayer;
    private GameObject selectedEnemy;

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

        //生成包围框各个位置对象
        topLeftPoint = Instantiate(topLeftFrame, gameObject.transform);
        topRightPoint = Instantiate(topRightFrame, gameObject.transform);
        bottomLeftPoint = Instantiate(bottomLeftFrame, gameObject.transform);
        bottomRightPoint = Instantiate(bottomRightFrame, gameObject.transform);
        
        disableSelectionBox();
        
        mainCamera = Camera.main;
    }

    public void enableArrow(bool arrowEnabled)
    {
        isArrowEnable = arrowEnabled;
        foreach (var part in _arrow)
        {
            part.SetActive(arrowEnabled);
        }

        if (!arrowEnabled)
        {
            cancelSelectEnemy();
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

        var hitInfo = Physics2D.Raycast(mousePos, Vector3.forward, Mathf.Infinity, enemyLayer);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject != selectedEnemy ||
                selectedEnemy == null)
            {
                selectEnemy(hitInfo.collider.gameObject);
            }
        }
        else
        {
            cancelSelectEnemy();
        }
        
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

    private void selectEnemy(GameObject enemyObj)
    {
        selectedEnemy = enemyObj;

        var boxCollider = enemyObj.GetComponent<BoxCollider2D>();
        var size = boxCollider.size;
        var offset = boxCollider.offset;
        
        //通过boxcollider的大小size和偏移量offset，计算出包围框4个角的位置
        var topLeftLocation = offset + new Vector2(-size.x * 0.5f, size.y * 0.5f);
        var topLeftWorld = enemyObj.transform.TransformPoint(topLeftLocation);
        var topRightLocation = offset + new Vector2(size.x * 0.5f, size.y * 0.5f);
        var topRightWorld = enemyObj.transform.TransformPoint(topRightLocation);
        var bottomLeftLocation = offset + new Vector2(-size.x * 0.5f, -size.y * 0.5f);
        var bottomLeftWorld = enemyObj.transform.TransformPoint(bottomLeftLocation);
        var bottomRightLocation = offset + new Vector2(size.x * 0.5f, -size.y * 0.5f);
        var bottomRightWorld = enemyObj.transform.TransformPoint(bottomRightLocation);

        topLeftPoint.transform.position = topLeftWorld;
        topRightPoint.transform.position = topRightWorld;
        bottomLeftPoint.transform.position = bottomLeftWorld;
        bottomRightPoint.transform.position = bottomRightWorld;

        enableSelectionBox();
    }

    private void cancelSelectEnemy()
    {
        selectedEnemy = null;
        disableSelectionBox();
        foreach (var part in _arrow)
        {
            part.GetComponent<SpriteRenderer>().material.color = UnityEngine.Color.white;
        }
    }

    private void enableSelectionBox()
    {
        topLeftPoint.SetActive(true);
        topRightPoint.SetActive(true);
        bottomLeftPoint.SetActive(true);
        bottomRightPoint.SetActive(true);

        foreach (var part in _arrow)
        {
            part.GetComponent<SpriteRenderer>().material.color = UnityEngine.Color.red;
        }
    }
    
    private void disableSelectionBox()
    {
        topLeftPoint.SetActive(false);
        topRightPoint.SetActive(false);
        bottomLeftPoint.SetActive(false);
        bottomRightPoint.SetActive(false);
    }
}
