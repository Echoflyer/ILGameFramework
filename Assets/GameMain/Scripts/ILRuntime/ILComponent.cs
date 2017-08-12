//-----------------------------------------------------------------------
// <copyright file="ILComponent.cs" company="Codingworks Game Development">
//     Copyright (c) codingworks. All rights reserved.
// </copyright>
// <author> codingworks </author>
// <time> #CREATETIME# </time>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using ILRuntime.Runtime.Enviorment;
using System.IO;

namespace GameMain
{
    public class ILComponent : GameFrameworkComponent
    {
#region 接口
        public AppDomain _AppDomain
        {
            private set;
            get;
        }
        #endregion

#region 属性
#if UNITY_EDITOR
        [SerializeField]
        private TextAsset _DllBytes;
        [SerializeField]
        private TextAsset _PdbBytes;
#endif
#endregion

        // Use this for initialization
        void Start()
        {
            _AppDomain = new AppDomain();
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region 加载热更新
        public void LoadHotFixAssembly(byte[] _dllBytes)
        {
            byte[] _pdbBytes = null;
#if UNITY_EDITOR
            //在编辑器中加载
            if(_DllBytes)
                _dllBytes = _DllBytes.bytes;
            //加载调试数据
            if(_PdbBytes)
                _pdbBytes = _PdbBytes.bytes;
#endif
            OnHotFixLoaded(_dllBytes, _pdbBytes);
        }

        void OnHotFixLoaded(byte[] _dllBytes,byte[] _pdbBytes)
        {
            using (System.IO.MemoryStream fs = new MemoryStream(_dllBytes))
            {
                using (System.IO.MemoryStream p = new MemoryStream(_pdbBytes))
                {
                    _AppDomain.LoadAssembly(fs, p, new Mono.Cecil.Pdb.PdbReaderProvider());
                }
            }
            InitializeILRuntime();
        }

        void InitializeILRuntime()
        {
            //这里做一些ILRuntime的注册，HelloWorld示例暂时没有需要注册的
            _AppDomain.RegisterCrossBindingAdaptor(new ProcedureBaseAdaptor());
        }
#endregion

    }
}
