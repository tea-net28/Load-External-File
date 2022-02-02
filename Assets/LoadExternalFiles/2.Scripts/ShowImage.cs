using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ShowImage : MonoBehaviour
{
    [SerializeField] LoadFiles _loadFiles;

    [Header("�f�o�b�O�p Number")]
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
        // �e�N�X�`���̃T�C�Y���擾����
        _textureWidth = _imageData[_num].width;
        _textureHeight = _imageData[_num].height;

        // �擾�����e�N�X�`���̃T�C�Y�� Texture 2D ���쐬����
        _texture = new Texture2D(_textureWidth, _textureHeight);

        // �e�N�X�`���f�[�^���擾����
        _texture = _imageData[_num].texture;

        // �e�N�X�`���� RawImage �ɓ����
        _rawImage.texture = _texture;
    }
}
