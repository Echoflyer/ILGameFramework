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
        return new Adaptor(appdomain, instance);//创建一个新的实例
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

        IMethod _mOnEnter;
        IMethod _mOnLeave;

        object[] param1 = new object[1];
        object[] param2 = new object[2];

        //
        // 摘要:
        //     /// 进入状态时调用。 ///
        //
        // 参数:
        //   procedureOwner:
        //     流程持有者。
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            if (_mOnEnter==null)
            {
                _mOnEnter= instance.Type.GetMethod("OnEnter", 1);
            }
            param1[0] = procedureOwner;
           appdomain.Invoke(this._mOnEnter, instance, this.param1);
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            if (_mOnLeave == null)
            {
                _mOnLeave = instance.Type.GetMethod("OnLeave", 2);
            }
            param2[0] = procedureOwner;
            param2[1] = isShutdown;
            appdomain.Invoke(this._mOnLeave, instance, this.param2);
        }

    }
}
