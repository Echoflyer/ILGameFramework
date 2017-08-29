using ILRuntime.Runtime.Enviorment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ILRuntime.Runtime.Intepreter;
using System;
using ILRuntime.CLR.Method;
using UnityGameFramework.Runtime;

public class UIFormLogicAdaptor : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get
        {
            return typeof(UIFormLogic);
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(Adaptor);
        }
    }

    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adaptor(appdomain, instance);//创建一个新的实例
    }

    internal class Adaptor : UIFormLogic, CrossBindingAdaptorType
    {
        ILTypeInstance instance;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        public Adaptor()
        { }

        public Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }
        public ILTypeInstance ILInstance
        {
            get
            {
                return instance;
            }
        }

        object[] param1 = new object[1];
        object[] param2 = new object[2];

        IMethod _mOnInit;
        bool _isOnInitGot = false;
        bool _isOnInitInvoking = false;
        IMethod _mOnOpen;
        bool _isOnOpenGot = false;
        bool _isOnOpenInvoking = false;
        IMethod _mOnClose;
        bool _isOnCloseGot = false;
        bool _isOnCloseInvoking = false;
        IMethod _mOnPause;
        bool _isOnPauseGot = false;
        bool _isOnPauseInvoking = false;
        IMethod _mOnResume;
        bool _isOnResumeGot = false;
        bool _isOnResumeInvoking = false;
        IMethod _mOnCover;
        bool _isOnCoverGot = false;
        bool _isOnCoverInvoking = false;
        IMethod _mOnReveal;
        bool _isOnRevealGot = false;
        bool _isOnRevealInvoking = false;
        IMethod _mOnRefocus;
        bool _isOnRefocusGot = false;
        bool _isOnRefocusInvoking = false;
        IMethod _mOnUpdate;
        bool _isOnUpdateGot = false;
        bool _isOnUpdateInvoking = false;
        IMethod _mOnDepthChanged;
        bool _isOnDepthChangedGot = false;
        bool _isOnDepthChangedInvoking = false;

        /// <summary>
        /// 界面初始化。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        protected internal override void OnInit(object userData)
        {
            if (!_isOnInitGot)
            {
                _mOnInit = instance.Type.GetMethod("OnInit", 1);
                _isOnInitGot = true;
            }
            if (_mOnInit != null && !_isOnInitInvoking)
            {
                _isOnInitInvoking = true;
                param1[0] = userData;
                appdomain.Invoke(this._mOnInit, instance, this.param1);
                _isOnInitInvoking = false;
            }
            else
                base.OnInit(userData);
        }

        /// <summary>
        /// 界面打开。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        protected internal override void OnOpen(object userData)
        {
            if (!_isOnOpenGot)
            {
                _mOnOpen = instance.Type.GetMethod("OnOpen", 1);
                _isOnOpenGot = true;
            }
            if (_mOnOpen != null && !_isOnOpenInvoking)
            {
                _isOnOpenInvoking = true;
                param1[0] = userData;
                appdomain.Invoke(this._mOnOpen, instance, this.param1);
                _isOnOpenInvoking = false;
            }
            else
                base.OnOpen(userData);
        }

        /// <summary>
        /// 界面关闭。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        protected internal override void OnClose(object userData)
        {
            if (!_isOnCloseGot)
            {
                _mOnClose = instance.Type.GetMethod("OnClose", 1);
                _isOnCloseGot = true;
            }
            if (_mOnClose != null && !_isOnCloseInvoking)
            {
                _isOnCloseInvoking = true;
                param1[0] = userData;
                appdomain.Invoke(this._mOnClose, instance, this.param1);
                _isOnCloseInvoking = false;
            }
            else
                base.OnClose(userData);
        }

        /// <summary>
        /// 界面暂停。
        /// </summary>
        protected internal override void OnPause()
        {
            if (!_isOnPauseGot)
            {
                _mOnPause= instance.Type.GetMethod("OnPause", 0);
                _isOnPauseGot = true;
            }
            if (_mOnPause != null && !_isOnPauseInvoking)
            {
                _isOnPauseInvoking = true;
                appdomain.Invoke(this._mOnPause, instance, null);
                _isOnPauseInvoking = false;
            }
            else
                base.OnPause();
        }

        /// <summary>
        /// 界面暂停恢复。
        /// </summary>
        protected internal override void OnResume()
        {
            if (!_isOnResumeGot)
            {
                _mOnResume = instance.Type.GetMethod("OnResume", 0);
                _isOnResumeGot = true;
            }
            if (_mOnResume != null && !_isOnResumeInvoking)
            {
                _isOnResumeInvoking = true;
                appdomain.Invoke(this._mOnResume, instance, null);
                _isOnResumeInvoking = false;
            }
            else
                base.OnResume();
        }

        /// <summary>
        /// 界面遮挡。
        /// </summary>
        protected internal override void OnCover()
        {
            if (!_isOnCoverGot)
            {
                _mOnCover = instance.Type.GetMethod("OnCover", 0);
                _isOnCoverGot = true;
            }
            if (_mOnCover != null && !_isOnCoverInvoking)
            {
                _isOnCoverInvoking = true;
                appdomain.Invoke(this._mOnCover, instance, null);
                _isOnCoverInvoking = false;
            }
            else
                base.OnCover();
        }

        /// <summary>
        /// 界面遮挡恢复。
        /// </summary>
        protected internal override void OnReveal()
        {
            if (!_isOnRevealGot)
            {
                _mOnReveal = instance.Type.GetMethod("OnReveal", 0);
                _isOnRevealGot = true;
            }
            if (_mOnReveal != null && !_isOnRevealInvoking)
            {
                _isOnRevealInvoking = true;
                appdomain.Invoke(this._mOnReveal, instance, null);
                _isOnRevealInvoking = false;
            }
            else
                base.OnReveal();
        }

        /// <summary>
        /// 界面激活。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        protected internal override void OnRefocus(object userData)
        {
            if (!_isOnRefocusGot)
            {
                _mOnRefocus = instance.Type.GetMethod("OnRefocus", 1);
                _isOnRefocusGot = true;
            }
            if (_mOnRefocus != null && !_isOnRefocusInvoking)
            {
                _isOnRefocusInvoking = true;
                param1[0] = userData;
                appdomain.Invoke(this._mOnRefocus, instance, this.param1);
                _isOnRefocusInvoking = false;
            }
            else
                base.OnRefocus(userData);
        }

        /// <summary>
        /// 界面轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (!_isOnUpdateGot)
            {
                _mOnUpdate = instance.Type.GetMethod("OnUpdate", 2);
                _isOnUpdateGot = true;
            }
            if (_mOnUpdate != null && !_isOnUpdateInvoking)
            {
                _isOnUpdateInvoking = true;
                param2[0] = elapseSeconds;
                param2[1] = realElapseSeconds;
                appdomain.Invoke(this._mOnUpdate, instance, this.param2);
                _isOnUpdateInvoking = false;
            }
            else
                base.OnUpdate(elapseSeconds,realElapseSeconds);
        }

        /// <summary>
        /// 界面深度改变。
        /// </summary>
        /// <param name="uiGroupDepth">界面组深度。</param>
        /// <param name="depthInUIGroup">界面在界面组中的深度。</param>
        protected internal override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            if (!_isOnDepthChangedGot)
            {
                _mOnDepthChanged = instance.Type.GetMethod("OnDepthChanged", 2);
                _isOnDepthChangedGot = true;
            }
            if (_mOnDepthChanged != null && !_isOnDepthChangedInvoking)
            {
                _isOnDepthChangedInvoking = true;
                param2[0] = uiGroupDepth;
                param2[1] = depthInUIGroup;
                appdomain.Invoke(this._mOnDepthChanged, instance, this.param2);
                _isOnDepthChangedInvoking = false;
            }
            else
                base.OnDepthChanged(uiGroupDepth,depthInUIGroup);
        }
        
    }
}
