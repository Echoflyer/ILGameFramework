using ILRuntime.Runtime.Enviorment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ILRuntime.Runtime.Intepreter;
using System;
using GameFramework.Procedure;
using GameFramework.Fsm;
using ILRuntime.CLR.Method;

public class ProcedureBaseAdaptor : CrossBindingAdaptor
{

    public override Type BaseCLRType
    {
        get
        {
            return typeof(ProcedureBase);
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
        return new Adaptor(appdomain, instance); ;//创建一个新的实例
    }

    internal class Adaptor : ProcedureBase, CrossBindingAdaptorType
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

        IMethod _mOnInit;
        bool _isOnInitGot = false;
        bool _isOnInitInvoking = false;
        IMethod _mOnDestroy;
        bool _isOnDestroyGot = false;
        bool _isOnDestroyInvoking = false;
        IMethod _mOnEnter;
        bool _isOnEnterGot = false;
        bool _isOnEnterInvoking = false;
        IMethod _mOnLeave;
        bool _isOnLeaveGot = false;
        bool _isOnLeaveInvoking = false;
        IMethod _mOnUpdate;
        bool _isOnUpdateGot = false;
        bool _isOnUpdateInvoking = false;

        object[] param1 = new object[1];
        object[] param2 = new object[2];
        object[] param3 = new object[3];

        //
        // 摘要:
        //     /// 状态初始化时调用。 ///
        //
        // 参数:
        //   procedureOwner:
        //     流程持有者。
        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            if (!_isOnInitGot)
            {
                _mOnInit = instance.Type.GetMethod("OnInit", 1);
                _isOnInitGot = true;
            }
            if (_mOnInit != null && !_isOnInitInvoking)
            {
                _isOnInitInvoking = true;
                param1[0] = procedureOwner;
                appdomain.Invoke(this._mOnInit, instance, this.param1);
                _isOnInitInvoking = false;
            }
            else
                base.OnInit(procedureOwner);
        }
        //
        // 摘要:
        //     /// 状态销毁时调用。 ///
        //
        // 参数:
        //   procedureOwner:
        //     流程持有者。
        protected override void OnDestroy(IFsm<IProcedureManager> procedureOwner)
        {
            if (!_isOnDestroyGot)
            {
                _mOnDestroy = instance.Type.GetMethod("OnDestroy", 1);
                _isOnDestroyGot = true;
            }
            if (_mOnDestroy != null && !_isOnDestroyInvoking)
            {
                _isOnDestroyInvoking = true;
                param1[0] = procedureOwner;
                appdomain.Invoke(this._mOnDestroy, instance, this.param1);
                _isOnDestroyInvoking = false;
            }
            else
                base.OnDestroy(procedureOwner);
        }
        //
        // 摘要:
        //     /// 进入状态时调用。 ///
        //
        // 参数:
        //   procedureOwner:
        //     流程持有者。
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            if (!_isOnEnterGot)
            {
                _mOnEnter = instance.Type.GetMethod("OnEnter", 1);
                _isOnEnterGot = true;
            }
            if (_mOnEnter != null && !_isOnEnterInvoking)
            {
                _isOnEnterInvoking = true;
                param1[0] = procedureOwner;
                appdomain.Invoke(this._mOnEnter, instance, this.param1);
                _isOnEnterInvoking = false;
            }
            else
                base.OnEnter(procedureOwner);
        }

        //
        // 摘要:
        //     /// 离开状态时调用。 ///
        //
        // 参数:
        //   procedureOwner:
        //     流程持有者。
        //
        //   isShutdown:
        //     是否是关闭状态机时触发。
        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            if (!_isOnLeaveGot)
            {
                _mOnLeave = instance.Type.GetMethod("OnLeave", 2);
                _isOnLeaveGot = true;
            }
            if (_mOnLeave != null && !_isOnLeaveInvoking)
            {
                _isOnLeaveInvoking = true;
                param2[0] = procedureOwner;
                param2[1] = isShutdown;
                appdomain.Invoke(this._mOnLeave, instance, this.param2);
                _isOnLeaveInvoking = false;
            }
            else
                base.OnLeave(procedureOwner, isShutdown);
        }

        //
        // 摘要:
        //     /// 状态轮询时调用。 ///
        //
        // 参数:
        //   procedureOwner:
        //     流程持有者。
        //
        //   elapseSeconds:
        //     逻辑流逝时间，以秒为单位。
        //
        //   realElapseSeconds:
        //     真实流逝时间，以秒为单位。
        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            if (!_isOnUpdateGot)
            {
                _mOnUpdate = instance.Type.GetMethod("OnUpdate", 3);
                _isOnUpdateGot = true;
            }
            if (_mOnUpdate != null && !_isOnUpdateInvoking)
            {
                param3[0] = procedureOwner;
                param3[1] = elapseSeconds;
                param3[2] = realElapseSeconds;
                appdomain.Invoke(this._mOnUpdate, instance, this.param3);
            }
            else
                base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }
        
    }
}
