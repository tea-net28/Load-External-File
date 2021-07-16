using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI = UnityEngine.UI;

public partial class ShowImage : MonoBehaviour
{
    // LoadFiles.cs を読み込み
    public LoadFiles _loadFiles;

    UI.RawImage _image;

    [SerializeField] int x = 0;
    [SerializeField] int y = 0;

    Texture2D _tex;

    int _texWidth;
    int _texHeight;

    // Start is called before the first frame update
    void Start()
    {
        _image = this.GetComponent<UI.RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        // 対象のテクスチャのサイズを取得する
        _texWidth = _loadFiles._texSize[x][y][0];
        _texHeight = _loadFiles._texSize[x][y][1];

        // 取得したサイズのTexture2Dを作成し、対象のテクスチャを取得する
        _tex = new Texture2D(_texWidth, _texHeight);
        _tex = _loadFiles._imageTex[x][y];

        // テクスチャをRawImageに入れる
        _image.texture = _tex;
    }
}