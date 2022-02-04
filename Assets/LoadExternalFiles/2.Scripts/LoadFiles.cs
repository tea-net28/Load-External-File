using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public class LoadFiles : MonoBehaviour
{
    [Header("データ格納用のフォルダ名")]
    [SerializeField] string _subDir = "_files";

    [Header("指定フォルダの中にさらにフォルダを作成する場合はチェック")]
    [SerializeField] bool _useFolders = false;

    string _path;
    string _filePath;

    string[] _subDirectories;
    string[][] _subFile;

    [NonSerialized] public Texture2D[][] _imageTex;
    [NonSerialized] public int[][][] _texSize;

    [NonSerialized] public List<ImageData> _imageData;


    void Awake()
    {
        // 自分自身の実行ファイルのパスを取得する
        // /Library/ScriptAssemblies/ に作成されたため、変更
        //_path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        _path = Application.dataPath + "/.."; // "/.."を外すと、Assetsフォルダ内に作成される

        // 読み込み用のフォルダパスを作成
        _filePath = Path.Combine(_path, _subDir).Replace('/', Path.DirectorySeparatorChar);
        Debug.Log(_filePath);

        // ディレクトリが存在しなければ作成
        if (!Directory.Exists(_filePath))
        {
            Directory.CreateDirectory(_filePath);
            Debug.Log("Create folder.");
        }

        // _useFoldersにチェックが入っている場合は
        // ディレクトリ内に存在するフォルダ一覧を取得する
        if (_useFolders)
        {
            // ディレクトリ内に存在するフォルダ一覧を取得する
            _subDirectories = Directory.GetDirectories(_filePath);
            Debug.Log("Sub directories : " + _subDirectories[0]);

            // ディレクトリの数だけ１次元目の配列を作成する
            _subFile = new string[_subDirectories.Length][];

            // ジャグ配列にファイル名を格納していく
            int i = 0;
            // ディレクトリ分ファイル一覧を取得する
            foreach (string subDir in _subDirectories)
            {
                int j = 0;

                // pngファイルのファイル名一覧を取得する
                string[] files = Directory.GetFiles(_subDirectories[i], "*png");

                // 取得したファイル名の数だけ、２次元目の配列を作成する
                _subFile[i] = new string[files.Length];

                foreach (var file in files)
                {
                    // 多次元配列にファイル名を格納する
                    _subFile[i][j] = file;
                    j++;
                }

                i++;
            }

            LoadImageInFolders();
        }
        else
        {
            _imageData = new List<ImageData>();

            // ファイル名を取得する
            string[] files = Directory.GetFiles(_filePath, "*png");

            // 構造体の作成
            for (int i = 0; i < files.Length; i++)
            {
                ImageData data = new ImageData();

                // id の付与
                data.id = i;

                // ファイル名を構造体に保存する
                data.fileName = files[i].Split(Path.DirectorySeparatorChar).Last();

                // ファイルパスを構造体に保存する
                data.filePath = files[i];

                // 画像をバイト型で読み込み
                byte[] bytes = File.ReadAllBytes(files[i]);
                // Texture 2Dに変換する
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(bytes);
                // 構造体に保存する
                data.texture = tex;

                // テクスチャのサイズを構造体に保存する
                data.width = tex.width;
                data.height = tex.height;

                // 作成した構造体を配列に格納する
                _imageData.Add(data);
            }

            Debug.Log("Complete");
        }
    }



    public void LoadImageInFolders()
    {
        // _subFileからTexture2Dのジャグ配列を作成する
        // 同時にテクスチャのサイズを保存するジャグ配列も作成する
        _imageTex = new Texture2D[_subFile.Length][];
        _texSize = new int[_subFile.Length][][];

        for (int i = 0; i < _subFile.Length; i++)
        {
            _imageTex[i] = new Texture2D[_subFile[i].Length];
            _texSize[i] = new int[_subFile[i].Length][];

            for (int j = 0; j < _subFile[i].Length; j++)
            {
                _texSize[i][j] = new int[2];
            }
        }

        // 画像ファイルからテクスチャを作成する
        for (int x = 0; x < _subFile.Length; x++)
        {
            for (int y = 0; y < _subFile[x].Length; y++)
            {
                // 画像ファイルをバイト型で読み込み
                byte[] bytes = File.ReadAllBytes(_subFile[x][y]);

                // Texture2Dに変換、多次元配列に保存する
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(bytes);
                _imageTex[x][y] = tex;

                // テクスチャのサイズを格納する
                _texSize[x][y][0] = tex.width;
                _texSize[x][y][1] = tex.height;
            }
        }
        Debug.Log("Complete.");
    }
}

public struct ImageData
{
    public int id;
    public string fileName;
    public string filePath;
    public Texture2D texture;
    public int width;
    public int height;
}