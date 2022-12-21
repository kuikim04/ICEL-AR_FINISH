using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
public class MeasureDepth : MonoBehaviour
{
    public delegate void NewTriggerPoints(List<Vector2> triggerPoints);
    public static event NewTriggerPoints OnTriggerPoints = null;

    public MultiSourceManager mMultiSource;
    public Texture2D mDepthTexture;

    //Cutoffs
    [Range(0, 1.0f)]
    public float mDepthSensitivity = 1;
    [Range(-10, 10f)]
    public float mWallDepth = -10;

    [Header("Top and Bottom")]
    [Range(-1, 1f)]
    public float mTopCutoff = 1;
    [Range(-1, 1f)]
    public float mBottomCutoff = -1;
    [Header("Left and Right")]
    [Range(-1, 1f)]
    public float mLeftCutoff = -1;
    [Range(-1, 1f)]
    public float mRightCutoff = 1;

    //Depth

    private ushort[] mDepthData = null;
    private CameraSpacePoint[] mCameraSpacePoints = null;
    private ColorSpacePoint[] mColorSpacePoints = null;
    private List<ValidPoint> mValidPoints = null;
    private List<Vector2> mTriggerPoint = null;

    //Kinect
    private KinectSensor mSensor = null;
    private CoordinateMapper mMapper = null;
    private Camera mCamera = null;

    private readonly Vector2Int mDepthResolution = new Vector2Int(512, 424);
    private Rect mRect;
    private void Awake()
    {
        mSensor = KinectSensor.GetDefault();
        mMapper = mSensor.CoordinateMapper;
        mCamera = Camera.main;
        int arraySize = mDepthResolution.x * mDepthResolution.y;

        mCameraSpacePoints = new CameraSpacePoint[arraySize];
        mColorSpacePoints = new ColorSpacePoint[arraySize];
    }

    private void Update()
    {
        mValidPoints = DepthToColor();
        mTriggerPoint = FilterToTrigger(mValidPoints);
        if(OnTriggerPoints != null && mTriggerPoint.Count != 0)      
            OnTriggerPoints(mTriggerPoint);
        


        if (Input.GetKeyDown(KeyCode.Space))
        {
            mRect = CreateRect(mValidPoints);

            mDepthTexture = CreateTexture(mValidPoints);
        }
    }
    private void OnGUI()
    {
        GUI.Box(mRect, "");

        if (mTriggerPoint == null)
            return;

        foreach(Vector2 point in mTriggerPoint)
        {
            Rect rect = new Rect(point, new Vector2(10, 10));
            GUI.Box(rect, "");
        }
    }

    private List<ValidPoint> DepthToColor()
    {
        //point to Return
        List<ValidPoint> validPoints = new List<ValidPoint>();

        // Get depth
        mDepthData = mMultiSource.GetDepthData();

        //map

        mMapper.MapDepthFrameToCameraSpace(mDepthData, mCameraSpacePoints);
        mMapper.MapDepthFrameToColorSpace(mDepthData, mColorSpacePoints);

        //Filter
        for(int i = 0;i < mDepthResolution.x / 8; i++)
        {
            for (int j = 0; j < mDepthResolution.y / 8; j++)
            {
                //sample Index
                int sampleIndex = (j * mDepthResolution.x) + i;
                sampleIndex *= 8;

                //cutoff Test
                if (mCameraSpacePoints[sampleIndex].X < mLeftCutoff)
                    continue;

                if (mCameraSpacePoints[sampleIndex].X < mRightCutoff)
                    continue;

                if (mCameraSpacePoints[sampleIndex].Y < mTopCutoff)
                    continue;

                if (mCameraSpacePoints[sampleIndex].Y < mBottomCutoff)
                    continue;

                //Create Point
                ValidPoint newPoint = new ValidPoint(mColorSpacePoints[sampleIndex], mCameraSpacePoints[sampleIndex].Z);

                //Depth Test
                if (mCameraSpacePoints[sampleIndex].Z >= mWallDepth)
                    newPoint.mWithinWallDepth = true;

                //Add 
                validPoints.Add(newPoint);
            }
        }

        return validPoints;
    }
    private List<Vector2> FilterToTrigger(List<ValidPoint> validPoints)
    {
        List<Vector2> triggerPoint = new List<Vector2>();

        foreach(ValidPoint point in validPoints)
        {
            if(!point.mWithinWallDepth)
            {      
                if(point.z < mWallDepth * mDepthSensitivity)
                {
                    Vector2 screenPoint = ScreenToCamera(new Vector2(point.colorSpace.X, point.colorSpace.Y));              

                    triggerPoint.Add(screenPoint);
                }
            }

        }
        return triggerPoint;
    }
    private Texture2D CreateTexture(List<ValidPoint> validPoints)
    {
        Texture2D newTexture = new Texture2D(1920, 1080, TextureFormat.Alpha8, false);

        for(int x = 0; x < 1920; x++)
        {
            for (int y = 0; y < 1080; y++)
            {
                newTexture.SetPixel(x, y, Color.clear);
            }
        }

        foreach(ValidPoint point in validPoints)
        {
            newTexture.SetPixel((int)point.colorSpace.X,(int)point.colorSpace.Y, Color.black);
        }

        newTexture.Apply();

        return newTexture;
    }

    #region Rect Creation
    private Rect CreateRect(List<ValidPoint> points)
    {
        if (points.Count == 0)
            return new Rect();

        // Get corners of rect
        Vector2 topLeft = GetTopLeft(points);
        Vector2 bottomRight = GetBottomRight(points);

        // Translate to viewport
        Vector2 screenTopleft = ScreenToCamera(topLeft);
        Vector2 scrrenBottomRight = ScreenToCamera(bottomRight);

        // Rect Dimensions;
        int width = (int)(scrrenBottomRight.x - screenTopleft.x);
        int height = (int)(scrrenBottomRight.y - screenTopleft.y);

        //create
        Vector2 size = new Vector2(width, height);
        Rect rect = new Rect(screenTopleft, size);

        return rect;
    }
    private Vector2 GetTopLeft(List<ValidPoint> points)
    {
        Vector2 topLeft = new Vector2(int.MaxValue, int.MaxValue);

        foreach(ValidPoint point in points) 
        {
            // left most x
            if (point.colorSpace.X < topLeft.x)
                topLeft.x = point.colorSpace.X;


            // Top most y
            if (point.colorSpace.Y < topLeft.y)
                topLeft.y = point.colorSpace.Y;
        }

        return topLeft;
    }
    private Vector2 GetBottomRight(List<ValidPoint> points)
    {
        Vector2 bomtomRight = new Vector2(int.MinValue, int.MinValue);

        foreach (ValidPoint point in points)
        {
            // Right most x
            if (point.colorSpace.X > bomtomRight.x)
                bomtomRight.x = point.colorSpace.X;


            // Bottom most y
            if (point.colorSpace.Y > bomtomRight.y)
                bomtomRight.y = point.colorSpace.Y;
        }

        return bomtomRight;
    }
    private Vector2 ScreenToCamera(Vector2 screenPosition)
    {
        Vector2 normalizedScreen = new Vector2(Mathf.InverseLerp(0, 1920,
            screenPosition.x), Mathf.InverseLerp(0, 1080, screenPosition.y));

        Vector2 screenPoint = new Vector2(normalizedScreen.x * mCamera.pixelWidth, normalizedScreen.y * mCamera.pixelHeight);

        return screenPoint;
    }

    #endregion
}
public class ValidPoint
{
    public ColorSpacePoint colorSpace;
    public float z = 0.0f;

    public bool mWithinWallDepth = false;
    public ValidPoint(ColorSpacePoint newColorSpace, float newZ)
    {
        colorSpace = newColorSpace;
        z = newZ;
    }
}
