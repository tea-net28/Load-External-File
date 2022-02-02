using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ShowImage : MonoBehaviour
{
    [SerializeField] LoadFiles _loadFiles;

    [Header("デバッグ用 Number")]
    [SerializeField] int _num = 0;

    private List<ImageData> _imageData;
    private RawImage _rawImage;

    private Texture2D _texture;
    private int _textureWidth = 0;
    private int _textureHeight = 0;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(_loadFiles, "*** _loadFiles *** is null");

        _imageData = _loadFiles._imageData;
        _rawImage = this.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        // テクスチャのサイズを取得する
        _textureWidth = _imageData[_num].width;
        _textureHeight = _imageData[_num].height;

        // 取得したテクスチャのサイズの Texture 2D を作成する
        _texture = new Texture2D(_textureWidth, _textureHeight);

        // テクスチャデータを取得する
        _texture = _imageData[_num].texture;

        // テクスチャを RawImage に入れる
        _rawImage.texture = _texture;
    }
}
