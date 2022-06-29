using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundImageManager : MonoBehaviour
{

    //materials
    public Material _material;

    //instantiate parents
    public GameObject BaseBackGroundImageParent;
    public GameObject FrontBackGroundImageParent;
    public GameObject MiddleBackGroundImageParent;
    public GameObject MainBackGroundImageParent;

    //prefabs
    public GameObject BaseBackgroundImagePrefab;
    public GameObject[] FrontBackGroundImagePrefabs;
    public GameObject[] MiddleBackGroundImagePrefabs;
    public GameObject[] MainBackGroundImagePrefabs;

    //画像をランダムに生成する
    public void InstantiateBackGroundImagePrefabFromParent(GameObject[] gameObjects, int[] weight, int maximun, GameObject parent)
    {
        for (int i = 0; i < maximun; i++)
        {
            int random = Common.GetRandomInt.GetRandomIndex(weight);

            Instantiate(gameObjects[random], Vector3.zero, Quaternion.identity, parent.transform);
        }
    }



    //画像全てのY軸を一括で変更する
    public void SetBackGroundImagesYFromParent(float y, GameObject parent)
    {
        int _childCount = parent.transform.childCount;
        for (int i = 0; i < _childCount; i++)
        {
            GameObject gameObject = parent.transform.GetChild(i).gameObject;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, y, 0);
        }
    }

    //画像を横に均等に並べる
    public void BaseSortBackGroundImageXFromParent(GameObject parent)
    {
        int _childCount = parent.transform.childCount;
        float _storeX = 0;
        float _beforeImageSizeX = 0;
        for (int i = 0; i < _childCount; i++)
        {
            GameObject gameObject = parent.transform.GetChild(i).gameObject;
            _storeX += _beforeImageSizeX;
            gameObject.transform.position = new Vector3(_storeX, gameObject.transform.position.y, 0);
            _beforeImageSizeX = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }
    //画像にランダムにスペースを作る
    public void AddSpaceBackGroundImageXToRandomFromParent(float[] spaces, int[] weight, GameObject parent)
    {
        int _childCount = parent.transform.childCount;
        int _randNum;

        for (int i = 0; i < _childCount; i++)
        {
            _randNum = Common.GetRandomInt.GetRandomIndex(weight);
            GameObject gameObject = parent.transform.GetChild(i).gameObject;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + spaces[_randNum], gameObject.transform.position.y, 0);
        }
    }

    //画像のスケールをランダムに変更する
    public void ToImageScaleAtRandomFromParent(float[] scaleXs, float[] scaleYs, int[] weight, GameObject parent)
    {
        int _childCount = parent.transform.childCount;
        int _randNum;

        for (int i = 0; i < _childCount; i++)
        {
            _randNum = Common.GetRandomInt.GetRandomIndex(weight);
            GameObject gameObject = parent.transform.GetChild(i).gameObject;
            gameObject.transform.localScale = new Vector3(scaleXs[_randNum], scaleYs[_randNum], 1);
        }
    }

    //画像のY軸をランダムに変更する
    public void ToImageYAtRandomFromParent(float[] ys, int[] weight, GameObject parent)
    {
        int _childCount = parent.transform.childCount;
        int _randNum;

        for (int i = 0; i < _childCount; i++)
        {
            _randNum = Common.GetRandomInt.GetRandomIndex(weight);
            GameObject gameObject = parent.transform.GetChild(i).gameObject;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + ys[_randNum], 0);
        }

    }

    //画像をランダムに回転させる
    public void ToImageRotateAtRandomFromParent(float[] rotateZs, int[] weight, GameObject parent)
    {
        int _childCount = parent.transform.childCount;
        int _randNum;

        for (int i = 0; i < _childCount; i++)
        {
            _randNum = Common.GetRandomInt.GetRandomIndex(weight);
            GameObject gameObject = parent.transform.GetChild(i).gameObject;
            gameObject.transform.Rotate(new Vector3(0, 0, rotateZs[_randNum]));
        }
    }

    //画像をランダムに反転させる
    public void ToImageFlipAtRandomFromParent(string[] xs, int[] weight, GameObject parent)
    {
        int _childCount = parent.transform.childCount;
        int _randNum;

        for (int i = 0; i < _childCount; i++)
        {
            _randNum = Common.GetRandomInt.GetRandomIndex(weight);
            GameObject gameObject = parent.transform.GetChild(i).gameObject;
            if (xs[_randNum] == "x")
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }


    void InstantiateBaseSkyPrefab()
    {
        InstantiateBackGroundImagePrefabFromParent(
            new GameObject[] { BaseBackgroundImagePrefab },
            new int[] { 1 },
            10,
            BaseBackGroundImageParent
        );
        BaseSortBackGroundImageXFromParent(BaseBackGroundImageParent);
    }

    void InstantiateFrontBackGroundImagePrefabs()
    {
        //画像をランダムに生成する
        InstantiateBackGroundImagePrefabFromParent(
            FrontBackGroundImagePrefabs,
            new int[] { 3, 7 },
            120,
            FrontBackGroundImageParent
        );

        //画像のスケールをランダムに変更する
        ToImageScaleAtRandomFromParent(new float[] { 0.7f, 0.5f, 1 }, new float[] { 1, 1, 1 }, new int[] { 5, 3, 10 }, FrontBackGroundImageParent);

        //画像を横に並べる
        BaseSortBackGroundImageXFromParent(FrontBackGroundImageParent);

        //画像のY軸を変更する
        SetBackGroundImagesYFromParent(0.6f, FrontBackGroundImageParent);

        //画像にランダムにスペースを作る
        AddSpaceBackGroundImageXToRandomFromParent(new float[] { 0, 0.5f, 1 }, new int[] { 5, 3, 10 }, FrontBackGroundImageParent);

        //画像のY軸をランダムに変更する
        //ToImageYAtRandomFromParent(new float[] { 0.1f, 0.2f, 0 }, new int[] { 5, 3, 10 }, FrontBackGroundImageParent);

        //画像をランダムに回転させる
        //ToImageRotateAtRandomFromParent(new float[] { 10, 20, 0 }, new int[] { 5, 3, 10 }, FrontBackGroundImageParent);
    }

    void InstantiateMiddleBackGroundImagePrefabs()
    {
        InstantiateBackGroundImagePrefabFromParent(
            MiddleBackGroundImagePrefabs,
            new int[] { 1 },
            120,
            MiddleBackGroundImageParent
        );
        ToImageScaleAtRandomFromParent(new float[] { 0.7f, 0.5f, 1 }, new float[] { 1, 0.8f, 1 }, new int[] { 5, 3, 10 }, MiddleBackGroundImageParent);
        BaseSortBackGroundImageXFromParent(MiddleBackGroundImageParent);
        ToImageYAtRandomFromParent(new float[] { -0.5f, -0.3f, -0.2f }, new int[] { 3, 2, 5 }, MiddleBackGroundImageParent);
        AddSpaceBackGroundImageXToRandomFromParent(new float[] { 0.2f, 0.5f, 1 }, new int[] { 5, 3, 10 }, MiddleBackGroundImageParent);
    }

    void InstantiateMainBackGroundImagePrefabs()
    {
        InstantiateBackGroundImagePrefabFromParent(
            MainBackGroundImagePrefabs,
            new int[] { 4, 6 },
            60,
            MainBackGroundImageParent
        );
        ToImageScaleAtRandomFromParent(new float[] { 0.7f, 0.5f, 1 }, new float[] { 0.7f, 0.5f, 1 }, new int[] { 5, 3, 10 }, MainBackGroundImageParent);
        BaseSortBackGroundImageXFromParent(MainBackGroundImageParent);
        AddSpaceBackGroundImageXToRandomFromParent(new float[] { 1f, 2f, 10 }, new int[] { 5, 3, 10 }, MainBackGroundImageParent);
        ToImageYAtRandomFromParent(new float[] { -0.2f, -0.3f, 0 }, new int[] { 5, 3, 10 }, FrontBackGroundImageParent);
        //ToImageFlipAtRandomFromParent(new string[] { "x", "none" }, new int[] { 2, 7 }, MainBackGroundImageParent);
    }

    private void Awake()
    {
        InstantiateBaseSkyPrefab();
        InstantiateFrontBackGroundImagePrefabs();
        InstantiateMiddleBackGroundImagePrefabs();
        InstantiateMainBackGroundImagePrefabs();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
