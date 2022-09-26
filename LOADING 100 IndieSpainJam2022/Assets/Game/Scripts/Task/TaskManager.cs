using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Collections.Specialized.BitVector32;
using Object = UnityEngine.Object;

public class TaskManager : MonoBehaviour
{
    // Start is called before the first frame update
    //private float timeUntilActivateSpeed;
    //private float timeUntilActivateProjectile;
    private float timeUntilNextExec;
    void Start()
    {
        timeUntilNextExec = 2f;
    }

    // Update is called once per frame
    void Update()
    {
       /* foreach (Tasks.task t in Tasks._tasklist)
        {
            if (Time.time > t.InitTime) {
                t.Action(t.Arg1,t.Arg2);
                t.InitTime = t.InitTime + Time.time;
                 
            }
        }*/
    }

     
}
/*public static class Tasks
{
    public class task
    {
        private float initTime;
        private Image arg1;
        private float arg2;
        private Action<Image, float> action;

        public task(float initTime, Action<Image, float> action,Image arg1,float arg2 )
        {
            this.initTime = initTime;
            this.action = action;
            this.Arg1 = arg1;
            this.Arg2 = arg2;
        }

        public float InitTime { get => initTime; set => initTime = value; }
        public Action<Image, float> Action { get => action; set => action = value; }
        public Image Arg1 { get => arg1; set => arg1 = value; }
        public float Arg2 { get => arg2; set => arg2 = value; }
    }
    public static List<task> _tasklist = new List<task>();
    public static void NewTask(float time, Action<Image, float> action , Image arg1, float arg2)
    {
        _tasklist.Add(new task(time, action, arg1, arg2));
    }

   
}*/

 