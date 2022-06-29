using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _playerFollowCamera;

    //マウススクロールに応じてカメラをズームする
    public void UpdateOrthographicSize(float mouseScrollWeelAxis)
    {
        float _scrollWeelAxis = mouseScrollWeelAxis;
        float _scrollSpeed = 0.5f;
        float _maxOrthographicSize = 9;
        float _minOrthographicSize = 5.7f;

        if (_scrollWeelAxis < 0)
        {
            if (_playerFollowCamera.m_Lens.OrthographicSize < _minOrthographicSize)
            {
                return;
            }
            _playerFollowCamera.m_Lens.OrthographicSize += _scrollWeelAxis * _scrollSpeed;

        }
        else if (_scrollWeelAxis > 0)
        {
            if (_playerFollowCamera.m_Lens.OrthographicSize > _maxOrthographicSize)
            {
                return;
            }

            _playerFollowCamera.m_Lens.OrthographicSize += _scrollWeelAxis * _scrollSpeed;
        }
    }

    public void UpdateManaged(float mouseScrollWeelAxis)
    {
        UpdateOrthographicSize(mouseScrollWeelAxis);
    }
}
