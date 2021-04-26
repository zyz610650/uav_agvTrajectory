using UnityEngine;

using System.Collections;
using System;
using System.Reflection;

public class DrawLines : MonoBehaviour

{

    public Material lineMat;
    public int uavNum = 16;
    //初始无人机
    public GameObject[] init_uav;
    //优化过的无人机
    public GameObject[] optim_uav;

    //无人机资源位置
    private string start_uavPath = "UAV/startUav";
    private string optim_uavPath = "UAV/optimUav";

    struct Point
    {
        public int x;
        public int y;
        public int z;
    };


    //无人机初始和优化过的位置
    double[] init = { 0.427062347759695, 0.955372102971021, 0.724247033520084, 0.580891712363774, 0.540257907420780, 0.705441191564081, 0.00502888330112106, 0.782515778836242, 0.926859573385509, 0.00829565739263971, 0.824628342666394, 0.767335868027880, 0.997136895348686, 0.227653072058219, 0.919542206194465, 0.641999305521780, 7.38418767360661, 44.0508166401978, 3.1596208916383, 49.6799421779149, 10.24136544401531, 99.38315489695107, 40.0873797601841, 89.3474311740186, 77.5820073116048, 70.0856738133542, 49.4456347383399, 6.77854884858151, 89.7646542373691, 28.8565308881261, 26.9046814753517, 51.4194175430363, 47.5879024056308, 36.8311038073403, 65.5611090496500, 93.8200405804263, 62.0425185783994, 28.2840091614325, 20.5181251932987, 43.9134081593786, 2.72502196608166, 87.6184335960346, 61.0092246274455, 20.3592380287419, 51.9916824790401, 5.38243021501204, 86.2187431116878, 44.2934666053869, 100, 100, 100, 100, 100, 100, 100, 100, 0, 0, 0, 0, 0, 0, 0, 0 };

    double[] optimal = { 0.655287255112211, 0.653471696369592, 0.722235974309432, 0.728015930737135, 0.773178894585431, 0.718091020225905, 0.652449125449163, 0.771721136548227, 0.734429511044003, 0.635051564814241, 0.665374395895557, 0.755367130015742, 0.710000602000029, 0.684249568075404, 0.691673855880947, 0.723512595501220, 37.1316623031787, 28.4459781902852, 35.8187360267302, 48.7324302264522, 30.9631747309186, 27.8065578291397, 36.0035674254726, 11.8149859335371, 19.5007223624072, 19.2714512056511, 22.0675740196570, 54.5529174972730, 46.1864305118330, 40.1192343865455, 13.5966671284996, 60.8907363103658, 36.2586396619187, 42.0167777173888, 19.0495618896316, 68.3022538074797, 28.0339434872096, 26.8529277545481, 11.4810985860976, 23.7105430426302, 40.7256659409272, 44.3513877029699, 34.5117462287101, 41.4352953838657, 23.2114643958491, 28.0883307383773, 10.2605241764887, 27.9114497150094, 101.551526636460, 101.602830629083, 100.943732109826, 101.065207910129, 101.253234202145, 101.102817598877, 101.732231407286, 101.515703177467, 0, 0, 0, 0, 0, 0, 0, 0 };



   

   





    //变量类型转换
    float convertToFloat(double tmp)
    {
        return float.Parse(tmp.ToString());
        
        
    }


    //public void set
    //自定义事件
    void DrawConnectingLines()

    {
      
        if (init_uav.Length > 0 && optim_uav.Length > 0)

        {


            // Loop through each point to connect to the mainPoint
            lineMat.SetPass(0);
            GL.Begin(GL.LINES);
            //GL.Color(new Color(lineMat.color.r, lineMat.color.g, lineMat.color.b, lineMat.color.a));
            GL.Color(new Color(1, 1, 0, 0.8F));
            for (int i = 0; i < uavNum; i++)
            {


                GameObject initPoint = init_uav[i];
                GameObject optimPoint = optim_uav[i];
                //

                if (initPoint && optimPoint)
                {

                    Vector3 initPointPos = initPoint.transform.position;
                    Vector3 optimPointPos = optimPoint.transform.position;
            
                    // print(    );

                    GL.Vertex3(initPointPos.x, initPointPos.y, initPointPos.z);

                    GL.Vertex3(optimPointPos.x, optimPointPos.y, optimPointPos.z);

                    
                }
            }
            GL.End();


        }
        print("===================OVER===============================");

    }
    //根据无人机的标号获取无人机的坐标
    //cnt是当前第几个无人机
    Point getPointByIndex(int cnt)
    {
        Point point = new Point();

        //Unity3d里 y是上下
        point.x = uavNum + cnt;
        point.z = uavNum * 2 + cnt;
        point.y = uavNum * 3 + cnt;
       
        return point;


    }

    ////把资源加载到内存中
    //Object uavTemplate = Resources.Load(uavPath, typeof(GameObject));
    ////用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
    //GameObject uavs = Instantiate(uavTemplate) as GameObject;
    //创建无人机
    void createUav()
    {

       

        init_uav = new GameObject[uavNum];
        optim_uav = new GameObject[uavNum];
        Point point;
        //创建无人机
        for (int i = 0; i < uavNum; i++)
        {
            //把资源加载到内存中
            UnityEngine.Object start_uav_template = Resources.Load(start_uavPath, typeof(GameObject));
            UnityEngine.Object optim_uav_template = Resources.Load(optim_uavPath, typeof(GameObject));
            point = getPointByIndex(i);
            init_uav[i] = Instantiate(start_uav_template) as GameObject;
            optim_uav[i] = Instantiate(optim_uav_template) as GameObject;

            //init_uav[i].GetComponent<Renderer>().material=lineMat;
            //optim_uav[i].GetComponent<Renderer>().material = lineMat;

            init_uav[i].name = "uav_init_" + i;
            init_uav[i].transform.position = new Vector3(convertToFloat(init[point.x]) * 50, convertToFloat(init[point.y])*10, convertToFloat(init[point.z]) * 50);
            init_uav[i].transform.localScale = new Vector3(50, 50, 50);
            //init_uav[i].GetComponent<MeshRenderer>().material.color = Color.red;

            optim_uav[i].name = "uav_optim_" + i;
            optim_uav[i].transform.position = new Vector3(convertToFloat(optimal[point.x] ) * 50, convertToFloat(optimal[point.y]) * 10, convertToFloat(optimal[point.z]) * 50);
            //optim_uav[i].GetComponent<MeshRenderer>().material.color = Color.yellow;
            optim_uav[i].transform.localScale = new Vector3(5, 5, 5);
            


        }

    }

  



    //系统触发的事件
    void OnRenderObject()

    {
        DrawConnectingLines();


    }
  

    private void Start()
    {
        ////把资源加载到内存中
        //UnityEngine.Object uavTemplate = Resources.Load(uavPath, typeof(GameObject));

        ////UnityEngine.Object uavTemplate1 = Resources.Load(uavPath, typeof(GameObject));
    

        //////用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
        ////GameObject uavs = Instantiate(uavTemplate) as GameObject;

        //GameObject uav1 = Instantiate(uavTemplate) as GameObject;
        //GameObject uav2 = Instantiate(uavTemplate) as GameObject;


        //uav1.name = "uav_init_" + 1;
        //uav1.transform.position = new Vector3(10, 160, 0);


        //uav2.name = "uav_optim_" + 2;
        //uav2.transform.position = new Vector3(30, 50, 0);
 
        
        createUav();

    }


}