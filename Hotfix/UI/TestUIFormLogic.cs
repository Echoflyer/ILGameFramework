using System;
using UnityGameFramework.Runtime;
using GameFramework;

namespace Hotfix
{
    class TestUIFormLogic:UIFormLogic
    {
        protected override void OnClose(object userData)
        {
            Log.Debug("TestUIFormLogic--OnClose");
            base.OnClose(userData);
        }
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            Log.Debug("TestUIFormLogic--OnInit");
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            Log.Debug("TestUIFormLogic--OnOpen");
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            Log.Debug("TestUIFormLogic--OnUpdate");
        }
    }
}
