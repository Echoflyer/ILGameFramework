//------------------------------------------------------------
// Game Framework v3.x
// Copyright © 2013-2017 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

using GameFramework.Event;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// UIForm初始化的事件
    /// </summary>
    public sealed class OpenUIFormInitStartEvenArgs : GameEventArgs
    {
        /// <summary>
        //
        /// </summary>
        /// <param name="e">自定义事件</param>
        public OpenUIFormInitStartEvenArgs(UIForm _uiForm, int serialId, string uiFormAssetName, object userData)
        {
            UIForm = _uiForm;
            SerialId = serialId;
            UIFormAssetName = uiFormAssetName;
            UserData = userData;
        }

        /// <summary>
        /// 获取打开界面成功事件编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return (int)EventId.OpenUIFormInitStart;
            }
        }

        /// <summary>
        /// 获取打开成功的界面。
        /// </summary>
        public UIForm UIForm
        {
            get;
            private set;
        }

        /// <summary>
        /// 界面序列编号
        /// </summary>
        public int SerialId
        {
            get;
            private set;
        }

        /// <summary>
        /// 界面资源名称
        /// </summary>
        public string UIFormAssetName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取用户自定义数据。
        /// </summary>
        public object UserData
        {
            get;
            private set;
        }
    }
}
